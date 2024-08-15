create table Sellers
(
    Id int IDENTITY PRIMARY KEY,
    FullName varchar(60) NOT NULL
)
create table Books(
    Id int IDENTITY PRIMARY KEY,
    SellerId int NOT NULL REFERENCES Sellers(Id),
)
create table BooksDetails(
     Id int IDENTITY PRIMARY KEY,
     BooksId int NOT NULL REFERENCES Books(Id),
     OrderDate date NOT NULL,
     Quantity int NOT NULL,
     Price money NOT NULL
)

insert into Sellers values
    ('Alex Petrov'),
    ('John Smith'),
    ('Tom Rabinovich'),
    ('Sam Nikolas')


insert into Books values
    (1),(1),(2),(3),(2),(1),(3),(1),(1)

insert into BooksDetails values
     (1,'2020-10-25',7, 300),
     (2,'2020-10-04',3, 150),
     (3,'2019-09-21',10, 500),
     (4,'2020-08-17',4, 250),
     (5,'2020-10-16',6, 275),
     (6,'2020-06-08',2, 333),
     (7,'2020-11-12',1, 147),
     (8,'2020-12-28',15, 123),
     (9,'2020-11-22',32, 1870);



SELECT s.FullName, SUM(bd.Quantity) AS TotalQuantity
FROM Sellers s
JOIN Books b ON s.Id = b.SellerId
JOIN BooksDetails bd ON b.Id = bd.BooksId
WHERE YEAR(bd.OrderDate) = 2020
GROUP BY s.FullName
HAVING SUM(bd.Quantity) > 30;


