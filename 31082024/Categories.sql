CREATE TABLE [Categories] (
    [Id] INT IDENTITY,
    [Name] NVARCHAR(50),
    [ParentCategoryID] INT
);

INSERT INTO Categories VALUES ('Electronics', NULL);
INSERT INTO Categories VALUES ('Laptops', 1);
INSERT INTO Categories VALUES ('Smartphones', 1);
INSERT INTO Categories VALUES ('Clothing', NULL);
INSERT INTO Categories VALUES ('Shoes', 4);
INSERT INTO Categories VALUES ('T-Shirts', 4);

WITH CategoryHierarchy AS
(
     SELECT
         Id,
         Name,
         ParentCategoryID,
         0 AS Level
     FROM
         Categories
     WHERE
         ParentCategoryID IS NULL
    
     UNION ALL
    
     SELECT
         c.Id,
         c.Name,
         c.ParentCategoryID,
         ch.Level + 1 AS Level
     FROM
         Categories c
             INNER JOIN
         CategoryHierarchy ch ON c.ParentCategoryID = ch.Id
)

SELECT
    REPLICATE('    ', Level) + Name AS CategoryName,
    Level
FROM
    CategoryHierarchy
ORDER BY
    Level, CategoryName;
