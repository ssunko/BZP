USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_Classifieds]') AND type in (N'U'))
DROP TABLE [dbo].u_Classifieds
GO



CREATE TABLE [dbo].[u_Classifieds](
	[ClassifiedID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[ShortDescription] [nvarchar](300) NOT NULL,
	[Text] [nvarchar](3600) NULL,
	[StateID] [smallint] NULL,
	[CityID] [int] NULL,
	[ZipID] [int] NULL,
	[City] [nvarchar](50) NULL,
	[Zip] [nchar](5) NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[CategoryID] [smallint] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Stands] [smallint] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_u_Classifieds] PRIMARY KEY CLUSTERED 
(
	[ClassifiedID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[u_Classifieds]  WITH CHECK ADD  CONSTRAINT [FK_u_Classifieds_u_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[u_Categories] ([CategoryID])
GO

ALTER TABLE [dbo].[u_Classifieds] CHECK CONSTRAINT [FK_u_Classifieds_u_Categories]
GO

ALTER TABLE [dbo].[u_Classifieds]  WITH CHECK ADD  CONSTRAINT [FK_u_Classifieds_u_USCities] FOREIGN KEY([CityID])
REFERENCES [dbo].[u_USCities] ([CityID])
GO

ALTER TABLE [dbo].[u_Classifieds] CHECK CONSTRAINT [FK_u_Classifieds_u_USCities]
GO

ALTER TABLE [dbo].[u_Classifieds]  WITH CHECK ADD  CONSTRAINT [FK_u_Classifieds_u_USStates] FOREIGN KEY([StateID])
REFERENCES [dbo].[u_USStates] ([StateID])
GO

ALTER TABLE [dbo].[u_Classifieds] CHECK CONSTRAINT [FK_u_Classifieds_u_USStates]
GO

ALTER TABLE [dbo].[u_Classifieds]  WITH CHECK ADD  CONSTRAINT [FK_u_Classifieds_u_USZips] FOREIGN KEY([ZipID])
REFERENCES [dbo].[u_USZips] ([ZipID])
GO

ALTER TABLE [dbo].[u_Classifieds] CHECK CONSTRAINT [FK_u_Classifieds_u_USZips]
GO





