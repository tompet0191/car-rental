namespace Domain.RentalRates;

public class SmallCarRentalRate : IRentalRateStrategy
{
    public decimal CalculateRental(int numberOfDays, int numberOfKm, decimal baseDayRental, decimal baseKmPrice)
    {
       var dailyCost = baseDayRental * numberOfDays;

       return dailyCost;
    }
}
