USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_PassRecoverQuestions]') AND type in (N'U'))
DROP TABLE [dbo].u_PassRecoverQuestions
GO

CREATE TABLE [dbo].u_PassRecoverQuestions(
	[QuestionID] [smallint] NOT NULL,
	[Question] [nvarchar](60) NOT NULL,
	Active bit default 1 NOT NULL
) ON [PRIMARY]

GO

