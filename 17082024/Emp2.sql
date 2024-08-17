-- use master;
-- GO
-- drop database FirstDB;
-- GO
-- create database FirstDB;
-- GO
-- use FirstDB;


-- CREATE TABLE Employees (
--     EmployeeID INT PRIMARY KEY IDENTITY(1,1),
--     FirstName NVARCHAR(50) NOT NULL,
--     LastName NVARCHAR(50) NULL,
--     Salary DECIMAL(10, 2) NULL,
--     DepartmentID INT NOT NULL
-- );

-- INSERT INTO Employees (FirstName, LastName, Salary, DepartmentID) VALUES
-- ('John', 'Doe', 50000, 1),
-- ('Jane', 'Smith', 70000, 1),
-- ('Bill', 'Brown', 60000, 2),
-- ('Anna', 'Johnson', 80000, 3),
-- ('Tom', 'White', 40000, 1),
-- ('Lucy', 'Davis', NULL, 2),
-- ('Mike', 'Lee', 65000, 3);


-- -- 1) Используя переменную, увеличить заработную плату всех сотрудников в отделе "IT" на 10%. Предположим, что ID отдела "IT" равен 1.
-- DECLARE @ITDepartmentID INT = 1;

-- UPDATE Employees
-- SET Salary = Salary * 1.1
-- WHERE DepartmentID = @ITDepartmentID;

-- -- 2) Использовать цикл для вывода имен всех сотрудников.
-- DECLARE @counter INT = 1;
-- DECLARE @totalEmployees INT;
-- DECLARE @FirstName NVARCHAR(50);

-- SELECT @totalEmployees = COUNT(*) FROM Employees;

-- WHILE @counter <= @totalEmployees
-- BEGIN
--     SELECT @FirstName = FirstName 
--     FROM Employees
--     WHERE EmployeeID = @counter;

--     PRINT @FirstName;

--     SET @counter = @counter + 1;
-- END;


-- -- 3) Вывести список сотрудников с указанием статуса в зависимости от их заработной платы.
-- SELECT 
--     FirstName,
--     LastName,
--     Salary,
--     CASE
--         WHEN Salary > 60000 THEN 'High Salary'
--         WHEN Salary BETWEEN 30000 AND 60000 THEN 'Medium Salary'
--         ELSE 'Low Salary'
--     END AS SalaryStatus
-- FROM Employees;


-- -- 4) Вывести информацию о сотрудниках, заработная плата которых превышает значение переменной.
-- DECLARE @MinSalary DECIMAL(10, 2) = 50000;

-- SELECT 
--     FirstName,
--     LastName,
--     Salary
-- FROM Employees
-- WHERE Salary > @MinSalary;


-- -- 5) Рассчитать бонус для сотрудников в зависимости от их текущей зарплаты. Если зарплата более $60000, бонус составит 5%, иначе 3%. Полученный бонус добавить каждому сотруднику в таблице, через оператор Update.
-- UPDATE Employees
-- SET Salary = Salary + CASE
--     WHEN Salary > 60000 THEN Salary * 0.05
--     ELSE Salary * 0.03
-- END;


-- -- 6)  Используйте цикл для вывода суммарной зарплаты для каждого отдела, в следующем виде:
-- -- Department 1: Total salary - 432322.00
-- -- Department 2: Total salary - 404340.00
-- -- Department 3: Total salary - 315370.00
-- DECLARE @DepartmentID INT = 1;
-- DECLARE @TotalSalary DECIMAL(18, 2);
-- DECLARE @MaxDeptID INT;

-- SELECT @MaxDeptID = MAX(DepartmentID) FROM Employees;

-- WHILE @DepartmentID <= @MaxDeptID
-- BEGIN
--     SELECT @TotalSalary = SUM(Salary)
--     FROM Employees
--     WHERE DepartmentID = @DepartmentID;

--     PRINT 'Department ' + CAST(@DepartmentID AS NVARCHAR) + ': Total salary - ' + CAST(@TotalSalary AS NVARCHAR);

--     SET @DepartmentID = @DepartmentID + 1;
-- END;

-- -- 7) Вывести информацию о сотрудниках с зарплатой выше средней, при условии, что средняя зарплата в отделе выше $55000.
-- WITH DeptAverage AS (
--     SELECT 
--         DepartmentID,
--         AVG(Salary) AS AvgSalary
--     FROM Employees
--     GROUP BY DepartmentID
-- )
-- SELECT 
--     e.FirstName,
--     e.LastName,
--     e.Salary,
--     e.DepartmentID
-- FROM Employees e
-- JOIN DeptAverage da
-- ON e.DepartmentID = da.DepartmentID
-- WHERE e.Salary > da.AvgSalary
--   AND da.AvgSalary > 55000;


-- -- 8) Удалить всех сотрудников в отделе "HR" с зарплатой ниже переменной @MinSalary.
-- DECLARE @MinSal DECIMAL(10, 2) = 45000;
-- DECLARE @HRDepartmentID INT = 2;

-- DELETE FROM Employees
-- WHERE DepartmentID = @HRDepartmentID
--   AND Salary < @MinSal;

-- -- 9) Рассчитать общую зарплату для каждого отдела.
-- SELECT 
--     DepartmentID,
--     SUM(Salary) AS TotalSalary
-- FROM Employees
-- GROUP BY DepartmentID;


-- -- 10) Замените все NULL значения в столбце Salary средним значением зарплаты по компании.
-- DECLARE @AvgSalary DECIMAL(10, 2);

-- SELECT @AvgSalary = AVG(Salary) FROM Employees WHERE Salary IS NOT NULL;

-- UPDATE Employees
-- SET Salary = @AvgSalary
-- WHERE Salary IS NULL;

CREATE TABLE [Employees](
                            [ID] INT NOT NULL,
                            [Name] NVARCHAR(30),
                            [Birthday] DATE,
                            [Email] NVARCHAR(30),
                            [Position] NVARCHAR(30),
                            [Department] NVARCHAR(30)
);

ALTER TABLE [Employees]
    ADD PRIMARY KEY (ID);

ALTER TABLE [Employees]
    ALTER COLUMN [Name] NVARCHAR(30) NOT NULL;

ALTER TABLE [Employees]
    ADD [Salary] DECIMAL(10, 2);

INSERT INTO [Employees] (ID, Name, Birthday, Email, Position, Department, Salary)
VALUES
    (1, 'John Doe', '1975-06-20', 'john.doe@example.com', 'Manager', 'IT', 5000),
    (2, 'Jane Smith', '1980-03-15', 'jane.smith@example.com', 'Accountant', 'Finance', 4500),
    (3, 'Mike Brown', '1968-12-05', NULL, 'Developer', 'IT', 6000),
    (4, 'Emily Davis', '1972-08-10', 'emily.davis@example.com', 'HR', 'HR', 3000),
    (5, 'Tom White', '1990-11-01', NULL, 'Marketer', 'Marketing', 4000);


SELECT
    ID,
    Name,
    Salary,
    CASE
        WHEN Salary >= 6000 THEN 'High Salary'
        WHEN Salary BETWEEN 4000 AND 5999 THEN 'Medium Salary'
        ELSE 'Low Salary'
        END AS SalaryRange
FROM Employees;


SELECT
    Name,
    Salary,
    CASE
        WHEN Department = 'IT' THEN Salary * 0.15
        WHEN Department = 'Finance' THEN Salary * 0.10
        ELSE Salary * 0.05
        END AS Bonus
FROM Employees;


SELECT ID, Name, Salary
FROM Employees
WHERE Salary <= 2500

UNION ALL

SELECT ID, Name, Salary
FROM Employees
WHERE Salary > 2500;


UPDATE Employees
SET Email = Name + '@gmail.com'
WHERE Department = 'Marketing' AND Email IS NULL;

SELECT ID, Name, Email
FROM Employees
WHERE Department = 'Marketing';


ALTER TABLE Employees
    ADD Bonus MONEY NULL;

UPDATE Employees
SET Bonus = 1500
WHERE Department IN ('Marketing', 'Finance');

SELECT *,
       ISNULL(Salary + Bonus, Salary) AS Total
FROM Employees;


SELECT *
INTO New_Employees
FROM Employees
WHERE ID % 2 = 0 AND Name LIKE '%aa%' OR Name LIKE '%ee%';


SELECT *,
       ISNULL(Salary + Bonus, Salary) AS TotalSalary
FROM Employees
ORDER BY ISNULL(Salary + Bonus, Salary);


SELECT *
FROM Employees
ORDER BY 1, 2, 3;


DECLARE @MinSalary DECIMAL(10,2), @MaxBonusSalary DECIMAL(10,2);

SELECT @MinSalary = MIN(Salary), @MaxBonusSalary = MAX(Bonus)
FROM Employees;

UPDATE Employees
SET Salary = Salary + (@MinSalary * 0.10)
WHERE Bonus = @MaxBonusSalary;


DECLARE @Money MONEY = 5000.75;

SELECT CAST(@Money AS VARCHAR(30)) AS MoneyValue;



SELECT
    COUNT(*) AS TotalEmployees,
    COUNT(DISTINCT Position) AS UniquePositions,
    COUNT(Bonus) AS EmployeesWithBonus,
    MAX(Bonus) AS MaxBonus,
    MIN(Bonus) AS MinBonus,
    SUM(Bonus) AS TotalBonuses,
    AVG(Bonus) AS AvgBonus,
    AVG(Salary) AS AvgSalary
FROM Employees;


SELECT
    SUM(CASE WHEN Birthday BETWEEN '1970-01-01' AND '1979-12-31' THEN 1 ELSE 0 END) AS BornIn70s,
    SUM(CASE WHEN Birthday BETWEEN '1980-01-01' AND '1989-12-31' THEN 1 ELSE 0 END) AS BornIn80s,
    SUM(CASE WHEN Birthday < '1970-01-01' THEN 1 ELSE 0 END) AS BornBefore70,
    SUM(CASE WHEN Birthday IS NULL THEN 1 ELSE 0 END) AS NoBirthdayInfo
FROM Employees;
