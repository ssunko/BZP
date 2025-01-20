USE [BZP]
GO
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[P_PassRecoverQuestions_GET]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].P_PassRecoverQuestions_GET
GO

CREATE PROCEDURE [dbo].P_PassRecoverQuestions_GET
	
	@LoginID nvarchar(50),
	@RecTryNum tinyint

AS
BEGIN
	declare @UserID bigint
	
	select	@UserID = UserID
	from	u_Users
	where	(LoginID	= @LoginID
	or		Email		= @LoginID)
	
	declare	@Qns table(ID int identity, Question nvarchar(60), Answer nvarchar(100))
	
	insert	@Qns(Question, Answer)
	select	Question,
			Answer
	from	u_PassRecoverQuestions rq
	inner join	u_Users_PassRecoverQuestions urq
	on		rq.QuestionID = urq.QuestionID
	where	urq.UserID = ISNULL(@UserID,0)
	
	if @RecTryNum > (select MAX(ID) from @Qns)
		select @RecTryNum = MAX(ID) from @Qns
	
	select	Question,
			Answer
	from	@Qns
	where	ID = @RecTryNum
	


END

GO
-- P_PassRecoverQuestions_GET 'serega', 6
