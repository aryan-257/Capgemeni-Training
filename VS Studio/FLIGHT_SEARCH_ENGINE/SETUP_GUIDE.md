# 🚀 Quick Setup Guide

## Step 1: Database (2 minutes)
1. Open SQL Server Management Studio (SSMS)
2. Execute: `Database/FlightSearchEngine_Database.sql`
3. Verify: 16 Flights, 6 Hotels created

## Step 2: Connection String (1 minute)
Open `appsettings.json` and update:

**For SQL Server Express (most common):**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=FlightSearchEngineDB;Integrated Security=True;TrustServerCertificate=True;Encrypt=False;"
}
```

**For default SQL Server:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(local);Database=FlightSearchEngineDB;Integrated Security=True;TrustServerCertificate=True;Encrypt=False;"
}
```

## Step 3: Run (1 minute)
```bash
dotnet run
```

Open browser: `https://localhost:5001`

## Testing
- **Test page (simple):** `https://localhost:5001/Flight/Test`
- **Main page:** `https://localhost:5001/Flight/Index`

## Quick Test
1. Source: Delhi
2. Destination: Bangalore
3. Persons: 1
4. Click "Search Flights Only"
5. Should show 1 flight result

## Troubleshooting
- **Empty dropdowns?** Check connection string
- **SQL Server not found?** Ensure SQL Server service is running
- **Build errors?** Run `dotnet restore` then `dotnet build`

