USE smafacpya
GO

CREATE SCHEMA Catalogo
GO

CREATE SCHEMA Reporte
GO

CREATE SCHEMA Configuracion
GO

CREATE TABLE Catalogo.Motivo
(
	Id						INT IDENTITY(1,1),
	Motivo					VARCHAR(100),
	Descripcion				VARCHAR(250),
	Activo					BIT,
	CONSTRAINT PK_Motivo_Id	PRIMARY KEY(Id)
)

CREATE TABLE Catalogo.Proyecto
(
	Id						INT IDENTITY(1,1),
	Nombre					VARCHAR(50),
	Descripcion				VARCHAR(250),
	Activo					BIT,
	CONSTRAINT PK_Proyecto_Id PRIMARY KEY(Id)
)

CREATE TABLE Configuracion.Estatus
(
	Id						INT IDENTITY(1,1),
	Nombre					VARCHAR(40),
	Llave					VARCHAR(40),
	CONSTRAINT PK_Estatus_Id PRIMARY KEY(Id)
)

CREATE TABLE Reporte.Reporte
(
	Id						INT IDENTITY(1,1),
	Folio					VARCHAR(100),
	IdMotivo				INT,
	IdProyecto				INT,
	FechaReporte			DATE,
	ComentarioReporte		VARCHAR(250),
	IdEstatusReporte		INT,
	UsuarioCreacion			INT,
	FechaCreacion			DATETIME,
	UsuarioEdicion			INT,
	FechaEdicion			DATETIME,
	UsuarioElimincion		INT,
	FechaEliminacion		DATETIME,
	IdUsuarioAsignacion		INT,
	FechaAsignacion			DATETIME,
	ComentarioAsignacion	VARCHAR(250),
	FechaFinalizacion		DATETIME,
	ComentarioFinalizacion	VARCHAR(250),
	CONSTRAINT PK_Reporte_Id PRIMARY KEY(Id),
	CONSTRAINT FK_Reporte_Motivo_IdMotivo FOREIGN KEY(IdMotivo)
	REFERENCES Catalogo.Motivo(Id),
	CONSTRAINT FK_Reporte_Proyecto_IdProyecto FOREIGN KEY(IdProyecto)
	REFERENCES Catalogo.Proyecto(Id),
	CONSTRAINT FK_Reporte_Estatus_IdEstatusReporte FOREIGN KEY(IdEstatusReporte)
	REFERENCES Configuracion.Estatus(Id),
	CONSTRAINT FK_Reporte_Usuario_IdUsuarioAsignacion FOREIGN KEY(IdUsuarioAsignacion)
	REFERENCES Seguridad.Usuario(Id)
)

CREATE TABLE Reporte.Evidencia
(
	Id						INT IDENTITY(1,1),
	IdReporte				INT,
	Nombre					VARCHAR(50),
	Extension				VARCHAR(10),	
	Imagen					VARBINARY(MAX),
	CONSTRAINT PK_Evidencia_Id PRIMARY KEY(Id),
	CONSTRAINT FK_Evidencia_Reporte_IdReporte FOREIGN KEY(IdReporte)
	REFERENCES Reporte.Reporte(Id)
)
