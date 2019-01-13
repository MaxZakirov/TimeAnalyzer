CREATE TABLE [dbo].[ActivityTypes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(40) NOT NULL,
	[ColorValue] NCHAR(7) NULL DEFAULT 'E5F50B',
	[ImportanceFactor] INT not null DEFAULT 0
)
