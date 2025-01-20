USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_GetZIPsWithinRange]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[f_GetZIPsWithinRange]
GO

-- =============================================
/*
Declare @ts datetime, @te datetime
select  @ts = GETDATE()
select count(1) ZipCode from dbo.f_GetZIPsWithinRange('11223',500)
select  @te = GETDATE()
select DATEDIFF(ms,@ts,@te )
*/
-- =============================================
CREATE FUNCTION [dbo].[f_GetZIPsWithinRange](
	@ZIP nchar(5),
	@distance int
)
RETURNS @ZIPs TABLE(ZipCode nchar(5))
AS
BEGIN
		DECLARE	@GeogPos geography
		SELECT	@GeogPos	= Position
		FROM	u_USZips
		WHERE	ZipCode		= @ZIP
	
		INSERT	@ZIPs(ZipCode)
		SELECT	ZipCode
		FROM	u_USZips
		WHERE	Position.STDistance(@GeogPos)<=(@distance * 1609.344)
			
	RETURN 
END
GO
/*
		update	@ZIPs
		set		Distance = z.Position.STDistance(@GeogPos) / 1609.344
		from    @ZIPs z
*/