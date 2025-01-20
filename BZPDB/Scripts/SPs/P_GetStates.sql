USE [BZP]
GO
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[P_GetStates]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].P_GetStates
GO

CREATE PROCEDURE [dbo].P_GetStates

AS
BEGIN
	SET NOCOUNT	ON
	select	StateID,
			StateSN,
			StateLN
	from	u_USStates

END

GO
