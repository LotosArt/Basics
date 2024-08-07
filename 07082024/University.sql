CREATE DATABASE University;
GO

USE University;
GO

CREATE TABLE Faculties (
                           id INT PRIMARY KEY,
                           dean VARCHAR(50),
                           name VARCHAR(50)
);

CREATE TABLE Departments (
                             id INT PRIMARY KEY,
                             financing DECIMAL(10, 2),
                             name VARCHAR(50)
);

CREATE TABLE Groups (
                        id INT PRIMARY KEY,
                        name VARCHAR(50),
                        rating DECIMAL(3, 2),
                        year INT
);

CREATE TABLE Teachers (
                          id INT PRIMARY KEY,
                          employmentDate DATE,
                          isAssistant BIT,
                          isProfessor BIT,
                          name VARCHAR(50),
                          position VARCHAR(50),
                          premium DECIMAL(10, 2),
                          salary DECIMAL(10, 2),
                          surname VARCHAR(50)
);
GO

INSERT INTO Faculties (id, dean, name) VALUES
                                           (1, 'Dr. Smith', 'Engineering'),
                                           (2, 'Dr. Johnson', 'Arts'),
                                           (3, 'Dr. Williams', 'Computer Science');

INSERT INTO Departments (id, financing, name) VALUES
                                                  (1, 10000, 'Mathematics'),
                                                  (2, 30000, 'Physics'),
                                                  (3, 25000, 'Chemistry'),
                                                  (4, 8000, 'Software Development');

INSERT INTO Groups (id, name, rating, year) VALUES
                                                (1, 'Group A', 3.5, 3),
                                                (2, 'Group B', 4.2, 5),
                                                (3, 'Group C', 2.8, 5),
                                                (4, 'Group D', 3.9, 4);

INSERT INTO Teachers (id, employmentDate, isAssistant, isProfessor, name, position, premium, salary, surname) VALUES
                                                                                                                  (1, '1995-08-15', 0, 1, 'Alice', 'Professor', 500, 1500, 'Johnson'),
                                                                                                                  (2, '2001-09-12', 1, 0, 'Bob', 'Assistant', 300, 1000, 'Smith'),
                                                                                                                  (3, '1998-01-23', 0, 1, 'Charlie', 'Professor', 700, 1200, 'Brown'),
                                                                                                                  (4, '1999-11-30', 1, 0, 'David', 'Assistant', 200, 900, 'White'),
                                                                                                                  (5, '2010-06-05', 0, 0, 'Eva', 'Lecturer', 150, 800, 'Black');
GO

SELECT name, financing, id FROM Departments;
GO

SELECT name AS "Group Name", rating AS "Group Rating" FROM Groups;
GO

SELECT surname,
       (premium / salary) * 100 AS "Premium Percentage",
       (salary / (salary + premium)) * 100 AS "Salary Percentage"
FROM Teachers;
GO

SELECT CONCAT('The dean of faculty ', name, ' is ', dean, '.') AS FacultyInfo FROM Faculties;
GO

SELECT surname FROM Teachers WHERE isProfessor = 1 AND salary > 1050;
GO

SELECT name FROM Departments WHERE financing < 11000 OR financing > 25000;
GO

SELECT name FROM Faculties WHERE name != 'Computer Science';
GO

SELECT surname, position FROM Teachers WHERE isProfessor = 0;
GO

SELECT surname, position, salary, premium
FROM Teachers
WHERE isAssistant = 1 AND premium BETWEEN 160 AND 550;
GO

SELECT surname, salary FROM Teachers WHERE isAssistant = 1;
GO

SELECT surname, position FROM Teachers WHERE employmentDate < '2000-01-01';
GO

SELECT name AS "Name of Department"
FROM Departments
WHERE name < 'Software Development'
ORDER BY name ASC;
GO

SELECT surname
FROM Teachers
WHERE isAssistant = 1 AND (salary + premium) <= 1200;
GO

SELECT name
FROM Groups
WHERE year = 5 AND rating BETWEEN 2 AND 4;
GO

SELECT surname
FROM Teachers
WHERE isAssistant = 1 AND (salary < 550 OR premium < 200);

