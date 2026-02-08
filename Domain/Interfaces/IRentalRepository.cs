using Domain.Models;

namespace Domain.Interfaces;

public interface IRentalRepository
{
    Task<bool> RegisterRental(string bookingNumber, int carId, string ssno, int mileage, DateTime? pickupDateTimeUtc = null);
    Task<bool> RegisterReturn(string bookingNumber, int finalMileage, DateTime? returnDateTimeUtc = null);
    Task<Rental?> GetByBookingNumber(string bookingNumber);
    Task<bool> IsCarCurrentlyRented(int carId);
}
