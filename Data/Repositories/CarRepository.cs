using Microsoft.Data.Sqlite;
using Repository.Models;

namespace Repository.Repositories;

public class CarRepository
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
