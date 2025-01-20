USE [BZP]
GO
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[P_GetRecoverQuestions]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].P_GetRecoverQuestions
GO

CREATE PROCEDURE [dbo].P_GetRecoverQuestions

AS
BEGIN
	SET NOCOUNT	ON
	select	QuestionID,
			Question
	from	u_PassRecoverQuestions
	where	Active = 1

END

GO
