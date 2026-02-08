namespace Domain.Models.DTOs;

public class RentalCompletedResponse
{
    public decimal TotalCost { get; set; }
    public int DaysRented { get; set; }
    public int KilometersDriven { get; set; }
    public CompletedRentalDetails Rental { get; set; }
}
