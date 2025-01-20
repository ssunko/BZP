USE [BZP]
GO
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[P_GetAdTypes_XML]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].P_GetAdTypes_XML
GO

CREATE PROCEDURE [dbo].P_GetAdTypes_XML
	
AS
BEGIN
	SET NOCOUNT	ON
	
	SELECT 	Tag					= 1, 
       		Parent				= NULL,
       		[t!1!n]				= Type,
       		[c!2!n]				= NULL,
       		[t!1!id]			= TypeID,
       		[c!2!id]			= NULL
	FROM 	u_CategoryTypes
	WHERE	Active = 1
	UNION ALL
	SELECT 	2, 
       		1,
       		t.Type,
       		c.Category,
       		t.TypeID,
       		c.CategoryID
	FROM 	u_CategoryTypes t
	INNER JOIN	u_Categories c
	ON		t.TypeID = c.TypeID
	WHERE	t.Active = 1
	AND		c.Active = 1
	ORDER BY  [t!1!id],[t!1!n],[c!2!id] 
	FOR XML EXPLICIT
	
END

GO
