using Domain.Interfaces;
using Domain.Models;

namespace Domain.Calculators;

public class RentalCostCalculator
{
    private readonly IRateRepository _rateRepo;

    public RentalCostCalculator(IRateRepository rateRepo)
    {
        _rateRepo = rateRepo;
    }

    public decimal Calculate(Rental rental, Car car, int finalMileage)
    {
        // TODO
        // get current rates
        // calculate cost based on car.Type
        throw new NotImplementedException();
    }
}
