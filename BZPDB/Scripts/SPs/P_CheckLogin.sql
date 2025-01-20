USE [BZP]
GO
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[P_CheckLogin]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].P_CheckLogin
GO

CREATE PROCEDURE [dbo].P_CheckLogin
	
	@LoginID	nvarchar(50),
	@Password	nvarchar(20)

AS
BEGIN
		
	select	FName,
			UserID,
			UserTypeID,
			Email,
			Phone	= case when Phone1 is null then case when Phone2 is null then '' else Phone2 end
					else Phone1 end,
			ZIP		= case when ZIP is null then '' else ZIP end
	from	u_Users
	where	(LoginID	= @LoginID
	or		Email		= @LoginID)
	and		Password	= @Password
	and		Active		= 1

END

GO
-- P_CheckLogin 'serega', '¡¡Tjux'