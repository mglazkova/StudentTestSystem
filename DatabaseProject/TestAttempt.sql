CREATE TABLE [dbo].[TestAttempt]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TestId] INT NOT NULL, 
    [StudentId] INT NOT NULL, 
    [StartTime] DATETIME NOT NULL, 
    [FinishTime] DATETIME NULL, 
    [AttemptKey] UNIQUEIDENTIFIER NOT NULL, 
    [IsTimeIsUp] BIT NOT NULL DEFAULT 0, 
    [Grade] INT NULL, 
    CONSTRAINT [FK_TestAttempt_ToTest] FOREIGN KEY ([TestId]) REFERENCES [dbo].[TestInstance]([Id]),
	CONSTRAINT [FK_TestAttempt_ToStudent] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student]([Id]),

)
