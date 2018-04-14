CREATE TABLE [dbo].[AttemptResult]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [QuestionId] INT NOT NULL, 
    [AnswerTime] DATETIME NOT NULL, 
    [AttemptId] INT NOT NULL, 
    [IsCorrect] BIT NOT NULL DEFAULT 0, 
	CONSTRAINT [FK_AttemptResult_ToQuestion] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Question]([Id]),
	CONSTRAINT [FK_AttemptResult_ToAttempt] FOREIGN KEY ([AttemptId]) REFERENCES [dbo].[TestAttempt]([Id]),
    
)
