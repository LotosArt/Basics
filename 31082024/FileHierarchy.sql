CREATE TABLE [Files] (
    [Id] INT IDENTITY PRIMARY KEY,
    [FileName] NVARCHAR(50),
    [ParentFolderID] INT
);

INSERT INTO Files VALUES ('FolderA', NULL),
    ('File1.txt', 1), ('File2.txt', 1), ('FolderB', NULL),
    ('File3.txt', 4), ('File4.txt', 4), ('FolderC', NULL),
    ('File5.txt', 7);


WITH FileHierarchy AS
 (
     SELECT
         Id,
         FileName,
         ParentFolderID,
         0 AS Level
     FROM
         Files
     WHERE
         ParentFolderID IS NULL

     UNION ALL

     SELECT
         f.Id,
         f.FileName,
         f.ParentFolderID,
         fh.Level + 1 AS Level
     FROM
         Files f
             INNER JOIN
         FileHierarchy fh ON f.ParentFolderID = fh.Id
 )

SELECT
    REPLICATE('    ', Level) + FileName AS FileName,
    Level
FROM
    FileHierarchy
ORDER BY
    Level, FileName;

