USE [CarphoneWfm]
GO
/****** Object:  Table [dbo].[AccountEntryDetail]    Script Date: 10/10/2016 15:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountEntryDetail](
	[AccountEntryDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountEntryHeaderId] [bigint] NOT NULL,
	[AccountEntrySubTypeId] [int] NULL,
	[DetailTitle] [nvarchar](50) NOT NULL,
	[DetailText] [nvarchar](500) NULL,
	[ActualAmount] [decimal](18, 3) NULL,
	[BudgetAmount] [decimal](18, 3) NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_AccountEntryDetail_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL CONSTRAINT [DF_AccountEntryDetail_DateModified]  DEFAULT (getdate()),
 CONSTRAINT [PK_AccountEntryDetail] PRIMARY KEY CLUSTERED 
(
	[AccountEntryDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AccountEntryHeader]    Script Date: 10/10/2016 15:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountEntryHeader](
	[AccountEntryHeaderId] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountEntryHeaderText] [nvarchar](500) NULL,
	[PeriodYear] [nvarchar](50) NULL,
	[PeriodMonth] [nvarchar](50) NULL,
	[StoreNumber] [int] NULL,
	[ManagerName] [nvarchar](500) NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_AccountEntryHeader_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL CONSTRAINT [DF_AccountEntryHeader_DateModified]  DEFAULT (getdate()),
 CONSTRAINT [PK_AccountEntryHeader] PRIMARY KEY CLUSTERED 
(
	[AccountEntryHeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AccountEntryType]    Script Date: 10/10/2016 15:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountEntryType](
	[AccountEntryTypeId] [smallint] IDENTITY(1,1) NOT NULL,
	[AccountEntryName] [nvarchar](50) NOT NULL,
	[AccountEntryDescription] [nvarchar](250) NULL,
	[DateModified] [datetime] NULL CONSTRAINT [DF_AccountEntryType_DateModified]  DEFAULT (getdate()),
	[DateCreated] [datetime] NULL CONSTRAINT [DF_AccountEntryType_DateCreated]  DEFAULT (getdate()),
 CONSTRAINT [PK_AccountEntryType] PRIMARY KEY CLUSTERED 
(
	[AccountEntryTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AccountEntySubType]    Script Date: 10/10/2016 15:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountEntySubType](
	[AccountEntySubTypeId] [int] IDENTITY(1,1) NOT NULL,
	[AccountEntryTypeId] [smallint] NOT NULL,
	[AccountEntySubTypeName] [nvarchar](100) NOT NULL,
	[AccountEntySubTypeSummary] [nvarchar](250) NULL,
	[AccountEntySubTypeDescription] [nvarchar](1000) NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_AccountEntySubType_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL CONSTRAINT [DF_AccountEntySubType_DateModified]  DEFAULT (getdate()),
 CONSTRAINT [PK_AccountEntySubType] PRIMARY KEY CLUSTERED 
(
	[AccountEntySubTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[AccountEntryDetail] ON 

GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (1, 1, 1, N'Controllable Gross Margin', NULL, CAST(44799.000 AS Decimal(18, 3)), CAST(53942.000 AS Decimal(18, 3)), CAST(N'2016-10-04 15:01:12.737' AS DateTime), CAST(N'2016-10-04 15:01:12.737' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (2, 1, 1, N'New post pay margin', NULL, CAST(20000.000 AS Decimal(18, 3)), CAST(23798.000 AS Decimal(18, 3)), CAST(N'2016-10-04 15:01:12.740' AS DateTime), CAST(N'2016-10-04 15:01:12.740' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (3, 1, 1, N'Post pay upgrade margin', NULL, CAST(10000.000 AS Decimal(18, 3)), CAST(12345.000 AS Decimal(18, 3)), CAST(N'2016-10-04 15:01:12.740' AS DateTime), CAST(N'2016-10-04 15:01:12.740' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (4, 1, 1, N'New pre pay margin', NULL, CAST(7000.000 AS Decimal(18, 3)), CAST(9600.000 AS Decimal(18, 3)), CAST(N'2016-10-04 15:01:12.740' AS DateTime), CAST(N'2016-10-04 15:01:12.740' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (5, 1, 1, N'Pre pay upgrade margin', NULL, CAST(4678.000 AS Decimal(18, 3)), CAST(3800.000 AS Decimal(18, 3)), CAST(N'2016-10-04 15:01:12.740' AS DateTime), CAST(N'2016-10-04 15:01:12.740' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (6, 1, 1, N'Sim only margin', NULL, CAST(3000.000 AS Decimal(18, 3)), CAST(2999.000 AS Decimal(18, 3)), CAST(N'2016-10-04 15:01:12.743' AS DateTime), CAST(N'2016-10-04 15:01:12.743' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (7, 1, 1, N'Accessory margin', NULL, CAST(354.000 AS Decimal(18, 3)), CAST(700.000 AS Decimal(18, 3)), CAST(N'2016-10-04 15:01:12.743' AS DateTime), CAST(N'2016-10-04 15:01:12.743' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (8, 1, 1, N'Multiplay margin', NULL, CAST(457.000 AS Decimal(18, 3)), CAST(700.000 AS Decimal(18, 3)), CAST(N'2016-10-04 15:01:12.743' AS DateTime), CAST(N'2016-10-04 15:01:12.743' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (9, 1, 5, N'Customer care voucher deductions', NULL, CAST(260.000 AS Decimal(18, 3)), NULL, CAST(N'2016-10-04 15:01:12.743' AS DateTime), CAST(N'2016-10-04 15:01:12.743' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (10, 1, 5, N'Customer incentive deductions', NULL, CAST(430.000 AS Decimal(18, 3)), NULL, CAST(N'2016-10-04 15:01:12.743' AS DateTime), CAST(N'2016-10-04 15:01:12.743' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (11, 1, 2, N'Basic Salaries	X Charge Detail', NULL, CAST(2734.000 AS Decimal(18, 3)), CAST(2675.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:25:54.717' AS DateTime), CAST(N'2016-10-04 17:25:54.717' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (12, 1, 2, N'Overtime', NULL, CAST(158.000 AS Decimal(18, 3)), NULL, CAST(N'2016-10-04 17:25:54.720' AS DateTime), CAST(N'2016-10-04 17:25:54.720' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (13, 1, 2, N'Seasonal Pay', NULL, NULL, NULL, CAST(N'2016-10-04 17:25:54.720' AS DateTime), CAST(N'2016-10-04 17:25:54.720' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (14, 1, 2, N'Overpayments', NULL, CAST(345.000 AS Decimal(18, 3)), NULL, CAST(N'2016-10-04 17:25:54.720' AS DateTime), CAST(N'2016-10-04 17:25:54.720' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (15, 1, 3, N'Stock Loss', NULL, CAST(750.000 AS Decimal(18, 3)), CAST(0.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:33:21.443' AS DateTime), CAST(N'2016-10-04 17:33:21.443' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (16, 1, 3, N'Trade up Non Compliance', NULL, CAST(250.000 AS Decimal(18, 3)), CAST(0.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:33:21.443' AS DateTime), CAST(N'2016-10-04 17:33:21.443' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (17, 1, 3, N'FMIP non compliance', NULL, CAST(56.000 AS Decimal(18, 3)), CAST(0.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:33:21.443' AS DateTime), CAST(N'2016-10-04 17:33:21.443' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (18, 1, 3, N'Stationery', NULL, CAST(22.750 AS Decimal(18, 3)), CAST(40.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:33:21.443' AS DateTime), CAST(N'2016-10-04 17:33:21.443' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (19, 1, 3, N'Acrylics', NULL, CAST(185.000 AS Decimal(18, 3)), CAST(150.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:33:21.443' AS DateTime), CAST(N'2016-10-04 17:33:21.443' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (20, 1, 3, N'Cash/Credit/Banking losses', NULL, CAST(0.000 AS Decimal(18, 3)), CAST(0.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:33:21.443' AS DateTime), CAST(N'2016-10-04 17:33:21.443' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (21, 1, 3, N'Electricity', NULL, CAST(1652.000 AS Decimal(18, 3)), CAST(1500.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:33:21.443' AS DateTime), CAST(N'2016-10-04 17:33:21.443' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (22, 1, 3, N'Petty cash Expenses', NULL, CAST(1015.910 AS Decimal(18, 3)), CAST(100.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:33:21.447' AS DateTime), CAST(N'2016-10-04 17:33:21.447' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (23, 1, 6, N'Water', NULL, CAST(185.000 AS Decimal(18, 3)), CAST(267.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:43:26.850' AS DateTime), CAST(N'2016-10-04 17:43:26.850' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (24, 1, 6, N'Gas', NULL, CAST(118.000 AS Decimal(18, 3)), CAST(250.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:43:26.853' AS DateTime), CAST(N'2016-10-04 17:43:26.853' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (25, 1, 6, N'Security equipment', NULL, NULL, NULL, CAST(N'2016-10-04 17:43:26.853' AS DateTime), CAST(N'2016-10-04 17:43:26.853' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (26, 1, 6, N'Security Guards', NULL, CAST(500.000 AS Decimal(18, 3)), CAST(500.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:43:26.853' AS DateTime), CAST(N'2016-10-04 17:43:26.853' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (27, 1, 6, N'Cash In Transit', NULL, CAST(58.000 AS Decimal(18, 3)), CAST(58.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:43:26.853' AS DateTime), CAST(N'2016-10-04 17:43:26.853' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (28, 1, 7, N'Maintenance Complete (ipsos/videra)', NULL, CAST(387.000 AS Decimal(18, 3)), CAST(500.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:52:56.253' AS DateTime), CAST(N'2016-10-04 17:52:56.253' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (29, 1, 9, N'Refuse', NULL, CAST(128.000 AS Decimal(18, 3)), CAST(180.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:52:56.253' AS DateTime), CAST(N'2016-10-04 17:52:56.253' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (30, 1, 9, N'Cleaning', NULL, CAST(30.000 AS Decimal(18, 3)), CAST(60.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:52:56.253' AS DateTime), CAST(N'2016-10-04 17:52:56.253' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (31, 1, 10, N'Uniforms', NULL, NULL, CAST(50.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:52:56.253' AS DateTime), CAST(N'2016-10-04 17:52:56.253' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (32, 1, 10, N'Travel & Accomodation', NULL, CAST(152.260 AS Decimal(18, 3)), CAST(250.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:52:56.253' AS DateTime), CAST(N'2016-10-04 17:52:56.253' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (33, 1, 10, N'Vehicle Hire', NULL, NULL, NULL, CAST(N'2016-10-04 17:52:56.253' AS DateTime), CAST(N'2016-10-04 17:52:56.253' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (34, 1, 10, N'Payroll Expenses', NULL, NULL, NULL, CAST(N'2016-10-04 17:52:56.253' AS DateTime), CAST(N'2016-10-04 17:52:56.253' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (35, 1, 10, N'Printing Usage', NULL, CAST(123.000 AS Decimal(18, 3)), CAST(214.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:52:56.253' AS DateTime), CAST(N'2016-10-04 17:52:56.253' AS DateTime))
GO
INSERT [dbo].[AccountEntryDetail] ([AccountEntryDetailId], [AccountEntryHeaderId], [AccountEntrySubTypeId], [DetailTitle], [DetailText], [ActualAmount], [BudgetAmount], [DateCreated], [DateModified]) VALUES (36, 1, 10, N'Printing Fixed', NULL, CAST(48.000 AS Decimal(18, 3)), CAST(48.000 AS Decimal(18, 3)), CAST(N'2016-10-04 17:52:56.257' AS DateTime), CAST(N'2016-10-04 17:52:56.257' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[AccountEntryDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[AccountEntryHeader] ON 

GO
INSERT [dbo].[AccountEntryHeader] ([AccountEntryHeaderId], [AccountEntryHeaderText], [PeriodYear], [PeriodMonth], [StoreNumber], [ManagerName], [DateCreated], [DateModified]) VALUES (1, N'2016 - 2017 P&L Data', N'2016', N'10', 2, N'JB', CAST(N'2016-10-04 14:06:11.003' AS DateTime), CAST(N'2016-10-04 14:06:11.003' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[AccountEntryHeader] OFF
GO
SET IDENTITY_INSERT [dbo].[AccountEntryType] ON 

GO
INSERT [dbo].[AccountEntryType] ([AccountEntryTypeId], [AccountEntryName], [AccountEntryDescription], [DateModified], [DateCreated]) VALUES (1, N'Asset', N'Assets', CAST(N'2016-10-03 14:29:10.210' AS DateTime), CAST(N'2016-10-03 14:29:10.210' AS DateTime))
GO
INSERT [dbo].[AccountEntryType] ([AccountEntryTypeId], [AccountEntryName], [AccountEntryDescription], [DateModified], [DateCreated]) VALUES (2, N'Liability', N'Liabilities', CAST(N'2016-10-03 14:30:06.990' AS DateTime), CAST(N'2016-10-03 14:30:06.990' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[AccountEntryType] OFF
GO
SET IDENTITY_INSERT [dbo].[AccountEntySubType] ON 

GO
INSERT [dbo].[AccountEntySubType] ([AccountEntySubTypeId], [AccountEntryTypeId], [AccountEntySubTypeName], [AccountEntySubTypeSummary], [AccountEntySubTypeDescription], [DateCreated], [DateModified]) VALUES (1, 1, N'Margin', N'Margin', NULL, CAST(N'2016-10-04 13:57:33.080' AS DateTime), CAST(N'2016-10-04 13:57:33.080' AS DateTime))
GO
INSERT [dbo].[AccountEntySubType] ([AccountEntySubTypeId], [AccountEntryTypeId], [AccountEntySubTypeName], [AccountEntySubTypeSummary], [AccountEntySubTypeDescription], [DateCreated], [DateModified]) VALUES (2, 2, N'Payroll', N'Payroll Expenses', NULL, CAST(N'2016-10-04 13:57:59.910' AS DateTime), CAST(N'2016-10-04 13:57:59.910' AS DateTime))
GO
INSERT [dbo].[AccountEntySubType] ([AccountEntySubTypeId], [AccountEntryTypeId], [AccountEntySubTypeName], [AccountEntySubTypeSummary], [AccountEntySubTypeDescription], [DateCreated], [DateModified]) VALUES (3, 2, N'Controlable Costs', N'Controlable Costs', NULL, CAST(N'2016-10-04 13:59:16.803' AS DateTime), CAST(N'2016-10-04 13:59:16.803' AS DateTime))
GO
INSERT [dbo].[AccountEntySubType] ([AccountEntySubTypeId], [AccountEntryTypeId], [AccountEntySubTypeName], [AccountEntySubTypeSummary], [AccountEntySubTypeDescription], [DateCreated], [DateModified]) VALUES (4, 2, N'Other Costs To Sales', N'Other Costs To Sales', NULL, CAST(N'2016-10-04 14:00:25.740' AS DateTime), CAST(N'2016-10-04 14:00:25.740' AS DateTime))
GO
INSERT [dbo].[AccountEntySubType] ([AccountEntySubTypeId], [AccountEntryTypeId], [AccountEntySubTypeName], [AccountEntySubTypeSummary], [AccountEntySubTypeDescription], [DateCreated], [DateModified]) VALUES (5, 2, N'Deductible Margin', N'Margin', NULL, CAST(N'2016-10-04 14:09:04.237' AS DateTime), CAST(N'2016-10-04 14:09:04.237' AS DateTime))
GO
INSERT [dbo].[AccountEntySubType] ([AccountEntySubTypeId], [AccountEntryTypeId], [AccountEntySubTypeName], [AccountEntySubTypeSummary], [AccountEntySubTypeDescription], [DateCreated], [DateModified]) VALUES (6, 2, N'Utilities', N'Utilities', NULL, CAST(N'2016-10-04 17:34:29.790' AS DateTime), CAST(N'2016-10-04 17:34:29.790' AS DateTime))
GO
INSERT [dbo].[AccountEntySubType] ([AccountEntySubTypeId], [AccountEntryTypeId], [AccountEntySubTypeName], [AccountEntySubTypeSummary], [AccountEntySubTypeDescription], [DateCreated], [DateModified]) VALUES (7, 2, N'Store Systems', N'Store Systems', NULL, CAST(N'2016-10-04 17:38:50.247' AS DateTime), CAST(N'2016-10-04 17:38:50.247' AS DateTime))
GO
INSERT [dbo].[AccountEntySubType] ([AccountEntySubTypeId], [AccountEntryTypeId], [AccountEntySubTypeName], [AccountEntySubTypeSummary], [AccountEntySubTypeDescription], [DateCreated], [DateModified]) VALUES (9, 2, N'Store Maintenance', N'Store Maintenance', NULL, CAST(N'2016-10-04 17:39:23.713' AS DateTime), CAST(N'2016-10-04 17:39:23.713' AS DateTime))
GO
INSERT [dbo].[AccountEntySubType] ([AccountEntySubTypeId], [AccountEntryTypeId], [AccountEntySubTypeName], [AccountEntySubTypeSummary], [AccountEntySubTypeDescription], [DateCreated], [DateModified]) VALUES (10, 2, N'Miscellaneous Costs', N'Miscellaneous Costs', NULL, CAST(N'2016-10-04 17:46:46.430' AS DateTime), CAST(N'2016-10-04 17:46:46.430' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[AccountEntySubType] OFF
GO
ALTER TABLE [dbo].[AccountEntryDetail]  WITH CHECK ADD  CONSTRAINT [FK_AccountEntryDetail_AccountEntryHeader] FOREIGN KEY([AccountEntryHeaderId])
REFERENCES [dbo].[AccountEntryHeader] ([AccountEntryHeaderId])
GO
ALTER TABLE [dbo].[AccountEntryDetail] CHECK CONSTRAINT [FK_AccountEntryDetail_AccountEntryHeader]
GO
ALTER TABLE [dbo].[AccountEntryDetail]  WITH CHECK ADD  CONSTRAINT [FK_AccountEntryDetail_AccountEntySubType] FOREIGN KEY([AccountEntrySubTypeId])
REFERENCES [dbo].[AccountEntySubType] ([AccountEntySubTypeId])
GO
ALTER TABLE [dbo].[AccountEntryDetail] CHECK CONSTRAINT [FK_AccountEntryDetail_AccountEntySubType]
GO
ALTER TABLE [dbo].[AccountEntySubType]  WITH CHECK ADD  CONSTRAINT [FK_AccountEntySubType_AccountEntryType] FOREIGN KEY([AccountEntryTypeId])
REFERENCES [dbo].[AccountEntryType] ([AccountEntryTypeId])
GO
ALTER TABLE [dbo].[AccountEntySubType] CHECK CONSTRAINT [FK_AccountEntySubType_AccountEntryType]
GO
