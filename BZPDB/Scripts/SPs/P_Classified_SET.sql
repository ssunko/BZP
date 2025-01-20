
USE [BZP]
GO
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[P_Classified_SET]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].P_Classified_SET
GO

CREATE PROCEDURE [dbo].P_Classified_SET

	@Action				char(1), -- 'I' - insert, 'U' - update, 'D' - delete
	@ClassifiedID		bigint			= 0,
	@Title				nvarchar(60)	= NULL,
	@ShortDescription	nvarchar(300)	= NULL,
	@Text				nvarchar(3600)  = NULL,
	@ZipCode			nchar(5)		= NULL,
	@Phone				nvarchar(20)	= NULL,
	@Email				nvarchar(50)	= NULL,
	@CategoryID			smallint		= NULL,
	@Stands				smallint		= NULL,
	@Active				bit				= NULL,
	@UserID				bigint			= NULL,
	@PicNo				tinyint			= NULL
	
AS

SET NOCOUNT	ON


declare @Created datetime,
		@Res bigint
select	@Res = 0

if @Action = 'I'
BEGIN
	select @Created = GETDATE()
	insert u_Classifieds(
		Title,
		ShortDescription,
		Text,
		ZipCode,
		Phone,
		Email,
		CategoryID,
		Created,
		Stands,
		Active,
		UserID,
		PicNo
	)
	select 
		@Title,
		@ShortDescription,
		@Text,
		@ZipCode,
		@Phone,
		@Email,
		@CategoryID,
		@Created,
		@Stands,
		@Active,
		@UserID,
		@PicNo
		
	select	@Res = MAX(ClassifiedID) from u_Classifieds WITH(NOLOCK) where UserID = @UserID

END

if @Action = 'U'
BEGIN
	update u_Classifieds
	set		Title				= case when @Title is null then Title else @Title end,
			ShortDescription	= case when @ShortDescription is null then ShortDescription else @ShortDescription end,
			Text				= case when @Text is null then Text else @Text end,
			ZipCode				= case when @ZipCode is null then ZipCode else @ZipCode end,
			Phone				= case when @Phone is null then Phone else @Phone end,
			Email				= case when @Email is null then Email else @Email end,
			CategoryID			= case when @CategoryID is null then CategoryID else @CategoryID end,
			Stands				= case when @Stands is null then Stands else @Stands end,
			Active				= case when @Active is null then Active else @Active end,
			PicNo				= case when @PicNo is null then PicNo else @PicNo end
	where	ClassifiedID		= @ClassifiedID
	
	select	@Res = 1
END

if @Action = 'D'
BEGIN
	delete	u_Classifieds
	where	ClassifiedID		= @ClassifiedID
	
	select	@Res = 1
END

if (@@ERROR = 0)
	select @Res
else
	select 0


GO
