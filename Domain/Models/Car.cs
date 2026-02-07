namespace Domain.Models;

public class Car
{
    public int Id { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;
    public CarType Type { get; set; }
    public int Mileage { get; set; }
}
