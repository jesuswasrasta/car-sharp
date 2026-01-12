// ABOUTME: Test suite for the OOP Fleet implementation.
// We use Example-Based testing (Facts) to verify specific state transitions.

using CarSharp.Oop;

namespace CarSharp.Oop.Tests;

public class FleetTests
{
    [Fact]
    public void NewFleet_ShouldBeEmpty()
    {
        // Arrange & Act
        var fleet = new Fleet();

        // Assert
        // We verify that a freshly created fleet has a count of zero.
        Assert.Equal(0, fleet.TotalCars);
    }

    [Fact]
    public void AddCar_ShouldIncreaseTotalCount()
    {
        // Arrange
        var fleet = new Fleet();
        var car = new Car();

        // Act
        // We mutate the fleet state by adding a car.
        fleet.AddCar(car);

        // Assert
        // The count should have increased to 1.
        Assert.Equal(1, fleet.TotalCars);
    }

    [Fact]
    public void RemoveCar_ShouldDecreaseTotalCount_WhenCarExists()
    {
        // Arrange
        var fleet = new Fleet();
        var car = new Car();
        fleet.AddCar(car);

        // Act
        // We remove the specific car instance.
        bool removed = fleet.RemoveCar(car);

        // Assert
        Assert.True(removed);
        Assert.Equal(0, fleet.TotalCars);
    }

    [Fact]
    public void RemoveCar_ShouldReturnFalse_WhenCarDoesNotExist()
    {
        // Arrange
        var fleet = new Fleet();
        var car = new Car();

        // Act
        bool removed = fleet.RemoveCar(car);

        // Assert
        Assert.False(removed);
        Assert.Equal(0, fleet.TotalCars);
    }

    [Fact]
    public void TotalCars_ShouldBeInstantaneous_RegardlessOfVolume()
    {
        // Arrange
        var fleet = new Fleet();
        for (int i = 0; i < 10_000; i++) fleet.AddCar(new Car());

        // Act
        var watch = System.Diagnostics.Stopwatch.StartNew();
        var count = fleet.TotalCars;
        watch.Stop();

        // Assert
        // In OOP, List.Count is an O(1) operation because the count is 
        // cached and updated during mutation.
        Assert.Equal(10_000, count);
        Assert.True(watch.ElapsedMilliseconds < 10, $"Count took {watch.ElapsedMilliseconds}ms");
    }
}
