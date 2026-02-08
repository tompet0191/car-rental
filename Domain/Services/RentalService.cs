using Domain.Calculators;
using Domain.Interfaces;
using Domain.Models.DTOs;

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

    public RegisterRentalResponse RegisterRental(string bookingNumber, string regNumber, string ssno)
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

        var pickupTime = DateTime.UtcNow;
        _rentalRepo.RegisterRental(bookingNumber, car.Id, ssno, car.Mileage, pickupTime);

        return new RegisterRentalResponse
        {
            Rental = new RentalDetails
            {
                BookingNumber = bookingNumber,
                CarType = car.Type.ToString(),
                PickupDate = pickupTime,
                RegistrationNumber = car.RegistrationNumber,
                SocialSecurityNuber = ssno,
                StartKm = car.Mileage
            }
        };
    }

    public RentalCompletedResponse RegisterDropoff(string bookingNumber, int finalMileage)
    {
        var rental = _rentalRepo.GetByBookingNumber(bookingNumber);
        if (rental is null)
        {
            throw new InvalidOperationException($"Rental with booking number {bookingNumber} not found");
        }

        if (!rental.IsActive)
        {
            throw new InvalidOperationException($"Rental is already returned");
        }

        var car = _carRepo.GetById(rental.CarId);
        if (car is null)
        {
            throw new InvalidOperationException($"Car not found");
        }

        var returnDate = DateTime.UtcNow;
        _rentalRepo.RegisterReturn(bookingNumber, finalMileage, returnDate);
        _carRepo.UpdateMileage(car.Id, finalMileage);
        rental.ReturnDateTimeUtc = returnDate;
        rental.FinalKm = finalMileage;

        var rentalCost = _costCalculator.Calculate(rental, car);

        return new RentalCompletedResponse
        {
            TotalCost = rentalCost,
            DaysRented = rental.DaysRented.Value,
            KilometersDriven = rental.KmDriven.Value,
            Rental = new CompletedRentalDetails
            {
                BookingNumber = rental.BookingNumber,
                RegistrationNumber = car.RegistrationNumber,
                SocialSecurityNumber = rental.SocialSecurityNumber,
                CarType = car.Type.ToString(),
                PickupDate = rental.PickupDateTimeUtc,
                ReturnDate = rental.ReturnDateTimeUtc.Value,
                StartKm = rental.StartKm,
                FinalKm = rental.FinalKm.Value
            }
        };
    }
}
