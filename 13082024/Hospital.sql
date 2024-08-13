CREATE DATABASE [Hospital]
GO

use [Hospital]
GO


CREATE TABLE [Doctors]
(
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL,
    [Phone] NVARCHAR(50) NOT NULL,
    [Premium] MONEY NOT NULL,
    [Salary] MONEY NOT NULL,
    [Surname] NVARCHAR(50) NOT NULL
)

CREATE TABLE [Examinations]
(
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL,
)

CREATE TABLE [Departments]
(
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Building] NVARCHAR(50) NOT NULL,
    [Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE [Sponsors]
(
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE [Donations]
(
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Amount] MONEY NOT NULL,
    [Date] DATE NOT NULL,
    [DepartmentId] INT NOT NULL REFERENCES [Departments] ([Id]),
    [SponsorId] INT NOT NULL REFERENCES [Sponsors] ([Id])
)

CREATE TABLE [Wards]
(
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL,
    [Places] INT NOT NULL,
    [DepartmentId] INT NOT NULL REFERENCES [Departments] ([Id])
)

CREATE TABLE [DoctorsExaminations]
(
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [EndTime] TIME NOT NULL,
    [StartTime] TIME NOT NULL,
    [DoctorId] INT NOT NULL REFERENCES [Doctors] ([Id]),
    [ExaminationId] INT NOT NULL REFERENCES [Examinations] ([Id]),
    [WardId] INT NOT NULL REFERENCES [Wards] ([Id])
)


INSERT INTO [Doctors] ([Name], [Phone], [Premium], [Salary], [Surname])
VALUES
    ('Иван', '123-456-7890', 2000.00, 5000.00, 'Иванов'),
    ('Елена', '987-654-3210', 1500.00, 4500.00, 'Петрова'),
    ('Алексей', '555-111-2222', 1800.00, 4800.00, 'Сидоров'),
    ('Мария', '789-321-6540', 2200.00, 5500.00, 'Кузнецова'),
    ('Павел', '111-222-3334', 1700.00, 4600.00, 'Смирнов'),
    ('Наталья', '444-555-6667', 1900.00, 4900.00, 'Козлова'),
    ('Дмитрий', '777-888-9990', 2100.00, 5200.00, 'Федоров'),
    ('Ольга', '222-333-4445', 1600.00, 4300.00, 'Лебедева'),
    ('Сергей', '555-444-3332', 2000.00, 5100.00, 'Андреев'),
    ('Анна', '999-888-7776', 2300.00, 5600.00, 'Павлова');

INSERT INTO [Examinations] ([Name])
VALUES
    ('Общий осмотр'),
    ('Анализ крови'),
    ('УЗИ'),
    ('ЭКГ'),
    ('Магнитно-резонансная томография');

INSERT INTO [Departments] ([Building], [Name])
VALUES
    ('Главное здание', 'Терапевтическое отделение'),
    ('Палата №2', 'Хирургическое отделение'),
    ('Крыло Север', 'Отделение кардиологии'),
    ('Крыло Юг', 'Отделение неврологии'),
    ('Отделение рентгенологии', 'Рентгенология'),
    ('Детское отделение', 'Педиатрия'),
    ('Ортопедическое отделение', 'Ортопедия');

INSERT INTO [Sponsors] ([Name])
VALUES
    ('Компания "Здоровье"'),
    ('Благотворительный фонд "Надежда"'),
    ('Медицинская корпорация "Луч"'),
    ('Частный инвестор "ЗаЗдоровье"'),
    ('Благотворительная организация "Добро"'),
    ('Государственный фонд медицины'),
    ('Компания "МедТех"');

INSERT INTO [Donations] ([Amount], [Date], [DepartmentId], [SponsorId])
VALUES
    (5000.00, '2023-01-15', 1, 1),
    (3000.00, '2023-02-20', 2, 2),
    (8000.00, '2023-03-10', 3, 3),
    (2000.00, '2023-04-05', 1, 4),
    (6000.00, '2023-05-18', 2, 5),
    (4500.00, '2023-06-22', 3, 6),
    (7000.00, '2023-07-12', 1, 7),
    (3500.00, '2023-08-30', 2, 4),
    (9000.00, '2023-09-14', 3, 6),
    (2500.00, '2023-10-02', 1, 1),
    (5500.00, '2023-11-25', 2, 2),
    (7500.00, '2023-12-08', 3, 5);

INSERT INTO [Wards] ([Name], [Places], [DepartmentId])
VALUES
    ('Палата №101', 15, 1),
    ('Палата №201', 20, 2),
    ('Палата №301', 10, 3),
    ('Палата №102', 18, 1),
    ('Палата №202', 25, 4),
    ('Палата №302', 12, 3),
    ('Палата №103', 14, 5),
    ('Палата №203', 22, 2);


INSERT INTO [DoctorsExaminations] ([EndTime], [StartTime], [DoctorId], [ExaminationId], [WardId])
VALUES
    ('10:30', '09:00', 1, 1, 3),
    ('13:45', '12:30', 2, 2, 2),
    ('15:00', '14:15', 3, 3, 3),
    ('11:20', '10:00', 4, 4, 5),
    ('16:30', '15:45', 5, 5, 2),
    ('14:10', '13:30', 6, 1, 6),
    ('17:45', '16:00', 7, 2, 1),
    ('12:15', '11:30', 8, 3, 2) ;

-- 1) Вывести названия отделений, что находятся в том же корпусе, что и отделение “Cardiology”. 
SELECT Name
FROM Departments
WHERE Building = (SELECT Building FROM Departments WHERE Name = 'Отделение кардиологии');

-- 2) Вывести названия отделений, что находятся в том же корпусе, что и отделения “Gastroenterology” и “General Surgery”. 
SELECT DISTINCT Name
FROM Departments
WHERE Building IN (
    SELECT Building FROM Departments WHERE Name = 'Отделение гастроэнтерологии'
    UNION
    SELECT Building FROM Departments WHERE Name = 'Общая хирургия'
);

-- 3) Вывести название отделения, которое получило меньше всего пожертвований. 
SELECT Name
FROM Departments
WHERE Id = (
    SELECT TOP 1 DepartmentId
    FROM Donations
    GROUP BY DepartmentId
    ORDER BY SUM(Amount) ASC
);

-- 4) Вывести фамилии врачей, ставка которых больше, чем у врача “Thomas Gerada”. 
SELECT Surname
FROM Doctors
WHERE Salary > (
    SELECT Salary
    FROM Doctors
    WHERE Surname = 'Иванов' AND Name = 'Иван'
    -- WHERE Surname = 'Gerada' AND Name = 'Thomas'
);

-- 5) Вывести названия палат, вместимость которых больше, чем средняя вместимость в палатах отделения “Microbiology”. 
SELECT Name
FROM Wards
WHERE Places > (
    SELECT AVG(Places)
    FROM Wards
    WHERE DepartmentId = (
        SELECT Id FROM Departments WHERE Name = 'Отделение микробиологии'
    )
);

-- 6) Вывести полные имена врачей, зарплаты которых (сумма ставки и надбавки) превышают более чем на 100 зарплату врача “Anthony Davis”. 
SELECT Name + ' ' + Surname AS FullName
FROM Doctors
WHERE (Salary + Premium) > (
    SELECT Salary + Premium + 100
    FROM Doctors
    WHERE Surname = 'Иванов' AND Name = 'Иван'
    -- WHERE Name = 'Anthony' AND Surname = 'Davis'
);

-- 7) Вывести названия отделений, в которых проводит обследования врач “Joshua Bell”. 
SELECT DISTINCT d.Name
FROM Departments d
         JOIN Wards w ON d.Id = w.DepartmentId
         JOIN DoctorsExaminations de ON w.Id = de.WardId
         JOIN Doctors doc ON de.DoctorId = doc.Id
WHERE doc.Surname = 'Иванов' AND doc.Name = 'Иван';
-- WHERE doc.Name = 'Joshua' AND doc.Surname = 'Bell';

-- 8) Вывести названия спонсоров, которые не делали пожертвования отделениям “Neurology” и “Oncology”. 
SELECT Name
FROM Sponsors
WHERE Id NOT IN (
    SELECT SponsorId
    FROM Donations
    WHERE DepartmentId IN (
        SELECT Id FROM Departments WHERE Name IN ('Отделение неврологии', 'Онкология')
    )
);

-- 9) Вывести фамилии врачей, которые проводят обследования в период с 12:00 до 15:00.
SELECT DISTINCT doc.Surname
FROM Doctors doc
         JOIN DoctorsExaminations de ON doc.Id = de.DoctorId
WHERE de.StartTime >= '12:00' AND de.EndTime <= '15:00';
