/****** Object:  Table [dbo].[Applications]    Script Date: 28/04/2020 16:46:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[applicationID] [bigint] IDENTITY(1,1) NOT NULL,
	[exeName] [varchar](255) NOT NULL,
	[applicationTitle] [varchar](255) NULL,
	[applicationIcon] [varbinary](max) NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[applicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SessionApplicationsUsage]    Script Date: 28/04/2020 16:46:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionApplicationsUsage](
	[sessionID] [bigint] NOT NULL,
	[applicationID] [bigint] NOT NULL,
	[usageTime] [bigint] NOT NULL,
 CONSTRAINT [PK_SessionApplicationUsage] PRIMARY KEY CLUSTERED 
(
	[sessionID] ASC,
	[applicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SessionEvents]    Script Date: 28/04/2020 16:46:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionEvents](
	[eventID] [bigint] IDENTITY(1,1) NOT NULL,
	[sessionID] [bigint] NOT NULL,
	[eventDateTime] [datetime] NULL,
	[eventCode] [int] NOT NULL,
	[eventData] [varchar](max) NULL,
 CONSTRAINT [PK_SessionEvents] PRIMARY KEY CLUSTERED 
(
	[eventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 28/04/2020 16:46:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[userID] [bigint] IDENTITY(1,1) NOT NULL,
	[userSID] [varchar](85) NOT NULL,
	[userName] [varchar](128) NOT NULL,
	[userDomain] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSessions]    Script Date: 28/04/2020 16:46:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSessions](
	[sessionID] [bigint] IDENTITY(1,1) NOT NULL,
	[userID] [bigint] NOT NULL,
	[machineName] [varchar](128) NULL,
	[machineDomain] [varchar](128) NULL,
	[timeStarted] [datetime] NULL,
	[timeEnded] [datetime] NULL,
	[timeLastUpdate] [datetime] NULL,
	[idleTime] [bigint] NOT NULL,
 CONSTRAINT [PK_UserSessions] PRIMARY KEY CLUSTERED 
(
	[sessionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwSessionsApplicationsUsage]    Script Date: 28/04/2020 16:46:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwSessionsApplicationsUsage]
AS
SELECT        dbo.Users.userSID, dbo.UserSessions.sessionID, dbo.Users.userName, dbo.Users.userDomain, dbo.UserSessions.machineName, dbo.UserSessions.machineDomain, dbo.Applications.exeName, 
                         dbo.Applications.applicationTitle, dbo.SessionApplicationsUsage.usageTime
FROM            dbo.Applications INNER JOIN
                         dbo.SessionApplicationsUsage ON dbo.Applications.applicationID = dbo.SessionApplicationsUsage.applicationID INNER JOIN
                         dbo.UserSessions ON dbo.SessionApplicationsUsage.sessionID = dbo.UserSessions.sessionID INNER JOIN
                         dbo.Users ON dbo.UserSessions.userID = dbo.Users.userID
GO
ALTER TABLE [dbo].[UserSessions] ADD  CONSTRAINT [DF_UserSessions_idleTime]  DEFAULT ((0)) FOR [idleTime]
GO
ALTER TABLE [dbo].[SessionApplicationsUsage]  WITH CHECK ADD  CONSTRAINT [FK_SessionApplicationsUsage_Applications] FOREIGN KEY([applicationID])
REFERENCES [dbo].[Applications] ([applicationID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SessionApplicationsUsage] CHECK CONSTRAINT [FK_SessionApplicationsUsage_Applications]
GO
ALTER TABLE [dbo].[SessionApplicationsUsage]  WITH CHECK ADD  CONSTRAINT [FK_SessionApplicationsUsage_UserSessions] FOREIGN KEY([sessionID])
REFERENCES [dbo].[UserSessions] ([sessionID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SessionApplicationsUsage] CHECK CONSTRAINT [FK_SessionApplicationsUsage_UserSessions]
GO
ALTER TABLE [dbo].[UserSessions]  WITH CHECK ADD  CONSTRAINT [FK_UserSessions_Users] FOREIGN KEY([userID])
REFERENCES [dbo].[Users] ([userID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserSessions] CHECK CONSTRAINT [FK_UserSessions_Users]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Application ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Applications', @level2type=N'COLUMN',@level2name=N'applicationID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Excutable File Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Applications', @level2type=N'COLUMN',@level2name=N'exeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Window Title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Applications', @level2type=N'COLUMN',@level2name=N'applicationTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Application Icon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Applications', @level2type=N'COLUMN',@level2name=N'applicationIcon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'User ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'userID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Domain' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'userDomain'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Session ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserSessions', @level2type=N'COLUMN',@level2name=N'sessionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Machine Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserSessions', @level2type=N'COLUMN',@level2name=N'machineName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Machine Domain' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserSessions', @level2type=N'COLUMN',@level2name=N'machineDomain'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Timestamp when session is started' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserSessions', @level2type=N'COLUMN',@level2name=N'timeStarted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Timestamp when session is stopped' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserSessions', @level2type=N'COLUMN',@level2name=N'timeEnded'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[23] 4[24] 2[14] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Applications"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 172
               Right = 228
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SessionApplicationsUsage"
            Begin Extent = 
               Top = 6
               Left = 266
               Bottom = 152
               Right = 456
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 494
               Bottom = 151
               Right = 684
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserSessions"
            Begin Extent = 
               Top = 6
               Left = 722
               Bottom = 208
               Right = 912
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
       ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwSessionsApplicationsUsage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'  Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwSessionsApplicationsUsage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwSessionsApplicationsUsage'
GO
