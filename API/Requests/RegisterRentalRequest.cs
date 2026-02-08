namespace API.Requests;

public record RegisterRentalRequest(
    string BookingNumber,
    string RegNumber,
    string Ssno
);
