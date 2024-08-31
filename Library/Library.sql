CREATE DATABASE Library;
GO

USE Library;
GO

CREATE TABLE Authors (
    AuthorId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(255) NOT NULL,
    Country NVARCHAR(255)
);

CREATE TABLE Genres (
    GenreId INT PRIMARY KEY IDENTITY,
    GenreName NVARCHAR(255) NOT NULL
);

CREATE TABLE Books (
    BookId INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(255) NOT NULL,
    AuthorId INT NOT NULL,
    PublishedYear INT,
    GenreId INT NOT NULL,
    AvailableCopies INT NOT NULL,
    FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId),
    FOREIGN KEY (GenreId) REFERENCES Genres(GenreId)
);

CREATE TABLE Readers (
     ReaderId INT PRIMARY KEY IDENTITY,
     Name NVARCHAR(255) NOT NULL,
     Email NVARCHAR(255),
     PhoneNumber NVARCHAR(50)
);

CREATE TABLE Loans (
    LoanId INT PRIMARY KEY IDENTITY,
    BookId INT NOT NULL,
    ReaderId INT NOT NULL,
    LoanDate DATETIME NOT NULL,
    ReturnDate DATETIME NULL,
    FOREIGN KEY (BookId) REFERENCES Books(BookId),
    FOREIGN KEY (ReaderId) REFERENCES Readers(ReaderId)
);

GO

INSERT INTO Authors (Name, Country) VALUES
    ('George Orwell', 'United Kingdom'),
    ('Harper Lee', 'United States'),
    ('J.K. Rowling', 'United Kingdom'),
    ('J.R.R. Tolkien', 'United Kingdom');

INSERT INTO Genres (GenreName) VALUES
    ('Dystopian'),
    ('Classics'),
    ('Fantasy'),
    ('Adventure');

INSERT INTO Books (Title, AuthorId, PublishedYear, GenreId, AvailableCopies) VALUES
     ('1984', 1, 1949, 1, 5),
     ('To Kill a Mockingbird', 2, 1960, 2, 3),
     ('Harry Potter and the Sorcerer''s Stone', 3, 1997, 3, 10),
     ('The Hobbit', 4, 1937, 4, 7);

INSERT INTO Readers (Name, Email, PhoneNumber) VALUES
    ('Alice Smith', 'alice.smith@example.com', '123-456-7890'),
    ('Bob Johnson', 'bob.johnson@example.com', '234-567-8901'),
    ('Charlie Brown', 'charlie.brown@example.com', '345-678-9012');

INSERT INTO Loans (BookId, ReaderId, LoanDate, ReturnDate) VALUES
    (1, 1, '2024-08-01', NULL),
    (2, 2, '2024-08-10', '2024-08-20'),
    (3, 3, '2024-08-15', NULL);
GO