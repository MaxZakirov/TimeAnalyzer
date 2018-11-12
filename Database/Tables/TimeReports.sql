CREATE TABLE [dbo].[TimeReports]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Date] DATE NOT NULL,
	[Durablility] SMALLINT NOT NULL,
	[ActivityType] TINYINT,
	[UserId] INT NOT NULL,
	CONSTRAINT FK_UserReport FOREIGN KEY (UserId)
	REFERENCES Users(Id)
)
