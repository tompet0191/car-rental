using Domain.Models;

namespace Domain.Interfaces;

public interface IRentalRepository
{
    bool RegisterRental(string bookingNumber, int carId, string ssno, int mileage, DateTime? pickupDateTimeUtc = null);
    bool RegisterReturn(string bookingNumber, int finalMileage, DateTime? returnDateTimeUtc = null);
    Rental? GetByBookingNumber(string bookingNumber);
    bool IsCarCurrentlyRented(int carId);
}
