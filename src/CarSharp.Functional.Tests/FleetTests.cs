// ABOUTME: Test suite for the Functional Fleet implementation.
// We use Property-Based testing (FsCheck) to verify invariants across random inputs.

using CarSharp.Functional;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace CarSharp.Functional.Tests;

public class FleetTests
{
    [Fact]
    public void EmptyFleet_ShouldHaveZeroCars()
    {
        // Assert
        Assert.Equal(0, Fleet.Empty.TotalCars);
    }

    [Property]
    public bool AddingNCars_ShouldResultInTotalCountN(PositiveInt n)
    {
        // Arrange
        // We start with an empty fleet.
        var fleet = Fleet.Empty;
        var count = n.Get;

        // Act
        // We 'fold' the addition over the fleet 'n' times.
        // In FP, this is a sequence of transformations, each yielding a new fleet instance.
        for (int i = 0; i < count; i++)
        {
            fleet = fleet.AddCar(new Car());
        }

        // Assert
        // The final fleet instance should reflect the total number of additions.
        return (fleet.TotalCars == count);
    }

    [Fact]
    public void LargeVolume_ShouldBeInstantaneous()
    {
        // Arrange
        var fleet = Fleet.Empty;
        var largeCount = 10_000;

        // Act
        for (int i = 0; i < largeCount; i++)
        {
            fleet = fleet.AddCar(new Car());
        }

        // We measure the performance of the TotalCars property access.
        var watch = System.Diagnostics.Stopwatch.StartNew();
        var count = fleet.TotalCars;
        watch.Stop();

        // Assert
        // In Functional C#, ImmutableList.Count is also an O(1) operation 
        // as the collection maintains its count internally.
        Assert.Equal(largeCount, count);
        Assert.True(watch.ElapsedMilliseconds < 10, $"Count took {watch.ElapsedMilliseconds}ms");
    }

    [Property]
    public bool RemovingCar_ShouldDecreaseCount_WhenCarExists(PositiveInt n)
    {
        // Arrange
        var fleet = Fleet.Empty;
        var cars = new List<Car>();
        for (int i = 0; i < n.Get; i++)
        {
            var car = new Car();
            cars.Add(car);
            fleet = fleet.AddCar(car);
        }

        // Pick a random car from the list
        var target = cars[new Random().Next(cars.Count)];

        // Act
        var updatedFleet = fleet.RemoveCar(target);

        // Assert
        return updatedFleet.TotalCars == fleet.TotalCars - 1;
    }

    [Fact]
    public void RemovingFromEmpty_ShouldReturnSameFleet()
    {
        // Arrange
        var fleet = Fleet.Empty;
        var car = new Car();

        // Act
        var updatedFleet = fleet.RemoveCar(car);

        // Assert
        Assert.Same(fleet, updatedFleet);
        Assert.Equal(0, updatedFleet.TotalCars);
    }
}
