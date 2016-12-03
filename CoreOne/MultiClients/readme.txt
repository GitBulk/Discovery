https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/existing-db


Run the following command to create a model from the existing database (Package manager console)
Scaffold-DbContext "Server=localhost;Database=Chinook;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities

