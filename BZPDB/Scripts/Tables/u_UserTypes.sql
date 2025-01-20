USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_UserTypes]') AND type in (N'U'))
DROP TABLE [dbo].u_UserTypes
GO

CREATE TABLE [dbo].u_UserTypes(
	UserTypeID	smallint NOT NULL,
	UserType	nvarchar(25) NOT NULL,
	[Description] nvarchar(200) NULL,
) ON [PRIMARY]

GO

