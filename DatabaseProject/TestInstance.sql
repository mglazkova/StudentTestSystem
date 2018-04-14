CREATE TABLE [dbo].[TestInstance]
(
		[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [DisciplineType] INT NOT NULL DEFAULT 0, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [IsCurrent] BIT NOT NULL DEFAULT 1, 
    [QuestionCount] INT NOT NULL DEFAULT 0, 
    [MinuteTimeLimit] INT NOT NULL DEFAULT 0, 
)
