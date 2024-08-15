CREATE TABLE [Departments] (
    [Id] INT PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(50)
);

CREATE TABLE Employees (
    [Id] INT PRIMARY KEY IDENTITY,
    [FirstName] NVARCHAR(50),
    [LastName] NVARCHAR(50),
    [HireDate] DATE,
    [DepartmentID] INT,
    FOREIGN KEY ([DepartmentID]) REFERENCES [Departments]([Id])
);

CREATE TABLE [Orders] (
    [Id] INT PRIMARY KEY IDENTITY,
    [EmployeeID] INT,
    [OrderDate] DATE,
    [Amount] DECIMAL(10, 2),
    FOREIGN KEY (EmployeeID) REFERENCES Employees([Id])
);

-- 1) Выберите имена и фамилии сотрудников в верхнем регистре.
SELECT UPPER(FirstName) AS FirstNameUpper, UPPER(LastName) AS LastNameUpper
FROM Employees;

-- 2) Рассчитайте и выведите возраст сотрудников вместе с остальными столбцами.
SELECT *,
       DATEDIFF(YEAR, HireDate, GETDATE()) AS Age
FROM Employees;

-- 3) Выберите все заказы после определенной даты.
SELECT *
FROM Orders
WHERE OrderDate > '2023-01-01';  

-- 4) Выведите общую сумму заказов для каждого сотрудника.
SELECT EmployeeID, SUM(Amount) AS TotalOrders
FROM Orders
GROUP BY EmployeeID;

-- 5) Выведите количество сотрудников в каждом отделе.
SELECT d.Name AS DepartmentName, COUNT(e.Id) AS EmployeeCount
FROM Departments d
LEFT JOIN Employees e ON d.Id = e.DepartmentID
GROUP BY d.Name;

-- 6) Рассчитайте среднюю продолжительность работы сотрудников в днях.
SELECT AVG(DATEDIFF(DAY, HireDate, GETDATE())) AS AvgTenureDays
FROM Employees;

-- 7) Выведите длины имен всех сотрудников.
SELECT FirstName, LastName, LEN(FirstName) AS FirstNameLength, LEN(LastName) AS LastNameLength
FROM Employees;

-- 8) Выведите сумму заказа со знаком доллара, используя функцию FORMAT.
SELECT EmployeeID, FORMAT(Amount, 'C', 'en-US') AS FormattedAmount
FROM Orders;

-- 9) Выведите первые 3 символа имен сотрудников.
SELECT LEFT(FirstName, 3) AS FirstNameInitials
FROM Employees;

-- 10) Определите количество знаков после запятой в сумме заказа.
SELECT Amount, LEN(CAST(Amount AS NVARCHAR)) - CHARINDEX('.', CAST(Amount AS NVARCHAR)) AS DecimalPlaces
FROM Orders
WHERE Amount LIKE '%.%';

-- 11) Получите список сотрудников с самым длинным именем.
SELECT TOP 1 *, LEN(FirstName) AS NameLength
FROM Employees
ORDER BY LEN(FirstName) DESC;

-- 12) Выведите среднюю продолжительность работы сотрудников в каждом отделе.
SELECT d.Name AS DepartmentName, AVG(DATEDIFF(DAY, e.HireDate, GETDATE())) AS AvgTenureDays
FROM Departments d
JOIN Employees e ON d.Id = e.DepartmentID
GROUP BY d.Name;

-- 13) Определите сумму заказов для каждого сотрудника за последние 90 дней.
SELECT EmployeeID, SUM(Amount) AS TotalOrders
FROM Orders
WHERE OrderDate >= DATEADD(DAY, -90, GETDATE())
GROUP BY EmployeeID;

-- 14) Выведите Топ-3 сотрудников с наибольшей общей суммой заказов.
SELECT TOP 3 EmployeeID, SUM(Amount) AS TotalOrders
FROM Orders
GROUP BY EmployeeID
ORDER BY SUM(Amount) DESC;

-- 15) При выборке из таблицы сотрудников создайте столбец, который будет включать в себя и имя и общую сумму заказов.
SELECT
    CONCAT(e.FirstName, ' ', e.LastName) AS FullName,
    ISNULL(SUM(o.Amount), 0) AS TotalOrders
FROM Employees e
         LEFT JOIN Orders o ON e.Id = o.EmployeeID
GROUP BY e.FirstName, e.LastName;
