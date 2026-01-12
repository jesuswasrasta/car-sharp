# Data Model: Phase 1 - Minimal Fleet Management

## Entities

### Car
Represents a vehicle in the fleet.
- **OOP (`CarSharp.Oop`)**: A standard class. In this phase, it holds no properties. Identity is based on memory reference.
- **Functional (`CarSharp.Functional`)**: A `record` type. In this phase, it holds no properties. Identity is based on value equality (since all instances are currently empty, they are technically equal by value, but represent distinct tokens).

### Fleet
Manages the collection of cars.
- **OOP (`CarSharp.Oop`)**: A class encapsulating a `List<Car>`.
- **Functional (`CarSharp.Functional`)**: A `record` encapsulating an `ImmutableList<Car>`.

## State Transitions

### Add Car
- **OOP**: `void AddCar(Car car)` - In-place mutation of the `_cars` list.
- **Functional**: `Fleet AddCar(Car car)` - Returns a new `Fleet` record with the updated `ImmutableList`.

### Remove Car
- **OOP**: `bool RemoveCar(Car car)` - Finds the car by reference and removes it in-place.
- **Functional**: `Fleet RemoveCar(Car car)` - Returns a new `Fleet` record without the specified car value.

### Total Count
- **OOP**: `int TotalCars => _cars.Count;`
- **Functional**: `int TotalCars => Cars.Count;`