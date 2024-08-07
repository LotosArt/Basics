CREATE DATABASE Hospital;
GO

USE Hospital;
GO

CREATE TABLE Patients (
                          PatientID INT PRIMARY KEY,
                          FirstName VARCHAR(50),
                          LastName VARCHAR(50),
                          Age INT,
                          Department VARCHAR(50),
                          AdmissionDate DATE,
                          DischargeDate DATE,
                          Disease VARCHAR(100),
                          Doctor VARCHAR(50),
                          MobileOperator VARCHAR(50)
);
GO

INSERT INTO Patients (PatientID, FirstName, LastName, Age, Department, AdmissionDate, DischargeDate, Disease, Doctor, MobileOperator) VALUES
                                                                                                                                          (1, 'John', 'Doe', 30, 'Cardiology', '2024-05-01', NULL, 'Heart Disease', 'Dr. Smith', 'AT&T'),
                                                                                                                                          (2, 'Jane', 'Smith', 40, 'Neurology', '2024-04-15', '2024-06-01', 'Migraine', 'Dr. Johnson', 'Verizon'),
                                                                                                                                          (3, 'Bob', 'Johnson', 25, 'Oncology', '2024-03-20', NULL, 'Cancer', 'Dr. Brown', 'T-Mobile'),
                                                                                                                                          (4, 'Alice', 'Williams', 35, 'Cardiology', '2024-02-10', '2024-03-20', 'Arrhythmia', 'Dr. Smith', 'AT&T'),
                                                                                                                                          (5, 'Charlie', 'Brown', 50, 'Orthopedics', '2024-01-15', NULL, 'Fracture', 'Dr. White', 'Verizon'),
                                                                                                                                          (6, 'Dave', 'Wilson', 60, 'Cardiology', '2024-01-10', NULL, 'Heart Attack', 'Dr. Smith', 'T-Mobile'),
                                                                                                                                          (7, 'Eva', 'Moore', 28, 'Neurology', '2024-03-01', NULL, 'Epilepsy', 'Dr. Johnson', 'AT&T'),
                                                                                                                                          (8, 'Frank', 'Taylor', 45, 'Oncology', '2023-11-20', '2024-01-20', 'Leukemia', 'Dr. Brown', 'Verizon'),
                                                                                                                                          (9, 'Grace', 'Anderson', 55, 'Cardiology', '2024-04-01', NULL, 'Hypertension', 'Dr. Smith', 'T-Mobile'),
                                                                                                                                          (10, 'Hank', 'Thomas', 33, 'Orthopedics', '2024-02-15', '2024-04-15', 'Arthritis', 'Dr. White', 'AT&T'),
                                                                                                                                          (11, 'John', 'Smith', 35, 'Neurology', '2024-02-15', '2024-07-15', 'Epilepsy', 'Dr. Johnson', 'AT&T');

GO

SELECT * FROM Patients WHERE DischargeDate IS NULL;

GO

SELECT * FROM Patients WHERE Department = 'Cardiology';
GO

SELECT DISTINCT Department FROM Patients;
GO

SELECT * FROM Patients
WHERE AdmissionDate <= DATEADD(MONTH, -1, GETDATE())
ORDER BY AdmissionDate ASC;
GO

SELECT * FROM Patients
WHERE DischargeDate BETWEEN DATEADD(MONTH, -1, DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)) AND DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0);
GO

SELECT * FROM Patients
WHERE AdmissionDate BETWEEN '2023-10-01' AND '2023-12-31'
  AND Department = 'Oncology';
GO

SELECT TOP 1 *, DATEDIFF(YEAR, GETDATE(), DATEADD(YEAR, Age, GETDATE())) AS YoungestAge
FROM Patients
ORDER BY Age ASC;
GO

SELECT * FROM Patients
WHERE Department IN (SELECT Dep.Department FROM (SELECT * FROM Patients ORDER BY NEWID() OFFSET 0 ROWS FETCH NEXT 3 ROWS ONLY) Dep);
GO

SELECT * FROM Patients WHERE LastName LIKE 'P%';
GO

SELECT * FROM Patients WHERE Doctor = 'Dr. Smith' AND Disease = 'Heart Disease';
GO

SELECT * FROM Patients WHERE MobileOperator = 'AT&T';
GO

UPDATE Patients
SET Department = 'NewDepartmentName'
WHERE Department = 'Cardiology';
GO

DELETE FROM Patients
WHERE DischargeDate < DATEADD(MONTH, -6, GETDATE());