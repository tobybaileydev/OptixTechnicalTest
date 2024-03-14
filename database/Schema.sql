SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Movie](
	[Release_Date] [datetime2](7) NOT NULL,
	[Title] [nvarchar](2000) NOT NULL,
	[Overview] [nvarchar](max) NOT NULL,
	[Popularity] [decimal](18, 5) NOT NULL,
	[Vote_Count] [int] NOT NULL,
	[Vote_Average] [decimal](18, 5) NOT NULL,
	[Original_Language] [nvarchar](50) NOT NULL,
	[Genre] [nvarchar](2000) NOT NULL,
	[Poster_Url] [nvarchar](2000) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE CLUSTERED INDEX [IX_Movie_Title] ON [dbo].[Movie]
(
	[Title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO