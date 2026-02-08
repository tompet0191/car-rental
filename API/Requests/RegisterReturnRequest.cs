namespace API.Requests;

public record RegisterReturnRequest(
    string BookingNumber,
    int FinalMileage
);
