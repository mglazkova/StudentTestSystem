CREATE TABLE [dbo].[Question]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Content] NVARCHAR(MAX) NOT NULL, 
    [Image] VARBINARY(MAX) NULL, 
    [Created] DATETIME NOT NULL, 
    [Modifiyed] DATETIME NULL, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
)
