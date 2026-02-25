# Flexible Inventory Management System

## Overview
A comprehensive C# console application for managing retail inventory across multiple product categories including Electronics, Groceries, and Clothing.

## Features Implemented
- Abstract Product base class with inheritance hierarchy
- Three product types: ElectronicProduct, GroceryProduct, ClothingProduct
- Full CRUD operations (Create, Read, Update, Delete)
- Advanced reporting capabilities
- Input validation and custom exception handling
- LINQ-based queries for data operations
- Thread-safe inventory management

## Project Structure
```
FlexibleInventorySystem/
├── Models/
│   ├── Product.cs (Abstract base class)
│   ├── ElectronicProduct.cs
│   ├── GroceryProduct.cs
│   └── ClothingProduct.cs
├── Interfaces/
│   ├── IInventoryOperations.cs
│   └── IReportGenerator.cs
├── Services/
│   └── InventoryManager.cs
├── Exceptions/
│   └── InventoryException.cs
├── Utilities/
│   └── ProductValidator.cs
└── Program.cs
```

## How to Run
1. Open solution in Visual Studio or use command line
2. Build: `dotnet build FlexibleInventorySystem.sln`
3. Run: `dotnet run --project FlexibleInventorySystem`

## Key Features
- Polymorphic product handling with specialized calculations
- Automatic discounts for near-expiry groceries (20% within 3 days)
- Seasonal discounts for clothing (15% off-season)
- Warranty tracking for electronics
- Low stock alerts
- Comprehensive reporting system

## Assumptions
- Current season determined by month (June-August = Summer, else Winter)
- Sample data loaded automatically on startup
- Thread-safe operations using lock mechanism
- Prices stored as decimal for precision

## Technologies Used
- .NET 10.0
- C# with OOP principles
- LINQ for data queries
- Custom exception handling
