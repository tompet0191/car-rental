using Data;
using Data.Repositories;
using Domain.Calculators;
using Domain.Interfaces;
using Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var db = new DatabaseSetup();
db.Initialize();
var connectionString = db.GetConnectionString();

builder.Services.AddScoped<IRentalRepository>(_ => new RentalRepository(connectionString));
builder.Services.AddScoped<ICarRepository>(_ => new CarRepository(connectionString));
builder.Services.AddScoped<IRateRepository>(_ => new RateRepository(connectionString));builder.Services.AddScoped<RentalService>();
builder.Services.AddScoped<RentalCostCalculator>();
builder.Services.AddScoped<RentalService>();

var app = builder.Build();

app.MapControllers();

app.Run();
