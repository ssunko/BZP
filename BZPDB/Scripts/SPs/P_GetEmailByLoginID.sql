USE [BZP]
GO
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[P_GetEmailByLoginID]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].P_GetEmailByLoginID
GO

CREATE PROCEDURE [dbo].P_GetEmailByLoginID
	
	@LoginID	nvarchar(50)

AS
BEGIN
		
	select	Email,
			[Password],
			FName
	from	u_Users
	where	(LoginID	= @LoginID
	or		Email		= @LoginID)

END

GO
-- P_GetEmailByLoginID 'serega'
