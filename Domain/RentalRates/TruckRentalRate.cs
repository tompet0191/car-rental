namespace Domain.RentalRates;

public class TruckRentalRate : IRentalRateStrategy
{
    private const decimal TruckDailyRateFactor = 1.5m;
    private const decimal TruckKmRateFactor = 1.5m;

    public decimal CalculateRental(int numberOfDays, int numberOfKm, decimal baseDayRental, decimal baseKmPrice)
    {
        var dailyCost = baseDayRental * numberOfDays * TruckDailyRateFactor;
        var kmCost =  baseKmPrice * numberOfKm * TruckKmRateFactor;

        return dailyCost + kmCost;
    }
}
