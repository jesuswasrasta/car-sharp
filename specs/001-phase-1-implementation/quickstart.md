# Quickstart: Phase 1 - Minimal Fleet Management

## OOP Usage

```csharp
using CarSharp.Oop;

var fleet = new Fleet();
var car = new Car();

fleet.AddCar(car);
Console.WriteLine($"Total cars: {fleet.TotalCars}"); // Output: 1

bool removed = fleet.RemoveCar(car);
Console.WriteLine($"Removed: {removed}, Total cars: {fleet.TotalCars}"); // Output: True, 0
```

## Functional Usage

```csharp
using CarSharp.Functional;
using System.Collections.Immutable;

var fleet = Fleet.Empty;
var car = new Car();

var updatedFleet = fleet.AddCar(car);
Console.WriteLine($"Total cars: {updatedFleet.TotalCars}"); // Output: 1

var finalFleet = updatedFleet.RemoveCar(car);
Console.WriteLine($"Total cars: {finalFleet.TotalCars}"); // Output: 0
```