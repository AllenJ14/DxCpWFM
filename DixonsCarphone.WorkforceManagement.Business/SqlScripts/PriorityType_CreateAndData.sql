USE [CarphoneWfm]
GO
/****** Object:  Table [dbo].[PriorityType]    Script Date: 08/11/2016 20:37:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriorityType](
	[PriorityTypeId] [smallint] IDENTITY(1,1) NOT NULL,
	[PriorityTypeText] [nvarchar](50) NOT NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_PriorityType_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL CONSTRAINT [DF_PriorityType_DateModified]  DEFAULT (getdate()),
 CONSTRAINT [PK_PriorityType] PRIMARY KEY CLUSTERED 
(
	[PriorityTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Activity] ADD PriorityTypeId smallint
GO

ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_ActivityType] FOREIGN KEY([PriorityTypeId])
REFERENCES [dbo].[PriorityType] ([PriorityTypeId])
GO


insert into prioritytype (prioritytypetext) values('High')
insert into prioritytype (prioritytypetext) values('Medium')
insert into prioritytype (prioritytypetext) values('Low')
