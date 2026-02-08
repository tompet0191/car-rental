using Domain.RentalRates;
using FluentAssertions;

namespace Tests.RentalRates;

public class TruckRentalRateTests
{
    private readonly TruckRentalRate _strategy;

    public TruckRentalRateTests()
    {
        _strategy = new TruckRentalRate();
    }

    [Fact]
    public void CalculateRental_1Day0Km_ReturnsOnlyDailyCost()
    {
        // Arrange
        var baseDayRental = 100m;

        // Act
        var cost = _strategy.CalculateRental(1, 0, baseDayRental, 2m);

        // Assert
        cost.Should().Be(150m, "1 day * 100 * 1.5 factor = 150");
    }

    [Fact]
    public void CalculateRental_0Days100Km_ReturnsOnlyKmCost()
    {
        // Arrange
        var baseKmPrice = 2m;

        // Act
        var cost = _strategy.CalculateRental(0, 100, 100m, baseKmPrice);

        // Assert
        cost.Should().Be(300m, "100 km * 2 * 1.5 factor = 300");
    }

    [Fact]
    public void CalculateRental_3Days100Km_ReturnsCorrectCost()
    {
        // Arrange
        var baseDayRental = 100m;
        var baseKmPrice = 2m;

        // Act
        var cost = _strategy.CalculateRental(3, 100, baseDayRental, baseKmPrice);

        // Assert
        // (3 * 100 * 1.5) + (100 * 2 * 1.5) = 450 + 300 = 750
        cost.Should().Be(750m);
    }

    [Fact]
    public void CalculateRental_5Days200Km_ReturnsCorrectCost()
    {
        // Arrange
        var baseDayRental = 50m;
        var baseKmPrice = 2.5m;

        // Act
        var cost = _strategy.CalculateRental(5, 200, baseDayRental, baseKmPrice);

        // Assert
        // (5 * 50 * 1.5) + (200 * 2.5 * 1.5) = 375 + 750 = 1125
        cost.Should().Be(1125m);
    }
}
