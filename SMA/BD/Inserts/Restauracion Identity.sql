DBCC CHECKIDENT ('NombreTabla', RESEED, IdAnteriorAlqueQuieresRestaurar);
GO

--Resetear tabla proyectos

DBCC CHECKIDENT ('Catalogo.Proyecto', RESEED, 0);
GO

--Resetear tabla motivos

DBCC CHECKIDENT ('Catalogo.Motivo', RESEED, 0);
GO

--Resetear tabla reportes

DBCC CHECKIDENT ('Reporte.Reporte', RESEED, 0);
GO

--Ejemplo quer