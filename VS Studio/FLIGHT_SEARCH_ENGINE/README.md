# ✈️ Flight Search Engine - ASP.NET Core MVC

## Quick Setup

### 1. Database Setup
1. Open SQL Server Management Studio (SSMS)
2. Execute: `Database/FlightSearchEngine_Database.sql`

### 2. Connection String
Update `appsettings.json` with your SQL Server instance:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=FlightSearchEngineDB;Integrated Security=True;TrustServerCertificate=True;Encrypt=False;"
}
```

### 3. Run Application
```bash
dotnet run
```
Open: `https://localhost:5001`

## Project Structure
- `Controllers/FlightController.cs` - Main controller
- `Models/` - SearchViewModel, FlightResult, FlightHotelResult
- `Views/Flight/` - Index (search form), Results (display), Test (simple test)
- `Data/DatabaseHelper.cs` - Database operations
- `Database/FlightSearchEngine_Database.sql` - Complete DB script

## Features
- Search flights by source and destination
- Search flight+hotel packages
- Dynamic dropdowns from database
- Client and server-side validation
- Responsive UI with Bootstrap 5

## Testing
Test page (no JavaScript): `https://localhost:5001/Flight/Test`
Main page: `https://localhost:5001/Flight/Index`

