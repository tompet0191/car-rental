# car-rental

TODO 
- Refactor pickup/dropoff into one table, seems simpler, named Rental. Add FK to car instead
- Implement Rent calculator
  - Switch? or strategy pattern? testing 
- implement baserates in db
    -with applydate so we can change without deploying app

if time:
- some console app to register/drop off rentals

Design choices:
- Started with a CQRS structure in the repo, but ultimately decided that the simple traditional repo pattern would be sufficient in this case.
- Initially modelled the db as two tables representing pickup/dropoff, but decided one table was simpler and made more sense in this case. (No need to track multiple return attempts for example)
 