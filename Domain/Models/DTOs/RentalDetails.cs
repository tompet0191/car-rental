namespace Domain.Models.DTOs;

public class RentalDetails
{
    public string BookingNumber { get; set; }
    public string RegistrationNumber { get; set; }
    public string SocialSecurityNuber { get; set; }
    public string CarType { get; set; }
    public DateTime PickupDate { get; set; }
    public int StartKm { get; set; }
}
