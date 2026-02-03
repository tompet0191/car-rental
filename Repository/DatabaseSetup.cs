using Microsoft.Data.Sqlite;

namespace Repository;

public class DatabaseSetup
{
    private readonly string _connectionString;

    public DatabaseSetup(string dbPath = "data.db")
    {
        _connectionString = $"Data Source={dbPath}";
    }

    public void Initialize()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var createTableCommand = connection.CreateCommand();
        createTableCommand.CommandText = @"
            CREATE TABLE IF NOT EXISTS Cars (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                RegistrationNumber TEXT NOT NULL UNIQUE,
                Type INTEGER NOT NULL,
                Mileage INTEGER NOT NULL DEFAULT 0
            )
        ";
        createTableCommand.ExecuteNonQuery();

        var checkCars = connection.CreateCommand();
        checkCars.CommandText = "SELECT COUNT(*) FROM Cars";
        var carCount = (long)(checkCars.ExecuteScalar() ?? 0);

        if (carCount == 0)
        {
            var addCars = connection.CreateCommand();
            addCars.CommandText = @"
                INSERT INTO Cars (RegistrationNumber, Type, Mileage) VALUES 
                ('ABC123', 1, 1500),
                ('XYZ789', 2, 4500),
                ('DEF456', 1, 800),
                ('GHI012', 3, 1200)
            ";
            addCars.ExecuteNonQuery();
        }

    }

    public string GetConnectionString() => _connectionString;
}
