import unittest
import functools
import cvrp

# TODO: Can probably use something like: https://stackoverflow.com/questions/4398967/python-unit-testing-automatically-running-the-debug_modelger-when-a-test-fails
# TODO: Generalize test cases so they can be re-used for GA implementation

def debug_model(model):
    print(model.viz_model())
    print(f"c: {model.c}")
    print(f"A_ub: {model.A_ub}")
    print(f"A_eq: {model.A_eq}")
    print(f"b_eq: {model.b_eq}")
    print(model.result)
    for var_indx, var_val in enumerate(model.result.x):
        print(f"{model.index_to_var[var_indx]} = {var_val}")

class CVRPTestCases(unittest.TestCase):
    def setUp(self):
        pass

    def tearDown(self):
        pass

    def test_get_euclidean_dist(self):
        location_1 = (0, 0)
        location_2 = tuple()
        with self.assertRaises(IndexError) as context_manager:
            cvrp.get_euclidean_dist(location_1, location_2)

        location_1 = (0, 0)
        location_2 = (0, 0)
        self.assertEqual(cvrp.get_euclidean_dist(location_1, location_2), 0)

        location_1 = (0, 1)
        location_2 = (0, 1)
        self.assertEqual(cvrp.get_euclidean_dist(location_1, location_2), 0)

        location_1 = (0, 0)
        location_2 = (0, 1)
        self.assertEqual(cvrp.get_euclidean_dist(location_1, location_2), 1)

        location_1 = (0, 0)
        location_2 = (1, 0)
        self.assertEqual(cvrp.get_euclidean_dist(location_1, location_2), 1)

        location_1 = (0, 0)
        location_2 = (1, 1)
        self.assertEqual(round(cvrp.get_euclidean_dist(location_1, location_2), 6),
            1.414214)

        location_1 = (-1, -1)
        location_2 = (0, 0)
        self.assertEqual(round(cvrp.get_euclidean_dist(location_1, location_2), 6),
            1.414214)

        location_1 = (-1, -1)
        location_2 = (1, 0)
        self.assertEqual(round(cvrp.get_euclidean_dist(location_1, location_2), 6),
            2.236068)

        location_1 = (-1, -1)
        location_2 = (0, 1)
        self.assertEqual(round(cvrp.get_euclidean_dist(location_1, location_2), 6),
            2.236068)

        location_1 = (-1, -1)
        location_2 = (0, 5)
        self.assertEqual(round(cvrp.get_euclidean_dist(location_1, location_2), 6),
            6.082763)

    def test_profit_basic(self):
        depot = (0, 0)
        trucks = [cvrp.Truck(truck_id="t1", capacity=10, per_mile_cost=0, max_dist=100)]
        deliveries = [cvrp.Delivery(delivery_id="d1", location=(0, 1), profit=10, size=1)]
        opt = cvrp.CVRP(depot_location=depot, trucks=trucks, deliveries=deliveries)
        opt.init_all()
        opt.solve()
        # Negative cost, so profit
        try:
            self.assertEqual(opt.mip_result.fun, -10)
        except AssertionError:
            debug_model(opt)
            raise

    def test_profit_multiple_deliveries(self):
        depot = (0, 0)
        trucks = [cvrp.Truck(truck_id="t1", capacity=10, per_mile_cost=0, max_dist=100)]
        deliveries = [cvrp.Delivery(delivery_id="d1", location=(0, 1), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d2", location=(0, 2), profit=10, size=1)]
        opt = cvrp.CVRP(depot_location=depot, trucks=trucks, deliveries=deliveries)
        opt.init_all()
        opt.solve()
        try:
            self.assertEqual(opt.mip_result.fun, -20)
        except AssertionError:
            debug_model(opt)
            raise

    def test_per_mile_cost_basic(self):
        # 1 mile out, 1 mile back at cost of 1 per mile and 10 profit.
        depot = (0, 0)
        trucks = [cvrp.Truck(truck_id="t1", capacity=10, per_mile_cost=1, max_dist=100)]
        deliveries = [cvrp.Delivery(delivery_id="d1", location=(0, 1), profit=10, size=1)]
        opt = cvrp.CVRP(depot_location=depot, trucks=trucks, deliveries=deliveries)
        opt.init_all()
        opt.solve()
        try:
            self.assertEqual(opt.mip_result.fun, -8)
        except AssertionError:
            debug_model(opt)
            raise

    def test_per_mile_cost_negative_distances(self):
        # Should have the exact same profit as the basic example, just with negative locations.
        depot = (0, 0)
        trucks = [cvrp.Truck(truck_id="t1", capacity=10, per_mile_cost=1, max_dist=100)]
        deliveries = [cvrp.Delivery(delivery_id="d1", location=(0, -1), profit=10, size=1)]
        opt = cvrp.CVRP(depot_location=depot, trucks=trucks, deliveries=deliveries)
        opt.init_all()
        opt.solve()
        try:
            self.assertEqual(opt.mip_result.fun, -8)
        except AssertionError:
            debug_model(opt)
            raise

    def test_per_mile_multiple_deliveries(self):
        depot = (0, 0)
        trucks = [cvrp.Truck(truck_id="t1", capacity=10, per_mile_cost=1, max_dist=100)]
        deliveries = [cvrp.Delivery(delivery_id="d1", location=(0, 1), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d2", location=(0, 2), profit=10, size=1)]
        opt = cvrp.CVRP(depot_location=depot, trucks=trucks, deliveries=deliveries)
        opt.init_all()
        opt.solve()
        try:
            self.assertEqual(opt.mip_result.fun, -16)
        except AssertionError:
            debug_model(opt)
            raise

    def test_capacity_basic(self):
        # Can't fit both, so pick the more profitable delivery (10 > 9).
        depot = (0, 0)
        trucks = [cvrp.Truck(truck_id="t1", capacity=1, per_mile_cost=0, max_dist=100)]
        deliveries = [cvrp.Delivery(delivery_id="d1", location=(0, 1), profit=9, size=1),
                      cvrp.Delivery(delivery_id="d2", location=(1, 0), profit=10, size=1)]
        opt = cvrp.CVRP(depot_location=depot, trucks=trucks, deliveries=deliveries)
        opt.init_all()
        opt.solve()
        try:
            self.assertEqual(opt.mip_result.fun, -10)
        except AssertionError:
            debug_model(opt)
            raise

    def test_capacity_multiple_deliveries(self):
        # Fit the 2 most profitable, skip the 3rd.
        depot = (0, 0)
        trucks = [cvrp.Truck(truck_id="t1", capacity=2, per_mile_cost=0, max_dist=100)]
        deliveries = [cvrp.Delivery(delivery_id="d1", location=(0, 1), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d2", location=(0, 2), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d3", location=(1, 0), profit=9, size=1)]
        opt = cvrp.CVRP(depot_location=depot, trucks=trucks, deliveries=deliveries)
        opt.init_all()
        opt.solve()
        try:
            self.assertEqual(opt.mip_result.fun, -20)
        except AssertionError:
            debug_model(opt)
            raise

    def test_max_dist_skip_all(self):
        # Skip all deliveries
        depot = (0, 0)
        trucks = [cvrp.Truck(truck_id="t1", capacity=100, per_mile_cost=0, max_dist=1)]
        deliveries = [cvrp.Delivery(delivery_id="d1", location=(0, 2), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d2", location=(2, 0), profit=9, size=1)]
        opt = cvrp.CVRP(depot_location=depot, trucks=trucks, deliveries=deliveries)
        opt.init_all()
        opt.solve()
        try:
            self.assertEqual(opt.mip_result, None)
        except AssertionError:
            debug_model(opt)
            raise
    
    def test_max_dist_skip_furthest(self):
        # Skip 2nd, farther away, delivery
        depot = (0, 0)
        trucks = [cvrp.Truck(truck_id="t1", capacity=100, per_mile_cost=0, max_dist=2)]
        deliveries = [cvrp.Delivery(delivery_id="d1", location=(0, 1), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d2", location=(2, 0), profit=9, size=1)]
        opt = cvrp.CVRP(depot_location=depot, trucks=trucks, deliveries=deliveries)
        opt.init_all()
        opt.solve()
        try:
            self.assertEqual(opt.mip_result.fun, -10)
        except AssertionError:
            debug_model(opt)
            raise

    def test_many_nodes(self):
        # Test with 15 nodes to ensure branch and bound doesn't fail
        depot = (0, 0)
        trucks = [cvrp.Truck(truck_id="t1", capacity=100, per_mile_cost=0, max_dist=100)]
        deliveries = [cvrp.Delivery(delivery_id="d1", location=(0, 1), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d2", location=(0, 2), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d3", location=(0, 3), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d4", location=(0, 4), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d5", location=(1, 4), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d6", location=(2, 4), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d7", location=(3, 4), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d8", location=(4, 4), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d9", location=(4, 3), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d10", location=(4, 2), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d11", location=(4, 1), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d12", location=(4, 0), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d13", location=(3, 0), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d14", location=(2, 0), profit=10, size=1),
                      cvrp.Delivery(delivery_id="d15", location=(1, 0), profit=10, size=1)]
        opt = cvrp.CVRP(depot_location=depot, trucks=trucks, deliveries=deliveries)
        opt.init_all()
        opt.solve()
        try:
            self.assertEqual(opt.mip_result.fun, -150)
        except AssertionError:
            debug_model(opt)
            raise


if __name__ == '__main__':
    unittest.main(verbosity=2)