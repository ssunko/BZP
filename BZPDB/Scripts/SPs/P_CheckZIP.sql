USE [BZP]
GO
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[P_CheckZIP]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].P_CheckZIP
GO

CREATE PROCEDURE [dbo].P_CheckZIP
	
	@ZIP	nchar(5)
	
AS
BEGIN
	IF EXISTS(select 1 from	u_USZips where	ZipCode = @ZIP)
		SELECT	1
	ELSE
		SELECT	0
END

GO
-- P_CheckZIP '99999'