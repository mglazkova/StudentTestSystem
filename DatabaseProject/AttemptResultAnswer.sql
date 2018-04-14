CREATE TABLE [dbo].[AttemptResultAnswer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ResultId] INT NOT NULL, 
    [AnswerId] INT NOT NULL, 
	CONSTRAINT [FK_AttemptResultAnswer_ToResult] FOREIGN KEY ([ResultId]) REFERENCES [dbo].[AttemptResult]([Id]),
	CONSTRAINT [FK_AttemptResultAnswer_ToAnswer] FOREIGN KEY ([AnswerId]) REFERENCES [dbo].[Answer]([Id]),

)
