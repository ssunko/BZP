USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_BuidContainsSQL]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].f_BuidContainsSQL
GO

-- =============================================
/*
Declare @ts datetime, @te datetime
select  @ts = GETDATE()
select [dbo].f_BuidContainsSQL('test super aws d roma fork  s   jjh ghjgh hhhhhjkl','tre ghfgh6  45 5456 6546456',3,1)
select  @te = GETDATE()
select DATEDIFF(ms,@ts,@te )
*/
-- =============================================
CREATE FUNCTION [dbo].f_BuidContainsSQL(
	@SearchFor		NVARCHAR(50),
	@Exclude		NVARCHAR(30),
	@SearchOpt		TINYINT,
	@ExcludeOpt		TINYINT
)
RETURNS NVARCHAR(300)
AS
BEGIN		
		DECLARE	@Res NVARCHAR(300)	SELECT	@Res = ''
		DECLARE @i		TINYINT,
				@cnt	TINYINT
		-- PROCESS Search words
		DECLARE	@ResSearch NVARCHAR(200)
		
		IF(@SearchOpt=1)
		BEGIN
			SELECT	@ResSearch = '"' + @SearchFor + '"' 
		END
		ELSE
		BEGIN
			SELECT	@ResSearch = ''
			DECLARE @TSearchFor TABLE(i TINYINT IDENTITY(1,1), SearchWord NVARCHAR(50))
			
			INSERT	@TSearchFor(SearchWord)
			SELECT	data
			FROM	dbo.f_Split(@SearchFor,' ')
			WHERE	RTRIM(data) != ''
			AND		LEN(data) > 2
			
			SELECT	@i		= 1,
					@cnt	= COUNT(1) + 1 FROM @TSearchFor
			WHILE(@i < @cnt)
			BEGIN
				IF(@ResSearch != '')
					SELECT	@ResSearch = @ResSearch + CASE WHEN (@SearchOpt = 2) THEN ' & ' ELSE ' | ' END
				SELECT	@ResSearch = @ResSearch + '"' + SearchWord + '"' 
				FROM	@TSearchFor
				WHERE	i = @i				
				SELECT	@i = @i + 1		
			END
		END		
		SELECT	@ResSearch = ' (' + @ResSearch + ')'
		-- PROCESS Exclude words
		DECLARE	@ResExclude NVARCHAR(200)	SELECT	@ResExclude = ''				
		IF(@Exclude IS NOT NULL)
		BEGIN
			IF(@ExcludeOpt=1)
			BEGIN
				SELECT	@ResExclude = ' &! "' + @Exclude + '"' 
			END
			ELSE
			BEGIN
				DECLARE @TExclude TABLE(i TINYINT IDENTITY(1,1), ExcludeWord NVARCHAR(30))
				
				INSERT	@TExclude(ExcludeWord)
				SELECT	data
				FROM	dbo.f_Split(@Exclude,' ')
				WHERE	RTRIM(data) != ''
				AND		LEN(data) > 2
				
				SELECT	@i		= 1,
						@cnt	= COUNT(1) + 1 FROM @TExclude
				
				WHILE(@i < @cnt)
				BEGIN
					SELECT	@ResExclude = @ResExclude + ' &! '
					SELECT	@ResExclude = @ResExclude + '"' + ExcludeWord + '"' 
					FROM	@TExclude
					WHERE	i = @i				
					SELECT	@i = @i + 1		
				END
			END
		END
		SELECT	@Res = @ResSearch + @ResExclude + ' '
					
	RETURN @Res
END
GO