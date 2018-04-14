CREATE TABLE [dbo].[Question2Test]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TestId] INT NOT NULL, 
    [QuestionId] INT NOT NULL, 
	CONSTRAINT [FK_Question2Test_ToTest] FOREIGN KEY ([TestId]) REFERENCES [dbo].[TestInstance]([Id]),
	CONSTRAINT [FK_Question2Test_ToQestion] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Question]([Id]),

)
