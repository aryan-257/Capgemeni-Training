USE LPU_Db;
GO

CREATE TABLE Sales (
    ProductId INT,
    SaleMonth DATE,
    Amount INT
);

SELECT * FROM Sales;

INSERT INTO Sales (ProductId, SaleMonth, Amount)
VALUES
(1, '2024-01-01', 1000),
(1, '2024-02-01', 1500),
(1, '2024-03-01', 1200),
(2, '2024-01-01', 2000),
(2, '2024-02-01', 2500),
(2, '2024-03-01', 3000);

SELECT * FROM Sales;


SELECT
    ProductId,
    SaleMonth,
    Amount,
    SUM(Amount) OVER (
        PARTITION BY ProductId
        ORDER BY SaleMonth
        ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW
    ) AS CumulativeSales
FROM Sales
ORDER BY ProductId, SaleMonth;

