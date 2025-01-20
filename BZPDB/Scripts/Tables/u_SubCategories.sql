USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_SubCategories]') AND type in (N'U'))
DROP TABLE [dbo].u_SubCategories
GO

CREATE TABLE [dbo].u_SubCategories(
	[SubCategoryID] [smallint] NOT NULL,
	[CategoryID] [smallint] NOT NULL,
	[SubCategory] [nvarchar](60) NOT NULL,
	Active bit default 1 NOT NULL
) ON [PRIMARY]

GO

