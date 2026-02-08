using Domain.Interfaces;
using Domain.Models;
using Microsoft.Data.Sqlite;

namespace Repository.Repositories;

public class CarRepository : ICarRepository
{
    private readonly string _connectionString;

    public CarRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Car? GetByRegistrationNumber(string registrationNumber)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

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

        using var reader = command.ExecuteReader();

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

    public Car GetById(int carId)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

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

        using var reader = command.ExecuteReader();

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

    public bool UpdateMileage(int carId, int newMileage)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

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

        return command.ExecuteNonQuery() > 0;
    }

    public IEnumerable<Car> GetAll()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

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

        using var reader = command.ExecuteReader();
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
