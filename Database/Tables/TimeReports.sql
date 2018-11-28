﻿CREATE TABLE [dbo].[TimeReports]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Date] DATE NOT NULL,
	[Duration] INT NOT NULL,
	[ActivityId] INT NOT NULL,
	[UserId] INT NOT NULL,
	CONSTRAINT FK_UserReport FOREIGN KEY (UserId)
	REFERENCES Users(Id),
	CONSTRAINT FK_ActivityReport FOREIGN KEY ([ActivityId])
	REFERENCES Activities(Id)
)
