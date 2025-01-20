USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_USZips]') AND type in (N'U'))
DROP TABLE [dbo].u_USZips
GO

CREATE TABLE [dbo].u_USZips(
	ZipID int identity NOT NULL,
	ZipCode [nchar](5) NOT NULL,
	Latitude  [nchar](10)  NULL,
	Longitude [nchar](11)  NULL,
	CityID [int] NOT  NULL,
	StateID  smallint  NOT  NULL,
	County  [nvarchar](35) NULL,
	Zip_Class  [nvarchar](15) NOT  NULL
	
) ON [PRIMARY]

GO
