0. Please clone this repository and [HotelBackend.Tests](https://github.com/rpetrano/HotelBackend.Tests) into the same directory!

1. Install the EF Core Tools

```
dotnet tool install --global dotnet-ef
```

2. Make sure the database is running

View [run-db.md](run-db.md)

3. Apply the initial migrations

```
dotnet ef database update
```

4. Run the tests (from the HotelBackend.Tests folder). This runs Unit, Integration and Functional tests. Functional tests are spawning a copy of api server with a test database.
```
dotnet test
```

4. Run the application (with Main database -> back from the HotelBackend folder):
```
dotnet run
```

5. You can now [access Swagger](http://localhost:5022/swagger/index.html).

6. You can play around in Swagger, it's should be pretty straightforward. PUT, POST and DELETE methods requires ApiKey to be set, it's "1234567890"

7. If you want to have the database populated by some reasonable initial data, I recommend the following approach:

8. Kill the server if it's running and switch to "Testing" database:
```
export DATABASE_ENVIRONMENT=Testing
```

9. Run the integration test that populates the Testing database (from the HotelBackend.Tests folder):
```
dotnet test --filter FullyQualifiedName=HotelBackend.FunctionalTests.HotelsControllerFunctionalTests.TestInsertApi
```

10. Back in HotelBackend folder, run the server again (in the same Terminal where the export from step 8 was done):
```
dotnet run
```

11. You can now again [access Swagger](http://localhost:5022/swagger/index.html).

12. A good location to use the search API is: 45.769841, 15.9500648

