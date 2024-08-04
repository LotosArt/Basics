CREATE DATABASE Casino;
GO

USE Casino;
GO

CREATE TABLE Card (
    game_date DATE,
    win_amount INT,
    game_description VARCHAR(255),
    machine_weight INT,
    seat_count INT
);
GO

INSERT INTO Card (game_date, win_amount, game_description, machine_weight, seat_count)
VALUES
    ('2024-07-01', 500, 'Poker', 2000, 4),
    ('2024-07-02', 1500, 'Blackjack', 2500, 6),
    ('2024-07-03', 100, 'Slot Machine', 1500, 1);
GO

EXEC sp_rename 'Card', 'Automate';
GO

DELETE FROM Automate;
GO

DROP TABLE Automate;
GO

CREATE TABLE Automate (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    game_date DATE NOT NULL,
    win_amount INT NOT NULL CHECK (win_amount BETWEEN 1 AND 1000000),
    game_description VARCHAR(255) NOT NULL,
    machine_weight INT NOT NULL,
    seat_count INT NOT NULL
);
GO

INSERT INTO Automate (game_date, win_amount, game_description, machine_weight, seat_count)
VALUES
    ('2024-07-01', 500, 'Poker', 2000, 4),
    ('2024-07-02', 1500, 'Blackjack', 2500, 6),
    ('2024-07-03', 100, 'Slot Machine', 1500, 1);
GO

INSERT INTO Automate (game_date, win_amount, game_description, machine_weight, seat_count)
VALUES ('2024-07-04', -500, 'Roulette', 2200, 5);

INSERT INTO Automate (game_date, win_amount, machine_weight, seat_count)
VALUES ('2024-07-05', 300, 1800, 3);

