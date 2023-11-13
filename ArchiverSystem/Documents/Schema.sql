CREATE TABLE [dbo].[Album] (
    [Id]          INT         IDENTITY (1, 1) NOT NULL,
    [Name]        NCHAR   NULL,
    [Description] NCHAR  NULL,
    [InputDate]   DATETIME    NULL,
    [UpdateDate]  DATETIME    NULL
);

CREATE TABLE [dbo].[Item]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [AlbumId] INT NOT NULL, 
    [Name] NCHAR NULL, 
    [Description] NCHAR NULL, 
    [Qty] FLOAT NULL, 
    [InputDate] DATETIME NULL, 
    [UpdateDate] DATETIME NULL, 
    [Image] IMAGE null
);