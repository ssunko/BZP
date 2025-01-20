USE [BZP]
GO
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[P_UpdateUser]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].P_UpdateUser
GO

CREATE PROCEDURE [dbo].P_UpdateUser

	@UserID			bigint,
	@LoginID		nvarchar(20),
	@Password		nvarchar(20),
	@Email			nvarchar(50),
	@FName			nvarchar(25),
	@LName			nvarchar(25) =  NULL,
	@Phone1			nvarchar(15) =  NULL,
	@Phone2			nvarchar(15) =  NULL,
	@FAX			nvarchar(15) =  NULL,
	@Address1		nvarchar(100) =  NULL,
	@Address2		nvarchar(100) =  NULL,
	@City			nvarchar(50) =  NULL,
	@StateID		smallint =  NULL,
	@ZIP			nchar(5) =  NULL,
	@QuestionID1	smallint,
	@Answer1		nvarchar(100),
	@QuestionID2	smallint =  NULL,
	@Answer2		nvarchar(100) =  NULL,
	@QuestionID3	smallint =  NULL,
	@Answer3		nvarchar(100) =  NULL,
	@Created		datetime = NULL,
	@LastModified	datetime = NULL
	
AS
BEGIN
	SET NOCOUNT	ON
declare	@Res int
		
select	@Res = 0

EXEC @Res = P_CheckLoginExistence  @LoginID, @Email, @UserID
if @Res != 1
begin
	select @Res
	return
end

declare @PassRecoverQuestions TABLE (
	UserID		bigint,
	QuestionID	smallint,
	Answer		nvarchar(100)
)

insert	@PassRecoverQuestions(UserID, QuestionID, Answer)
select	@UserID, @QuestionID1, @Answer1

if (@QuestionID2 IS NOT NULL)
	insert	@PassRecoverQuestions(UserID, QuestionID, Answer)
	select	@UserID, @QuestionID2, @Answer2
if (@QuestionID3 IS NOT NULL)
	insert	@PassRecoverQuestions(UserID, QuestionID, Answer)
	select	@UserID, @QuestionID3, @Answer3

begin tran
update	u_Users
set		LoginID			= @LoginID,
		Email			= @Email,
		FName			= @FName,
		LName			= @LName,
		Phone1			= @Phone1,
		Phone2			= @Phone2,
		FAX				= @FAX,
		Address1		= @Address1,
		Address2		= @Address2,
		City			= @City,
		StateID			= @StateID,
		ZIP				= @ZIP,
		Password		= @Password,
		LastModified	= GETDATE()
where	UserID			= @UserID
		
if @@ERROR != 0
	select	@Res = 0
else
	delete	u_Users_PassRecoverQuestions where UserID = @UserID
if (@@ERROR = 0 and @Res = 1)	
	insert	u_Users_PassRecoverQuestions(UserID, QuestionID, Answer)
	select UserID, QuestionID, Answer from @PassRecoverQuestions

if (@@ERROR = 0 and @Res = 1)
begin
	commit tran
	select 1	
end
else
begin
	rollback tran
	select 0
end
	
END

GO
