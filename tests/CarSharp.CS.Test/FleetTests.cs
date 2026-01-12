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
}