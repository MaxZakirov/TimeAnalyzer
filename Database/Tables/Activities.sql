﻿CREATE TABLE [dbo].[Activities]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
    [TypeId] INT NOT NULL,
	CONSTRAINT FK_Activities FOREIGN KEY ([TypeId])
	REFERENCES ActivityTypes(Id)
)
