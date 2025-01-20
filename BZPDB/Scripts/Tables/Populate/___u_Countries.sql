USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_Countries]') AND type in (N'U'))
DROP TABLE [dbo].[u_Countries]
GO

CREATE TABLE [dbo].[u_Countries](
	[CountryID] [smallint] identity NOT NULL,
	[CountryName] [nvarchar](60) NOT NULL
) ON [PRIMARY]

GO



