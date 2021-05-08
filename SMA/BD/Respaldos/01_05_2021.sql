/****** Object:  Database [smafacpya]    Script Date: 01/05/2021 03:38:35 p. m. ******/
CREATE DATABASE smafacpya
ALTER DATABASE [smafacpya] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [smafacpya] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [smafacpya] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [smafacpya] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [smafacpya] SET ARITHABORT OFF 
GO
ALTER DATABASE [smafacpya] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [smafacpya] SET AUTO_SHRINK ON 
GO
ALTER DATABASE [smafacpya] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [smafacpya] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [smafacpya] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [smafacpya] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [smafacpya] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [smafacpya] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [smafacpya] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [smafacpya] SET  DISABLE_BROKER 
GO
ALTER DATABASE [smafacpya] SET AUTO_UPDATE_STATISTICS_ASYNC ON 
GO
ALTER DATABASE [smafacpya] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [smafacpya] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [smafacpya] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [smafacpya] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [smafacpya] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [smafacpya] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [smafacpya] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [smafacpya] SET  MULTI_USER 
GO
ALTER DATABASE [smafacpya] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [smafacpya] SET DB_CHAINING OFF 
GO
ALTER DATABASE [smafacpya] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [smafacpya] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [smafacpya] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'smafacpya', N'ON'
GO
ALTER DATABASE [smafacpya] SET QUERY_STORE = OFF
GO
USE [smafacpya]
GO
/****** Object:  Schema [Catalogo]    Script Date: 01/05/2021 03:38:40 p. m. ******/
CREATE SCHEMA [Catalogo]
GO
/****** Object:  Schema [Configuracion]    Script Date: 01/05/2021 03:38:40 p. m. ******/
CREATE SCHEMA [Configuracion]
GO
/****** Object:  Schema [Reporte]    Script Date: 01/05/2021 03:38:40 p. m. ******/
CREATE SCHEMA [Reporte]
GO
/****** Object:  Schema [Seguridad]    Script Date: 01/05/2021 03:38:40 p. m. ******/
CREATE SCHEMA [Seguridad]
GO
/****** Object:  Table [Catalogo].[Motivo]    Script Date: 01/05/2021 03:38:40 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Motivo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Motivo] [varchar](100) NULL,
	[Descripcion] [varchar](250) NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_Motivo_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Catalogo].[Proyecto]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Proyecto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](250) NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_Proyecto_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Configuracion].[Estatus]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Configuracion].[Estatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](40) NULL,
	[Llave] [varchar](40) NULL,
 CONSTRAINT [PK_Estatus_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Reporte].[Evidencia]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Reporte].[Evidencia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdReporte] [int] NULL,
	[Nombre] [varchar](50) NULL,
	[Extension] [varchar](10) NULL,
	[Imagen] [varbinary](max) NULL,
 CONSTRAINT [PK_Evidencia_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Reporte].[Reporte]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Reporte].[Reporte](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Folio] [varchar](100) NULL,
	[IdMotivo] [int] NULL,
	[IdProyecto] [int] NULL,
	[FechaReporte] [date] NULL,
	[ComentarioReporte] [varchar](250) NULL,
	[IdEstatusReporte] [int] NULL,
	[UsuarioCreacion] [int] NULL,
	[FechaCreacion] [datetime] NULL,
	[UsuarioEdicion] [int] NULL,
	[FechaEdicion] [datetime] NULL,
	[UsuarioElimincion] [int] NULL,
	[FechaEliminacion] [datetime] NULL,
	[IdUsuarioAsignacion] [int] NULL,
	[FechaAsignacion] [datetime] NULL,
	[ComentarioAsignacion] [varchar](250) NULL,
	[FechaFinalizacion] [datetime] NULL,
	[ComentarioFinalizacion] [varchar](250) NULL,
 CONSTRAINT [PK_Reporte_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Seguridad].[Modulo]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[Modulo](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](250) NULL,
	[Activo] [bit] NOT NULL,
	[Llave] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Modulo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Seguridad].[ModuloPantalla]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[ModuloPantalla](
	[IdModulo] [smallint] NOT NULL,
	[IdPantalla] [smallint] NOT NULL,
 CONSTRAINT [PK_Seguridad.ModuloPantalla] PRIMARY KEY CLUSTERED 
(
	[IdModulo] ASC,
	[IdPantalla] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Seguridad].[Pantalla]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[Pantalla](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](250) NULL,
	[Ruta] [varchar](max) NOT NULL,
	[Activo] [bit] NOT NULL,
	[Llave] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Pantalla] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Seguridad].[PantallaPermiso]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[PantallaPermiso](
	[IdPantalla] [smallint] NOT NULL,
	[IdPermiso] [int] NOT NULL,
 CONSTRAINT [PK_PantallaPermiso] PRIMARY KEY CLUSTERED 
(
	[IdPantalla] ASC,
	[IdPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Seguridad].[Permiso]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[Permiso](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](250) NULL,
	[Llave] [varchar](250) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Seguridad].[Rol]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[Rol](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](250) NULL,
	[FechaCreacion] [datetime] NULL,
	[UsuarioCreacion] [int] NULL,
	[FechaEdicion] [datetime] NULL,
	[UsuarioEdicion] [int] NULL,
	[FechaEliminacion] [datetime] NULL,
	[UsuarioEliminacion] [int] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Seguridad].[RolApp]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[RolApp](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Llave] [varchar](250) NOT NULL,
	[Activo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Seguridad].[RolModuloPantallaPermiso]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[RolModuloPantallaPermiso](
	[IdRol] [smallint] NOT NULL,
	[IdModulo] [smallint] NOT NULL,
	[IdPantalla] [smallint] NOT NULL,
	[IdPermiso] [int] NOT NULL,
 CONSTRAINT [PK_RolModuloPantallaPermiso] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC,
	[IdModulo] ASC,
	[IdPantalla] ASC,
	[IdPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Seguridad].[Usuario]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](80) NOT NULL,
	[ApellidoPaterno] [varchar](80) NOT NULL,
	[ApellidoMaterno] [varchar](80) NOT NULL,
	[FechaNacimiento] [datetime] NULL,
	[FechaCreacion] [datetime] NULL,
	[UsuarioCreacion] [int] NULL,
	[FechaEdicion] [datetime] NULL,
	[UsuarioEdicion] [int] NULL,
	[FechaEliminacion] [datetime] NULL,
	[UsuarioEliminacion] [int] NULL,
	[Correo] [varchar](1000) NOT NULL,
	[Contraseña] [varbinary](max) NOT NULL,
	[Activo] [bit] NOT NULL,
	[IdSeccion] [int] NULL,
	[IdRolApp] [smallint] NULL,
	[IdEstatusUsuario] [int] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Seguridad].[UsuarioCodigo]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[UsuarioCodigo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdTipoCodigo] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[Codigo] [varchar](10) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaVigencia] [datetime] NOT NULL,
 CONSTRAINT [PK_UsuarioCodigo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Seguridad].[UsuarioContraseña]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[UsuarioContraseña](
	[Id] [uniqueidentifier] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaVigencia] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_UsuarioContraseña] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Seguridad].[UsuarioLogin]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[UsuarioLogin](
	[Id] [uniqueidentifier] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaVigencia] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_UsuarioLogin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Seguridad].[UsuarioRol]    Script Date: 01/05/2021 03:38:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[UsuarioRol](
	[IdUsuario] [int] NOT NULL,
	[IdRol] [smallint] NOT NULL,
 CONSTRAINT [PK_UsuarioRol] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC,
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Catalogo].[Motivo] ON 
GO
INSERT [Catalogo].[Motivo] ([Id], [Motivo], [Descripcion], [Activo]) VALUES (1, N'Error', N'Error en la pantalla menú', 1)
GO
INSERT [Catalogo].[Motivo] ([Id], [Motivo], [Descripcion], [Activo]) VALUES (2, N'Cambio Proceso', N'Cambio en pantalla', 1)
GO
INSERT [Catalogo].[Motivo] ([Id], [Motivo], [Descripcion], [Activo]) VALUES (3, N'Nuevo Proceso', N'Agregar en pantalla validaciones', 1)
GO
SET IDENTITY_INSERT [Catalogo].[Motivo] OFF
GO
SET IDENTITY_INSERT [Catalogo].[Proyecto] ON 
GO
INSERT [Catalogo].[Proyecto] ([Id], [Nombre], [Descripcion], [Activo]) VALUES (1, N'Proyecto AppCenter', N'Proyecto para facpya cetif', 1)
GO
INSERT [Catalogo].[Proyecto] ([Id], [Nombre], [Descripcion], [Activo]) VALUES (2, N'ProyectoMesaAyuda', N'Proyecto para el seguimiento de procesos', 1)
GO
INSERT [Catalogo].[Proyecto] ([Id], [Nombre], [Descripcion], [Activo]) VALUES (3, N'SAC', N'Proyecto Alta Candidatos', 1)
GO
INSERT [Catalogo].[Proyecto] ([Id], [Nombre], [Descripcion], [Activo]) VALUES (4, N'SMA', N'Proyecto especficado a la mesa de ayuda', 1)
GO
INSERT [Catalogo].[Proyecto] ([Id], [Nombre], [Descripcion], [Activo]) VALUES (5, N'SPA', N'sistema de pendientes de meibtech', 1)
GO
SET IDENTITY_INSERT [Catalogo].[Proyecto] OFF
GO
SET IDENTITY_INSERT [Configuracion].[Estatus] ON 
GO
INSERT [Configuracion].[Estatus] ([Id], [Nombre], [Llave]) VALUES (1, N'Abierto', N'abierto')
GO
INSERT [Configuracion].[Estatus] ([Id], [Nombre], [Llave]) VALUES (2, N'Asignado', N'asignado')
GO
INSERT [Configuracion].[Estatus] ([Id], [Nombre], [Llave]) VALUES (3, N'Cerrado', N'cerrado')
GO
SET IDENTITY_INSERT [Configuracion].[Estatus] OFF
GO
SET IDENTITY_INSERT [Reporte].[Reporte] ON 
GO
INSERT [Reporte].[Reporte] ([Id], [Folio], [IdMotivo], [IdProyecto], [FechaReporte], [ComentarioReporte], [IdEstatusReporte], [UsuarioCreacion], [FechaCreacion], [UsuarioEdicion], [FechaEdicion], [UsuarioElimincion], [FechaEliminacion], [IdUsuarioAsignacion], [FechaAsignacion], [ComentarioAsignacion], [FechaFinalizacion], [ComentarioFinalizacion]) VALUES (1, N'RP001', 1, 2, CAST(N'2021-03-12' AS Date), N'Error en la pantalla del login', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [Reporte].[Reporte] ([Id], [Folio], [IdMotivo], [IdProyecto], [FechaReporte], [ComentarioReporte], [IdEstatusReporte], [UsuarioCreacion], [FechaCreacion], [UsuarioEdicion], [FechaEdicion], [UsuarioElimincion], [FechaEliminacion], [IdUsuarioAsignacion], [FechaAsignacion], [ComentarioAsignacion], [FechaFinalizacion], [ComentarioFinalizacion]) VALUES (2, N'RP002', 2, 1, CAST(N'2021-04-10' AS Date), N'Cambio de regla de negocio', 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [Reporte].[Reporte] ([Id], [Folio], [IdMotivo], [IdProyecto], [FechaReporte], [ComentarioReporte], [IdEstatusReporte], [UsuarioCreacion], [FechaCreacion], [UsuarioEdicion], [FechaEdicion], [UsuarioElimincion], [FechaEliminacion], [IdUsuarioAsignacion], [FechaAsignacion], [ComentarioAsignacion], [FechaFinalizacion], [ComentarioFinalizacion]) VALUES (3, N'RP003', 3, 3, CAST(N'2021-04-10' AS Date), N'Nueva regla de negocio', 3, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [Reporte].[Reporte] OFF
GO
SET IDENTITY_INSERT [Seguridad].[Modulo] ON 
GO
INSERT [Seguridad].[Modulo] ([Id], [Nombre], [Descripcion], [Activo], [Llave]) VALUES (1, N'Seguridad', NULL, 1, N'seguridad')
GO
INSERT [Seguridad].[Modulo] ([Id], [Nombre], [Descripcion], [Activo], [Llave]) VALUES (2, N'Catálogos', NULL, 1, N'catalogo')
GO
INSERT [Seguridad].[Modulo] ([Id], [Nombre], [Descripcion], [Activo], [Llave]) VALUES (3, N'Reportes', NULL, 1, N'reporte')
GO
SET IDENTITY_INSERT [Seguridad].[Modulo] OFF
GO
INSERT [Seguridad].[ModuloPantalla] ([IdModulo], [IdPantalla]) VALUES (1, 1)
GO
INSERT [Seguridad].[ModuloPantalla] ([IdModulo], [IdPantalla]) VALUES (1, 2)
GO
INSERT [Seguridad].[ModuloPantalla] ([IdModulo], [IdPantalla]) VALUES (2, 3)
GO
INSERT [Seguridad].[ModuloPantalla] ([IdModulo], [IdPantalla]) VALUES (2, 4)
GO
INSERT [Seguridad].[ModuloPantalla] ([IdModulo], [IdPantalla]) VALUES (3, 5)
GO
INSERT [Seguridad].[ModuloPantalla] ([IdModulo], [IdPantalla]) VALUES (3, 6)
GO
SET IDENTITY_INSERT [Seguridad].[Pantalla] ON 
GO
INSERT [Seguridad].[Pantalla] ([Id], [Nombre], [Descripcion], [Ruta], [Activo], [Llave]) VALUES (1, N'Usuarios', NULL, N'C/U', 1, N'usuario')
GO
INSERT [Seguridad].[Pantalla] ([Id], [Nombre], [Descripcion], [Ruta], [Activo], [Llave]) VALUES (2, N'Roles', NULL, N'C/R', 1, N'rol')
GO
INSERT [Seguridad].[Pantalla] ([Id], [Nombre], [Descripcion], [Ruta], [Activo], [Llave]) VALUES (3, N'Proyectos', NULL, N'C/P', 1, N'proyecto')
GO
INSERT [Seguridad].[Pantalla] ([Id], [Nombre], [Descripcion], [Ruta], [Activo], [Llave]) VALUES (4, N'Motivos', NULL, N'C/M', 1, N'motivo')
GO
INSERT [Seguridad].[Pantalla] ([Id], [Nombre], [Descripcion], [Ruta], [Activo], [Llave]) VALUES (5, N'Reportes', NULL, N'C/R', 1, N'reporte')
GO
INSERT [Seguridad].[Pantalla] ([Id], [Nombre], [Descripcion], [Ruta], [Activo], [Llave]) VALUES (6, N'ReportesAdministrador', NULL, N'C/A', 1, N'reporteadministrador')
GO
SET IDENTITY_INSERT [Seguridad].[Pantalla] OFF
GO
INSERT [Seguridad].[PantallaPermiso] ([IdPantalla], [IdPermiso]) VALUES (1, 1)
GO
INSERT [Seguridad].[PantallaPermiso] ([IdPantalla], [IdPermiso]) VALUES (1, 2)
GO
INSERT [Seguridad].[PantallaPermiso] ([IdPantalla], [IdPermiso]) VALUES (2, 1)
GO
INSERT [Seguridad].[PantallaPermiso] ([IdPantalla], [IdPermiso]) VALUES (2, 2)
GO
INSERT [Seguridad].[PantallaPermiso] ([IdPantalla], [IdPermiso]) VALUES (3, 1)
GO
INSERT [Seguridad].[PantallaPermiso] ([IdPantalla], [IdPermiso]) VALUES (3, 2)
GO
INSERT [Seguridad].[PantallaPermiso] ([IdPantalla], [IdPermiso]) VALUES (4, 1)
GO
INSERT [Seguridad].[PantallaPermiso] ([IdPantalla], [IdPermiso]) VALUES (4, 2)
GO
INSERT [Seguridad].[PantallaPermiso] ([IdPantalla], [IdPermiso]) VALUES (5, 1)
GO
INSERT [Seguridad].[PantallaPermiso] ([IdPantalla], [IdPermiso]) VALUES (5, 2)
GO
INSERT [Seguridad].[PantallaPermiso] ([IdPantalla], [IdPermiso]) VALUES (6, 1)
GO
INSERT [Seguridad].[PantallaPermiso] ([IdPantalla], [IdPermiso]) VALUES (6, 2)
GO
SET IDENTITY_INSERT [Seguridad].[Permiso] ON 
GO
INSERT [Seguridad].[Permiso] ([Id], [Nombre], [Descripcion], [Llave]) VALUES (1, N'Lectura', NULL, N'Read')
GO
INSERT [Seguridad].[Permiso] ([Id], [Nombre], [Descripcion], [Llave]) VALUES (2, N'Escritura', NULL, N'Write')
GO
SET IDENTITY_INSERT [Seguridad].[Permiso] OFF
GO
SET IDENTITY_INSERT [Seguridad].[Rol] ON 
GO
INSERT [Seguridad].[Rol] ([Id], [Nombre], [Descripcion], [FechaCreacion], [UsuarioCreacion], [FechaEdicion], [UsuarioEdicion], [FechaEliminacion], [UsuarioEliminacion], [Activo]) VALUES (1, N'Administrador', NULL, NULL, NULL, CAST(N'2021-04-28T00:25:47.070' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [Seguridad].[Rol] ([Id], [Nombre], [Descripcion], [FechaCreacion], [UsuarioCreacion], [FechaEdicion], [UsuarioEdicion], [FechaEliminacion], [UsuarioEliminacion], [Activo]) VALUES (2, N'Cliente', N'', CAST(N'2020-11-20T09:12:07.360' AS DateTime), 1, CAST(N'2021-04-28T00:21:57.713' AS DateTime), 1, CAST(N'2020-11-20T09:12:31.073' AS DateTime), 1, 1)
GO
SET IDENTITY_INSERT [Seguridad].[Rol] OFF
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (1, 1, 1, 1)
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (1, 1, 1, 2)
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (1, 1, 2, 1)
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (1, 1, 2, 2)
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (1, 2, 3, 1)
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (1, 2, 3, 2)
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (1, 2, 4, 1)
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (1, 2, 4, 2)
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (1, 3, 6, 1)
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (1, 3, 6, 2)
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (2, 3, 5, 1)
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (2, 3, 5, 2)
GO
SET IDENTITY_INSERT [Seguridad].[Usuario] ON 
GO
INSERT [Seguridad].[Usuario] ([Id], [Nombre], [ApellidoPaterno], [ApellidoMaterno], [FechaNacimiento], [FechaCreacion], [UsuarioCreacion], [FechaEdicion], [UsuarioEdicion], [FechaEliminacion], [UsuarioEliminacion], [Correo], [Contraseña], [Activo], [IdSeccion], [IdRolApp], [IdEstatusUsuario]) VALUES (1, N'Alan Roberto', N'Pitones', N'de León', CAST(N'2001-01-27T00:00:00.000' AS DateTime), CAST(N'2020-07-31T11:26:34.147' AS DateTime), NULL, CAST(N'2021-04-19T21:13:24.507' AS DateTime), 1, CAST(N'2020-11-02T18:33:18.990' AS DateTime), 2, N'alan.roberto@outlook.com', 0x8E489AA560F44CAD45D1942A1E001658B417534099B6670A74DF796CD70C32C2, 1, NULL, NULL, NULL)
GO
INSERT [Seguridad].[Usuario] ([Id], [Nombre], [ApellidoPaterno], [ApellidoMaterno], [FechaNacimiento], [FechaCreacion], [UsuarioCreacion], [FechaEdicion], [UsuarioEdicion], [FechaEliminacion], [UsuarioEliminacion], [Correo], [Contraseña], [Activo], [IdSeccion], [IdRolApp], [IdEstatusUsuario]) VALUES (2, N'Brandon', N'Vargas', N'Chapa', CAST(N'2021-04-07T00:00:00.000' AS DateTime), CAST(N'2021-04-19T19:26:48.873' AS DateTime), 1, CAST(N'2021-04-22T16:10:29.007' AS DateTime), 1, NULL, NULL, N'brandon@gmail.com', 0x8E489AA560F44CAD45D1942A1E001658B417534099B6670A74DF796CD70C32C2, 1, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [Seguridad].[Usuario] OFF
GO
SET IDENTITY_INSERT [Seguridad].[UsuarioCodigo] ON 
GO
INSERT [Seguridad].[UsuarioCodigo] ([Id], [IdUsuario], [IdTipoCodigo], [FechaCreacion], [Codigo], [Activo], [FechaVigencia]) VALUES (25, 29, 1, CAST(N'2020-11-03T23:38:58.443' AS DateTime), N'935445', 0, CAST(N'2020-11-03T23:53:58.443' AS DateTime))
GO
SET IDENTITY_INSERT [Seguridad].[UsuarioCodigo] OFF
GO
INSERT [Seguridad].[UsuarioContraseña] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'849bb8d2-40fc-43de-87c9-678e7db2fccb', 1, CAST(N'2021-04-22T00:01:25.530' AS DateTime), CAST(N'2021-04-22T00:16:25.530' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioContraseña] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'aeb15204-c0f5-45e8-a83d-8275df7203e4', 1, CAST(N'2021-04-21T23:55:52.457' AS DateTime), CAST(N'2021-04-22T00:10:52.457' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioContraseña] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'99bf2b1c-33fe-494d-8d7d-f7889f6a9de8', 1, CAST(N'2021-04-21T23:54:38.510' AS DateTime), CAST(N'2021-04-22T00:09:38.510' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioContraseña] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'c35fbe5e-9906-4b2c-be75-fac3d3e68181', 1, CAST(N'2021-04-22T00:01:47.200' AS DateTime), CAST(N'2021-04-22T00:16:47.200' AS DateTime), 1)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'ae63fa9e-2a85-44ce-8616-003376cb51a0', 29, CAST(N'2020-12-16T12:22:02.370' AS DateTime), CAST(N'2021-01-16T12:22:02.373' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'd35e13de-441c-4246-9ebe-0424420609f4', 1, CAST(N'2021-04-27T18:03:09.447' AS DateTime), CAST(N'2021-05-27T18:03:09.447' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'004aa208-cde3-4472-87df-0ec479eed149', 1, CAST(N'2021-04-28T00:35:45.307' AS DateTime), CAST(N'2021-05-28T00:35:45.307' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'8c088a9c-62b6-43eb-8652-1022fed4af9c', 1, CAST(N'2021-04-28T00:41:35.757' AS DateTime), CAST(N'2021-05-28T00:41:35.757' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'f4ec9538-f843-4717-b3ba-18d556de23a4', 1, CAST(N'2021-04-28T00:22:37.753' AS DateTime), CAST(N'2021-05-28T00:22:37.753' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'3db6ab57-77e6-474b-ba6d-1e010ceea673', 1, CAST(N'2021-04-28T00:01:59.777' AS DateTime), CAST(N'2021-05-28T00:01:59.777' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'a0a3bd9f-eb3a-4a22-94cd-1faaad7124f6', 1, CAST(N'2021-04-19T21:39:18.680' AS DateTime), CAST(N'2021-05-19T21:39:18.680' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'c042930c-f753-40da-9ade-212a6df9ebf5', 1, CAST(N'2021-04-20T00:36:44.430' AS DateTime), CAST(N'2021-05-20T00:36:44.430' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'8c14188a-5b8a-4514-8b42-2253d600daaa', 1, CAST(N'2021-04-28T00:07:04.323' AS DateTime), CAST(N'2021-05-28T00:07:04.323' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'fab37a69-8b72-41dd-94a8-23c60112c8b4', 1, CAST(N'2021-04-22T16:10:52.987' AS DateTime), CAST(N'2021-05-22T16:10:52.987' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'0b42645c-c999-426d-b7f9-2638b25986df', 1, CAST(N'2021-04-28T00:21:37.207' AS DateTime), CAST(N'2021-05-28T00:21:37.207' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'da3654bc-64e2-4be1-a67c-27f269e9d319', 1, CAST(N'2021-04-21T23:38:46.977' AS DateTime), CAST(N'2021-05-21T23:38:46.977' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'02079fe2-035a-4835-9189-31291e4f5532', 1, CAST(N'2021-04-19T19:35:09.627' AS DateTime), CAST(N'2021-05-19T19:35:09.647' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'182fc3e5-0e1d-4816-9c4f-367061422d67', 1, CAST(N'2021-04-21T23:20:08.460' AS DateTime), CAST(N'2021-05-21T23:20:08.460' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'4ead0d73-e79b-4185-8d7c-3a89fcaab071', 2, CAST(N'2021-04-22T16:11:14.220' AS DateTime), CAST(N'2021-05-22T16:11:14.220' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'4e801815-9f16-4e6a-96bd-3ded9b0960fb', 1, CAST(N'2021-04-19T16:27:22.527' AS DateTime), CAST(N'2021-05-19T16:27:22.547' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'4019cdb2-ff4a-489d-881d-52e6aafd12b8', 1, CAST(N'2021-04-27T16:30:19.543' AS DateTime), CAST(N'2021-05-27T16:30:19.560' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'f993ca60-4900-492d-9211-540452ff115e', 1, CAST(N'2021-04-30T18:06:21.893' AS DateTime), CAST(N'2021-05-30T18:06:21.907' AS DateTime), 1)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'6d73a408-b00e-41b6-89ec-56477b2fdc1c', 1, CAST(N'2021-04-19T21:49:37.427' AS DateTime), CAST(N'2021-05-19T21:49:37.427' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'28ee9c86-c042-47e5-bb9a-5e9877d25806', 2, CAST(N'2021-04-28T00:06:25.043' AS DateTime), CAST(N'2021-05-28T00:06:25.043' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'4485234d-8db0-4169-a9b4-6012b7bc2aeb', 1, CAST(N'2021-04-27T17:53:19.390' AS DateTime), CAST(N'2021-05-27T17:53:19.390' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'293d078b-2353-4543-85f1-6446a01f3e4b', 1, CAST(N'2021-04-20T00:31:42.967' AS DateTime), CAST(N'2021-05-20T00:31:42.967' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'583611f0-497a-49f2-9b82-6658458515d3', 2, CAST(N'2021-04-28T00:41:14.960' AS DateTime), CAST(N'2021-05-28T00:41:14.960' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'1c933187-0214-4275-824f-6bdac639432c', 1, CAST(N'2021-04-28T00:01:15.797' AS DateTime), CAST(N'2021-05-28T00:01:15.807' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'695458de-33e9-458e-82d5-76502ae74c68', 1, CAST(N'2021-04-19T22:17:01.433' AS DateTime), CAST(N'2021-05-19T22:17:01.433' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'9aaa096b-c534-42ca-a826-83b069b42da0', 1, CAST(N'2021-04-27T19:29:31.767' AS DateTime), CAST(N'2021-05-27T19:29:31.767' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'37cbe81f-549b-4120-92c8-8a887951f365', 1, CAST(N'2021-04-19T21:14:52.893' AS DateTime), CAST(N'2021-05-19T21:14:52.893' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'68774c6c-7b8f-4558-96d4-8b690aee4cad', 2, CAST(N'2021-04-28T00:01:37.000' AS DateTime), CAST(N'2021-05-28T00:01:37.000' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'c0e1075f-5bc2-4e14-82f3-8d08b019d405', 1, CAST(N'2021-04-19T21:38:57.093' AS DateTime), CAST(N'2021-05-19T21:38:57.110' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'0057e89e-a479-452f-be3d-910264f80f23', 1, CAST(N'2021-04-21T19:18:46.513' AS DateTime), CAST(N'2021-05-21T19:18:46.527' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'69a85863-f794-4d00-ac79-a6cbbbabd3e4', 1, CAST(N'2021-04-19T23:55:18.943' AS DateTime), CAST(N'2021-05-19T23:55:18.957' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'dba7719b-7cb7-44f4-89e2-accfc33b788e', 1, CAST(N'2021-04-28T00:24:04.570' AS DateTime), CAST(N'2021-05-28T00:24:04.570' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'54213b63-4e3d-423a-9803-b19798fab4e4', 1, CAST(N'2021-04-27T17:43:17.837' AS DateTime), CAST(N'2021-05-27T17:43:17.860' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'1c3a71f8-6b91-45fd-b5f2-b5a160ffdd6c', 1, CAST(N'2021-04-19T20:37:36.687' AS DateTime), CAST(N'2021-05-19T20:37:36.700' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'987babb3-6e57-4a32-a84b-b66bb4066941', 1, CAST(N'2021-04-19T19:25:03.287' AS DateTime), CAST(N'2021-05-19T19:25:03.310' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'9e0ffbb4-73ae-47c0-9008-b88b6bdbe71a', 1, CAST(N'2021-04-27T23:37:15.633' AS DateTime), CAST(N'2021-05-27T23:37:15.633' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'97fb1acc-6783-4d7b-afc4-c39db0ca2638', 1, CAST(N'2021-04-21T23:54:04.057' AS DateTime), CAST(N'2021-05-21T23:54:04.057' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'b1054925-1569-4306-97bc-c7349828324f', 1, CAST(N'2021-04-22T16:12:10.557' AS DateTime), CAST(N'2021-05-22T16:12:10.557' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'204889c7-3dd8-44bf-897a-cd94faad7417', 1, CAST(N'2021-04-28T00:40:16.063' AS DateTime), CAST(N'2021-05-28T00:40:16.063' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'6105940f-f228-4140-824b-d3cc48ae67b4', 1, CAST(N'2021-04-27T19:36:15.217' AS DateTime), CAST(N'2021-05-27T19:36:15.217' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'6e3ed27f-a134-4f0c-b849-d43a1e0f5c1b', 1, CAST(N'2021-04-27T23:36:04.287' AS DateTime), CAST(N'2021-05-27T23:36:04.290' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'cd5fb2b7-aa8f-4182-b5f1-d99f4bca56c3', 1, CAST(N'2021-04-28T00:25:13.520' AS DateTime), CAST(N'2021-05-28T00:25:13.520' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'582d70a5-ce27-47b9-ba58-daaba5634488', 1, CAST(N'2021-04-21T22:23:00.960' AS DateTime), CAST(N'2021-05-21T22:23:00.990' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'b7ddfd4e-343a-467f-8a6a-e1ae413afeb7', 2, CAST(N'2021-04-28T00:21:18.623' AS DateTime), CAST(N'2021-05-28T00:21:18.623' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'7dd4a349-5810-4736-aea0-e2975fb236e5', 1, CAST(N'2021-04-22T16:08:44.390' AS DateTime), CAST(N'2021-05-22T16:08:44.407' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'be756589-e7c1-42cd-98ac-eadca68cd6a7', 1, CAST(N'2021-04-22T00:04:23.863' AS DateTime), CAST(N'2021-05-22T00:04:23.863' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'80224f39-0bcb-4f91-a686-f45adc150a6c', 1, CAST(N'2021-04-28T00:31:25.237' AS DateTime), CAST(N'2021-05-28T00:31:25.237' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'4cb80f81-c839-4162-ae1b-f5dc5a3e2532', 1, CAST(N'2021-04-21T23:25:39.500' AS DateTime), CAST(N'2021-05-21T23:25:39.500' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'8be4006a-4c74-4fd5-b090-fcfdcb92f59a', 1, CAST(N'2021-04-19T21:20:10.167' AS DateTime), CAST(N'2021-05-19T21:20:10.167' AS DateTime), 0)
GO
INSERT [Seguridad].[UsuarioRol] ([IdUsuario], [IdRol]) VALUES (1, 1)
GO
INSERT [Seguridad].[UsuarioRol] ([IdUsuario], [IdRol]) VALUES (2, 2)
GO
/****** Object:  Index [PK_Permiso]    Script Date: 01/05/2021 03:39:27 p. m. ******/
ALTER TABLE [Seguridad].[Permiso] ADD  CONSTRAINT [PK_Permiso] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [Seguridad].[Modulo] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Seguridad].[Modulo] ADD  DEFAULT ('') FOR [Llave]
GO
ALTER TABLE [Seguridad].[Pantalla] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Seguridad].[Pantalla] ADD  DEFAULT ('') FOR [Llave]
GO
ALTER TABLE [Seguridad].[Rol] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Reporte].[Evidencia]  WITH CHECK ADD  CONSTRAINT [FK_Evidencia_Reporte_IdReporte] FOREIGN KEY([IdReporte])
REFERENCES [Reporte].[Reporte] ([Id])
GO
ALTER TABLE [Reporte].[Evidencia] CHECK CONSTRAINT [FK_Evidencia_Reporte_IdReporte]
GO
ALTER TABLE [Reporte].[Reporte]  WITH CHECK ADD  CONSTRAINT [FK_Reporte_Estatus_IdEstatusReporte] FOREIGN KEY([IdEstatusReporte])
REFERENCES [Configuracion].[Estatus] ([Id])
GO
ALTER TABLE [Reporte].[Reporte] CHECK CONSTRAINT [FK_Reporte_Estatus_IdEstatusReporte]
GO
ALTER TABLE [Reporte].[Reporte]  WITH CHECK ADD  CONSTRAINT [FK_Reporte_Motivo_IdMotivo] FOREIGN KEY([IdMotivo])
REFERENCES [Catalogo].[Motivo] ([Id])
GO
ALTER TABLE [Reporte].[Reporte] CHECK CONSTRAINT [FK_Reporte_Motivo_IdMotivo]
GO
ALTER TABLE [Reporte].[Reporte]  WITH CHECK ADD  CONSTRAINT [FK_Reporte_Proyecto_IdProyecto] FOREIGN KEY([IdProyecto])
REFERENCES [Catalogo].[Proyecto] ([Id])
GO
ALTER TABLE [Reporte].[Reporte] CHECK CONSTRAINT [FK_Reporte_Proyecto_IdProyecto]
GO
ALTER TABLE [Reporte].[Reporte]  WITH CHECK ADD  CONSTRAINT [FK_Reporte_Usuario_IdUsuarioAsignacion] FOREIGN KEY([IdUsuarioAsignacion])
REFERENCES [Seguridad].[Usuario] ([Id])
GO
ALTER TABLE [Reporte].[Reporte] CHECK CONSTRAINT [FK_Reporte_Usuario_IdUsuarioAsignacion]
GO
ALTER TABLE [Seguridad].[ModuloPantalla]  WITH CHECK ADD  CONSTRAINT [FK_ModuloPantalla_Modulo] FOREIGN KEY([IdModulo])
REFERENCES [Seguridad].[Modulo] ([Id])
GO
ALTER TABLE [Seguridad].[ModuloPantalla] CHECK CONSTRAINT [FK_ModuloPantalla_Modulo]
GO
ALTER TABLE [Seguridad].[PantallaPermiso]  WITH CHECK ADD  CONSTRAINT [FK_PantallaPermiso_Pantalla] FOREIGN KEY([IdPantalla])
REFERENCES [Seguridad].[Pantalla] ([Id])
GO
ALTER TABLE [Seguridad].[PantallaPermiso] CHECK CONSTRAINT [FK_PantallaPermiso_Pantalla]
GO
ALTER TABLE [Seguridad].[PantallaPermiso]  WITH CHECK ADD  CONSTRAINT [FK_PantallaPermiso_Permiso] FOREIGN KEY([IdPermiso])
REFERENCES [Seguridad].[Permiso] ([Id])
GO
ALTER TABLE [Seguridad].[PantallaPermiso] CHECK CONSTRAINT [FK_PantallaPermiso_Permiso]
GO
ALTER TABLE [Seguridad].[Rol]  WITH CHECK ADD  CONSTRAINT [FK_Rol_UsuarioCreacion] FOREIGN KEY([UsuarioCreacion])
REFERENCES [Seguridad].[Usuario] ([Id])
GO
ALTER TABLE [Seguridad].[Rol] CHECK CONSTRAINT [FK_Rol_UsuarioCreacion]
GO
ALTER TABLE [Seguridad].[Rol]  WITH CHECK ADD  CONSTRAINT [FK_Rol_UsuarioEliminacion] FOREIGN KEY([UsuarioEliminacion])
REFERENCES [Seguridad].[Usuario] ([Id])
GO
ALTER TABLE [Seguridad].[Rol] CHECK CONSTRAINT [FK_Rol_UsuarioEliminacion]
GO
ALTER TABLE [Seguridad].[RolModuloPantallaPermiso]  WITH CHECK ADD  CONSTRAINT [FK_RolModuloPantallaPermiso_Modulo] FOREIGN KEY([IdModulo])
REFERENCES [Seguridad].[Modulo] ([Id])
GO
ALTER TABLE [Seguridad].[RolModuloPantallaPermiso] CHECK CONSTRAINT [FK_RolModuloPantallaPermiso_Modulo]
GO
ALTER TABLE [Seguridad].[RolModuloPantallaPermiso]  WITH CHECK ADD  CONSTRAINT [FK_RolModuloPantallaPermiso_Pantalla] FOREIGN KEY([IdPantalla])
REFERENCES [Seguridad].[Pantalla] ([Id])
GO
ALTER TABLE [Seguridad].[RolModuloPantallaPermiso] CHECK CONSTRAINT [FK_RolModuloPantallaPermiso_Pantalla]
GO
ALTER TABLE [Seguridad].[RolModuloPantallaPermiso]  WITH CHECK ADD  CONSTRAINT [FK_RolModuloPantallaPermiso_Permiso] FOREIGN KEY([IdPermiso])
REFERENCES [Seguridad].[Permiso] ([Id])
GO
ALTER TABLE [Seguridad].[RolModuloPantallaPermiso] CHECK CONSTRAINT [FK_RolModuloPantallaPermiso_Permiso]
GO
ALTER TABLE [Seguridad].[RolModuloPantallaPermiso]  WITH CHECK ADD  CONSTRAINT [FK_RolModuloPantallaPermiso_Rol] FOREIGN KEY([IdRol])
REFERENCES [Seguridad].[Rol] ([Id])
GO
ALTER TABLE [Seguridad].[RolModuloPantallaPermiso] CHECK CONSTRAINT [FK_RolModuloPantallaPermiso_Rol]
GO
ALTER TABLE [Seguridad].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_RolApp] FOREIGN KEY([IdRolApp])
REFERENCES [Seguridad].[RolApp] ([Id])
GO
ALTER TABLE [Seguridad].[Usuario] CHECK CONSTRAINT [FK_Usuario_RolApp]
GO
ALTER TABLE [Seguridad].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_UsuarioCreacion] FOREIGN KEY([UsuarioCreacion])
REFERENCES [Seguridad].[Usuario] ([Id])
GO
ALTER TABLE [Seguridad].[Usuario] CHECK CONSTRAINT [FK_Usuario_UsuarioCreacion]
GO
ALTER TABLE [Seguridad].[UsuarioContraseña]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioContraseña_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [Seguridad].[Usuario] ([Id])
GO
ALTER TABLE [Seguridad].[UsuarioContraseña] CHECK CONSTRAINT [FK_UsuarioContraseña_Usuario]
GO
ALTER TABLE [Seguridad].[UsuarioRol]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioRol_Rol] FOREIGN KEY([IdRol])
REFERENCES [Seguridad].[Rol] ([Id])
GO
ALTER TABLE [Seguridad].[UsuarioRol] CHECK CONSTRAINT [FK_UsuarioRol_Rol]
GO
ALTER TABLE [Seguridad].[UsuarioRol]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioRol_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [Seguridad].[Usuario] ([Id])
GO
ALTER TABLE [Seguridad].[UsuarioRol] CHECK CONSTRAINT [FK_UsuarioRol_Usuario]
GO
USE [master]
GO
ALTER DATABASE [smafacpya] SET  READ_WRITE 
GO
