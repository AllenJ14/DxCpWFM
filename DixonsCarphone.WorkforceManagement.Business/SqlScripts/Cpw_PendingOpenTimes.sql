USE [CarphoneWfm]
GO
/****** Object:  Table [dbo].[PendingStoreOpeningTimes]    Script Date: 29/09/2016 15:06:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PendingStoreOpeningTimes](
	[PendingOpenTimeId] [bigint] IDENTITY(1,1) NOT NULL,
	[StoreOpenTimeId] [int] NOT NULL,
	[StoreNumber] [int] NULL,
	[StoreName] [nvarchar](500) NULL,
	[Week_Commence] [int] NULL,
	[SUN_OPEN] [int] NULL,
	[SUN_CLOSE] [int] NULL,
	[MON_OPEN] [int] NULL,
	[MON_CLOSE] [int] NULL,
	[TUE_OPEN] [int] NULL,
	[TUE_CLOSE] [int] NULL,
	[WED_OPEN] [int] NULL,
	[WED_CLOSE] [int] NULL,
	[THU_OPEN] [int] NULL,
	[THU_CLOSE] [int] NULL,
	[FRI_OPEN] [int] NULL,
	[FRI_CLOSE] [int] NULL,
	[SAT_OPEN] [int] NULL,
	[SAT_CLOSE] [int] NULL,
	[DateSubmitted] [datetime] NULL CONSTRAINT [DF_PendingStoreOpeningTimes_DateSubmitted]  DEFAULT (getdate()),
	[IsRejected] [bit] NULL,
	[DateRejected] [datetime] NULL,
	[ReasonRejected] [nvarchar](250) NULL,
	[DateModified] [datetime] NULL CONSTRAINT [DF_PendingStoreOpeningTimes_DateModified]  DEFAULT (getdate()),
 CONSTRAINT [PK_PendingStoreOpeningTimes] PRIMARY KEY CLUSTERED 
(
	[PendingOpenTimeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[PendingStoreOpeningTimes] ON 

GO
INSERT [dbo].[PendingStoreOpeningTimes] ([PendingOpenTimeId], [StoreOpenTimeId], [StoreNumber], [StoreName], [Week_Commence], [SUN_OPEN], [SUN_CLOSE], [MON_OPEN], [MON_CLOSE], [TUE_OPEN], [TUE_CLOSE], [WED_OPEN], [WED_CLOSE], [THU_OPEN], [THU_CLOSE], [FRI_OPEN], [FRI_CLOSE], [SAT_OPEN], [SAT_CLOSE], [DateSubmitted], [IsRejected], [DateRejected], [ReasonRejected], [DateModified]) VALUES (1, 1734, 2833, NULL, 0, 44, 64, 36, 76, 36, 76, 36, 76, 36, 76, 36, 76, 36, 72, CAST(N'2016-09-29 12:44:45.750' AS DateTime), 0, NULL, NULL, CAST(N'2016-09-29 12:44:45.750' AS DateTime))
GO
INSERT [dbo].[PendingStoreOpeningTimes] ([PendingOpenTimeId], [StoreOpenTimeId], [StoreNumber], [StoreName], [Week_Commence], [SUN_OPEN], [SUN_CLOSE], [MON_OPEN], [MON_CLOSE], [TUE_OPEN], [TUE_CLOSE], [WED_OPEN], [WED_CLOSE], [THU_OPEN], [THU_CLOSE], [FRI_OPEN], [FRI_CLOSE], [SAT_OPEN], [SAT_CLOSE], [DateSubmitted], [IsRejected], [DateRejected], [ReasonRejected], [DateModified]) VALUES (3, 1736, 4760, NULL, 0, 52, 72, 36, 72, 36, 72, 36, 72, 36, 72, 36, 72, 36, 72, CAST(N'2016-09-29 12:44:45.750' AS DateTime), 1, CAST(N'2016-09-29 15:03:00.450' AS DateTime), NULL, CAST(N'2016-09-29 15:03:01.187' AS DateTime))
GO
INSERT [dbo].[PendingStoreOpeningTimes] ([PendingOpenTimeId], [StoreOpenTimeId], [StoreNumber], [StoreName], [Week_Commence], [SUN_OPEN], [SUN_CLOSE], [MON_OPEN], [MON_CLOSE], [TUE_OPEN], [TUE_CLOSE], [WED_OPEN], [WED_CLOSE], [THU_OPEN], [THU_CLOSE], [FRI_OPEN], [FRI_CLOSE], [SAT_OPEN], [SAT_CLOSE], [DateSubmitted], [IsRejected], [DateRejected], [ReasonRejected], [DateModified]) VALUES (4, 1737, 110, NULL, 0, 44, 68, 40, 84, 40, 84, 40, 84, 40, 84, 40, 84, 36, 80, CAST(N'2016-09-29 12:44:45.750' AS DateTime), 0, NULL, NULL, CAST(N'2016-09-29 12:44:45.750' AS DateTime))
GO
INSERT [dbo].[PendingStoreOpeningTimes] ([PendingOpenTimeId], [StoreOpenTimeId], [StoreNumber], [StoreName], [Week_Commence], [SUN_OPEN], [SUN_CLOSE], [MON_OPEN], [MON_CLOSE], [TUE_OPEN], [TUE_CLOSE], [WED_OPEN], [WED_CLOSE], [THU_OPEN], [THU_CLOSE], [FRI_OPEN], [FRI_CLOSE], [SAT_OPEN], [SAT_CLOSE], [DateSubmitted], [IsRejected], [DateRejected], [ReasonRejected], [DateModified]) VALUES (5, 2122, 2, N'Marylebone Rd (146)', 0, 40, 64, 30, 52, 40, 80, 40, 80, 40, 80, 40, 80, 36, 80, CAST(N'2016-09-29 12:44:45.750' AS DateTime), 0, NULL, NULL, CAST(N'2016-09-29 12:44:45.750' AS DateTime))
GO
INSERT [dbo].[PendingStoreOpeningTimes] ([PendingOpenTimeId], [StoreOpenTimeId], [StoreNumber], [StoreName], [Week_Commence], [SUN_OPEN], [SUN_CLOSE], [MON_OPEN], [MON_CLOSE], [TUE_OPEN], [TUE_CLOSE], [WED_OPEN], [WED_CLOSE], [THU_OPEN], [THU_CLOSE], [FRI_OPEN], [FRI_CLOSE], [SAT_OPEN], [SAT_CLOSE], [DateSubmitted], [IsRejected], [DateRejected], [ReasonRejected], [DateModified]) VALUES (6, 2486, 2833, NULL, 0, 44, 64, 36, 72, 36, 80, 36, 80, 36, 80, 36, 80, 36, 72, CAST(N'2016-09-29 12:44:45.750' AS DateTime), 0, NULL, NULL, CAST(N'2016-09-29 12:44:45.750' AS DateTime))
GO
INSERT [dbo].[PendingStoreOpeningTimes] ([PendingOpenTimeId], [StoreOpenTimeId], [StoreNumber], [StoreName], [Week_Commence], [SUN_OPEN], [SUN_CLOSE], [MON_OPEN], [MON_CLOSE], [TUE_OPEN], [TUE_CLOSE], [WED_OPEN], [WED_CLOSE], [THU_OPEN], [THU_CLOSE], [FRI_OPEN], [FRI_CLOSE], [SAT_OPEN], [SAT_CLOSE], [DateSubmitted], [IsRejected], [DateRejected], [ReasonRejected], [DateModified]) VALUES (7, 2487, 2833, NULL, 0, 44, 64, 36, 80, 36, 80, 36, 80, 36, 80, 36, 80, 36, 72, CAST(N'2016-09-29 12:44:45.750' AS DateTime), 0, NULL, NULL, CAST(N'2016-09-29 12:44:45.750' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[PendingStoreOpeningTimes] OFF
GO
ALTER TABLE [dbo].[PendingStoreOpeningTimes]  WITH CHECK ADD  CONSTRAINT [FK_PendingStoreOpeningTimes_StoreOpeningTimes] FOREIGN KEY([StoreOpenTimeId])
REFERENCES [dbo].[StoreOpeningTimes] ([StoreOpenTimeId])
GO
ALTER TABLE [dbo].[PendingStoreOpeningTimes] CHECK CONSTRAINT [FK_PendingStoreOpeningTimes_StoreOpeningTimes]
GO
