// ABOUTME: The Fleet class manages a collection of cars using mutable state.
// This is a classic OOP approach where the object encapsulates data 
// and provides methods to modify it.

using System.Collections.Generic;

namespace CarSharp.Oop;

/// <summary>
/// Manages a collection of cars.
/// In OOP, the Fleet is a 'Container' that maintains its own internal state.
/// </summary>
public class Fleet
{
    // Internal mutable state.
    private readonly List<Car> _cars = new();

    /// <summary>
    /// Gets the total number of cars currently in the fleet.
    /// </summary>
    public int TotalCars => _cars.Count;

    /// <summary>
    /// Adds a car to the fleet.
    /// In this OOP model, we modify the internal list in-place.
    /// </summary>
    /// <param name="car">The car to add.</param>
    public void AddCar(Car car)
    {
        // Simple in-place mutation of the private state.
        _cars.Add(car);
    }

    /// <summary>
    /// Removes a car from the fleet.
    /// Returns true if the car was found and removed; otherwise, false.
    /// </summary>
    /// <param name="car">The car instance to remove.</param>
    public bool RemoveCar(Car car)
    {
        // List.Remove uses the default equality comparer.
        // For classes, this means reference equality.
        return _cars.Remove(car);
    }
}
