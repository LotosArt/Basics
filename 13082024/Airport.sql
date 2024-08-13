CREATE DATABASE Airport;
GO
USE Airport;
GO
CREATE TABLE Passengers (
                            id INT IDENTITY PRIMARY KEY,
                            name NVARCHAR(100) NOT NULL,
                            passport_number NVARCHAR(20) UNIQUE NOT NULL
);

CREATE TABLE Flights (
                         id INT IDENTITY PRIMARY KEY,
                         flight_number NVARCHAR(10) NOT NULL,
                         departure_city NVARCHAR(100) NOT NULL,
                         arrival_city NVARCHAR(100) NOT NULL,
                         departure_time DATETIME NOT NULL,
                         arrival_time DATETIME NOT NULL,
                         duration AS (CONVERT(TIME, arrival_time - departure_time)),
                         seats_economy INT NOT NULL,
                         seats_business INT NOT NULL
);

CREATE TABLE Tickets (
                         id INT IDENTITY PRIMARY KEY,
                         flight_id INT NOT NULL REFERENCES Flights(id) ON DELETE CASCADE,
                         passenger_id INT NOT NULL REFERENCES Passengers(id) ON DELETE CASCADE,
                         seat_class NVARCHAR(10) CHECK (seat_class IN ('Economy', 'Business')),
                         price MONEY NOT NULL,
                         purchase_date DATETIME NOT NULL
);

INSERT INTO Passengers (name, passport_number)
VALUES
    ('Иван Иванов', '1234567890'),
    ('Мария Петрова', '0987654321'),
    ('Алексей Смирнов', '1122334455'),
    ('Ольга Сидорова', '5566778899'),
    ('Дмитрий Кузнецов', '2233445566');

INSERT INTO Flights (flight_number, departure_city, arrival_city, departure_time, arrival_time, seats_economy, seats_business)
VALUES
    ('SU100', 'Киев', 'Нью-Йорк', '2024-08-15 08:00:00', '2024-08-15 12:00:00', 150, 20),
    ('SU200', 'Москва', 'Лондон', '2024-08-15 09:00:00', '2024-08-15 11:00:00', 150, 20),
    ('SU300', 'Харьков', 'Париж', '2024-08-15 10:00:00', '2024-08-15 12:30:00', 150, 20),
    ('SU400', 'Львов', 'Токио', '2024-08-15 11:00:00', '2024-08-15 21:00:00', 150, 20),
    ('SU500', 'Днепр', 'Берлин', '2024-08-15 12:00:00', '2024-08-15 14:00:00', 150, 20);

INSERT INTO Tickets (flight_id, passenger_id, seat_class, price, purchase_date)
VALUES
    (1, 1, 'Economy', 500.00, '2024-08-10'),
    (1, 2, 'Business', 1000.00, '2024-08-10'),
    (2, 3, 'Economy', 400.00, '2024-08-11'),
    (3, 4, 'Business', 900.00, '2024-08-12'),
    (4, 5, 'Economy', 700.00, '2024-08-12');

-- 1) Вывести все рейсы в определенный город на произвольную дату, упорядочив их по времени вылета;
SELECT *
FROM Flights
WHERE arrival_city = 'Лондон' AND CONVERT(DATE, departure_time) = '2024-08-15'
ORDER BY departure_time;

-- 2) Вывести информацию о рейсе с наибольшей длительностью полета по времени;
SELECT TOP 1 *
FROM Flights
ORDER BY duration DESC;

-- 3) Показать все рейсы, длительность полета которых превышает два часа;
SELECT *
FROM Flights
WHERE duration > CAST('02:00:00' AS TIME);

-- 4) Получить количество рейсов в каждый город;
SELECT arrival_city, COUNT(*) AS number_of_flights
FROM Flights
GROUP BY arrival_city;

-- 5) Показать город, в который наиболее часто осуществляются полеты;
SELECT TOP 1 arrival_city, COUNT(*) AS number_of_flights
FROM Flights
GROUP BY arrival_city
ORDER BY COUNT(*) DESC;

-- 6) Получить информацию о количестве рейсов в каждый город и общее количество рейсов за определенный месяц;
SELECT arrival_city, COUNT(*) AS number_of_flights
FROM Flights
WHERE MONTH(departure_time) = 8 AND YEAR(departure_time) = 2024
GROUP BY arrival_city
UNION ALL
SELECT 'Total', COUNT(*)
FROM Flights
WHERE MONTH(departure_time) = 8 AND YEAR(departure_time) = 2024;

-- 7) Вывести список рейсов, вылетающих сегодня, на которые есть свободные места в бизнес классе;
SELECT *
FROM Flights
WHERE CONVERT(DATE, departure_time) = CONVERT(DATE, GETDATE()) AND seats_business > 0;

-- 8) Получить информацию о количестве проданных билетов на все рейсы за указанный день и их общую сумму;
SELECT flight_id, COUNT(*) AS tickets_sold, SUM(price) AS total_amount
FROM Tickets
WHERE CONVERT(DATE, purchase_date) = '2024-08-10'
GROUP BY flight_id;

-- 9) Вывести информацию о предварительной продаже билетов на определенную дату с указанием всех рейсов и количестве проданных на них билетов;
SELECT f.flight_number, COUNT(t.id) AS tickets_sold
FROM Flights f
         LEFT JOIN Tickets t ON f.id = t.flight_id AND CONVERT(DATE, t.purchase_date) = '2024-08-10'
GROUP BY f.flight_number;

-- 10) Вывести номера всех рейсов и названия всех городов, в которые совершаются полеты из данного аэропорта.
SELECT flight_number, arrival_city
FROM Flights
WHERE departure_city = 'Львов';