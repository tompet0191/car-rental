// See https://aka.ms/new-console-template for more information

using Repository;
using Repository.Queries.Cars;

var dbSetup = new DatabaseSetup();
dbSetup.Initialize();

var cars = new GetAllCarsQuery(dbSetup.GetConnectionString()).Execute();

foreach (var car in cars)
{
    Console.WriteLine($"{car.Id}: {car.RegistrationNumber}, Type: {car.Type}, Mileage: {car.Mileage}");
}
