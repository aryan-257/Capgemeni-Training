-- Flight Search Engine - Database Setup Script

USE master;
GO

-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'FlightSearchEngineDB')
BEGIN
    CREATE DATABASE FlightSearchEngineDB;
END
GO

USE FlightSearchEngineDB;
GO

-- Create Flights Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Flights')
BEGIN
    CREATE TABLE Flights (
        FlightId INT PRIMARY KEY IDENTITY(1,1),
        FlightName NVARCHAR(100) NOT NULL,
        FlightType NVARCHAR(50) NOT NULL,
        Source NVARCHAR(100) NOT NULL,
        Destination NVARCHAR(100) NOT NULL,
        PricePerSeat DECIMAL(18,2) NOT NULL
    );
END
GO

-- Create Hotels Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Hotels')
BEGIN
    CREATE TABLE Hotels (
        HotelId INT PRIMARY KEY IDENTITY(1,1),
        HotelName NVARCHAR(100) NOT NULL,
        HotelType NVARCHAR(50) NOT NULL,
        Location NVARCHAR(100) NOT NULL,
        PricePerDay DECIMAL(18,2) NOT NULL
    );
END
GO

-- Insert Sample Data
IF NOT EXISTS (SELECT * FROM Flights)
BEGIN
    INSERT INTO Flights (FlightName, FlightType, Source, Destination, PricePerSeat) VALUES
    ('Air India AI-101', 'Domestic', 'Mumbai', 'Delhi', 5500.00),
    ('Air India AI-102', 'Domestic', 'Delhi', 'Mumbai', 5200.00),
    ('IndiGo 6E-201', 'Domestic', 'Mumbai', 'Bangalore', 4800.00),
    ('IndiGo 6E-202', 'Domestic', 'Bangalore', 'Mumbai', 4700.00),
    ('SpiceJet SG-301', 'Domestic', 'Delhi', 'Bangalore', 4500.00),
    ('SpiceJet SG-302', 'Domestic', 'Bangalore', 'Delhi', 4600.00),
    ('Vistara UK-401', 'Domestic', 'Mumbai', 'Kolkata', 6200.00),
    ('Vistara UK-402', 'Domestic', 'Kolkata', 'Mumbai', 6100.00),
    ('IndiGo 6E-203', 'Domestic', 'Delhi', 'Kolkata', 5800.00),
    ('IndiGo 6E-204', 'Domestic', 'Kolkata', 'Delhi', 5700.00),
    ('Air India AI-501', 'International', 'Mumbai', 'Dubai', 18500.00),
    ('Air India AI-502', 'International', 'Dubai', 'Mumbai', 18000.00),
    ('Emirates EK-601', 'International', 'Delhi', 'Dubai', 19500.00),
    ('Emirates EK-602', 'International', 'Dubai', 'Delhi', 19000.00),
    ('Singapore Airlines SQ-701', 'International', 'Mumbai', 'Singapore', 25000.00),
    ('Singapore Airlines SQ-702', 'International', 'Singapore', 'Mumbai', 24500.00);
END
GO

IF NOT EXISTS (SELECT * FROM Hotels)
BEGIN
    INSERT INTO Hotels (HotelName, HotelType, Location, PricePerDay) VALUES
    ('Taj Palace Hotel', '5-Star', 'Delhi', 8500.00),
    ('The Oberoi', '5-Star', 'Mumbai', 9200.00),
    ('ITC Grand', '5-Star', 'Bangalore', 7800.00),
    ('The Park Hotel', '4-Star', 'Kolkata', 6500.00),
    ('Burj Al Arab', '7-Star', 'Dubai', 35000.00),
    ('Marina Bay Sands', '5-Star', 'Singapore', 28000.00);
END
GO

-- Stored Procedures

-- Get all source cities
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetSources')
    DROP PROCEDURE sp_GetSources;
GO

CREATE PROCEDURE sp_GetSources
AS
BEGIN
    SELECT DISTINCT Source FROM Flights ORDER BY Source;
END
GO

-- Get all destination cities
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetDestinations')
    DROP PROCEDURE sp_GetDestinations;
GO

CREATE PROCEDURE sp_GetDestinations
AS
BEGIN
    SELECT DISTINCT Destination FROM Flights ORDER BY Destination;
END
GO

-- Search for flights
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_SearchFlights')
    DROP PROCEDURE sp_SearchFlights;
GO

CREATE PROCEDURE sp_SearchFlights
    @Source NVARCHAR(100),
    @Destination NVARCHAR(100),
    @Persons INT
AS
BEGIN
    SELECT 
        FlightId,
        FlightName,
        FlightType,
        Source,
        Destination,
        (PricePerSeat * @Persons) AS TotalCost
    FROM Flights
    WHERE Source = @Source AND Destination = @Destination
    ORDER BY TotalCost;
END
GO

-- Search for flight + hotel packages
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_SearchFlightsWithHotels')
    DROP PROCEDURE sp_SearchFlightsWithHotels;
GO

CREATE PROCEDURE sp_SearchFlightsWithHotels
    @Source NVARCHAR(100),
    @Destination NVARCHAR(100),
    @Persons INT
AS
BEGIN
    SELECT 
        F.FlightId,
        F.FlightName,
        F.Source,
        F.Destination,
        H.HotelName,
        ((F.PricePerSeat * @Persons) + H.PricePerDay) AS TotalCost
    FROM Flights F
    INNER JOIN Hotels H ON F.Destination = H.Location
    WHERE F.Source = @Source AND F.Destination = @Destination
    ORDER BY TotalCost;
END
GO

-- Verification
PRINT 'Database Setup Complete!';
PRINT 'Tables: Flights (16 records), Hotels (6 records)';
PRINT 'Stored Procedures: 4 created';
GO
