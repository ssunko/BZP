USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_GetZIPsAroundCity]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].f_GetZIPsAroundCity
GO

-- =============================================
-- Author:		SERGEY SUNKO
-- Create date: 02/23/2011
-- Description:	returns all ZIP codes within given distance  
--				to given City, State
-- EXAMPLE OF USE:
/*
Declare @ts datetime, @te datetime
select  @ts = GETDATE()
select  ZipCode, Distance, CityID, StateID from dbo.f_GetZIPsAroundCity('Bronx', 'NY', 250)
select  @te = GETDATE()
select DATEDIFF(ms,@ts,@te )
*/
-- =============================================
CREATE FUNCTION f_GetZIPsAroundCity(
	@City varchar(200),
	@State varchar(2),
	@Radius smallint
)
RETURNS @ZIPs TABLE(
	ZipCode nchar(5),
	Distance int,
	CityID int,
	StateID smallint
)
AS
BEGIN

	DECLARE @Max int,
			@center Geography
	SELECT	@Max = (@Radius * 1609.344)
			
	SELECT	@center = c.Center
	FROM	u_USCities c
	INNER JOIN u_USStates s ON s.StateID = c.StateID
	WHERE	c.City		= @City
	AND		s.StateSN	= @State

	INSERT	@ZIPs(
		ZipCode,
		Distance,
		CityID,
		StateID
	)	
	SELECT	ZipCode,
			Position.STDistance(@center) / 1609.344,
			CityID,
			StateID
	FROM	u_USZips
	WHERE	Position.STDistance(@center) < @Max	

	RETURN 
END
GO