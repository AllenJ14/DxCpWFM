USE [CarphoneWfm]
GO
/****** Object:  Table [dbo].[ContentDetail]    Script Date: 13/10/2016 18:18:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContentDetail](
	[ContentDetailId] [int] IDENTITY(1,1) NOT NULL,
	[ContentHeaderId] [int] NULL,
	[ContentDetailTitle] [nvarchar](250) NOT NULL,
	[ContentDetailSummary] [nvarchar](500) NULL,
	[ContentDetailText] [nvarchar](max) NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_ContentDetail_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL CONSTRAINT [DF_ContentDetail_DateModified]  DEFAULT (getdate()),
 CONSTRAINT [PK_ContentDetail] PRIMARY KEY CLUSTERED 
(
	[ContentDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ContentHeader]    Script Date: 13/10/2016 18:18:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContentHeader](
	[ContentHeaderId] [int] IDENTITY(1,1) NOT NULL,
	[HeaderTitle] [nvarchar](250) NOT NULL,
	[HeaderDescription] [nvarchar](500) NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_ContentHeader_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL CONSTRAINT [DF_ContentHeader_DateModified]  DEFAULT (getdate()),
	[isActive] [bit] NULL CONSTRAINT [DF_ContentHeader_isActive]  DEFAULT ((1)),
 CONSTRAINT [PK_ContentHeader] PRIMARY KEY CLUSTERED 
(
	[ContentHeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[ContentDetail] ON 

GO
INSERT [dbo].[ContentDetail] ([ContentDetailId], [ContentHeaderId], [ContentDetailTitle], [ContentDetailSummary], [ContentDetailText], [DateCreated], [DateModified]) VALUES (1, 1, N'FAQs', N'This section details Frequently Asked Questions', N'<ul>
	<li>Test 1&nbsp;</li>
	<li>Test 2</li>
</ul>
', CAST(N'2016-10-13 18:04:57.063' AS DateTime), CAST(N'2016-10-13 18:04:48.910' AS DateTime))
GO
INSERT [dbo].[ContentDetail] ([ContentDetailId], [ContentHeaderId], [ContentDetailTitle], [ContentDetailSummary], [ContentDetailText], [DateCreated], [DateModified]) VALUES (2, 1, N'Overviews', N'This section covers overviews', N'<p>hjfkjsskfsbkfbsjbkfkssbfjbksnffbbkfbksckbkc</p>
', CAST(N'2016-10-13 18:11:37.617' AS DateTime), CAST(N'2016-10-13 18:11:33.493' AS DateTime))
GO
INSERT [dbo].[ContentDetail] ([ContentDetailId], [ContentHeaderId], [ContentDetailTitle], [ContentDetailSummary], [ContentDetailText], [DateCreated], [DateModified]) VALUES (3, 1, N'Best Practice', N'This section covers best practice', N'<p>nfnsnclsnclklcnskslncnancajnca</p>
', CAST(N'2016-10-13 18:17:08.697' AS DateTime), CAST(N'2016-10-13 18:17:08.643' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[ContentDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[ContentHeader] ON 

GO
INSERT [dbo].[ContentHeader] ([ContentHeaderId], [HeaderTitle], [HeaderDescription], [DateCreated], [DateModified], [isActive]) VALUES (1, N'Best Practice', N'This section is for Best Practice Content', CAST(N'2016-10-13 15:40:47.403' AS DateTime), CAST(N'2016-10-13 15:40:47.403' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[ContentHeader] OFF
GO
ALTER TABLE [dbo].[ContentDetail]  WITH CHECK ADD  CONSTRAINT [FK_ContentDetail_ContentHeader] FOREIGN KEY([ContentHeaderId])
REFERENCES [dbo].[ContentHeader] ([ContentHeaderId])
GO
ALTER TABLE [dbo].[ContentDetail] CHECK CONSTRAINT [FK_ContentDetail_ContentHeader]
GO
