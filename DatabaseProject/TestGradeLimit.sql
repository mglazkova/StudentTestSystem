CREATE TABLE [dbo].[TestGradeLimit]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FromPer] INT NOT NULL, 
    [ToPer] INT NOT NULL, 
    [Grade] INT NOT NULL, 
    [TestId] INT NOT NULL,   
	CONSTRAINT [FK_GradeLimit_ToTest] FOREIGN KEY ([TestId]) REFERENCES [dbo].[TestInstance]([Id]), 
)
