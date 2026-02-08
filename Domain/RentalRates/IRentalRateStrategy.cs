namespace Domain.RentalRates;

public interface IRentalRateStrategy
{
    public decimal CalculateRental(int numberOfDays, int numberOfKm, decimal baseDayRental, decimal baseKmPrice);
}
