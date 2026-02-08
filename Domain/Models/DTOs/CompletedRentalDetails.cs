namespace Domain.Models.DTOs;

public class CompletedRentalDetails
{
    public string BookingNumber { get; set; }
    public string RegistrationNumber { get; set; }
    public string SocialSecurityNumber { get; set; }
    public string CarType { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public int StartKm { get; set; }
    public int FinalKm { get; set; }
}
