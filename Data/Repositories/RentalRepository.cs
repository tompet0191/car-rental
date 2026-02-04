using Microsoft.Data.Sqlite;
using Repository.Models;

namespace Repository.Repositories;

public class RentalRepository
{
    private readonly string _connectionString;

    public RentalRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool RegisterRental(string bookingNumber, int carId, string ssno, int mileage, DateTime? pickupDateTimeUtc = null)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.Parameters.AddWithValue("$bookingNumber", bookingNumber);
        command.Parameters.AddWithValue("$carId", carId);
        command.Parameters.AddWithValue("$ssno", ssno);
        command.Parameters.AddWithValue("$pickup", (pickupDateTimeUtc ?? DateTime.UtcNow).ToString("o")); // TODO isoformat set constant
        command.Parameters.AddWithValue("$mileage", mileage);

        command.CommandText = """
            INSERT INTO
                Rentals (BookingNumber, SocialSecurityNumber, CarId, PickupDateTimeUtc, StartMileage )
                VALUES  ($bookingNumber, $ssno, $carId, $pickup, $mileage)
        """;

        return command.ExecuteNonQuery() > 0;
    }

    public bool RegisterReturn(string bookingNumber, int finalMileage, DateTime? returnDateTimeUtc = null)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.Parameters.AddWithValue("$bookingNumber", bookingNumber);
        command.Parameters.AddWithValue("$finalMileage", finalMileage);
        command.Parameters.AddWithValue("$return", returnDateTimeUtc ?? DateTime.UtcNow); // TODO isoformat set constant

        command.CommandText = """
            UPDATE
                Rentals
            SET
                FinalMileage = $finalMileage,
                ReturnDateTimeUtc = $return
            WHERE
                BookingNumber = $bookingNumber
        """;

        return command.ExecuteNonQuery() > 0;
    }

    public bool IsCarCurrentlyRented(string registrationNumber)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.Parameters.AddWithValue("$regNumber", registrationNumber);
        command.CommandText = """
            SELECT 
                COUNT(*)
            FROM 
                Rentals r
                JOIN Cars c ON 
                    r.CarId = c.Id
            WHERE 
                c.RegistrationNumber = $regNumber AND
                r.ReturnDateTimeUtc IS NULL
        """;

        var count = (long)command.ExecuteScalar()!;
        return count > 0;
    }

    public Rental? GetByBookingNumber(string bookingNumber)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.Parameters.AddWithValue("$bookingNumber", bookingNumber);
        command.CommandText = """
            SELECT 
                Id, 
                BookingNumber, 
                SocialSecurityNumber, 
                CarId, 
                PickupDateTimeUtc, 
                StartMileage, 
                ReturnDateTimeUtc, 
                FinalMileage
            FROM 
                Rentals
            WHERE 
                BookingNumber = $bookingNumber
        """;

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Rental
            {
                Id = reader.GetInt32(0),
                BookingNumber = reader.GetString(1),
                SocialSecurityNumber = reader.GetString(2),
                CarId = reader.GetInt32(3),
                PickupDateTimeUtc = DateTime.Parse(reader.GetString(4)),
                StartMileage = reader.GetInt32(5),
                ReturnDateTimeUtc = reader.IsDBNull(6) ? null : DateTime.Parse(reader.GetString(6)),
                FinalMileage = reader.IsDBNull(7) ? null : reader.GetInt32(7)
            };
        }

        return null;
    }
}
