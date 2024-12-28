# Skatfreunde Backend

The backend for the Skatfreunde Jöllenbeck website, for displaying analytics on the website. 
It contains a SQLite database which is handled by EF Core.

## Prerequisites

Before setting up the project, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- Your preferred database provider (e.g., SQL Server, SQLite, etc.)

## Getting Started

### Clone the Repository

```bash
git clone <repository-url>
cd <repository-folder>
```

### Restore Dependencies

Restore the required NuGet packages:

```bash
dotnet restore
```

### Configure the Database

1. Update the `appsettings.json` file with your database connection string:

   ```json
   {
       "ConnectionStrings": {
           "DefaultConnection": "YourConnectionStringHere"
       }
   }
   ```

2. Apply migrations to set up the database:

   ```bash
   dotnet ef database update
   ```

   If migrations are not created, generate them using:

   ```bash
   dotnet ef migrations add InitialCreate
   ```

### Run the Application

Start the application with the following command:

```bash
dotnet run
```

The application will be available at `http://localhost:5000` (or another configured port).

## Project Structure

- **Controllers**: Handles HTTP requests and returns responses.
- **Models**: Contains entity and data models.
- **Data**: Includes the database context and configurations.
- **Services**: Encapsulates business logic.

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- SQLite

## Contributing

1. Fork the repository.
2. Create a feature branch: `git checkout -b feature-name`
3. Commit changes: `git commit -m 'Add some feature'`
4. Push to the branch: `git push origin feature-name`
5. Open a pull request.
