USE [master]
GO

/****** Object:  Database [PriceForecastDb]    Script Date: 8/3/2020 4:37:27 PM ******/

DROP DATABASE IF EXISTS [PriceForecastDb]
GO
 
CREATE DATABASE [PriceForecastDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PriceForecastDb', FILENAME = N'/var/opt/mssql/data/PriceForecastDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PriceForecastDb_log', FILENAME = N'/var/opt/mssql/data/PriceForecastDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PriceForecastDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [PriceForecastDb] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [PriceForecastDb] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [PriceForecastDb] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [PriceForecastDb] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [PriceForecastDb] SET ARITHABORT OFF 
GO

ALTER DATABASE [PriceForecastDb] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [PriceForecastDb] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [PriceForecastDb] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [PriceForecastDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [PriceForecastDb] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [PriceForecastDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [PriceForecastDb] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [PriceForecastDb] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [PriceForecastDb] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [PriceForecastDb] SET  ENABLE_BROKER 
GO

ALTER DATABASE [PriceForecastDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [PriceForecastDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [PriceForecastDb] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [PriceForecastDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [PriceForecastDb] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [PriceForecastDb] SET READ_COMMITTED_SNAPSHOT ON 
GO

ALTER DATABASE [PriceForecastDb] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [PriceForecastDb] SET RECOVERY FULL 
GO

ALTER DATABASE [PriceForecastDb] SET  MULTI_USER 
GO

ALTER DATABASE [PriceForecastDb] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [PriceForecastDb] SET DB_CHAINING OFF 
GO

ALTER DATABASE [PriceForecastDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [PriceForecastDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [PriceForecastDb] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [PriceForecastDb] SET QUERY_STORE = OFF
GO

ALTER DATABASE [PriceForecastDb] SET  READ_WRITE 
GO

USE [PriceForecastDb]
GO

/*Create Table*/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PriceForecasts](
	[ForecastDateTime] [datetime2](7) NOT NULL,
	[ForecastModel] [nvarchar](450) NOT NULL,
	[Market] [nvarchar](450) NOT NULL,
	[Product] [nvarchar](450) NOT NULL,
	[CountryCode] [nvarchar](450) NOT NULL,
	[ForecastedDate] [datetime2](7) NOT NULL,
	[Category] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_PriceForecasts] PRIMARY KEY CLUSTERED
(
	[ForecastDateTime] ASC,
	[ForecastModel] ASC,
	[Market] ASC,
	[Product] ASC,
	[CountryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [ImportEvents] (
    [EventId] int NOT NULL IDENTITY,
    [EventDate] datetime2 NOT NULL DEFAULT (GETDATE()),
    [LastImportedForecastDateTime] datetime2 NULL,
    [LastImportedForecastModel] nvarchar(450) NULL,
    [LastImportedMarket] nvarchar(450) NULL,
    [LastImportedProduct] nvarchar(450) NULL,
    [LastImportedCountryCode] nvarchar(450) NULL,
    CONSTRAINT [PK_ImportEvents] PRIMARY KEY ([EventId]),
    CONSTRAINT [FK_ImportEvents_PriceForecasts_LastImportedForecastDateTime_LastImportedForecastModel_LastImportedMarket_LastImportedProduct_La~] FOREIGN KEY ([LastImportedForecastDateTime], [LastImportedForecastModel], [LastImportedMarket], [LastImportedProduct], [LastImportedCountryCode]) REFERENCES [PriceForecasts] ([ForecastDateTime], [ForecastModel], [Market], [Product], [CountryCode]) ON DELETE NO ACTION
);

GO





