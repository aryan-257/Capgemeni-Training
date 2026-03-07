# Library Book Management System - Repository Pattern

## Project Overview
This ASP.NET Core MVC application demonstrates the **Repository Pattern** implementation for a Library Book Management System. The project showcases how to design a system that can easily switch between different data storage implementations (In-Memory vs SQL Server) with minimal code changes.

---

## Architecture & Design Pattern

### Repository Pattern Benefits
- **Separation of Concerns**: Data access logic is isolated from business logic
- **Flexibility**: Easy to switch between different storage implementations
- **Testability**: Controllers can be tested with mock repositories
- **Maintainability**: Changes to data access are confined to repository classes

---

## Project Structure

```
EF_CoreApplication/
├── Models/
│   └── Book.cs                      # Book entity model
├── Repository/
│   ├── IBookRepository.cs           # Repository interface
│   ├── MemoryBookRepository.cs      # Phase 1: In-memory implementation
│   └── SqlBookRepository.cs         # Phase 2: SQL Server implementation
├── Data/
│   └── LibraryDbContext.cs          # Entity Framework DbContext
├── Controllers/
│   └── BookController.cs            # MVC Controller (unchanged between phases)
├── Views/Book/
│   ├── List.cshtml                  # Display all books
│   ├── Details.cshtml               # Display single book
│   ├── Create.cshtml                # Add new book
│   └── Delete.cshtml                # Delete confirmation
├── appsettings.json                 # Configuration & connection string
└── Program.cs                       # Dependency injection configuration
```

---

## Phase 1 - In-Memory Storage

### Implementation Details

**Model** (`Models/Book.cs`):
```csharp
public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
}
```

**Repository Interface** (`Repository/IBookRepository.cs`):
```csharp
public interface IBookRepository
{
    List<Book> GetAllBooks();
    Book? GetBookById(int id);
    void AddBook(Book book);
    void DeleteBook(int id);
}
```

**In-Memory Repository** (`Repository/MemoryBookRepository.cs`):
- Uses `Dictionary<int, Book>` for storage
- Pre-loaded with 3 sample books:
  - Clean Code - Robert C. Martin (₹45.99)
  - Design Patterns - GoF (₹54.99)
  - Refactoring - Martin Fowler (₹49.99)

**Configuration** (`Program.cs`):
```csharp
builder.Services.AddScoped<IBookRepository, MemoryBookRepository>();
```

### Phase 1 Characteristics
✅ No database required  
✅ Fast development and testing  
✅ Data stored in memory  
❌ Data lost on application restart  

---

## Phase 2 - SQL Server Storage

### Implementation Details

**DbContext** (`Data/LibraryDbContext.cs`):
```csharp
public class LibraryDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
}
```

**SQL Repository** (`Repository/SqlBookRepository.cs`):
- Implements `IBookRepository` using Entity Framework Core
- Performs CRUD operations on SQL Server database

**Configuration** (`Program.cs`):
```csharp
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBookRepository, SqlBookRepository>();
```

**Connection String** (`appsettings.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLExpress;Database=EfcoreAssessment;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

### Phase 2 Characteristics
✅ Persistent data storage  
✅ Production-ready  
✅ Data survives application restarts  
✅ Supports concurrent users  

---

## Controller Implementation

**BookController** uses **Constructor Injection** to receive the repository:

```csharp
public class BookController : Controller
{
    private readonly IBookRepository _repo;

    public BookController(IBookRepository repo)
    {
        _repo = repo;
    }

    // Actions: List, Details, Create, Delete
}
```

### Available Actions
| Action | HTTP Method | Purpose |
|--------|-------------|---------|
| `List` | GET | Display all books |
| `Details` | GET | Display single book details |
| `Create` | GET/POST | Add new book |
| `Delete` | GET/POST | Delete book |

---

## Switching Between Phases

### Switch to Phase 1 (In-Memory)
1. Open `Program.cs`
2. **Uncomment** Phase 1 configuration:
   ```csharp
   builder.Services.AddScoped<IBookRepository, MemoryBookRepository>();
   ```
3. **Comment out** Phase 2 configuration:
   ```csharp
   // builder.Services.AddDbContext<LibraryDbContext>(...);
   // builder.Services.AddScoped<IBookRepository, SqlBookRepository>();
   ```
4. Run: `dotnet run`

### Switch to Phase 2 (SQL Server)
1. Open `Program.cs`
2. **Comment out** Phase 1 configuration:
   ```csharp
   // builder.Services.AddScoped<IBookRepository, MemoryBookRepository>();
   ```
3. **Uncomment** Phase 2 configuration:
   ```csharp
   builder.Services.AddDbContext<LibraryDbContext>(...);
   builder.Services.AddScoped<IBookRepository, SqlBookRepository>();
   ```
4. Create/update database:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
5. Run: `dotnet run`

---

## Running the Application

### Prerequisites
- .NET 10.0 SDK
- SQL Server Express (for Phase 2)

### Commands
```bash
# Restore packages
dotnet restore

# Build project
dotnet build

# Run application
dotnet run
```

### Access Application
Navigate to: **http://localhost:5047/Book/List**

---

## Key Features

✅ **Add Books**: Create new book entries  
✅ **View Books**: List all books in a table  
✅ **Book Details**: View detailed information  
✅ **Delete Books**: Remove books from storage  
✅ **Responsive UI**: Bootstrap-styled interface  
✅ **Currency Display**: Prices shown in ₹ (Rupees)  

---

## Important Implementation Rules

### ✅ What Changes Between Phases
- Repository implementation (`MemoryBookRepository` ↔ `SqlBookRepository`)
- Dependency injection configuration in `Program.cs`

### ❌ What Remains Unchanged
- `Book` model
- `IBookRepository` interface
- `BookController` logic
- All views (`.cshtml` files)
- Controller actions and routing

**This demonstrates the power of the Repository Pattern!**

---

## Current Configuration

**Status**: Phase 2 (SQL Server) is currently active  
**Database**: EfcoreAssessment on SQL Express  
**URL**: http://localhost:5047/Book/List

---

## Technologies Used

- ASP.NET Core MVC (.NET 10.0)
- Entity Framework Core 9.0
- SQL Server Express
- Bootstrap 5 (UI Framework)
- Razor Views

---

## Conclusion

This project successfully demonstrates how the **Repository Pattern** enables flexible, maintainable, and testable code by abstracting data access logic. The ability to switch between in-memory and database storage with only configuration changes showcases the pattern's effectiveness in real-world applications.
