USE [CT_DREHER]
GO

/****** Object:  Table [dbo].[DST_DISTANCE]    Script Date: 01/13/2013 07:52:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DST_DISTANCE]') AND type in (N'U'))
DROP TABLE [dbo].[DST_DISTANCE]
GO

USE [CT_DREHER]
GO

/****** Object:  Table [dbo].[DST_DISTANCE]    Script Date: 01/13/2013 07:52:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DST_DISTANCE](
	[ID] [dbo].[TY_ID] IDENTITY(1,1) NOT NULL,
	[NOD_ID_FROM] [dbo].[TY_ID] NOT NULL,
	[NOD_ID_TO] [dbo].[TY_ID] NOT NULL,
	[SPP_ID] [dbo].[TY_NVALUE] NULL,
	[DST_DISTANCE] [dbo].[TY_NVALUE] NOT NULL,
	[DST_TIME] [dbo].[TY_NVALUE] NOT NULL,
	[DST_PATH] [text] NULL,
	[DST_EDGES] [varbinary](MAX) NULL,
	[DST_POINTS] [varbinary](MAX) NULL,
 CONSTRAINT [PK_DST_DISTANCE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


