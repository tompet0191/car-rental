using Domain.Interfaces;
using Domain.Models;
using Microsoft.Data.Sqlite;

namespace Repository.Repositories;

public class RateRepository : IRateRepository
{
    private readonly string _connectionString;

    public RateRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public RateConfig GetCurrentRates(DateTime forDate)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.Parameters.AddWithValue("$date", forDate.ToString("o"));
        command.CommandText = """
            SELECT
                BaseDayRental,
                PricePerKm
            FROM
               RateConfig
            WHERE
                ApplyDate <= $date
            ORDER BY
                ApplyDate DESC
            LIMIT 1
        """;

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new RateConfig
            {
                DailyRate = reader.GetDecimal(0),
                PricePerKm = reader.GetDecimal(1)
            };
        }

        throw new InvalidOperationException("No rates found"); // TODO custom error would be nice
    }
}
