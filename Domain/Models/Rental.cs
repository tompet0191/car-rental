namespace Domain.Models;

public class Rental
{
    public int Id { get; set; }
    public string BookingNumber { get; set; }
    public string SocialSecurityNumber { get; set; }
    public int CarId { get; set; }
    public DateTime PickupDateTimeUtc { get; set; }
    public int StartKm { get; set; }
    public DateTime? ReturnDateTimeUtc { get; set; }
    public int? FinalKm { get; set; }

    public bool IsActive => ReturnDateTimeUtc == null;
    public int? DaysRented => ReturnDateTimeUtc.HasValue
        ? Math.Max(1, (ReturnDateTimeUtc.Value - PickupDateTimeUtc).Days)
        : null;

    public int? KmDriven => FinalKm.HasValue
        ? FinalKm - StartKm
        : null;
}
