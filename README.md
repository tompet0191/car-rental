# car-rental

The solution consists  of 4 projects:

- **Data** - Repository layer handling database access (SQLite)
- **Domain** - Business logic, services, pricing strategies, and domain models
- **Tests** - Unit tests for pricing strategies using xUnit and FluentAssertions
- **API** - ASP.NET Core Web API

## How to run

1. Run the API project
2. Use `API.http` file to test endpoints (or use any HTTP client)
3. The SQLite database (`data.db`) will be created automatically on first run

### How to run tests

Run `dotnet test` from solution root

## Database Schema
- **Cars** - Available vehicles with registration numbers, type, and current mileage
- **Rentals** - Active/completed rentals with pickup/return dates and mileage
- **RateConfig** - Configurable base rates with effective dates

Assumptions:
- A car's type is static and does not change
- A rental can at minimum be rented 1 day (same day returns will be counted as 1 day)

Design choices:
- Started with a CQRS structure in the repo, but ultimately decided that the simple traditional repo pattern would be sufficient in this case.
- Initially modelled the db as two tables representing pickup/dropoff, but decided one table was simpler and made more sense in this case. (No need to track multiple return attempts for example)
- Implemented the base rates as a table in db, with an applydate. Advantage is that it can be changed without deploying the app, and because of the apply date changes in rates can be scheduled in advance.
- Strategy pattern for implementing the formulas. I decided that this would make tests isolated and easy to write and therefore opted for this pattern instead of doing something like a switch and keep the calculation in there.