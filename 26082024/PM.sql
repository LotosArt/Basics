CREATE TABLE [Projects] (
    [ProjectID] INT PRIMARY KEY IDENTITY,
    [ProjectName] NVARCHAR(100) NOT NULL,
    [ProjectStatus] NVARCHAR(50) NOT NULL,
    [ProjectBudget] DECIMAL(15, 2) NOT NULL
);

CREATE TABLE [ChangeRequests] (
    [ChangeRequestID] INT PRIMARY KEY IDENTITY,
    [ProjectID] INT,
    [ProposedStatus] NVARCHAR(50) NOT NULL,
    [ProposedBudget] DECIMAL(15, 2) NOT NULL,
    [RequestedBy] NVARCHAR(50) NOT NULL,
    [RequestedAt] DATETIME NOT NULL,
    [IsApproved] BIT DEFAULT 0,
    CONSTRAINT FK_ChangeRequests_Projects FOREIGN KEY ([ProjectID]) REFERENCES Projects([ProjectID])
);

GO 

CREATE PROCEDURE ApproveChangeRequest
@ChangeRequestID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @ProjectID INT;
        DECLARE @ProposedStatus NVARCHAR(50);
        DECLARE @ProposedBudget DECIMAL(15, 2);

        SELECT @ProjectID = ProjectID,
               @ProposedStatus = ProposedStatus,
               @ProposedBudget = ProposedBudget
        FROM ChangeRequests
        WHERE ChangeRequestID = @ChangeRequestID;

        UPDATE Projects
        SET ProjectStatus = @ProposedStatus,
            ProjectBudget = @ProposedBudget
        WHERE ProjectID = @ProjectID;

        UPDATE ChangeRequests
        SET IsApproved = 1
        WHERE ChangeRequestID = @ChangeRequestID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'Server error ' + CAST(ERROR_NUMBER() AS NVARCHAR(10)) + ': ' + ERROR_MESSAGE();
        PRINT 'Error state ' + CAST(ERROR_STATE() AS NVARCHAR(10));
        THROW;
    END CATCH
END;
