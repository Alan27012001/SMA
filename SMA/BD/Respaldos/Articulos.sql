USE smafacpya
GO
/****** Object:  Table [Seguridad].[Modulo]    Script Date: 19/04/2021 03:12:50 p. m. ******/
CREATE SCHEMA Seguridad
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
/****** Object:  Table [Seguridad].[ModuloPantalla]    Script Date: 19/04/2021 03:12:51 p. m. ******/
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
/****** Object:  Table [Seguridad].[Pantalla]    Script Date: 19/04/2021 03:12:51 p. m. ******/
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
/****** Object:  Table [Seguridad].[PantallaPermiso]    Script Date: 19/04/2021 03:12:51 p. m. ******/
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
/****** Object:  Table [Seguridad].[Permiso]    Script Date: 19/04/2021 03:12:51 p. m. ******/
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
/****** Object:  Table [Seguridad].[Rol]    Script Date: 19/04/2021 03:12:51 p. m. ******/
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
/****** Object:  Table [Seguridad].[RolApp]    Script Date: 19/04/2021 03:12:51 p. m. ******/
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
/****** Object:  Table [Seguridad].[RolModuloPantallaPermiso]    Script Date: 19/04/2021 03:12:51 p. m. ******/
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
/****** Object:  Table [Seguridad].[Usuario]    Script Date: 19/04/2021 03:12:51 p. m. ******/
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
/****** Object:  Table [Seguridad].[UsuarioCodigo]    Script Date: 19/04/2021 03:12:51 p. m. ******/
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
/****** Object:  Table [Seguridad].[UsuarioContraseña]    Script Date: 19/04/2021 03:12:51 p. m. ******/
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
/****** Object:  Table [Seguridad].[UsuarioLogin]    Script Date: 19/04/2021 03:12:51 p. m. ******/
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
/****** Object:  Table [Seguridad].[UsuarioRol]    Script Date: 19/04/2021 03:12:51 p. m. ******/
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
SET IDENTITY_INSERT [Seguridad].[Modulo] ON 
GO
INSERT [Seguridad].[Modulo] ([Id], [Nombre], [Descripcion], [Activo], [Llave]) VALUES (1, N'Seguridad', NULL, 1, N'')
GO
INSERT [Seguridad].[Modulo] ([Id], [Nombre], [Descripcion], [Activo], [Llave]) VALUES (2, N'Catálogos', NULL, 1, N'')
GO
SET IDENTITY_INSERT [Seguridad].[Modulo] OFF
GO
INSERT [Seguridad].[ModuloPantalla] ([IdModulo], [IdPantalla]) VALUES (1, 1)
GO
INSERT [Seguridad].[ModuloPantalla] ([IdModulo], [IdPantalla]) VALUES (1, 2)
GO
INSERT [Seguridad].[ModuloPantalla] ([IdModulo], [IdPantalla]) VALUES (2, 5)
GO
INSERT [Seguridad].[ModuloPantalla] ([IdModulo], [IdPantalla]) VALUES (2, 6)
GO
INSERT [Seguridad].[ModuloPantalla] ([IdModulo], [IdPantalla]) VALUES (2, 7)
GO
INSERT [Seguridad].[ModuloPantalla] ([IdModulo], [IdPantalla]) VALUES (2, 8)
GO
INSERT [Seguridad].[ModuloPantalla] ([IdModulo], [IdPantalla]) VALUES (2, 9)
GO
INSERT [Seguridad].[ModuloPantalla] ([IdModulo], [IdPantalla]) VALUES (2, 10)
GO
SET IDENTITY_INSERT [Seguridad].[Pantalla] ON 
GO
INSERT [Seguridad].[Pantalla] ([Id], [Nombre], [Descripcion], [Ruta], [Activo], [Llave]) VALUES (1, N'Usuarios', NULL, N'C/U', 1, N'usuario')
GO
INSERT [Seguridad].[Pantalla] ([Id], [Nombre], [Descripcion], [Ruta], [Activo], [Llave]) VALUES (2, N'Roles', NULL, N'C/R', 1, N'rol')
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
INSERT [Seguridad].[Rol] ([Id], [Nombre], [Descripcion], [FechaCreacion], [UsuarioCreacion], [FechaEdicion], [UsuarioEdicion], [FechaEliminacion], [UsuarioEliminacion], [Activo]) VALUES (1, N'Administrador', NULL, NULL, NULL, CAST(N'2020-12-17T12:54:29.507' AS DateTime), 2, NULL, NULL, 1)
GO
INSERT [Seguridad].[Rol] ([Id], [Nombre], [Descripcion], [FechaCreacion], [UsuarioCreacion], [FechaEdicion], [UsuarioEdicion], [FechaEliminacion], [UsuarioEliminacion], [Activo]) VALUES (2, N'Cliente', N'', CAST(N'2020-11-20T09:12:07.360' AS DateTime), 1, CAST(N'2020-12-17T10:11:03.737' AS DateTime), 2, CAST(N'2020-11-20T09:12:31.073' AS DateTime), 1, 1)
GO
SET IDENTITY_INSERT [Seguridad].[Rol] OFF
GO
SET IDENTITY_INSERT [Seguridad].[RolApp] ON 
GO
SET IDENTITY_INSERT [Seguridad].[RolApp] OFF
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (1, 1, 1, 1)
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (1, 1, 1, 2)
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (1, 1, 2, 1)
GO
INSERT [Seguridad].[RolModuloPantallaPermiso] ([IdRol], [IdModulo], [IdPantalla], [IdPermiso]) VALUES (1, 1, 2, 2)
GO

SET IDENTITY_INSERT [Seguridad].[Usuario] ON 
GO
INSERT [Seguridad].[Usuario] ([Id], [Nombre], [ApellidoPaterno], [ApellidoMaterno], [FechaNacimiento], [FechaCreacion], [UsuarioCreacion], [FechaEdicion], [UsuarioEdicion], [FechaEliminacion], [UsuarioEliminacion], [Correo], [Contraseña], [Activo], [IdSeccion], [IdRolApp], [IdEstatusUsuario]) VALUES (1, N'Alan Roberto', N'Pitones', N'de León', CAST(N'1991-10-16T00:00:00.000' AS DateTime), CAST(N'2020-07-31T11:26:34.147' AS DateTime), NULL, CAST(N'2020-11-02T18:33:28.223' AS DateTime), 2, CAST(N'2020-11-02T18:33:18.990' AS DateTime), 2, N'alan.roberto@outlook.com', 0x8E489AA560F44CAD45D1942A1E001658B417534099B6670A74DF796CD70C32C2, 1, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [Seguridad].[Usuario] OFF
GO
SET IDENTITY_INSERT [Seguridad].[UsuarioCodigo] ON 
GO
INSERT [Seguridad].[UsuarioCodigo] ([Id], [IdUsuario], [IdTipoCodigo], [FechaCreacion], [Codigo], [Activo], [FechaVigencia]) VALUES (25, 29, 1, CAST(N'2020-11-03T23:38:58.443' AS DateTime), N'935445', 0, CAST(N'2020-11-03T23:53:58.443' AS DateTime))
GO
SET IDENTITY_INSERT [Seguridad].[UsuarioCodigo] OFF
GO
INSERT [Seguridad].[UsuarioLogin] ([Id], [IdUsuario], [FechaCreacion], [FechaVigencia], [Activo]) VALUES (N'ae63fa9e-2a85-44ce-8616-003376cb51a0', 29, CAST(N'2020-12-16T12:22:02.370' AS DateTime), CAST(N'2021-01-16T12:22:02.373' AS DateTime), 0)
GO

INSERT [Seguridad].[UsuarioRol] ([IdUsuario], [IdRol]) VALUES (1, 1)
GO
/****** Object:  Index [PK_Permiso]    Script Date: 19/04/2021 03:12:56 p. m. ******/
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
ALTER TABLE [Seguridad].[ModuloPantalla]  WITH CHECK ADD  CONSTRAINT [FK_ModuloPantalla_Modulo] FOREIGN KEY([IdModulo])
REFERENCES [Seguridad].[Modulo] ([Id])
GO
ALTER TABLE [Seguridad].[ModuloPantalla] CHECK CONSTRAINT [FK_ModuloPantalla_Modulo]
GO
ALTER TABLE [Seguridad].[ModuloPantalla]  WITH CHECK ADD  CONSTRAINT [FK_ModuloPantalla_Pantalla] FOREIGN KEY([IdPantalla])
REFERENCES [Seguridad].[Pantalla] ([Id])
GO
ALTER TABLE [Seguridad].[ModuloPantalla] CHECK CONSTRAINT [FK_ModuloPantalla_Pantalla]
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
ALTER TABLE [Seguridad].[Rol]  WITH CHECK ADD  CONSTRAINT [FK_Rol_UsuarioEdicion] FOREIGN KEY([UsuarioEdicion])
REFERENCES [Seguridad].[Usuario] ([Id])
GO
ALTER TABLE [Seguridad].[Rol] CHECK CONSTRAINT [FK_Rol_UsuarioEdicion]
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
ALTER TABLE [Seguridad].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_EstatusUsuario] FOREIGN KEY([IdEstatusUsuario])
REFERENCES [Configuracion].[EstatusUsuario] ([Id])
GO
ALTER TABLE [Seguridad].[Usuario] CHECK CONSTRAINT [FK_Usuario_EstatusUsuario]
GO
ALTER TABLE [Seguridad].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_RolApp] FOREIGN KEY([IdRolApp])
REFERENCES [Seguridad].[RolApp] ([Id])
GO
ALTER TABLE [Seguridad].[Usuario] CHECK CONSTRAINT [FK_Usuario_RolApp]
GO
ALTER TABLE [Seguridad].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Seccion] FOREIGN KEY([IdSeccion])
REFERENCES [Catalogo].[Seccion] ([Id])
GO
ALTER TABLE [Seguridad].[Usuario] CHECK CONSTRAINT [FK_Usuario_Seccion]
GO
ALTER TABLE [Seguridad].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_UsuarioCreacion] FOREIGN KEY([UsuarioCreacion])
REFERENCES [Seguridad].[Usuario] ([Id])
GO
ALTER TABLE [Seguridad].[Usuario] CHECK CONSTRAINT [FK_Usuario_UsuarioCreacion]
GO
ALTER TABLE [Seguridad].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_UsuarioEdicion] FOREIGN KEY([UsuarioEdicion])
REFERENCES [Seguridad].[Usuario] ([Id])
GO
ALTER TABLE [Seguridad].[Usuario] CHECK CONSTRAINT [FK_Usuario_UsuarioEdicion]
GO
ALTER TABLE [Seguridad].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_UsuarioEliminacion] FOREIGN KEY([UsuarioEliminacion])
REFERENCES [Seguridad].[Usuario] ([Id])
GO
ALTER TABLE [Seguridad].[Usuario] CHECK CONSTRAINT [FK_Usuario_UsuarioEliminacion]
GO
ALTER TABLE [Seguridad].[UsuarioCodigo]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioCodigo_TipoCodigo] FOREIGN KEY([IdTipoCodigo])
REFERENCES [Configuracion].[TipoCodigo] ([Id])
GO
ALTER TABLE [Seguridad].[UsuarioCodigo] CHECK CONSTRAINT [FK_UsuarioCodigo_TipoCodigo]
GO
ALTER TABLE [Seguridad].[UsuarioCodigo]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioCodigo_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [Seguridad].[Usuario] ([Id])
GO
ALTER TABLE [Seguridad].[UsuarioCodigo] CHECK CONSTRAINT [FK_UsuarioCodigo_Usuario]
GO
ALTER TABLE [Seguridad].[UsuarioContraseña]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioContraseña_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [Seguridad].[Usuario] ([Id])
GO
ALTER TABLE [Seguridad].[UsuarioContraseña] CHECK CONSTRAINT [FK_UsuarioContraseña_Usuario]
GO
ALTER TABLE [Seguridad].[UsuarioLogin]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioLogin_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [Seguridad].[Usuario] ([Id])
GO
ALTER TABLE [Seguridad].[UsuarioLogin] CHECK CONSTRAINT [FK_UsuarioLogin_Usuario]
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
