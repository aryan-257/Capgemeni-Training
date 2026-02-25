-- =============================================
-- Product Management Database Setup Script
-- =============================================

-- Use existing database
USE LPU_Db;
GO

-- Create Products Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
BEGIN
    CREATE TABLE Products (
        ProdID INT PRIMARY KEY,
        ProdName NVARCHAR(100) NOT NULL,
        Price INT NOT NULL CHECK (Price >= 0),
        Description NVARCHAR(500),
        CategoryID INT NULL,
        CreatedDate DATETIME DEFAULT GETDATE(),
        ModifiedDate DATETIME DEFAULT GETDATE()
    );
END
GO

-- Create Categories Table (Optional - for future use)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories')
BEGIN
    CREATE TABLE Categories (
        CategoryID INT PRIMARY KEY IDENTITY(1,1),
        CategoryName NVARCHAR(100) NOT NULL,
        Description NVARCHAR(500)
    );
END
GO

-- Insert Sample Categories
IF NOT EXISTS (SELECT * FROM Categories)
BEGIN
    INSERT INTO Categories (CategoryName, Description) VALUES
    ('Electronics', 'Electronic devices and gadgets'),
    ('Clothing', 'Apparel and fashion items'),
    ('Books', 'Books and publications'),
    ('Home & Kitchen', 'Home appliances and kitchen items'),
    ('Sports', 'Sports equipment and accessories');
END
GO

-- Insert Sample Products
IF NOT EXISTS (SELECT * FROM Products)
BEGIN
    INSERT INTO Products (ProdID, ProdName, Price, Description, CategoryID) VALUES
    (1, 'Laptop', 45000, 'Dell Inspiron 15 - 8GB RAM, 512GB SSD', 1),
    (2, 'Smartphone', 25000, 'Samsung Galaxy A52 - 6GB RAM', 1),
    (3, 'T-Shirt', 500, 'Cotton T-Shirt - Size M', 2),
    (4, 'Jeans', 1200, 'Denim Jeans - Blue', 2),
    (5, 'Programming Book', 800, 'C# Programming Guide', 3),
    (6, 'Microwave', 8000, 'LG Microwave Oven - 20L', 4),
    (7, 'Football', 600, 'Nike Football - Size 5', 5),
    (8, 'Headphones', 2500, 'Sony Wireless Headphones', 1),
    (9, 'Coffee Maker', 3500, 'Philips Coffee Maker', 4),
    (10, 'Running Shoes', 3000, 'Adidas Running Shoes - Size 9', 5);
END
GO

-- Create Stored Procedures (Optional - for better performance)

-- Procedure to Add Product
CREATE OR ALTER PROCEDURE sp_AddProduct
    @ProdID INT,
    @ProdName NVARCHAR(100),
    @Price INT,
    @Description NVARCHAR(500),
    @CategoryID INT = NULL
AS
BEGIN
    INSERT INTO Products (ProdID, ProdName, Price, Description, CategoryID)
    VALUES (@ProdID, @ProdName, @Price, @Description, @CategoryID);
END
GO

-- Procedure to Update Product
CREATE OR ALTER PROCEDURE sp_UpdateProduct
    @ProdID INT,
    @ProdName NVARCHAR(100),
    @Price INT,
    @Description NVARCHAR(500)
AS
BEGIN
    UPDATE Products
    SET ProdName = @ProdName,
        Price = @Price,
        Description = @Description,
        ModifiedDate = GETDATE()
    WHERE ProdID = @ProdID;
END
GO

-- Procedure to Delete Product
CREATE OR ALTER PROCEDURE sp_DeleteProduct
    @ProdID INT
AS
BEGIN
    DELETE FROM Products WHERE ProdID = @ProdID;
END
GO

-- Procedure to Get All Products
CREATE OR ALTER PROCEDURE sp_GetAllProducts
AS
BEGIN
    SELECT * FROM Products ORDER BY ProdID;
END
GO

-- Procedure to Search Product by ID
CREATE OR ALTER PROCEDURE sp_SearchProductByID
    @ProdID INT
AS
BEGIN
    SELECT * FROM Products WHERE ProdID = @ProdID;
END
GO

-- Procedure to Get Top 3 Budget Products
CREATE OR ALTER PROCEDURE sp_GetTop3BudgetProducts
AS
BEGIN
    SELECT TOP 3 * FROM Products ORDER BY Price ASC;
END
GO

-- Create Indexes for better performance
CREATE NONCLUSTERED INDEX IX_Products_Price ON Products(Price);
CREATE NONCLUSTERED INDEX IX_Products_CategoryID ON Products(CategoryID);
GO

-- Display all products
SELECT * FROM Products;
GO

PRINT 'Database setup completed successfully!';
PRINT 'Total Products: ' + CAST((SELECT COUNT(*) FROM Products) AS VARCHAR);
GO
