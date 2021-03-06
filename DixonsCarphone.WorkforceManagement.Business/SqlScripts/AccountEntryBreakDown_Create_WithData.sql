USE [CarphoneWfm]
GO
/****** Object:  Table [dbo].[AccountEntryDetailBreakdown]    Script Date: 17/10/2016 12:28:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountEntryDetailBreakdown](
	[AccountEntryDetailBreakDownId] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountEntryDetailId] [bigint] NOT NULL,
	[BreakdownTitle] [nvarchar](50) NOT NULL,
	[BreakdownText] [nvarchar](500) NULL,
	[ActualAmount] [decimal](18, 3) NULL,
	[BudgetAmount] [decimal](18, 3) NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_AccountEntryDetailBreak_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL CONSTRAINT [DF_AccountEntryDetailBreak_DateModified]  DEFAULT (getdate()),
 CONSTRAINT [PK_AccountEntryDetailBreak] PRIMARY KEY CLUSTERED 
(
	[AccountEntryDetailBreakDownId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[AccountEntryDetailBreakdown] ON 

GO
INSERT [dbo].[AccountEntryDetailBreakdown] ([AccountEntryDetailBreakDownId], [AccountEntryDetailId], [BreakdownTitle], [BreakdownText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (1, 14, N'Full Time Employees', N'Full Time Salaries', CAST(1000.000 AS Decimal(18, 3)), CAST(2000.000 AS Decimal(18, 3)), CAST(N'2016-10-16 13:11:15.530' AS DateTime), CAST(N'2016-10-16 13:11:15.530' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetailBreakdown] ([AccountEntryDetailBreakDownId], [AccountEntryDetailId], [BreakdownTitle], [BreakdownText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (2, 14, N'Temp Employees', N'Temp Salaries', CAST(1500.000 AS Decimal(18, 3)), CAST(2000.000 AS Decimal(18, 3)), CAST(N'2016-10-16 13:11:15.533' AS DateTime), CAST(N'2016-10-16 13:11:15.533' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetailBreakdown] ([AccountEntryDetailBreakDownId], [AccountEntryDetailId], [BreakdownTitle], [BreakdownText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (3, 14, N'Contractors', N'Contractors', CAST(1000.000 AS Decimal(18, 3)), CAST(1500.000 AS Decimal(18, 3)), CAST(N'2016-10-16 13:11:15.533' AS DateTime), CAST(N'2016-10-16 13:11:15.533' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[AccountEntryDetailBreakdown] OFF
GO
ALTER TABLE [dbo].[AccountEntryDetailBreakdown]  WITH CHECK ADD  CONSTRAINT [FK_AccountEntryDetailBreak_AccountEntryDetail] FOREIGN KEY([AccountEntryDetailId])
REFERENCES [dbo].[AccountEntryDetail] ([AccountEntryDetailId])
GO
ALTER TABLE [dbo].[AccountEntryDetailBreakdown] CHECK CONSTRAINT [FK_AccountEntryDetailBreak_AccountEntryDetail]
GO
