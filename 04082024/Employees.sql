CREATE TABLE Employees (
    employee_id INT PRIMARY KEY,
    employee_name VARCHAR(50),
    department VARCHAR(50),
    salary DECIMAL(10,2)
);

GO

ALTER TABLE Employees
    ADD hire_date DATE NOT NULL;

GO

ALTER TABLE Employees
    ADD CONSTRAINT chk_salary_non_negative CHECK (salary >= 0);

GO

ALTER TABLE Employees
    ADD CONSTRAINT df_department_default DEFAULT 'Unknown' FOR department;

GO

ALTER TABLE Employees
    ADD CONSTRAINT uq_employee_name UNIQUE (employee_name);

GO

DROP TABLE Employees;
GO

CREATE TABLE Employees (
    employee_id INT IDENTITY(1000,1) PRIMARY KEY,
    employee_name VARCHAR(50) UNIQUE,
    department VARCHAR(50) DEFAULT 'Unknown',
    salary DECIMAL(10,2) CHECK (salary >= 0),
    hire_date DATE NOT NULL
);