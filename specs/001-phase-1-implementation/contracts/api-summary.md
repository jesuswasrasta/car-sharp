# API Contracts: Phase 1

## OOP Track (CarSharp.Oop)

```csharp
namespace CarSharp.Oop;

public class Car {}

public class Fleet
{
    public int TotalCars { get; }
    public void AddCar(Car car);
    public bool RemoveCar(Car car);
}
```

## Functional Track (CarSharp.Functional)

```csharp
namespace CarSharp.Functional;

public record Car();

public record Fleet(ImmutableList<Car> Cars)
{
    public static Fleet Empty { get; }
    public int TotalCars { get; }
}

public static class FleetExtensions
{
    public static Fleet AddCar(this Fleet fleet, Car car);
    public static Fleet RemoveCar(this Fleet fleet, Car car);
}
```
