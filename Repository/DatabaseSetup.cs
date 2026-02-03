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
                RegistrationNumber TEXT NOT NULL,
                Type INTEGER NOT NULL,
                Mileage INTEGER NOT NULL DEFAULT 0
            )
        ";
        createTableCommand.ExecuteNonQuery();
    }

    public string GetConnectionString() => _connectionString;
}
