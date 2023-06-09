USE [master]
GO
/****** Object:  Database [TestSystem]    Script Date: 11.04.2023 14:05:27 ******/
CREATE DATABASE [TestSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TestSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\TestSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TestSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\TestSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TestSystem] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TestSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TestSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TestSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TestSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TestSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [TestSystem] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [TestSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TestSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TestSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TestSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TestSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TestSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TestSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TestSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TestSystem] SET  MULTI_USER 
GO
ALTER DATABASE [TestSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TestSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TestSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TestSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TestSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TestSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TestSystem] SET QUERY_STORE = ON
GO
ALTER DATABASE [TestSystem] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TestSystem]
GO
/****** Object:  Table [dbo].[AnswerOpenTest]    Script Date: 11.04.2023 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnswerOpenTest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Answer] [varchar](max) NOT NULL,
	[IDTestOpen] [int] NOT NULL,
 CONSTRAINT [PK_AnswerOpenTest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnswerT]    Script Date: 11.04.2023 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnswerT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDTestAnswer] [int] NOT NULL,
	[Correct] [bit] NOT NULL,
	[Text] [varchar](max) NOT NULL,
	[NumberOfSelected] [int] NOT NULL,
	[TextUser] [varchar](max) NULL,
 CONSTRAINT [PK_AnswerT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnswerTheme]    Script Date: 11.04.2023 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnswerTheme](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDTheme] [int] NOT NULL,
	[IDQuestion] [int] NOT NULL,
	[AnswerText] [varchar](max) NOT NULL,
 CONSTRAINT [PK_AnswerTheme] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OpisTheme]    Script Date: 11.04.2023 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OpisTheme](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ImageName] [nvarchar](max) NULL,
	[Opis] [nvarchar](max) NULL,
	[ImageBit] [varbinary](max) NULL,
	[IdTheme] [int] NOT NULL,
	[TypeImage] [varchar](max) NULL,
	[link] [varchar](max) NULL,
 CONSTRAINT [PK_OpisTheme] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[question]    Script Date: 11.04.2023 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[question](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDTable] [int] NOT NULL,
	[Text] [varchar](max) NOT NULL,
	[Type] [varchar](max) NULL,
 CONSTRAINT [PK_question] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TableTest]    Script Date: 11.04.2023 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableTest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[IDTheme] [int] NOT NULL,
	[necessarily] [bit] NOT NULL,
	[Desp] [varchar](max) NULL,
 CONSTRAINT [PK_TableTest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestAnswer]    Script Date: 11.04.2023 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestAnswer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDTheme] [int] NOT NULL,
	[Question] [varchar](max) NOT NULL,
	[necessarily] [bit] NOT NULL,
 CONSTRAINT [PK_TestAnswer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestOpen]    Script Date: 11.04.2023 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestOpen](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDTheme] [int] NOT NULL,
	[Question] [varchar](max) NOT NULL,
	[necessarily] [bit] NOT NULL,
 CONSTRAINT [PK_TestOpen] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestSistem]    Script Date: 11.04.2023 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestSistem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[DateOpen] [varchar](max) NOT NULL,
	[DateClose] [varchar](max) NULL,
 CONSTRAINT [PK_TestSistem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestSort]    Script Date: 11.04.2023 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestSort](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDtheme] [int] NOT NULL,
	[IDques] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[Type] [varchar](max) NOT NULL,
 CONSTRAINT [PK_TestSort] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Theme]    Script Date: 11.04.2023 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Theme](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDTable] [int] NOT NULL,
	[Text] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Theme] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThemeTest]    Script Date: 11.04.2023 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThemeTest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDTestSistem] [int] NOT NULL,
	[Name] [varchar](max) NOT NULL,
 CONSTRAINT [PK_ThemeTest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSelectTest]    Script Date: 11.04.2023 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSelectTest](
	[Login] [varchar](max) NOT NULL,
	[TestID] [int] NOT NULL,
	[BoolPassed] [bit] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Password] [varchar](max) NOT NULL,
 CONSTRAINT [PK_UserSelectTest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AnswerT] ADD  CONSTRAINT [DF_AnswerT_Correct]  DEFAULT ((0)) FOR [Correct]
GO
ALTER TABLE [dbo].[AnswerT] ADD  CONSTRAINT [DF_AnswerT_NumberOfSelected]  DEFAULT ((0)) FOR [NumberOfSelected]
GO
ALTER TABLE [dbo].[TableTest] ADD  CONSTRAINT [DF_TableTest_necessarily]  DEFAULT ((0)) FOR [necessarily]
GO
ALTER TABLE [dbo].[TestAnswer] ADD  CONSTRAINT [DF_TestAnswer_necessarily]  DEFAULT ((1)) FOR [necessarily]
GO
ALTER TABLE [dbo].[TestOpen] ADD  CONSTRAINT [DF_TestOpen_necessarily]  DEFAULT ((0)) FOR [necessarily]
GO
ALTER TABLE [dbo].[UserSelectTest] ADD  CONSTRAINT [DF_UserSelectTest_BoolPassed]  DEFAULT ((0)) FOR [BoolPassed]
GO
ALTER TABLE [dbo].[AnswerOpenTest]  WITH CHECK ADD  CONSTRAINT [FK_AnswerOpenTest_TestOpen] FOREIGN KEY([IDTestOpen])
REFERENCES [dbo].[TestOpen] ([ID])
GO
ALTER TABLE [dbo].[AnswerOpenTest] CHECK CONSTRAINT [FK_AnswerOpenTest_TestOpen]
GO
ALTER TABLE [dbo].[AnswerT]  WITH CHECK ADD  CONSTRAINT [FK_AnswerT_TestAnswer] FOREIGN KEY([IDTestAnswer])
REFERENCES [dbo].[TestAnswer] ([ID])
GO
ALTER TABLE [dbo].[AnswerT] CHECK CONSTRAINT [FK_AnswerT_TestAnswer]
GO
ALTER TABLE [dbo].[AnswerTheme]  WITH CHECK ADD  CONSTRAINT [FK_AnswerTheme_question] FOREIGN KEY([IDQuestion])
REFERENCES [dbo].[question] ([ID])
GO
ALTER TABLE [dbo].[AnswerTheme] CHECK CONSTRAINT [FK_AnswerTheme_question]
GO
ALTER TABLE [dbo].[AnswerTheme]  WITH CHECK ADD  CONSTRAINT [FK_AnswerTheme_Theme] FOREIGN KEY([IDTheme])
REFERENCES [dbo].[Theme] ([ID])
GO
ALTER TABLE [dbo].[AnswerTheme] CHECK CONSTRAINT [FK_AnswerTheme_Theme]
GO
ALTER TABLE [dbo].[OpisTheme]  WITH CHECK ADD  CONSTRAINT [FK_OpisTheme_ThemeTest] FOREIGN KEY([IdTheme])
REFERENCES [dbo].[ThemeTest] ([ID])
GO
ALTER TABLE [dbo].[OpisTheme] CHECK CONSTRAINT [FK_OpisTheme_ThemeTest]
GO
ALTER TABLE [dbo].[question]  WITH CHECK ADD  CONSTRAINT [FK_question_TableTest] FOREIGN KEY([IDTable])
REFERENCES [dbo].[TableTest] ([ID])
GO
ALTER TABLE [dbo].[question] CHECK CONSTRAINT [FK_question_TableTest]
GO
ALTER TABLE [dbo].[TableTest]  WITH CHECK ADD  CONSTRAINT [FK_TableTest_ThemeTest] FOREIGN KEY([IDTheme])
REFERENCES [dbo].[ThemeTest] ([ID])
GO
ALTER TABLE [dbo].[TableTest] CHECK CONSTRAINT [FK_TableTest_ThemeTest]
GO
ALTER TABLE [dbo].[TestAnswer]  WITH CHECK ADD  CONSTRAINT [FK_TestAnswer_ThemeTest] FOREIGN KEY([IDTheme])
REFERENCES [dbo].[ThemeTest] ([ID])
GO
ALTER TABLE [dbo].[TestAnswer] CHECK CONSTRAINT [FK_TestAnswer_ThemeTest]
GO
ALTER TABLE [dbo].[TestOpen]  WITH CHECK ADD  CONSTRAINT [FK_TestOpen_ThemeTest] FOREIGN KEY([IDTheme])
REFERENCES [dbo].[ThemeTest] ([ID])
GO
ALTER TABLE [dbo].[TestOpen] CHECK CONSTRAINT [FK_TestOpen_ThemeTest]
GO
ALTER TABLE [dbo].[TestSort]  WITH CHECK ADD  CONSTRAINT [FK_TestSort_TableTest] FOREIGN KEY([IDques])
REFERENCES [dbo].[TableTest] ([ID])
GO
ALTER TABLE [dbo].[TestSort] CHECK CONSTRAINT [FK_TestSort_TableTest]
GO
ALTER TABLE [dbo].[TestSort]  WITH CHECK ADD  CONSTRAINT [FK_TestSort_TestAnswer] FOREIGN KEY([IDques])
REFERENCES [dbo].[TestAnswer] ([ID])
GO
ALTER TABLE [dbo].[TestSort] CHECK CONSTRAINT [FK_TestSort_TestAnswer]
GO
ALTER TABLE [dbo].[TestSort]  WITH CHECK ADD  CONSTRAINT [FK_TestSort_TestOpen] FOREIGN KEY([IDques])
REFERENCES [dbo].[TestOpen] ([ID])
GO
ALTER TABLE [dbo].[TestSort] CHECK CONSTRAINT [FK_TestSort_TestOpen]
GO
ALTER TABLE [dbo].[TestSort]  WITH CHECK ADD  CONSTRAINT [FK_TestSort_ThemeTest] FOREIGN KEY([IDtheme])
REFERENCES [dbo].[ThemeTest] ([ID])
GO
ALTER TABLE [dbo].[TestSort] CHECK CONSTRAINT [FK_TestSort_ThemeTest]
GO
ALTER TABLE [dbo].[Theme]  WITH CHECK ADD  CONSTRAINT [FK_Theme_TableTest] FOREIGN KEY([IDTable])
REFERENCES [dbo].[TableTest] ([ID])
GO
ALTER TABLE [dbo].[Theme] CHECK CONSTRAINT [FK_Theme_TableTest]
GO
ALTER TABLE [dbo].[ThemeTest]  WITH CHECK ADD  CONSTRAINT [FK_ThemeTest_TestSistem] FOREIGN KEY([IDTestSistem])
REFERENCES [dbo].[TestSistem] ([ID])
GO
ALTER TABLE [dbo].[ThemeTest] CHECK CONSTRAINT [FK_ThemeTest_TestSistem]
GO
ALTER TABLE [dbo].[UserSelectTest]  WITH CHECK ADD  CONSTRAINT [FK_UserSelectTest_TestSistem] FOREIGN KEY([TestID])
REFERENCES [dbo].[TestSistem] ([ID])
GO
ALTER TABLE [dbo].[UserSelectTest] CHECK CONSTRAINT [FK_UserSelectTest_TestSistem]
GO
USE [master]
GO
ALTER DATABASE [TestSystem] SET  READ_WRITE 
GO
