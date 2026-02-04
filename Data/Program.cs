// See https://aka.ms/new-console-template for more information

using Repository;
using Repository.Repositories;

var dbSetup = new DatabaseSetup();
dbSetup.Initialize();

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

var repo2 = new RentalRepository(dbSetup.GetConnectionString());

// var success = repo2.RegisterRental("XXX123", car2.Id, "123", car2.Mileage);

repo2.RegisterReturn("XXX123", 2000);

var rental = repo2.GetByBookingNumber("XXX123");

Console.WriteLine(repo2.IsCarCurrentlyRented("ABC123"));

