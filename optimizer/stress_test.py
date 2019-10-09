import cvrp
import time


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
times = []
n = 10
for _ in range(n):
    start = time.time()
    opt.solve()
    end = time.time()
    times.append(end - start)
print(f"After {n} iterations, average runtime of {sum(times) / len(times)}s")