namespace Repository.Models;

public class Rental
{
    public int Id { get; set; }
    public string BookingNumber { get; set; }
    public string SocialSecurityNumber { get; set; }
    public int CarId { get; set; }
    public DateTime PickupDateTimeUtc { get; set; }
    public int StartMileage { get; set; }
    public DateTime? ReturnDateTimeUtc { get; set; }
    public int? FinalMileage { get; set; }

    public bool IsActive => ReturnDateTimeUtc == null;
    public int? DaysRented => ReturnDateTimeUtc.HasValue
        ? (ReturnDateTimeUtc.Value - PickupDateTimeUtc).Days
        : null;
}
