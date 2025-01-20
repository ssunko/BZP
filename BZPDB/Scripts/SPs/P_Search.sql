USE [BZP]
GO
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[P_Search]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].P_Search
GO

CREATE PROCEDURE [dbo].P_Search

	@SearchFor		nvarchar(50),
	@SearchOpt		tinyint			= 1,
	@ZIP			nchar(5)		= null,
	@SearchWithin	smallint		= null,
	@StateID		smallint		= null,
	@City			nvarchar(50)	= null,
	@TypeID			smallint		= null,
	@CategoryID		smallint		= null,
	@TitleOnly		bit				= 1,
	@PictureOnly	bit				= 0,
	@PricedFrom		bigint			= null,
	@PricedTo		bigint			= null,
	@DateFrom		datetime		= null,
	@DateTo			datetime		= null,
	@ExcludeOpt		tinyint			= 1,
	@ExcludeWords	nvarchar(30)	= null
	
AS
BEGIN
	SET NOCOUNT	ON


CREATE TABLE #Classifieds(
	RECID				bigint IDENTITY(1,1),
	ClassifiedID		bigint,
	Title				nvarchar(60),
	ShortDescription	nvarchar(300),
	Text				nvarchar(3600),
	ZipCode				nchar(5),
	Phone				nvarchar(20),
	Email				nvarchar(50),
	CategoryID			smallint,
	Created				datetime,
	PicNo				tinyint
)
-- ZIPs
DECLARE @ZIPs TABLE (
	ZipCode nchar(5)
)
IF(@ZIP IS NOT NULL)
BEGIN
	IF(@SearchWithin != 0)
	INSERT	@ZIPs(ZipCode)
	SELECT  ZipCode
	FROM	dbo.f_GetZIPsWithinRange(@ZIP, @SearchWithin)	
END	
ELSE
BEGIN
	INSERT	@ZIPs(ZipCode)
	SELECT	z.ZipCode
	FROM	u_USZips z
	INNER JOIN u_USCities c ON z.CityID = c.CityID
	WHERE	c.StateID	= @StateID
	AND		c.City		= @City
END
-- Categories
DECLARE @Categories TABLE (
	CategoryID		smallint
)
IF(@TypeID = -1)
	INSERT	@Categories(CategoryID)
	SELECT	CategoryID
	FROM	u_Categories
ELSE IF(@CategoryID = -1)
	INSERT	@Categories(CategoryID)
	SELECT	CategoryID
	FROM	u_Categories
	WHERE	TypeID	= @TypeID
ELSE
	INSERT	@Categories(CategoryID)
	SELECT	@CategoryID

DECLARE @SQL_CONTAINS NVARCHAR(300)
SELECT	@SQL_CONTAINS = dbo.f_BuidContainsSQL(@SearchFor, @ExcludeWords, @SearchOpt, @ExcludeOpt)

INSERT	#Classifieds(
		ClassifiedID,
		Title,
		ShortDescription,
		Text,
		ZipCode,
		Phone,
		Email,
		CategoryID,
		Created,
		PicNo)
SELECT	c.ClassifiedID,
		c.Title,
		c.ShortDescription,
		c.Text,
		c.ZipCode,
		c.Phone,
		c.Email,
		c.CategoryID,
		c.Created,
		c.PicNo
FROM	u_Classifieds c
INNER JOIN @Categories cr  ON c.CategoryID = cr.CategoryID
WHERE	c.Active	= 1
AND		CONTAINS(Title, @SQL_CONTAINS)

	
	select * from #Classifieds


/*

!!! @ZIPs(ZipCode)

@TitleOnly		bit				= 1,
@PictureOnly	bit				= 0,
@PricedFrom		bigint			= null,
@PricedTo		bigint			= null,
@DateFrom		datetime		= null,
@DateTo			datetime		= null,

*/
	
END

GO

/*


	
	P_Search @SearchFor = 'test trest',
	@SearchOpt = 3,
	@ExcludeWords =  'cheapf',
	@ExcludeOpt = 1,
	@ZIP			 = '11223',
	@SearchWithin = 500,
	@TypeID = -1
	

select * from u_Classifieds
sp_fulltext_service 'restart_all_fdhosts'
*/