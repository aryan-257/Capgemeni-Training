# 📖 Code Explanation - Simple Language

## What This Project Does

This is a flight booking website where users can:
1. Search for flights between cities
2. Search for flight + hotel packages
3. See prices based on number of travelers

## How It Works (Simple Explanation)

### 1. Database (FlightSearchEngine_Database.sql)
- **What it does:** Stores all flight and hotel information
- **Tables:**
  - `Flights` table: Has 16 flights with prices
  - `Hotels` table: Has 6 hotels (one per city)
- **Stored Procedures:** 4 pre-written queries that fetch data

### 2. Models (Data Containers)
Think of models as boxes that hold information:

- **SearchViewModel.cs:** Holds search form data
  - Source city
  - Destination city
  - Number of persons
  
- **FlightResult.cs:** Holds flight search results
  - Flight name, type, price, etc.
  
- **FlightHotelResult.cs:** Holds package search results
  - Flight + hotel name and total price

### 3. DatabaseHelper.cs (Talks to Database)
This class connects to SQL Server and gets data:

- `GetSourcesAsync()` → Gets list of cities for "Source" dropdown
- `GetDestinationsAsync()` → Gets list of cities for "Destination" dropdown
- `SearchFlightsAsync()` → Searches for flights
- `SearchFlightsWithHotelsAsync()` → Searches for packages

**How it works:**
1. Opens connection to database
2. Calls stored procedure
3. Reads data row by row
4. Returns list of results

### 4. FlightController.cs (Handles User Requests)
This is the brain of the application:

- **Index()** → Shows search form with dropdowns
- **SearchFlights()** → When user clicks "Search Flights Only"
  - Validates input
  - Calls database to search
  - Shows results page
  
- **SearchFlightsWithHotels()** → When user clicks "Search Flight + Hotel"
  - Validates input
  - Calls database to search
  - Shows results page

**Flow:**
1. User fills form → Controller receives data
2. Controller validates data (checks if valid)
3. Controller calls DatabaseHelper
4. DatabaseHelper gets data from SQL Server
5. Controller sends data to Results page
6. User sees results

### 5. Views (What User Sees)

- **Index.cshtml:** Search form page
  - Two dropdowns (Source, Destination)
  - Number input (Persons)
  - Two buttons (Flights Only, Flight + Hotel)
  
- **Results.cshtml:** Results page
  - Shows table of flights or packages
  - Shows total cost
  - "Back to Search" button
  
- **Test.cshtml:** Simple test page (no JavaScript)

## Data Flow (Step by Step)

```
User Opens Website
    ↓
Index() method runs
    ↓
Gets cities from database
    ↓
Shows search form with dropdowns
    ↓
User fills form and clicks search
    ↓
SearchFlights() or SearchFlightsWithHotels() runs
    ↓
Validates user input
    ↓
Calls DatabaseHelper
    ↓
DatabaseHelper calls stored procedure
    ↓
SQL Server returns data
    ↓
Controller receives data
    ↓
Shows Results page with data
    ↓
User sees flights/packages
```

## Key Concepts Explained

### 1. MVC Pattern
- **Model:** Data containers (SearchViewModel, FlightResult)
- **View:** What user sees (Index.cshtml, Results.cshtml)
- **Controller:** Handles requests (FlightController)

### 2. Async/Await
- `async` and `await` make database calls non-blocking
- App doesn't freeze while waiting for database
- Better performance

### 3. Stored Procedures
- Pre-written SQL queries stored in database
- Faster and more secure than writing SQL in code
- Example: `sp_SearchFlights` searches for flights

### 4. Validation
- **Client-side:** JavaScript checks form before submitting
- **Server-side:** Controller checks data again for security
- Example: Source and Destination can't be same

### 5. SelectList
- Creates dropdown list from database data
- Example: Cities from database → Dropdown options

## Simple Analogy

Think of this like ordering food online:

1. **Database** = Restaurant menu (has all items and prices)
2. **Models** = Order form (holds your selections)
3. **DatabaseHelper** = Waiter (gets menu, takes order to kitchen)
4. **Controller** = Manager (receives order, validates it, coordinates)
5. **Views** = Website pages (menu page, order confirmation page)

**Flow:**
- You open website (Index page)
- See menu items in dropdowns (from database)
- Fill order form (select items)
- Click "Order" button (submit form)
- Manager validates order (Controller)
- Waiter takes order to kitchen (DatabaseHelper → Database)
- Kitchen prepares order (SQL query runs)
- Waiter brings food (data returns)
- You see confirmation (Results page)

## Code Comments Guide

All code now has simple comments explaining:
- What each method does
- What each variable holds
- How data flows
- Why we do certain things

Example:
```csharp
// Get list of all source cities for dropdown
public async Task<List<string>> GetSourcesAsync()
{
    var sources = new List<string>();  // Create empty list
    
    // Connect to database
    using (var connection = new SqlConnection(_connectionString))
    {
        // Call stored procedure
        using (var command = new SqlCommand("sp_GetSources", connection))
        {
            // ... rest of code
        }
    }
    
    return sources;  // Return list of cities
}
```

## Tips for Understanding

1. **Start with Models** - Understand what data we're working with
2. **Then DatabaseHelper** - See how we get data
3. **Then Controller** - See how we handle requests
4. **Finally Views** - See what user sees

5. **Follow one flow** - Pick "Search Flights" and follow it from start to finish

6. **Read comments** - Every important line has a comment explaining it

## Common Questions

**Q: Why use stored procedures?**
A: Faster, more secure, and easier to maintain than SQL in code.

**Q: Why async/await?**
A: So app doesn't freeze while waiting for database.

**Q: What's the difference between FlightResult and FlightHotelResult?**
A: FlightResult is for flights only. FlightHotelResult includes hotel info.

**Q: Why validate twice (client and server)?**
A: Client-side for user experience. Server-side for security (users can bypass client-side).

**Q: What if database connection fails?**
A: Code has try-catch blocks that show error messages instead of crashing.

## Summary

This is a simple 3-layer application:
1. **Presentation Layer** (Views) - What user sees
2. **Business Layer** (Controller) - Handles logic
3. **Data Layer** (DatabaseHelper + SQL) - Manages data

All layers work together to provide a smooth flight search experience!
