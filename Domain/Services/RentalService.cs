using Domain.Calculators;
using Domain.Interfaces;

namespace Domain.Services;

public class RentalService
{
    private readonly IRentalRepository _rentalRepo;
    private readonly ICarRepository _carRepo;
    private readonly RentalCostCalculator _costCalculator;

    public RentalService(
        IRentalRepository rentalRepo,
        ICarRepository carRepo,
        RentalCostCalculator costCalculator)
    {
        _rentalRepo = rentalRepo;
        _carRepo = carRepo;
        _costCalculator = costCalculator;
    }

    public bool RegisterRental(string bookingNumber, string regNumber, string ssno)
    {
        var car = _carRepo.GetByRegistrationNumber(regNumber);
        if (car is null)
        {
            throw new InvalidOperationException($"Car with registration {regNumber} not found");
        }

        if (_rentalRepo.IsCarCurrentlyRented(car.Id))
        {
            throw new InvalidOperationException($"Car {regNumber} is already rented");
        }

        return _rentalRepo.RegisterRental(bookingNumber, car.Id, ssno, car.Mileage);
    }

    public decimal RegisterDropoff(string bookingNumber, int finalMileage)
    {
        var rental = _rentalRepo.GetByBookingNumber(bookingNumber);
        if (rental is null)
        {
            throw new InvalidOperationException($"Rental with booking number {bookingNumber} not found");
        }

        if (rental.ReturnDateTimeUtc.HasValue)
        {
            throw new InvalidOperationException($"Rental is already returned");
        }

        var car = _carRepo.GetById(rental.CarId); // TODO implement
        if (car is null)
        {
            throw new InvalidOperationException($"Car not found");
        }

        _rentalRepo.RegisterReturn(bookingNumber, finalMileage, DateTime.UtcNow);
        _carRepo.UpdateMileage(car.Id, finalMileage);

        var rentalCost = _costCalculator.Calculate(rental, car, finalMileage);

        return rentalCost;
    }
}
