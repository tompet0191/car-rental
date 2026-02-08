using Domain.Interfaces;
using Domain.Models;
using Microsoft.Data.Sqlite;

namespace Data.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly string _connectionString;

    public RentalRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<bool> RegisterRental(string bookingNumber, int carId, string ssno, int mileage, DateTime? pickupDateTimeUtc = null)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.Parameters.AddWithValue("$bookingNumber", bookingNumber);
        command.Parameters.AddWithValue("$carId", carId);
        command.Parameters.AddWithValue("$ssno", ssno);
        command.Parameters.AddWithValue("$pickup", pickupDateTimeUtc ?? DateTime.UtcNow);
        command.Parameters.AddWithValue("$mileage", mileage);

        command.CommandText = """
            INSERT INTO
                Rentals (BookingNumber, SocialSecurityNumber, CarId, PickupDateTimeUtc, StartKm )
                VALUES  ($bookingNumber, $ssno, $carId, $pickup, $mileage)
        """;

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> RegisterReturn(string bookingNumber, int finalMileage, DateTime? returnDateTimeUtc = null)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.Parameters.AddWithValue("$bookingNumber", bookingNumber);
        command.Parameters.AddWithValue("$finalMileage", finalMileage);
        command.Parameters.AddWithValue("$return", returnDateTimeUtc ?? DateTime.UtcNow);

        command.CommandText = """
            UPDATE
                Rentals
            SET
                FinalKm = $finalMileage,
                ReturnDateTimeUtc = $return
            WHERE
                BookingNumber = $bookingNumber
        """;

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> IsCarCurrentlyRented(int carId)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.Parameters.AddWithValue("$carId", carId);
        command.CommandText = """
            SELECT 
                COUNT(*)
            FROM 
                Rentals r
            WHERE 
                r.CarId = $carId AND
                r.ReturnDateTimeUtc IS NULL
        """;

        var count = (long)(await command.ExecuteScalarAsync())!;
        return count > 0;
    }

    public async Task<Rental?> GetByBookingNumber(string bookingNumber)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.Parameters.AddWithValue("$bookingNumber", bookingNumber);
        command.CommandText = """
            SELECT 
                Id, 
                BookingNumber, 
                SocialSecurityNumber, 
                CarId, 
                PickupDateTimeUtc, 
                StartKm, 
                ReturnDateTimeUtc, 
                FinalKm
            FROM 
                Rentals
            WHERE 
                BookingNumber = $bookingNumber
        """;

        using var reader = await command.ExecuteReaderAsync();
        if (reader.Read())
        {
            return new Rental
            {
                Id = reader.GetInt32(0),
                BookingNumber = reader.GetString(1),
                SocialSecurityNumber = reader.GetString(2),
                CarId = reader.GetInt32(3),
                PickupDateTimeUtc = DateTime.Parse(reader.GetString(4)),
                StartKm = reader.GetInt32(5),
                ReturnDateTimeUtc = reader.IsDBNull(6) ? null : DateTime.Parse(reader.GetString(6)),
                FinalKm = reader.IsDBNull(7) ? null : reader.GetInt32(7)
            };
        }

        return null;
    }
}
