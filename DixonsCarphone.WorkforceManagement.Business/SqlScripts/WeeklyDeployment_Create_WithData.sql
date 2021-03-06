USE [CarphoneWfm]
GO
/****** Object:  Table [dbo].[WeeklyDeployment]    Script Date: 19/10/2016 19:15:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeeklyDeployment](
	[WeeklyDeploymentId] [int] IDENTITY(1,1) NOT NULL,
	[Year] [float] NULL,
	[Week] [float] NULL,
	[BranchNumber] [float] NULL,
	[SundayReq] [float] NULL,
	[SundaySpend] [float] NULL,
	[MondayReq] [float] NULL,
	[MondaySpend] [float] NULL,
	[TuesdayReq] [float] NULL,
	[TuesdaySpend] [float] NULL,
	[WednesdayReq] [float] NULL,
	[WednesdaySpend] [float] NULL,
	[ThursdayReq] [float] NULL,
	[ThursdaySpend] [float] NULL,
	[FridayReq] [float] NULL,
	[FridaySpend] [float] NULL,
	[SaturdayReq] [float] NULL,
	[SaturdaySpend] [float] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_WeeklyDeployment] PRIMARY KEY CLUSTERED 
(
	[WeeklyDeploymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[WeeklyDeployment] ON 

GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (1, 2016, 29, 2, 0.11, 0.12, 0.13, 0.12000000000000001, 0.17, 0.18000000000000002, 0.19, 0.18, 0.13, 0.14, 0.17, 0.16, 0.12, 0.13, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (2, 2017, 24, 4, 0, 0, 0.16, 0.15, 0.17, 0.18000000000000002, 0.21, 0.19999999999999998, 0.18, 0.19, 0.17, 0.16, 0.1, 0.11, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (3, 2017, 24, 11, 0.14, 0.15000000000000002, 0.14, 0.13, 0.17, 0.18000000000000002, 0.17, 0.16, 0.15, 0.16, 0.12, 0.11, 0.11, 0.12, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (4, 2017, 24, 33, 0.09, 0.099999999999999992, 0.17, 0.16, 0.13, 0.14, 0.21, 0.19999999999999998, 0.16, 0.17, 0.17, 0.16, 0.08, 0.09, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (5, 2017, 24, 34, 0.08, 0.09, 0.15, 0.13999999999999999, 0.17, 0.18000000000000002, 0.2, 0.19, 0.13, 0.14, 0.12, 0.11, 0.15, 0.16, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (6, 2017, 24, 135, 0, 0, 0.13, 0.12000000000000001, 0.17, 0.18000000000000002, 0.13, 0.12000000000000001, 0.19, 0.2, 0.19, 0.18, 0.18, 0.19, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (7, 2017, 24, 426, 0.1, 0.11, 0.19, 0.18, 0.21, 0.22, 0.1, 0.090000000000000011, 0.15, 0.16, 0.18, 0.16999999999999998, 0.06, 0.069999999999999993, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (8, 2017, 24, 796, 0, 0, 0.2, 0.19, 0.2, 0.21000000000000002, 0.21, 0.19999999999999998, 0.14, 0.15000000000000002, 0.16, 0.15, 0.08, 0.09, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (9, 2017, 24, 1198, 0, 0, 0.13, 0.12000000000000001, 0.16, 0.17, 0.19, 0.18, 0.13, 0.14, 0.23, 0.22, 0.16, 0.17, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (10, 2017, 24, 1285, 0.06, 0.069999999999999993, 0.17, 0.16, 0.16, 0.17, 0.21, 0.19999999999999998, 0.16, 0.17, 0.12, 0.11, 0.11, 0.12, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (11, 2017, 24, 1346, 0, 0, 0.19, 0.18, 0.19, 0.2, 0.2, 0.19, 0.17, 0.18000000000000002, 0.17, 0.16, 0.08, 0.09, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (12, 2017, 24, 1347, 0.13, 0.14, 0.16, 0.15, 0.11, 0.12, 0.16, 0.15, 0.12, 0.13, 0.14, 0.13, 0.17, 0.18000000000000002, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (13, 2017, 24, 1473, 0.08, 0.09, 0.13, 0.12000000000000001, 0.12, 0.13, 0.16, 0.15, 0.2, 0.21000000000000002, 0.17, 0.16, 0.13, 0.14, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (14, 2017, 24, 1679, 0.08, 0.09, 0.14, 0.13, 0.14, 0.15000000000000002, 0.18, 0.16999999999999998, 0.16, 0.17, 0.13, 0.12000000000000001, 0.17, 0.18000000000000002, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (15, 2017, 24, 1834, 0.07, 0.08, 0.17, 0.16, 0.15, 0.16, 0.18, 0.16999999999999998, 0.15, 0.16, 0.14, 0.13, 0.14, 0.15000000000000002, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (16, 2017, 24, 1916, 0, 0, 0.23, 0.22, 0.17, 0.18000000000000002, 0.15, 0.13999999999999999, 0.18, 0.19, 0.17, 0.16, 0.1, 0.11, NULL, NULL)
GO
INSERT [dbo].[WeeklyDeployment] ([WeeklyDeploymentId], [Year], [Week], [BranchNumber], [SundayReq], [SundaySpend], [MondayReq], [MondaySpend], [TuesdayReq], [TuesdaySpend], [WednesdayReq], [WednesdaySpend], [ThursdayReq], [ThursdaySpend], [FridayReq], [FridaySpend], [SaturdayReq], [SaturdaySpend], [DateCreated], [DateModified]) VALUES (17, 2017, 24, 2040, 0.13, 0.14, 0.12, 0.11, 0.14, 0.15000000000000002, 0.11, 0.1, 0.16, 0.17, 0.17, 0.16, 0.18, 0.19, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[WeeklyDeployment] OFF
GO
