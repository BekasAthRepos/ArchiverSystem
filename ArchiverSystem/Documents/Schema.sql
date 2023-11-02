CREATE TABLE [dbo].[Album] (
    [Id]          INT         IDENTITY (1, 1) NOT NULL,
    [Name]        NCHAR (20)  NULL,
    [Description] NCHAR (100) NULL,
    [InputDate]   DATETIME    NULL,
    [UpdateDate]  DATETIME    NULL
);

CREATE TABLE [dbo].[Item]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [AlbumId] INT NOT NULL, 
    [Name] NCHAR(20) NULL, 
    [Description] NCHAR(100) NULL, 
    [Qty] FLOAT NULL, 
    [InputDate] DATETIME NULL, 
    [UpdateDate] DATETIME NULL, 
    [Image] IMAGE null
);