USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_Split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].f_Split
GO

-- =============================================
/*
Declare @ts datetime, @te datetime
select  @ts = GETDATE()

declare @Split TABLE(data NVARCHAR(100))		
insert @Split(data) select data from dbo.f_Split('test values  here ',' ')
delete @Split where rtrim(data) = ''
select * from @Split
		
select  @te = GETDATE()
select DATEDIFF(ms,@ts,@te )
*/
-- =============================================
CREATE FUNCTION f_Split( 
      @input		NVARCHAR(500),
      @delimiter	NVARCHAR(5)
)

RETURNS	@Split TABLE(data NVARCHAR(500))
BEGIN 
	IF(charindex(@delimiter, @input)=0)
	BEGIN
		INSERT	@Split (data) VALUES (@input)
	END
	ELSE
	BEGIN
		DECLARE @tstr NVARCHAR(500)
		SET @tstr = @input
		WHILE(charindex(@delimiter,@tstr,0) > 0)
		BEGIN
			DECLARE	@t NVARCHAR(50)
			SET		@t = Substring(@tstr,0,(charindex(@delimiter,@tstr,0)))
			INSERT	@Split (data) VALUES (@t)
			SET		@tstr = Substring(@tstr,charindex(@delimiter,@tstr,0)+1,Len(@tstr))
			IF(charindex(@delimiter,@tstr,0) <=0)
			BEGIN
				INSERT	@Split (data) VALUES (@tstr)
			END
		END
	END
	 
	RETURN
END