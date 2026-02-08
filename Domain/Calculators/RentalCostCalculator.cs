using Domain.Interfaces;
using Domain.Models;
using Domain.RentalRates;

namespace Domain.Calculators;

public class RentalCostCalculator
{
    private readonly IRateRepository _rateRepo;

    public RentalCostCalculator(IRateRepository rateRepo)
    {
        _rateRepo = rateRepo;
    }

    public decimal Calculate(Rental rental, Car car)
    {
        if (!rental.DaysRented.HasValue || !rental.KmDriven.HasValue)
        {
            throw new InvalidOperationException("Can't calculate cost for incomplete rental");
        }
        var rates = _rateRepo.GetCurrentRates(rental.PickupDateTimeUtc);

        var pricingStrategy = RentalRateFactory.GetRateStrategy(car.Type);

        return pricingStrategy.CalculateRental(
            rental.DaysRented.Value,
            rental.KmDriven.Value,
            rates.DailyRate,
            rates.PricePerKm);
    }
}
