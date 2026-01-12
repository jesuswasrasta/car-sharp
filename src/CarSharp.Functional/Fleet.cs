// ABOUTME: The Fleet is modeled as an immutable record.
// Instead of modifying the fleet, operations return a new instance 
// representing the updated state.

using System.Collections.Immutable;

namespace CarSharp.Functional;

/// <summary>
/// Represents a fleet of cars as an immutable data structure.
/// In FP, the Fleet is 'Data' rather than an 'Object' with internal state.
/// </summary>
/// <param name="Cars">The immutable collection of cars.</param>
public record Fleet(ImmutableList<Car> Cars)
{
    /// <summary>
    /// Returns an empty fleet instance.
    /// This is the starting point for all transformations.
    /// </summary>
    public static Fleet Empty { get; } = new(ImmutableList<Car>.Empty);

    /// <summary>
    /// Gets the total number of cars in the fleet.
    /// </summary>
    public int TotalCars => Cars.Count;
}

/// <summary>
/// Extension methods for transforming a Fleet.
/// In FP, we often separate data from the functions that operate on it.
/// </summary>
public static class FleetExtensions
{
    /// <summary>
    /// 'Adds' a car to the fleet by returning a new Fleet instance.
    /// 
    /// Contrast with OOP:
    /// In OOP, fleet.AddCar(car) modifies the existing object.
    /// In Functional, fleet.AddCar(car) leaves the original fleet untouched 
    /// and returns a NEW fleet.
    /// </summary>
    public static Fleet AddCar(this Fleet fleet, Car car) =>
        fleet with { Cars = fleet.Cars.Add(car) };

    /// <summary>
    /// 'Removes' a car from the fleet by returning a new Fleet instance.
    /// If the car is not found, the original fleet instance is returned.
    /// 
    /// Contrast with OOP:
    /// In OOP, Remove(car) returns a boolean indicating success.
    /// In Functional, we always return a Fleet, which might be the same 
    /// instance if no changes were made (Structural sharing).
    /// </summary>
    public static Fleet RemoveCar(this Fleet fleet, Car car)
    {
        var newCars = fleet.Cars.Remove(car);
        // If the resulting collection is the same instance as the original,
        // it means nothing was removed. We return the original fleet.
        return ReferenceEquals(newCars, fleet.Cars) ? fleet : fleet with { Cars = newCars };
    }
}
