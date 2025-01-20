USE [BZP]
GO
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[P_GetUser]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].P_GetUser
GO

CREATE PROCEDURE [dbo].P_GetUser

	@UserID			bigint
	
AS
BEGIN
	SET NOCOUNT	ON


declare @PassRecoverQuestions TABLE (
	RecID int identity,
	QuestionID smallint,
	Answer nvarchar(100)
)
	
insert	@PassRecoverQuestions(QuestionID, Answer)
select	QuestionID, Answer
from	u_Users_PassRecoverQuestions
where	UserID = @UserID

select	UserID,
		UserTypeID,
		LoginID,
		Email,
		FName,
		LName = isnull(LName,''),
		Phone1 = isnull(Phone1,''),
		Phone2 = isnull(Phone2,''),
		FAX = isnull(FAX,''),
		Address1 = isnull(Address1,''),
		Address2 = isnull(Address2,''),
		City = isnull(City,''),
		StateID = isnull(StateID,''),
		ZIP = isnull(ZIP,''),
		Password,
		-- Active,
		Created,
		LastModified,
		QuestionID1 = (select QuestionID from @PassRecoverQuestions where RecID = 1),
		QuestionID2 = isnull((select QuestionID from @PassRecoverQuestions where RecID = 2),0),
		QuestionID3 = isnull((select QuestionID from @PassRecoverQuestions where RecID = 3),0),
		Answer1 = (select Answer from @PassRecoverQuestions where RecID = 1),
		Answer2 = isnull((select Answer from @PassRecoverQuestions where RecID = 2),''),
		Answer3 = isnull((select Answer from @PassRecoverQuestions where RecID = 3),'')
from	u_Users
where	UserID	= @UserID
and		Active	= 1

	
END

GO
-- P_GetUser 1 
-- select * from u_Users