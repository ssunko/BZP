USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_USCities]') AND type in (N'U'))
DROP TABLE [dbo].u_USCities
GO

CREATE TABLE [dbo].u_USCities(
	[CityID] [int] identity NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[StateID] [smallint] NOT NULL
) ON [PRIMARY]

GO

