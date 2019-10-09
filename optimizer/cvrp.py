"""
Route optimizer
Capacitated Vehicle Routing Problem
Route Scheduling Model/Module

Branch and bound for integer optimization
Sub-problems solved using scipy:
https://docs.scipy.org/doc/scipy/reference/generated/scipy.optimize.linprog.html

"""

import numpy as np
from scipy.optimize import linprog
import random as rnd
from typing import Dict


class Delivery:
    def __init__(self, delivery_id: str, location: tuple, profit: float = None, size: float = None):
        self.id = delivery_id
        self.location = location
        self.profit = profit
        self.size = size


class Truck:
    def __init__(self, truck_id: str, capacity: float, per_mile_cost: float, max_dist: float):
        self.id = truck_id
        self.capacity = capacity
        self.per_mile_cost = per_mile_cost
        self.max_dist = max_dist


class CVRP:
    def __init__(self, depot_location: tuple, trucks: list, deliveries: list):
        """Initializes the model for use with scipy.optimize.linprog and generates text formulation.
        Model should follow the format:
        min
            c @ x
        such that
            A_ub @ x <= b_ub
            A_eq @ x == b_eq
            lb <= x <= ub

        d0 represents the depot.
        Could add a dummy/slack truck with no profit for skipping deliveries.
        """
        self.depot_location = depot_location
        self.trucks = trucks
        self.deliveries = deliveries

        # Map variable name to index in array for translation between text and matrix formats
        self.var_to_index = {}
        self.index_to_var = {}
        index = 0
        for t in trucks:
            for d1 in deliveries:
                var = f"route_{t.id}_d0_{d1.id}"
                if var not in self.var_to_index:
                    self.var_to_index[var] = index
                    self.index_to_var[index] = var
                    index += 1
                
                var = f"route_{t.id}_{d1.id}_d0"
                if var not in self.var_to_index:
                    self.var_to_index[var] = index
                    self.index_to_var[index] = var
                    index += 1
                
                for d2 in deliveries:
                    if d1.id == d2.id:
                        continue

                    var = f"route_{t.id}_{d1.id}_{d2.id}"
                    if var not in self.var_to_index:
                        self.var_to_index[var] = index
                        self.index_to_var[index] = var
                        index += 1
                    
                    var = f"route_{t.id}_{d2.id}_{d1.id}"
                    if var not in self.var_to_index:
                        self.var_to_index[var] = index
                        self.index_to_var[index] = var
                        index += 1

        self.max_index = max(self.index_to_var) + 1
        # TODO: Pre-calculate number of constraints to more efficiently initialize arrays?
        self.c = np.zeros(self.max_index)
        self.A_ub = None
        self.A_eq = None
        # self.x_0 = np.zeros(self.max_index)
        self.b_ub = None
        self.b_eq = None
        self.bounds = [(0, 1) for _ in range(self.max_index)]
        self.result = None
        self.mip_result = None

    def init_all(self):
        """Initializes all the default constraints and bounds.
        Override this with MIP constraints if needed.
        """
        self.obj_func()
        self.cons_capacity()
        self.cons_max_dist()
        self.cons_node_balanced_flow()
        self.cons_node_arrivals()
        self.cons_node_departures()
        self.cons_start_at_depot()
        self.cons_end_at_depot()
        self.vars_bounds()

    def solve(self):
        """Solve the MIP CVRP model
        Uses a branch-and-bound technique and the Scipy.optimize solver to solve relaxed problems.
        
        15 nodes, 10 test runs:
            argmin + BFS: 2.656984043121338s
            (current) argmin + DFS: 2.5454505443573s
            argmax + BFS: 2.6716614723205567s
            argmax + DFS: 2.579980564117432s

        Note: BFS may be a better choice if parallelization is possible.
        """
        ctr = 0
        curr_best = float("inf")
        # TODO: Better starting node selection
        queue = [{0: 0}, {0: 1}]
        while len(queue) > 0:
            # Using .append and .pop() makes this LIFO, DFS
            # Using .append and .pop(0) makes this FIFO, BFS
            node = queue.pop()
            self.vars_bounds(node)
            self.run_linprog()
            ctr += 1
            # If a relaxed solution is worse than the current best, skip that branch.
            if self.result.success and self.result.status == 0 and self.result.fun < curr_best:
                # If feasible and integral and better than curr_best, set new curr_best and enqueue children
                if all_integral(self.result.x):
                    curr_best = self.result.fun
                    self.mip_result = self.result
                # If feasible and non-integer and better than curr_best, enqueue children
                next_node = self.get_next_node()
                for b in [0, 1]:
                    node[next_node] = b
                    queue.append(node)
        # TODO: Add verbose mode?
        # print(f"{ctr} nodes evaluated. Best solution: {curr_best}")
        if self.mip_result is None:
            print("No integer solution found")

    def get_next_node(self):
        """Heuristic for getting the next node in branch-and-bound algorithm.
        If a result exists, select the variable closest to 0.
        Could also select closest to 1?
        If no result exists yet, randomly select one that isn't already set.
        
        """
        if self.result.x.any():
            return np.argmin(self.result.x)
        else:
            eligible_indices = [k for k, v in enumerate(self.bounds) if v[0] != v[1]]
            return rnd.choice(eligible_indices)

    def run_linprog(self):
        self.result = linprog(c=self.c, A_ub=self.A_ub, b_ub=self.b_ub,
            A_eq=self.A_eq, b_eq=self.b_eq, bounds=self.bounds)

    def add_to_ub(self, arr, rhs):
        """Add the given array and right-hand-side values to the upper constraints
        """
        if self.A_ub is None:
            self.A_ub = np.array(arr)
        else:
            self.A_ub = np.vstack((self.A_ub, arr))

        if self.b_ub is None:
            self.b_ub = np.array(rhs)
        else:
            self.b_ub = np.append(self.b_ub, rhs)

    def add_to_eq(self, arr, rhs):
        """Add the given array and right-hand-side values to the equality constraints
        """
        if self.A_eq is None:
            self.A_eq = np.array(arr)
        else:
            self.A_eq = np.vstack((self.A_eq, arr))

        if self.b_eq is None:
            self.b_eq = np.array(rhs)
        else:
            self.b_eq = np.append(self.b_eq, rhs)

    def obj_func(self):
        """Generates the objective function and visualization for the model
        min route_t_d1_d2 * per_mile_cost_t * dist_d1_d2 - route_t_d0_d * profit_d
        """
        # Cost per route
        for t in self.trucks:
            # Depot-specific routes
            for d in self.deliveries:
                dist = get_euclidean_dist(self.depot_location, d.location)
                var = f"route_{t.id}_d0_{d.id}"
                # If the product is loaded at the depot get credit for the delivery?
                self.c[self.var_to_index[var]] = (t.per_mile_cost * dist) - d.profit
                # Handle returns to the depot
                var = f"route_{t.id}_{d.id}_d0"
                self.c[self.var_to_index[var]] = t.per_mile_cost * dist

            # Site-to-site routes
            for d1 in self.deliveries:
                for d2 in self.deliveries:
                    if d1.id == d2.id:
                        continue
                    dist = get_euclidean_dist(d1.location, d2.location)
                    var = f"route_{t.id}_{d1.id}_{d2.id}"
                    # print(f"var: {var}, d2.profit: {d2.profit}")
                    self.c[self.var_to_index[var]] = (t.per_mile_cost * dist) - d2.profit

    def cons_capacity(self):
        """Capacity constraint
        sum_d2(route_t_d1_d2 * size_d2) <= capacity_t, for t in trucks, d1, d2 in deliveries
        """
        for t in self.trucks:
            arr = np.zeros(self.max_index)
            for d2 in self.deliveries:
                var = f"route_{t.id}_d0_{d2.id}"
                arr[self.var_to_index[var]] = d2.size
                
                for d1 in self.deliveries:
                    if d1.id == d2.id:
                        continue
                    var = f"route_{t.id}_{d1.id}_{d2.id}"
                    arr[self.var_to_index[var]] = d2.size

            self.add_to_ub(arr, t.capacity)
                
    def cons_max_dist(self):
        """Maximum distance constraint
        sum_d1_d2(route_t_d1_d2 * dist_d1_d2) <= max_dist_t, for t in trucks
        """
        arr = np.zeros(self.max_index)
        for t in self.trucks:
            for d1 in self.deliveries:
                dist = get_euclidean_dist(self.depot_location, d1.location)
                var = f"route_{t.id}_d0_{d1.id}"
                arr[self.var_to_index[var]] = dist

                dist = get_euclidean_dist(d1.location, self.depot_location)
                var = f"route_{t.id}_{d1.id}_d0"
                arr[self.var_to_index[var]] = dist

                for d2 in self.deliveries:
                    if d1.id == d2.id:
                        continue
                    dist = get_euclidean_dist(d1.location, d2.location)
                    var = f"route_{t.id}_{d1.id}_{d2.id}"
                    arr[self.var_to_index[var]] = dist
            
            self.add_to_ub(arr, t.max_dist)

    def cons_node_balanced_flow(self):
        """Arrivals at a node must have a corresponding departure (but the node can be skipped)
        sum_d2(route_t_d2_d) - sum_d2(route_t_d_d2) = 0, for t in trucks, for d in deliveries
        
        Using this in conjunction with arrival and departure specific constraints.

        The TSP constraints would be:
            Arrivals to a node must come from another node
                sum_d2(route_t_d2_d) = 1, for t in trucks, for d in deliveries
            Departures from a node must go to another node
                sum_d2(route_t_d_d2) = 1, for t in trucks, for d in deliveries
        """
        for t in self.trucks:
            for d1 in self.deliveries:
                arr = np.zeros(self.max_index)
                var = f"route_{t.id}_{d1.id}_d0"
                arr[self.var_to_index[var]] = 1
                
                for d2 in self.deliveries:
                    if d1.id == d2.id:
                        continue
                    var = f"route_{t.id}_{d1.id}_{d2.id}"
                    arr[self.var_to_index[var]] = 1
                
                var = f"route_{t.id}_d0_{d1.id}"
                arr[self.var_to_index[var]] = -1

                for d2 in self.deliveries:
                    if d1.id == d2.id:
                        continue
                    var = f"route_{t.id}_{d2.id}_{d1.id}"
                    arr[self.var_to_index[var]] = -1
                
                self.add_to_eq(arr, 0)

    def cons_node_arrivals(self):
        """Arrivals to a node must come from another node, or the node is skipped
            sum_d2(route_t_d2_d) <= 1, for t in trucks, for d in deliveries
        """
        for t in self.trucks:
            for d1 in self.deliveries:
                arr = np.zeros(self.max_index)
                var = f"route_{t.id}_d0_{d1.id}"
                arr[self.var_to_index[var]] = 1

                for d2 in self.deliveries:
                    if d1.id == d2.id:
                        continue
                    var = f"route_{t.id}_{d2.id}_{d1.id}"
                    arr[self.var_to_index[var]] = 1
                
                self.add_to_ub(arr, 1)

    def cons_node_departures(self):
        """Departures from a node must go to another node
            sum_d2(route_t_d_d2) <= 1, for t in trucks, for d in deliveries
        """
        for t in self.trucks:
            for d1 in self.deliveries:
                arr = np.zeros(self.max_index)
                var = f"route_{t.id}_{d1.id}_d0"
                arr[self.var_to_index[var]] = 1

                for d2 in self.deliveries:
                    if d1.id == d2.id:
                        continue
                    var = f"route_{t.id}_{d1.id}_{d2.id}"
                    arr[self.var_to_index[var]] = 1
                
                self.add_to_ub(arr, 1)

    def cons_start_at_depot(self):
        """Every route must start at the depot
        sum_d(route_t_d0_d) = 1, for t in trucks
        """
        arr = np.zeros(self.max_index)
        for t in self.trucks:
            for d in self.deliveries:
                var = f"route_{t.id}_d0_{d.id}"
                arr[self.var_to_index[var]] = 1
            
            self.add_to_eq(arr, 1)

    def cons_end_at_depot(self):
        """Every route must end at the depot
        sum_d(route_t_d_d0) = 1, for t in trucks
        """
        arr = np.zeros(self.max_index)
        for t in self.trucks:
            for d in self.deliveries:
                var = f"route_{t.id}_{d.id}_d0"
                arr[self.var_to_index[var]] = 1
            
            self.add_to_eq(arr, 1)

    def vars_bounds(self, bounds: Dict[int, int] = dict()):
        """Variable bounds
        Set default binary bounds if no input is given, just 0 <= route_t_d1_d2 <= 1
        Otherwise, set incoming bounds.
        bounds parameter keys should be indices of the variable they are setting, and value of 0 or 1 for the new bound
        Unset variables use the default (0, 1) bounds.
        """
        # Reset default bounds before modifying with incoming dict
        self.bounds = [(0, 1) for _ in range(self.max_index)]
        for k, v in bounds.items():
            self.bounds[k] = (v, v)

    def viz_model(self) -> str:
        output = ["########## Optimization formulation ##########", "min"]
        # Objective function
        for col_indx, val in enumerate(self.c):
            temp = f" + ({val}) * {self.index_to_var[col_indx]}"
            output.append(temp)
        output.append("s.t.")
        
        # Upper constraints
        for row_indx, row in enumerate(self.A_ub):
            temp = ""
            for col_indx, val in enumerate(row):
                if val == 1:
                    temp += f" + {self.index_to_var[col_indx]}"
            temp += f" <= {self.b_ub[row_indx]}"
            output.append(temp)
        
        # Equality constraints
        for row_indx, row in enumerate(self.A_eq):
            temp = ""
            for col_indx, val in enumerate(row):
                if val == 1:
                    temp += f" + {self.index_to_var[col_indx]}"
            temp += f" = {self.b_eq[row_indx]}"
            output.append(temp)

        # Variable bounds
        return "\n".join(output)


def get_euclidean_dist(point1: tuple, point2: tuple):
    """Calculates the Euclidean distance between 2 points (tuples)
    Eventually will replace this with a street-based distances calculation.
    """
    return ((point1[0] - point2[0]) ** 2 + (point1[1] - point2[1]) ** 2) ** 0.5


def all_integral(result):
    return all(x % 1 == 0 for x in result)


if __name__ == "__main__":
    print("See main.py for an example of how to run this optimizer")