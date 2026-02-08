using Domain.RentalRates;
using FluentAssertions;

namespace Tests.RentalRates;

public class CombiRentalRateTests
{
    private readonly CombiRentalRate _strategy;

    public CombiRentalRateTests()
    {
        _strategy = new CombiRentalRate();
    }

    [Fact]
    public void CalculateRental_1Day0Km_ReturnsOnlyDailyCost()
    {
        // Arrange
        var baseDayRental = 100m;

        // Act
        var cost = _strategy.CalculateRental(1, 0, baseDayRental, 2m);

        // Assert
        cost.Should().Be(130m, "1 day * 100 * 1.3 factor = 130");
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
        // (3 * 100 * 1.3) + (100 * 2) = 390 + 200 = 590
        cost.Should().Be(590m);
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
        // (5 * 50 * 1.3) + (200 * 2.5) = 325 + 500 = 825
        cost.Should().Be(825m);
    }

    [Fact]
    public void CalculateRental_0Days100Km_ReturnsOnlyKmCost()
    {
        // Arrange
        var baseKmPrice = 3m;

        // Act
        var cost = _strategy.CalculateRental(0, 100, 100m, baseKmPrice);

        // Assert
        cost.Should().Be(300m, "0 days = no daily cost, only 100 km * 3");
    }
}
