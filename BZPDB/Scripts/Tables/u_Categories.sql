USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_Categories]') AND type in (N'U'))
DROP TABLE [dbo].u_Categories
GO

CREATE TABLE [dbo].u_Categories(
	[CategoryID] [smallint] NOT NULL,
	[Category] [nvarchar](60) NOT NULL,
	[RangeID] [smallint] NOT NULL,
	Active bit default 1 NOT NULL
) ON [PRIMARY]

GO

