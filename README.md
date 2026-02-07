# car-rental

TODO 
- Implement Rent calculator
  - Switch? or strategy pattern? testing 

if time:
- some console app / api to register/drop off rentals


Assumptions:
- Car types are static and does not change

Design choices:
- Started with a CQRS structure in the repo, but ultimately decided that the simple traditional repo pattern would be sufficient in this case.
- Initially modelled the db as two tables representing pickup/dropoff, but decided one table was simpler and made more sense in this case. (No need to track multiple return attempts for example)
- Implemented the base rates as a table in db, with an applydate. Advantage is that it can be changed without deploying the app, and because of the apply date changes in rates can be scheduled in advance.