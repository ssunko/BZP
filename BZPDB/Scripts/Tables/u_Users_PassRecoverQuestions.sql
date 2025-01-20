USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_Users_PassRecoverQuestions]') AND type in (N'U'))
DROP TABLE [dbo].u_Users_PassRecoverQuestions
GO

CREATE TABLE [dbo].u_Users_PassRecoverQuestions(
	UserID		bigint NOT NULL,
	QuestionID  smallint  NOT NULL,
	Answer		nvarchar(100)  NOT NULL
) ON [PRIMARY]

GO

