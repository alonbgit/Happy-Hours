USE [master]
GO
/****** Object:  Database [HappyHours]    Script Date: 30/12/2017 19:36:07 ******/
CREATE DATABASE [HappyHours]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HappyHours', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\HappyHours.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HappyHours_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\HappyHours_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [HappyHours] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HappyHours].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HappyHours] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HappyHours] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HappyHours] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HappyHours] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HappyHours] SET ARITHABORT OFF 
GO
ALTER DATABASE [HappyHours] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HappyHours] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HappyHours] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HappyHours] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HappyHours] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HappyHours] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HappyHours] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HappyHours] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HappyHours] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HappyHours] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HappyHours] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HappyHours] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HappyHours] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HappyHours] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HappyHours] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HappyHours] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HappyHours] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HappyHours] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HappyHours] SET  MULTI_USER 
GO
ALTER DATABASE [HappyHours] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HappyHours] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HappyHours] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HappyHours] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HappyHours] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HappyHours] SET QUERY_STORE = OFF
GO
USE [HappyHours]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [HappyHours]
GO
/****** Object:  User [demo1]    Script Date: 30/12/2017 19:36:07 ******/
CREATE USER [demo1] FOR LOGIN [demo1] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [alonb]    Script Date: 30/12/2017 19:36:07 ******/
CREATE USER [alonb] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [demo1]
GO
/****** Object:  Table [dbo].[EmailActivationTokens]    Script Date: 30/12/2017 19:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailActivationTokens](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Token] [uniqueidentifier] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_EmailActivationTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_EmailActivationTokens_Token] UNIQUE NONCLUSTERED 
(
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserArrivals]    Script Date: 30/12/2017 19:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserArrivals](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[ArrivalTime] [datetime] NOT NULL,
	[ExitTime] [datetime] NULL,
 CONSTRAINT [PK_UserArrivals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 30/12/2017 19:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[SystemEmail] [nvarchar](50) NOT NULL,
	[SystemPassword] [nvarchar](50) NOT NULL,
	[SystemNumber] [nvarchar](50) NOT NULL,
	[IsEmailVerified] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Users_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EmailActivationTokens] ADD  CONSTRAINT [DF_EmailActivationTokens_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserArrivals] ADD  CONSTRAINT [DF_UserArrivals_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserArrivals] ADD  CONSTRAINT [DF_UserArrivals_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsEmailVerified]  DEFAULT ((0)) FOR [IsEmailVerified]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[EmailActivationTokens]  WITH CHECK ADD  CONSTRAINT [FK_EmailActivationTokens_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[EmailActivationTokens] CHECK CONSTRAINT [FK_EmailActivationTokens_Users]
GO
ALTER TABLE [dbo].[UserArrivals]  WITH CHECK ADD  CONSTRAINT [FK_UserArrivals_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserArrivals] CHECK CONSTRAINT [FK_UserArrivals_Users]
GO
/****** Object:  StoredProcedure [dbo].[sp_ActivateEmail]    Script Date: 30/12/2017 19:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[sp_ActivateEmail] 
	@Token uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TokenId bigint
	DECLARE @UserId bigint
	DECLARE @InvalidToken bit = 0

	SELECT TOP 1 
	@TokenId = [Id],
	@UserId = [UserId]
	FROM [EmailActivationTokens]
	WHERE [Token] = @Token

	IF @TokenId is null
		BEGIN
			SET @InvalidToken = 1
			GOTO Result
		END

	BEGIN TRANSACTION

		UPDATE [Users]
		SET [IsEmailVerified] = 1
		WHERE [Id] = @UserId;

		DELETE FROM [EmailActivationTokens]
		WHERE [Id] = @TokenId;

	COMMIT TRANSACTION

	Result:
	SELECT @InvalidToken as InvalidToken
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckAndUpdateUserTimes]    Script Date: 30/12/2017 19:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[sp_CheckAndUpdateUserTimes] 
	@UserId bigint,
	@StartTime datetime,
	@EndTime datetime
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @NewArrivalTime bit = 0
	DECLARE @NewExitTime bit = 0;

	DECLARE @UserTimeId bigint
	DECLARE @CurrentArrivalTime datetime
	DECLARE @CurrentExitTime datetime

	-- first we check if today's date, even exist in the UserArrivals table for that specific user

	SELECT @UserTimeId = [Id],
		   @CurrentArrivalTime = [ArrivalTime],
		   @CurrentExitTime = [ExitTime]
	FROM [UserArrivals]
	WHERE [UserId] = @UserId AND CAST([ArrivalTime] AS DATE) = CAST(@StartTime AS DATE);

	IF @UserTimeId is null
		BEGIN
			INSERT INTO [UserArrivals] (
				[UserId],
				[ArrivalTime],
				[ExitTime]
			)
			VALUES (
				@UserId,
				@StartTime,
				@EndTime
			)

			-- because this is a new fresh row, than we just check if the input user arrivals are not null
			-- and than exit the method

			IF @StartTime is not null
				BEGIN
					SET @NewArrivalTime = 1
				END

			IF @EndTime is not null
				BEGIN
					SET @NewExitTime = 1
				END

		END
	ELSE
		BEGIN

			BEGIN TRANSACTION

				IF @StartTime is not null AND @CurrentArrivalTime is null
					BEGIN
						UPDATE [UserArrivals]
						SET [ArrivalTime] = @StartTime, [UpdateDate] = GETDATE()
						WHERE [Id] = @UserTimeId

						SET @NewArrivalTime = 1
					END

				IF @EndTime is not null AND @CurrentExitTime is null
					BEGIN
						UPDATE [UserArrivals]
						SET [ExitTime] = @EndTime, [UpdateDate] = GETDATE()
						WHERE [Id] = @UserTimeId

						SET @NewExitTime = 1
					END

			COMMIT TRANSACTION

		END

	SELECT @NewArrivalTime AS NewArrivalTime,
		   @NewExitTime AS NewExitTime
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserById]    Script Date: 30/12/2017 19:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[sp_GetUserById] 

	@UserId bigint

AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM [Users]
	WHERE [Id] = @UserId;
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserDetails]    Script Date: 30/12/2017 19:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[sp_GetUserDetails] 

AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM [Users]
	WHERE [IsEmailVerified] = 1;
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserStatusByEmail]    Script Date: 30/12/2017 19:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetUserStatusByEmail] 
	@Email nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1
	[Id],
	[IsEmailVerified]
	FROM [Users]
	WHERE [Email] = @Email;
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Signin]    Script Date: 30/12/2017 19:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Signin] 
	@Email nvarchar(50),
	@Password nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id],
		   [IsEmailVerified]
	FROM [Users]
	WHERE [Email] = @Email AND [Password] = @Password; 
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Signup]    Script Date: 30/12/2017 19:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_Signup] 
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Email nvarchar(50),
	@Password nvarchar(50),
	@SystemEmail nvarchar(50),
	@SystemPassword nvarchar(50),
	@SystemNumber nvarchar(50),
	@IsActivationRequired bit
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @UserId bigint
	DECLARE @EmailExist bit = 0

	SELECT @UserId = [Id]
	FROM [Users]
	WHERE [Email] = @Email AND [Password] = @Password;

	IF @UserId is not NULL
		BEGIN
			SET @EmailExist = 1
			GOTO Result
		END

	DECLARE @Token uniqueidentifier = NEWID()

	BEGIN TRANSACTION;

		INSERT INTO [Users] (
			[FirstName],
			[LastName],
			[Email],
			[Password],
			[SystemEmail],
			[SystemPassword],
			[SystemNumber]
		)
		VALUES (
			@FirstName,
			@LastName,
			@Email,
			@Password,
			@SystemEmail,
			@SystemPassword,
			@SystemNumber
		)

		IF @IsActivationRequired = 1
			BEGIN
				DECLARE @InsertedUserId bigint
				SET @InsertedUserId = SCOPE_IDENTITY()

				INSERT INTO [EmailActivationTokens] (
					[Token],
					[UserId]
				)
				VALUES (
					@Token,
					@InsertedUserId
				)
			END

	COMMIT TRANSACTION;

	Result:
	SELECT @EmailExist as EmailExist,
		   @Token as ActivationToken


END
GO
USE [master]
GO
ALTER DATABASE [HappyHours] SET  READ_WRITE 
GO
