using Domain.Models;

namespace Domain.RentalRates;

public class RentalRateFactory
{
    public static IRentalRateStrategy GetRateStrategy(CarType carType)
    {
        return carType switch
        {
            CarType.SmallCar => new SmallCarRentalRate(),
            CarType.Combi => new CombiRentalRate(),
            CarType.Truck => new TruckRentalRate(),
            _ => throw new ArgumentOutOfRangeException(nameof(carType), carType, null)
        };
    }
}
