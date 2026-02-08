using Domain.Models;

namespace Domain.Interfaces;

public interface ICarRepository
{
    Task<Car?> GetByRegistrationNumber(string registrationNumber);
    Task<Car?> GetById(int rentalCarId);
    Task<bool> UpdateMileage(int carId, int newMileage);
}
