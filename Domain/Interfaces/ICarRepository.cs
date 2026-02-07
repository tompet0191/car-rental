using Domain.Models;

namespace Domain.Interfaces;

public interface ICarRepository
{
    Car? GetByRegistrationNumber(string registrationNumber);
    bool UpdateMileage(int carId, int newMileage);
}
