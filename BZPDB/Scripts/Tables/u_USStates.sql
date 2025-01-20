USE [BZP]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_USStates]') AND type in (N'U'))
DROP TABLE [dbo].[u_USStates]
GO

CREATE TABLE [dbo].[u_USStates](
	[StateID] [smallint] identity NOT NULL,
	[StateSN] [nchar](2) NOT NULL,
	[StateLN] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO

