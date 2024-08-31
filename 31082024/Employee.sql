CREATE TABLE Employees
(
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50),
    Position NVARCHAR(50),
    Department NVARCHAR(50)
);

CREATE TABLE EmployeeDetails
(
    ID INT PRIMARY KEY IDENTITY,
    EmployeeID INT FOREIGN KEY REFERENCES Employees(ID),
    Email NVARCHAR(50),
    PhoneNumber NVARCHAR(15)
);

GO

INSERT INTO Employees (Name, Position, Department) VALUES
                                                       ('John Doe', 'Manager', 'Sales'),
                                                       ('Jane Smith', 'Developer', 'IT'),
                                                       ('Emily Johnson', 'Analyst', 'Finance');

INSERT INTO EmployeeDetails (EmployeeID, Email, PhoneNumber) VALUES
                                                                 (1, 'john.doe@example.com', '123-456-7890'),
                                                                 (2, 'jane.smith@example.com', '234-567-8901'),
                                                                 (3, 'emily.johnson@example.com', '345-678-9012');

GO
CREATE PROCEDURE AddEmployee
    @Name NVARCHAR(50),
    @Position NVARCHAR(50),
    @Department NVARCHAR(50),
    @Email NVARCHAR(50),
    @PhoneNumber NVARCHAR(15)
AS
BEGIN
    DECLARE @EmployeeID INT;

    INSERT INTO Employees (Name, Position, Department)
    VALUES (@Name, @Position, @Department);

    SET @EmployeeID = SCOPE_IDENTITY();

    INSERT INTO EmployeeDetails (EmployeeID, Email, PhoneNumber)
    VALUES (@EmployeeID, @Email, @PhoneNumber);
END;


GO
CREATE PROCEDURE GetEmployeeDetails
AS
BEGIN
    SELECT e.ID, e.Name, e.Position, e.Department, ed.Email, ed.PhoneNumber
    FROM Employees e
             JOIN EmployeeDetails ed ON e.ID = ed.EmployeeID;
END;


GO
CREATE PROCEDURE UpdateEmployee
    @EmployeeID INT,
    @Name NVARCHAR(50) = NULL,
    @Position NVARCHAR(50) = NULL,
    @Department NVARCHAR(50) = NULL,
    @Email NVARCHAR(50) = NULL,
    @PhoneNumber NVARCHAR(15) = NULL
AS
BEGIN
    UPDATE Employees
    SET
        Name = COALESCE(@Name, Name),
        Position = COALESCE(@Position, Position),
        Department = COALESCE(@Department, Department)
    WHERE ID = @EmployeeID;

    UPDATE EmployeeDetails
    SET
        Email = COALESCE(@Email, Email),
        PhoneNumber = COALESCE(@PhoneNumber, PhoneNumber)
    WHERE EmployeeID = @EmployeeID;
END;


GO
CREATE PROCEDURE DeleteEmployee
@EmployeeID INT
AS
BEGIN
    DELETE FROM EmployeeDetails WHERE EmployeeID = @EmployeeID;

    DELETE FROM Employees WHERE ID = @EmployeeID;
END;


GO
CREATE PROCEDURE GetEmployeeByParams
    @Name NVARCHAR(50) = NULL,
    @Position NVARCHAR(50) = NULL,
    @Department NVARCHAR(50) = NULL
AS
BEGIN
    SELECT e.ID, e.Name, e.Position, e.Department, ed.Email, ed.PhoneNumber
    FROM Employees e
             JOIN EmployeeDetails ed ON e.ID = ed.EmployeeID
    WHERE
        (e.Name = @Name OR @Name IS NULL) AND
        (e.Position = @Position OR @Position IS NULL) AND
        (e.Department = @Department OR @Department IS NULL);
END;


GO
CREATE PROCEDURE GetEmployeeContacts
    @EmployeeID INT,
    @Email NVARCHAR(50) OUTPUT,
    @PhoneNumber NVARCHAR(15) OUTPUT
AS
BEGIN
    SELECT @Email = ed.Email, @PhoneNumber = ed.PhoneNumber
    FROM EmployeeDetails ed
    WHERE ed.EmployeeID = @EmployeeID;
END;


GO
EXEC AddEmployee
     @Name = 'Alice Brown',
     @Position = 'Designer',
     @Department = 'Marketing',
     @Email = 'alice.brown@example.com',
     @PhoneNumber = '456-789-0123';

EXEC GetEmployeeDetails;

EXEC UpdateEmployee
     @EmployeeID = 1,
     @Name = 'Johnathan Doe',
     @Email = 'johnathan.doe@example.com';

EXEC DeleteEmployee @EmployeeID = 1;

EXEC GetEmployeeByParams @Department = 'IT';

DECLARE @Email NVARCHAR(50), @PhoneNumber NVARCHAR(15);
EXEC GetEmployeeContacts @EmployeeID = 2, @Email = @Email OUTPUT, @PhoneNumber = @PhoneNumber OUTPUT;
SELECT @Email AS Email, @PhoneNumber AS PhoneNumber;