USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_Users]') AND type in (N'U'))
DROP TABLE [dbo].u_Users
GO

CREATE TABLE [dbo].u_Users(
	UserID		bigint NOT NULL,
    UserTypeID	smallint NOT NULL,
	LoginID		nvarchar(20) NOT NULL,
	Email		nvarchar(50) NOT NULL,
	FName		nvarchar(25) NOT NULL,
	LName		nvarchar(25) NULL,
	Phone1		nvarchar(15) NULL,
	Phone2		nvarchar(15) NULL,
	FAX			nvarchar(15) NULL,
	Address1	nvarchar(100) NULL,
	Address2	nvarchar(100) NULL,
	City		nvarchar(50) NULL,
	StateID		smallint NULL,
	ZIP			nchar(5) NULL,
	[Password]	nvarchar(20) NOT NULL,
	Active		bit default 1 NOT NULL
) ON [PRIMARY]

GO

