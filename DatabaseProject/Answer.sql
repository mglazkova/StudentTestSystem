CREATE TABLE [dbo].[Answer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,    
    [QuestionId] INT NOT NULL, 
    [Content] NVARCHAR(MAX) NOT NULL, 
    [IsRight] BIT NOT NULL DEFAULT 0,
	[Created] DATETIME NOT NULL, 
    [Modifiyed] DATETIME NULL,  
	[IsDeleted] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Answer_ToQestion] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Question]([Id]),

)
