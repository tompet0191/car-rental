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
        createTableCommand.CommandText = """
            CREATE TABLE IF NOT EXISTS Cars (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                RegistrationNumber TEXT NOT NULL UNIQUE,
                Type INTEGER NOT NULL,
                Mileage INTEGER NOT NULL DEFAULT 0
            )
        """;
        createTableCommand.ExecuteNonQuery();

        var checkCars = connection.CreateCommand();
        checkCars.CommandText = "SELECT COUNT(*) FROM Cars";
        var carCount = (long)(checkCars.ExecuteScalar() ?? 0);

        if (carCount == 0)
        {
            var addCars = connection.CreateCommand();
            addCars.CommandText = """
                INSERT INTO 
                    Cars (RegistrationNumber, Type, Mileage) 
                VALUES 
                    ('ABC123', 1, 1500),
                    ('XYZ789', 2, 4500),
                    ('DEF456', 1, 800),
                    ('GHI012', 3, 1200)
            """;
            addCars.ExecuteNonQuery();
        }

		var createRentalTableCommand = connection.CreateCommand();
        createRentalTableCommand.CommandText = """
            CREATE TABLE IF NOT EXISTS Rentals (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                BookingNumber TEXT NOT NULL UNIQUE,
                SocialSecurityNumber TEXT NOT NULL,
                CarId INTEGER NOT NULL,
                PickupDateTimeUtc TEXT NOT NULL,
                StartKm INTEGER NOT NULL,
                ReturnDateTimeUtc TEXT NULL,
                FinalKm INTEGER NULL,
                FOREIGN KEY(CarId) REFERENCES Cars(Id)
            )
        """;
        createRentalTableCommand.ExecuteNonQuery();

        var createRateConfigTableCommand = connection.CreateCommand();
        createRateConfigTableCommand.CommandText = """
            CREATE TABLE IF NOT EXISTS RateConfig (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                BaseDayRental DECIMAL NOT NULL,
                PricePerKm DECIMAL NOT NULL,
                ApplyDate TEXT NOT NULL UNIQUE
            )
        """;
        createRateConfigTableCommand.ExecuteNonQuery();

        var checkRates = connection.CreateCommand();
        checkCars.CommandText = "SELECT COUNT(*) FROM RateConfig";
        var ratesCount = (long)(checkCars.ExecuteScalar() ?? 0);

        if (ratesCount == 0)
        {
            var addRate = connection.CreateCommand();
            addRate.CommandText = """
                                      INSERT INTO 
                                          RateConfig (BaseDayRental, PricePerKm, ApplyDate) 
                                      VALUES 
                                          (500, 2.5, '2026-01-01')
                                  """;
            addRate.ExecuteNonQuery();
        }
    }

       public string GetConnectionString() => _connectionString;
   }
