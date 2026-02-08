using Domain.Models;

namespace Domain.Interfaces;

public interface ICarRepository
{
    Car? GetByRegistrationNumber(string registrationNumber);
    Car? GetById(int rentalCarId);
    bool UpdateMileage(int carId, int newMileage);
}
