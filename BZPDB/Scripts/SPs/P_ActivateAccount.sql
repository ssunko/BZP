USE [BZP]
GO
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[P_ActivateAccount]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].P_ActivateAccount
GO

CREATE PROCEDURE [dbo].P_ActivateAccount
	
	@LoginID	nvarchar(20)

AS
BEGIN
		
	update	u_Users
	set		Active			= 1,
			LastModified	= GETDATE()
	where	LoginID		= @LoginID
	
	if exists(select 1 from u_Users where LoginID = @LoginID and Active	= 1)
		select	1
	else
		select	0

END

GO
