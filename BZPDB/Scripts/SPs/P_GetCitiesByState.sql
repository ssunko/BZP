USE [BZP]
GO
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[P_GetCitiesByState]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].P_GetCitiesByState
GO

CREATE PROCEDURE [dbo].P_GetCitiesByState
	@StateID smallint
AS
BEGIN
	SET NOCOUNT	ON
	SELECT	'''' + replace(City,'''','\''') + ''',' AS [text()]
	FROM	u_USCities
	WHERE	StateID = @StateID
	FOR XML PATH ('')
END

GO
-- P_GetCitiesByState 13
