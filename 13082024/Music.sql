create database Music
GO
use Music
GO
CREATE TABLE artist (
                        id INT IDENTITY PRIMARY KEY,
                        name VARCHAR(60) NOT NULL
);

CREATE TABLE albums(
                       id INT IDENTITY PRIMARY KEY,
                       title VARCHAR(60) NOT NULL,
                       artist_id INT,
                       FOREIGN KEY (artist_id) REFERENCES artist(id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE tracks (
                        id INT IDENTITY PRIMARY KEY,
                        title VARCHAR(60) NOT NULL,
                        second INT NOT NULL,
                        price MONEY NOT NULL,
                        album_id INT,
                        FOREIGN KEY (album_id) REFERENCES albums(id) ON DELETE CASCADE ON UPDATE CASCADE
);

INSERT INTO artist (name)
VALUES ('Вячеслав Бутусов'), ('Сплин'), ('Би-2');

INSERT INTO albums (title, artist_id)
VALUES ('25-й кадр', 2),('Биографика', 1),('Би-2', 3);

INSERT INTO albums (title)
VALUES ('Четвертый альбом'), ('Пятый альбом');

INSERT INTO tracks (title, second, price, album_id)
VALUES ('Девушка по городу', 193, 26.20, 2),('Песня идущего домой', 170, 22.10, 2),('Полковнику никто не пишет', 292, 32.15, 3),
       ('Мой друг', 291, 27.15, 3), ('Моё сердце', 249, 21.12, 1),('Линия жизни', 180, 41.12, 1),('Остаемся зимовать', 218, 17.62, 1);

INSERT INTO tracks (title, second, price)
VALUES ('Мертвый город', 180, 41.12), ('Звезда по имени Солнце', 218, 17.62);

-- 1) Вывести таблицу Traks и Albums, В результирующей таблице не должно быть треков, у которых нет исполнителей и нет альбомов.
SELECT tracks.id, tracks.title, tracks.second, tracks.price, albums.title AS album_title
FROM tracks
         JOIN albums ON tracks.album_id = albums.id
         JOIN artist ON albums.artist_id = artist.id;

-- 2) Вывести в таблицу: tracks.title, second, price, albums.title, только песни группы Сплин.
SELECT tracks.title, tracks.second, tracks.price, albums.title AS album_title
FROM tracks
         JOIN albums ON tracks.album_id = albums.id
         JOIN artist ON albums.artist_id = artist.id
WHERE artist.name = 'Сплин';

-- 3) Вывести все треки: tracks.title, tracks.second, tracks.price, albums.title, вместе с названием альбома (даже если у трека, нет альбома).
SELECT tracks.title, tracks.second, tracks.price, albums.title AS album_title
FROM tracks
         LEFT JOIN albums ON tracks.album_id = albums.id;

-- 4) Вывести название трека, альбома и исполнителя (tracks.title, albums.title, artist.name) в таблицу. 
-- Не выводить строку, если в каком-то из столбцов – значение NULL. Сразу после вывода, вывести ту же информацию, 
-- только вместе со столбцами, у которых значение NULL.
-- NOT NULL
SELECT tracks.title AS track_title, albums.title AS album_title, artist.name AS artist_name
FROM tracks
         JOIN albums ON tracks.album_id = albums.id
         JOIN artist ON albums.artist_id = artist.id
WHERE tracks.album_id IS NOT NULL AND albums.artist_id IS NOT NULL;

-- NULL
SELECT tracks.title AS track_title, albums.title AS album_title, artist.name AS artist_name
FROM tracks
         LEFT JOIN albums ON tracks.album_id = albums.id
         LEFT JOIN artist ON albums.artist_id = artist.id;

-- 5) В ваш магазин зашел посетитель. Его интересуют пластинки группы БИ-2, стоимость которых больше 25 долларов. 
-- Посетитель хочет увидеть следующие информацию из выборки: название пластинки, название альбома, имя артиста и стоимость. 
-- Выборка должна быть отсортирована, от самой дешевей пластинки, до самой дорогой. 
SELECT tracks.title AS track_title, albums.title AS album_title, artist.name AS artist_name, tracks.price
FROM tracks
         JOIN albums ON tracks.album_id = albums.id
         JOIN artist ON albums.artist_id = artist.id
WHERE artist.name = 'Би-2' AND tracks.price > 25
ORDER BY tracks.price ASC;

-- 6) После того как покупатель получил несколько пластинок, он не смог определить, какую приобрести, поэтому попросил вас, выдать ему одну случайную пластинку по его критериям, которые он изложил выше.
SELECT TOP 1 tracks.title AS track_title, albums.title AS album_title, artist.name AS artist_name, tracks.price
FROM tracks
         JOIN albums ON tracks.album_id = albums.id
         JOIN artist ON albums.artist_id = artist.id
WHERE artist.name = 'Би-2' AND tracks.price > 25
ORDER BY NEWID();

-- 7) Сразу же за первым посетителем, зашел второй и попросил ему выдать пластинку, которую он видел в рекламе по телевизору, но название пластинки забыл, говорит, что в названии, присутствовало слово ‘Солнце’. Покажите ему все пластинки (tracks.title, albums.title, artist.name, tracks.price), которые содержат в название данное слово.
SELECT tracks.title AS track_title, albums.title AS album_title, artist.name AS artist_name, tracks.price
FROM tracks
         JOIN albums ON tracks.album_id = albums.id
         JOIN artist ON albums.artist_id = artist.id
WHERE tracks.title LIKE '%Солнце%';

-- 8) Третий покупатель пришел за подарком для тещи. Он попросил выдать ему самую дешевую пластинку, главное, чтобы группа была не под названием ‘Сплин Пистолс’ или ‘Баба Сплин’, он не помнит точно, знает то, что в названии группы есть слово – ‘Сплин’.
SELECT TOP 1 tracks.title AS track_title, albums.title AS album_title, artist.name AS artist_name, tracks.price
FROM tracks
         JOIN albums ON tracks.album_id = albums.id
         JOIN artist ON albums.artist_id = artist.id
WHERE artist.name NOT LIKE '%Сплин%'
ORDER BY tracks.price ASC;

-- 9) У четвертого покупателя, запрос довольно простой. Ему продайте пластинку, которая находится в альбоме, с наибольшим количество треков, по сравнению со всеми остальными альбомами.
SELECT TOP 1 tracks.title AS track_title, albums.title AS album_title, artist.name AS artist_name, COUNT(tracks.id) AS track_count
FROM tracks
         JOIN albums ON tracks.album_id = albums.id
         JOIN artist ON albums.artist_id = artist.id
GROUP BY tracks.title, albums.title, artist.name
ORDER BY COUNT(tracks.id) DESC;
