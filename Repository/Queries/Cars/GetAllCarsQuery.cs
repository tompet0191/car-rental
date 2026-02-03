using Microsoft.Data.Sqlite;
using Repository.Models;

namespace Repository.Queries.Cars;

public class GetAllCarsQuery
{
    private readonly string _connectionString;

    public GetAllCarsQuery(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Car> Execute()
    {
        var cars = new List<Car>();

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
