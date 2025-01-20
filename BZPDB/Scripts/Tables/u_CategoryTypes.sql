USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_CategoryTypes]') AND type in (N'U'))
DROP TABLE [dbo].u_CategoryTypes
GO

CREATE TABLE [dbo].u_CategoryTypes(
	[TypeID] [smallint] NOT NULL,
	[Type] [nvarchar](60) NOT NULL
) ON [PRIMARY]

GO