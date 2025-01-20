USE [BZP]
GO
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[P_CheckLoginExistence]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].P_CheckLoginExistence
GO

CREATE PROCEDURE [dbo].P_CheckLoginExistence

	@LoginID		nvarchar(20),
	@Email			nvarchar(50),
	@UserID			bigint = null

AS
BEGIN
SET NOCOUNT	ON
	
declare	@Res int

select @Res = 1
if (@UserID is null)
BEGIN
	if exists (select 1 from u_Users where LoginID = @LoginID or Email = @LoginID)
		select @Res	= -1
		
	if exists (select 1 from u_Users where LoginID = @Email or Email = @Email)
		select @Res	= -2
END
else
BEGIN
	if exists (select 1 from u_Users where (LoginID = @LoginID or Email = @LoginID) and UserID != @UserID)
		select @Res	= -1
		
	if exists (select 1 from u_Users where (LoginID = @Email or Email = @Email) and UserID != @UserID)
		select @Res	= -2
END
	return @Res

END
GO
