using Domain.Interfaces;
using Domain.Models;
using Microsoft.Data.Sqlite;

namespace Data.Repositories;

public class CarRepository : ICarRepository
{
    private readonly string _connectionString;

    public CarRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Car?> GetByRegistrationNumber(string registrationNumber)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.Parameters.AddWithValue("$regNumber", registrationNumber);
        command.CommandText = """
            SELECT
                Id,
                RegistrationNumber,
                Type,
                Mileage
            FROM 
                Cars
            WHERE
                RegistrationNumber = $regNumber
        """;

        using var reader = await command.ExecuteReaderAsync();

        if (reader.Read())
        {
            return new Car
            {
                Id = reader.GetInt32(0),
                RegistrationNumber = reader.GetString(1),
                Type = (CarType)reader.GetInt32(2),
                Mileage = reader.GetInt32(3)
            };
        }

        return null;
    }

    public async Task<Car?> GetById(int carId)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.Parameters.AddWithValue("$carId", carId);
        command.CommandText = """
            SELECT
                Id,
                RegistrationNumber,
                Type,
                Mileage
            FROM 
                Cars
            WHERE
                Id = $carId
        """;

        using var reader = await command.ExecuteReaderAsync();

        if (reader.Read())
        {
            return new Car
            {
                Id = reader.GetInt32(0),
                RegistrationNumber = reader.GetString(1),
                Type = (CarType)reader.GetInt32(2),
                Mileage = reader.GetInt32(3)
            };
        }

        return null;
    }

    public async Task<bool> UpdateMileage(int carId, int newMileage)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.Parameters.AddWithValue("$id", carId);
        command.Parameters.AddWithValue("$mileage", newMileage);

        command.CommandText = """
            UPDATE 
                Cars
            SET 
                Mileage = $mileage
            WHERE 
                Id = $id
        """;

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<IEnumerable<Car>> GetAll()
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = """
          SELECT 
              Id, 
              RegistrationNumber, 
              Type, 
              Mileage 
          FROM 
              Cars
        """;

        using var reader = await command.ExecuteReaderAsync();
        var cars = new List<Car>();

        while (reader.Read())
        {
            cars.Add(new Car
            {
                Id = reader.GetInt32(0),
                RegistrationNumber = reader.GetString(1),
                Type = (CarType)reader.GetInt32(2),
                Mileage = reader.GetInt32(3)
            });
        }

        return cars;
    }
}
