CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NULL,
    Salary DECIMAL(10, 2) NULL,
    DepartmentID INT NOT NULL
);

INSERT INTO Employees (FirstName, LastName, Salary, DepartmentID) VALUES
    ('John', 'Doe', 50000, 1),
    ('Jane', 'Smith', 70000, 1),
    ('Bill', 'Brown', 60000, 2),
    ('Anna', 'Johnson', 80000, 3),
    ('Tom', 'White', 40000, 1),
    ('Lucy', 'Davis', NULL, 2),
    ('Mike', 'Lee', 65000, 3);


-- 1) Используя переменную, увеличить заработную плату всех сотрудников в отделе "IT" на 10%. Предположим, 
-- что ID отдела "IT" равен 1.
DECLARE @ITDepartmentID INT = 1;

UPDATE Employees
SET Salary = Salary * 1.1
WHERE DepartmentID = @ITDepartmentID;

-- 2) Использовать цикл для вывода имен всех сотрудников.
DECLARE @counter INT = 1;
DECLARE @totalEmployees INT;
DECLARE @FirstName NVARCHAR(50);

SELECT @totalEmployees = COUNT(*) FROM Employees;

WHILE @counter <= @totalEmployees
    BEGIN
        SELECT @FirstName = FirstName
        FROM Employees
        WHERE EmployeeID = @counter;

        PRINT @FirstName;

        SET @counter = @counter + 1;
    END;


-- 3) Вывести список сотрудников с указанием статуса в зависимости от их заработной платы.
SELECT
    FirstName,
    LastName,
    Salary,
    CASE
        WHEN Salary > 60000 THEN 'High Salary'
        WHEN Salary BETWEEN 30000 AND 60000 THEN 'Medium Salary'
        ELSE 'Low Salary'
        END AS SalaryStatus
FROM Employees;


-- 4) Вывести информацию о сотрудниках, заработная плата которых превышает значение переменной.
DECLARE @MinSalary DECIMAL(10, 2) = 50000;

SELECT
    FirstName,
    LastName,
    Salary
FROM Employees
WHERE Salary > @MinSalary;


-- 5) Рассчитать бонус для сотрудников в зависимости от их текущей зарплаты. Если зарплата более $60000, 
-- бонус составит 5%, иначе 3%. Полученный бонус добавить каждому сотруднику в таблице, через оператор Update.
UPDATE Employees
SET Salary = Salary + CASE
                          WHEN Salary > 60000 THEN Salary * 0.05
                          ELSE Salary * 0.03
    END;


-- 6)  Используйте цикл для вывода суммарной зарплаты для каждого отдела, в следующем виде:
-- Department 1: Total salary - 432322.00
-- Department 2: Total salary - 404340.00
-- Department 3: Total salary - 315370.00
DECLARE @DepartmentID INT = 1;
DECLARE @TotalSalary DECIMAL(18, 2);
DECLARE @MaxDeptID INT;

SELECT @MaxDeptID = MAX(DepartmentID) FROM Employees;

WHILE @DepartmentID <= @MaxDeptID
    BEGIN
        SELECT @TotalSalary = SUM(Salary)
        FROM Employees
        WHERE DepartmentID = @DepartmentID;

        PRINT 'Department ' + CAST(@DepartmentID AS NVARCHAR) + ': Total salary - ' + CAST(@TotalSalary AS NVARCHAR);

        SET @DepartmentID = @DepartmentID + 1;
    END;

-- 7) Вывести информацию о сотрудниках с зарплатой выше средней, при условии, что средняя зарплата в отделе выше $55000.
WITH DeptAverage AS (
    SELECT
        DepartmentID,
        AVG(Salary) AS AvgSalary
    FROM Employees
    GROUP BY DepartmentID
)
SELECT
    e.FirstName,
    e.LastName,
    e.Salary,
    e.DepartmentID
FROM Employees e
         JOIN DeptAverage da
              ON e.DepartmentID = da.DepartmentID
WHERE e.Salary > da.AvgSalary
  AND da.AvgSalary > 55000;


-- 8) Удалить всех сотрудников в отделе "HR" с зарплатой ниже переменной @MinSalary.
DECLARE @MinSal DECIMAL(10, 2) = 45000;
DECLARE @HRDepartmentID INT = 2;

DELETE FROM Employees
WHERE DepartmentID = @HRDepartmentID
  AND Salary < @MinSal;

-- 9) Рассчитать общую зарплату для каждого отдела.
SELECT
    DepartmentID,
    SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentID;


-- 10) Замените все NULL значения в столбце Salary средним значением зарплаты по компании.
DECLARE @AvgSalary DECIMAL(10, 2);

SELECT @AvgSalary = AVG(Salary) FROM Employees WHERE Salary IS NOT NULL;

UPDATE Employees
SET Salary = @AvgSalary
WHERE Salary IS NULL;
