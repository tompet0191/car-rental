using Domain.RentalRates;
using FluentAssertions;

namespace Tests.RentalRates;

public class SmallCarRentalRateTests
{
    private readonly SmallCarRentalRate _strategy;

    public SmallCarRentalRateTests()
    {
        _strategy = new SmallCarRentalRate();
    }

    [Fact]
    public void CalculateRental_1Day_ReturnsBaseDayRental()
    {
        // Arrange
        var baseDayRental = 100m;
        var baseKmPrice = 2m;

        // Act
        var cost = _strategy.CalculateRental(1, 0, baseDayRental, baseKmPrice);

        // Assert
        cost.Should().Be(100m);
    }

    [Fact]
    public void CalculateRental_3Days_ReturnsCorrectCost()
    {
        // Arrange
        var baseDayRental = 100m;
        var baseKmPrice = 2m;

        // Act
        var cost = _strategy.CalculateRental(3, 100, baseDayRental, baseKmPrice);

        // Assert
        cost.Should().Be(300m, "3 days * 100 per day = 300");
    }

    [Fact]
    public void CalculateRental_7Days_ReturnsCorrectCost()
    {
        // Arrange
        var baseDayRental = 50m;

        // Act
        var cost = _strategy.CalculateRental(7, 500, baseDayRental, 2.5m);

        // Assert
        cost.Should().Be(350m, "7 days * 50 per day = 350");
    }
}
