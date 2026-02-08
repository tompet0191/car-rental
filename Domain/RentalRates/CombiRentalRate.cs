namespace Domain.RentalRates;

public class CombiRentalRate : IRentalRateStrategy
{
    private const decimal CombiDailyRateFactor = 1.3m;

    public decimal CalculateRental(int numberOfDays, int numberOfKm, decimal baseDayRental, decimal baseKmPrice)
    {
        var dailyCost = baseDayRental * numberOfDays * CombiDailyRateFactor;
        var kmCost = baseKmPrice * numberOfKm;

        return dailyCost + kmCost;
    }
}
