﻿CREATE TABLE [dbo].[CredentialTypes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Code] NVARCHAR(50) NOT NULL,
	[Name] NVARCHAR(30) NOT NULL,
	[Position] INT
)
