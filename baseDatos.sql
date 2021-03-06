USE [master]
GO
/****** Object:  Database [RecluIT]    Script Date: 07/17/2014 13:05:22 ******/
CREATE DATABASE [RecluIT] ON  PRIMARY 
( NAME = N'RecluIT', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\RecluIT.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'RecluIT_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\RecluIT_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [RecluIT] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RecluIT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RecluIT] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [RecluIT] SET ANSI_NULLS OFF
GO
ALTER DATABASE [RecluIT] SET ANSI_PADDING OFF
GO
ALTER DATABASE [RecluIT] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [RecluIT] SET ARITHABORT OFF
GO
ALTER DATABASE [RecluIT] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [RecluIT] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [RecluIT] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [RecluIT] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [RecluIT] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [RecluIT] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [RecluIT] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [RecluIT] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [RecluIT] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [RecluIT] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [RecluIT] SET  DISABLE_BROKER
GO
ALTER DATABASE [RecluIT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [RecluIT] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [RecluIT] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [RecluIT] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [RecluIT] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [RecluIT] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [RecluIT] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [RecluIT] SET  READ_WRITE
GO
ALTER DATABASE [RecluIT] SET RECOVERY FULL
GO
ALTER DATABASE [RecluIT] SET  MULTI_USER
GO
ALTER DATABASE [RecluIT] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [RecluIT] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'RecluIT', N'ON'
GO
USE [RecluIT]
GO
/****** Object:  Table [dbo].[persona]    Script Date: 07/17/2014 13:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[persona](
	[idPersona] [int] NOT NULL,
	[nombre] [nchar](30) NULL,
	[apePaterno] [nchar](20) NULL,
	[apeMaterno] [nchar](20) NOT NULL,
 CONSTRAINT [PK_persona] PRIMARY KEY CLUSTERED 
(
	[idPersona] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tecnologia]    Script Date: 07/17/2014 13:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tecnologia](
	[idTecnologia] [int] NOT NULL,
	[nombre] [nchar](300) NOT NULL,
 CONSTRAINT [PK_tecnologia] PRIMARY KEY CLUSTERED 
(
	[idTecnologia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reclutador]    Script Date: 07/17/2014 13:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reclutador](
	[idReclutador] [int] NOT NULL,
	[usuario] [nchar](20) NOT NULL,
	[password] [nchar](20) NOT NULL,
	[idPersona] [int] NOT NULL,
	[permisos] [int] NULL,
 CONSTRAINT [PK_reclutador] PRIMARY KEY CLUSTERED 
(
	[idReclutador] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[candidato]    Script Date: 07/17/2014 13:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[candidato](
	[idCandidato] [int] NOT NULL,
	[CURP] [nchar](25) NOT NULL,
	[RFC] [nchar](20) NULL,
	[email] [nchar](30) NULL,
	[telefono] [nchar](20) NULL,
	[palabrasClave] [nchar](200) NULL,
	[fecha_registro] [datetime] NULL,
	[idPersona] [int] NULL,
	[idReclutador] [int] NULL,
 CONSTRAINT [PK_candidato] PRIMARY KEY CLUSTERED 
(
	[idCandidato] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[candidato_tecnologia]    Script Date: 07/17/2014 13:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[candidato_tecnologia](
	[idCandidato] [int] NOT NULL,
	[idTecnologia] [int] NOT NULL,
 CONSTRAINT [PK_candidato_tecnologia] PRIMARY KEY CLUSTERED 
(
	[idCandidato] ASC,
	[idTecnologia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_reclutador_persona]    Script Date: 07/17/2014 13:05:24 ******/
ALTER TABLE [dbo].[reclutador]  WITH CHECK ADD  CONSTRAINT [FK_reclutador_persona] FOREIGN KEY([idPersona])
REFERENCES [dbo].[persona] ([idPersona])
GO
ALTER TABLE [dbo].[reclutador] CHECK CONSTRAINT [FK_reclutador_persona]
GO
/****** Object:  ForeignKey [FK_candidato_persona]    Script Date: 07/17/2014 13:05:24 ******/
ALTER TABLE [dbo].[candidato]  WITH CHECK ADD  CONSTRAINT [FK_candidato_persona] FOREIGN KEY([idPersona])
REFERENCES [dbo].[persona] ([idPersona])
GO
ALTER TABLE [dbo].[candidato] CHECK CONSTRAINT [FK_candidato_persona]
GO
/****** Object:  ForeignKey [FK_candidato_reclutador]    Script Date: 07/17/2014 13:05:24 ******/
ALTER TABLE [dbo].[candidato]  WITH CHECK ADD  CONSTRAINT [FK_candidato_reclutador] FOREIGN KEY([idReclutador])
REFERENCES [dbo].[reclutador] ([idReclutador])
GO
ALTER TABLE [dbo].[candidato] CHECK CONSTRAINT [FK_candidato_reclutador]
GO
/****** Object:  ForeignKey [FK_candidato_tecnologia_candidato]    Script Date: 07/17/2014 13:05:24 ******/
ALTER TABLE [dbo].[candidato_tecnologia]  WITH CHECK ADD  CONSTRAINT [FK_candidato_tecnologia_candidato] FOREIGN KEY([idCandidato])
REFERENCES [dbo].[candidato] ([idCandidato])
GO
ALTER TABLE [dbo].[candidato_tecnologia] CHECK CONSTRAINT [FK_candidato_tecnologia_candidato]
GO
/****** Object:  ForeignKey [FK_candidato_tecnologia_tecnologia]    Script Date: 07/17/2014 13:05:24 ******/
ALTER TABLE [dbo].[candidato_tecnologia]  WITH CHECK ADD  CONSTRAINT [FK_candidato_tecnologia_tecnologia] FOREIGN KEY([idTecnologia])
REFERENCES [dbo].[tecnologia] ([idTecnologia])
GO
ALTER TABLE [dbo].[candidato_tecnologia] CHECK CONSTRAINT [FK_candidato_tecnologia_tecnologia]
GO
