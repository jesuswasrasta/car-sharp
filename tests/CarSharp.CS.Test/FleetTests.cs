using Xunit;
using CarSharp.CS;

namespace CarSharp.CS.Test;

public class FleetTests
{
        [Fact]
        public void NewFleet_Should_BeEmpty()
        {
            // Arrange
            var fleet = new Fleet();
    
            // Act & Assert
            // This will fail compilation until we add Count property
            Assert.Equal(0, fleet.Count);
        }
    
        [Fact]
        public void Add_Should_IncrementCount()
        {
            // Arrange
            var fleet = new Fleet();
            var car = new Car();
    
            // Act
            fleet.Add(car);
    
                    // Assert
                    Assert.Equal(1, fleet.Count);
                }
            
                [Fact]
                public void Remove_ExistingCar_Should_DecrementCount()
                {
                    // Arrange
                    var fleet = new Fleet();
                    var car = new Car();
                    fleet.Add(car);
            
                    // Act
                    var result = fleet.Remove(car);
            
                    // Assert
                    Assert.True(result);
                    Assert.Equal(0, fleet.Count);
                }
            }
            