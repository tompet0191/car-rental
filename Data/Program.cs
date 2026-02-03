// See https://aka.ms/new-console-template for more information

using Repository;
using Repository.Repositories;

var dbSetup = new DatabaseSetup();
// dbSetup.Initialize();

var repo = new CarRepository(dbSetup.GetConnectionString());

var cars = repo.GetAll();

foreach (var car in cars)
{
    Console.WriteLine($"{car.Id}: {car.RegistrationNumber}, Type: {car.Type}, Mileage: {car.Mileage}");
}


var car2 = repo.GetByRegistrationNumber("ABC123");
Console.WriteLine($"{car2.Id}: {car2.RegistrationNumber}, Type: {car2.Type}, Mileage: {car2.Mileage}");

var car3 = repo.GetByRegistrationNumber("ABC1235354");
Console.WriteLine(car3 is null
    ? "No such car"
    : $"{car3.Id}: {car3.RegistrationNumber}, Type: {car3.Type}, Mileage: {car3.Mileage}");
