USE [master]
GO
/****** Object:  Database [CarphoneWfm]    Script Date: 20/09/2016 10:19:24 ******/
CREATE DATABASE [CarphoneWfm]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CarphoneWfm', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\DATA\CarphoneWfm.mdf' , SIZE = 22528KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CarphoneWfm_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\DATA\CarphoneWfm_log.ldf' , SIZE = 43264KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [CarphoneWfm] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CarphoneWfm].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CarphoneWfm] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CarphoneWfm] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CarphoneWfm] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CarphoneWfm] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CarphoneWfm] SET ARITHABORT OFF 
GO
ALTER DATABASE [CarphoneWfm] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CarphoneWfm] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CarphoneWfm] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CarphoneWfm] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CarphoneWfm] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CarphoneWfm] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CarphoneWfm] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CarphoneWfm] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CarphoneWfm] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CarphoneWfm] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CarphoneWfm] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CarphoneWfm] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CarphoneWfm] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CarphoneWfm] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CarphoneWfm] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CarphoneWfm] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CarphoneWfm] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CarphoneWfm] SET RECOVERY FULL 
GO
ALTER DATABASE [CarphoneWfm] SET  MULTI_USER 
GO
ALTER DATABASE [CarphoneWfm] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CarphoneWfm] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CarphoneWfm] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CarphoneWfm] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [CarphoneWfm] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CarphoneWfm', N'ON'
GO
USE [CarphoneWfm]
GO
/****** Object:  User [carphone_web]    Script Date: 20/09/2016 10:19:24 ******/
CREATE USER [carphone_web] FOR LOGIN [carphone_web] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [carphone_web]
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 20/09/2016 10:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[Activityid] [bigint] IDENTITY(1,1) NOT NULL,
	[ActivityName] [nvarchar](50) NULL,
	[Summary] [nvarchar](500) NULL,
	[Detail] [nvarchar](max) NULL,
	[ActivityDate] [datetime] NULL,
	[StartTime] [nvarchar](10) NULL,
	[EndTime] [nvarchar](10) NULL,
	[ActivityTypeId] [int] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_Activity_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL CONSTRAINT [DF_Activity_DateModified]  DEFAULT (getdate()),
 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
(
	[Activityid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ActivityType]    Script Date: 20/09/2016 10:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityType](
	[ActivityTypeId] [int] IDENTITY(1,1) NOT NULL,
	[ActivityType] [nvarchar](50) NOT NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_ActivityType_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL CONSTRAINT [DF_ActivityType_DateModified]  DEFAULT (getdate()),
 CONSTRAINT [PK_ActivityType] PRIMARY KEY CLUSTERED 
(
	[ActivityTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BestPracticeCategoryTypes]    Script Date: 20/09/2016 10:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BestPracticeCategoryTypes](
	[CategoryId] [smallint] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[CategoryDescription] [nvarchar](250) NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_BestPracticeCategoryTypes_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL CONSTRAINT [DF_BestPracticeCategoryTypes_DateModified]  DEFAULT (getdate()),
 CONSTRAINT [PK_BestPracticeCategoryTypes] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BestPracticeContent]    Script Date: 20/09/2016 10:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BestPracticeContent](
	[BestPracticeContentId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [smallint] NOT NULL,
	[SubCategory] [nvarchar](250) NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Summary] [nvarchar](500) NULL,
	[BestPracticeText] [nvarchar](max) NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_BestPracticeContent] PRIMARY KEY CLUSTERED 
(
	[BestPracticeContentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DailyFootfall]    Script Date: 20/09/2016 10:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DailyFootfall](
	[DailyFootfallId] [int] IDENTITY(1,1) NOT NULL,
	[Branch] [int] NULL,
	[ProfileType] [nvarchar](255) NULL,
	[Day] [nvarchar](255) NULL,
	[0700] [float] NULL,
	[0800] [float] NULL,
	[0900] [float] NULL,
	[1000] [float] NULL,
	[1100] [float] NULL,
	[1200] [float] NULL,
	[1300] [float] NULL,
	[1400] [float] NULL,
	[1500] [float] NULL,
	[1600] [float] NULL,
	[1700] [float] NULL,
	[1800] [float] NULL,
	[1900] [float] NULL,
	[2000] [float] NULL,
	[2100] [float] NULL,
	[2200] [float] NULL,
 CONSTRAINT [PK_DailyFootfall] PRIMARY KEY CLUSTERED 
(
	[DailyFootfallId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DashBoardData]    Script Date: 20/09/2016 10:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DashBoardData](
	[DashBoardDataId] [int] IDENTITY(1,1) NOT NULL,
	[Year] [nvarchar](255) NULL,
	[Quarter] [float] NULL,
	[Period] [float] NULL,
	[WeekNumber] [float] NULL,
	[BranchNumber] [float] NULL,
	[UkBranchNumber] [nvarchar](255) NULL,
	[StoreName] [nvarchar](255) NULL,
	[Division] [nvarchar](255) NULL,
	[Region] [float] NULL,
	[RegionName] [nvarchar](255) NULL,
	[TotalHeadCount] [float] NULL,
	[TimeCardsCompleted] [float] NULL,
	[ZeroHour] [bit] NULL,
	[ContractHours] [float] NULL,
	[FTContractHours] [float] NULL,
	[PTContractHours] [float] NULL,
	[OriginalTarget] [float] NULL,
	[FinalTarget] [float] NULL,
	[HolidayTaken] [float] NULL,
	[LTS] [float] NULL,
	[FTLeakage] [float] NULL,
	[LeakageCount] [float] NULL,
	[AllApprovedHours] [float] NULL,
	[PayrollCorrections] [float] NULL,
	[SOH] [float] NULL,
	[OverTargetFlag] [bit] NULL,
	[OverContractFlag] [bit] NULL,
	[ComplianceScore] [float] NULL,
	[SOHUtilization] [float] NULL,
	[UtilizationZeroFlag] [bit] NULL,
	[GSContract] [float] NULL,
	[GSTarget] [float] NULL,
	[GSHolidayTaken] [float] NULL,
	[GSFTLeakage] [float] NULL,
	[GSAllApprovedHours] [float] NULL,
	[GSSOHSpend] [float] NULL,
	[GSOverTargetFlag] [float] NULL,
	[GSOverContractFlag] [float] NULL,
	[StoreFlag] [nvarchar](255) NULL,
	[StoreCount] [float] NULL,
	[FTHeadcountSAS] [float] NULL,
	[FTHeadcountSIS] [float] NULL,
	[ContractBaseTarget] [float] NULL,
	[PaidForHoursNotWorkedCompliance] [float] NULL,
	[SATSOH] [nvarchar](255) NULL,
	[SATTarget] [nvarchar](255) NULL,
	[ServiceLVSOH] [nvarchar](255) NULL,
	[ServiceLV1Target] [nvarchar](255) NULL,
	[ServiceLV2SOH] [nvarchar](255) NULL,
	[ServiceLV2Target] [nvarchar](255) NULL,
	[SATContractHours] [nvarchar](255) NULL,
	[ServiceLV1ContractHours] [nvarchar](255) NULL,
	[ServiceLV2ContractHours] [nvarchar](255) NULL,
	[25PercOver] [float] NULL,
	[25PercUnder] [float] NULL,
	[PlusMinus10Perc] [float] NULL,
	[Awol] [float] NULL,
	[Suspension] [float] NULL,
	[Sickness] [float] NULL,
 CONSTRAINT [PK_DashBoardData] PRIMARY KEY CLUSTERED 
(
	[DashBoardDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HrFeed]    Script Date: 20/09/2016 10:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HrFeed](
	[HR_FEED_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[STORE_NUM] [int] NULL,
	[EMP_NUM] [bigint] NULL,
	[FORENAME] [nvarchar](255) NULL,
	[SURNAME] [nvarchar](255) NULL,
	[CONTRACT_DAYS] [nvarchar](255) NULL,
	[CONTRACT_HOURS] [float] NULL,
	[SalaryType] [nvarchar](255) NULL,
	[HourlyBasic] [nvarchar](255) NULL,
	[AnnualBasic] [nvarchar](255) NULL,
	[ROLE] [nvarchar](255) NULL,
	[DOJ] [datetime] NULL,
	[DOL] [nvarchar](255) NULL,
	[DATE_IN_CURRENT_ROLE] [datetime] NULL,
	[DATE_IN_CURRENT_DEPARTMENT] [datetime] NULL,
	[StaffTypeId] [int] NULL,
	[EMP_CLASS] [nvarchar](255) NULL,
	[DATE_OF_BIRTH] [datetime] NULL,
	[AGE] [float] NULL,
	[SET_ID_DEPT] [nvarchar](255) NULL,
	[DX_STARTYPE] [nvarchar](255) NULL,
	[DIVISION] [nvarchar](255) NULL,
	[REGION] [float] NULL,
	[MAY_01_BALANCE] [float] NULL,
	[TODAYS_BALANCE] [float] NULL,
	[ABSENCE_TAKEN] [float] NULL,
 CONSTRAINT [PK_HrFeed] PRIMARY KEY CLUSTERED 
(
	[HR_FEED_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StaffType]    Script Date: 20/09/2016 10:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StaffType](
	[StaffTypeId] [int] IDENTITY(1,1) NOT NULL,
	[StaffTypeName] [nvarchar](20) NOT NULL,
	[StaffTypeDescription] [nvarchar](500) NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_StaffType_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL CONSTRAINT [DF_StaffType_DateModified]  DEFAULT (getdate()),
 CONSTRAINT [PK_StaffType] PRIMARY KEY CLUSTERED 
(
	[StaffTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Store]    Script Date: 20/09/2016 10:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[StoreId] [int] IDENTITY(1,1) NOT NULL,
	[StoreName] [nvarchar](500) NOT NULL,
	[CST_CNTR_ID] [int] NOT NULL,
	[Address1] [nvarchar](50) NULL,
	[Address2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Town] [nvarchar](50) NULL,
	[PostCode] [nvarchar](10) NULL,
	[Tel] [nvarchar](50) NULL,
	[IpRange] [nvarchar](150) NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_Store_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL CONSTRAINT [DF_Store_DateModified]  DEFAULT (getdate()),
	[KronosStoreName] [nvarchar](250) NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [StoreNumber] UNIQUE NONCLUSTERED 
(
	[CST_CNTR_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Store__A9CE5E39794731E5] UNIQUE NONCLUSTERED 
(
	[CST_CNTR_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreOpeningTimes]    Script Date: 20/09/2016 10:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreOpeningTimes](
	[StoreOpenTimeId] [int] IDENTITY(1,1) NOT NULL,
	[UNI_ID_OPEN] [bigint] NULL,
	[CST_CNTR_ID] [int] NOT NULL,
	[CST_CNTR_SID] [float] NULL,
	[WEEK_COMMENCE] [int] NULL,
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
	[DATE_SUBMIT] [datetime] NULL,
	[IS_CURRENT] [bit] NULL,
	[IS_PENDING] [bit] NULL,
	[USER_ID] [float] NULL,
	[UNI_REPLACEMENT] [nvarchar](255) NULL,
	[ADMIN_UPDATING] [float] NULL,
	[REJECTED_UPDATE] [float] NULL,
 CONSTRAINT [PK_StoreOpeningTimes] PRIMARY KEY CLUSTERED 
(
	[StoreOpenTimeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WeeklyFootfall]    Script Date: 20/09/2016 10:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeeklyFootfall](
	[WeeklyFootfallId] [int] IDENTITY(1,1) NOT NULL,
	[Branch] [int] NULL,
	[ProfileType] [nvarchar](255) NULL,
	[Sunday] [float] NULL,
	[Monday] [float] NULL,
	[Tuesday] [float] NULL,
	[Wednesday] [float] NULL,
	[Thursday] [float] NULL,
	[Friday] [float] NULL,
	[Saturday] [float] NULL,
	[InvoiceGroupYearName] [nvarchar](10) NULL,
	[InvoiceGroupWeekNumber] [int] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL CONSTRAINT [DF_WeeklyFootfall_DateModified]  DEFAULT (getdate()),
 CONSTRAINT [PK_WeeklyFootfall] PRIMARY KEY CLUSTERED 
(
	[WeeklyFootfallId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BestPracticeContent] ADD  CONSTRAINT [DF_BestPracticeContent_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[BestPracticeContent] ADD  CONSTRAINT [DF_BestPracticeContent_DateModified]  DEFAULT (getdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_ActivityType] FOREIGN KEY([ActivityTypeId])
REFERENCES [dbo].[ActivityType] ([ActivityTypeId])
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [FK_Activity_ActivityType]
GO
ALTER TABLE [dbo].[BestPracticeContent]  WITH CHECK ADD  CONSTRAINT [FK_BestPracticeContent_BestPracticeCategoryTypes] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[BestPracticeCategoryTypes] ([CategoryId])
GO
ALTER TABLE [dbo].[BestPracticeContent] CHECK CONSTRAINT [FK_BestPracticeContent_BestPracticeCategoryTypes]
GO
ALTER TABLE [dbo].[HrFeed]  WITH CHECK ADD  CONSTRAINT [FK_HrFeed_StaffType] FOREIGN KEY([StaffTypeId])
REFERENCES [dbo].[StaffType] ([StaffTypeId])
GO
ALTER TABLE [dbo].[HrFeed] CHECK CONSTRAINT [FK_HrFeed_StaffType]
GO
USE [master]
GO
ALTER DATABASE [CarphoneWfm] SET  READ_WRITE 
GO
