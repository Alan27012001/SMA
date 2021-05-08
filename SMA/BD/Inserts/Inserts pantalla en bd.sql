
SELECT * FROM Seguridad.Rol
SELECT * FROM Seguridad.Modulo
SELECT * FROM Seguridad.Pantalla
SELECT * FROM Seguridad.ModuloPantalla
SELECT * FROM Seguridad.Permiso
SELECT * FROM Seguridad.PantallaPermiso
SELECT * FROM Seguridad.RolModuloPantallaPermiso


INSERT INTO Seguridad.Pantalla VALUES('Motivos de Reporte', NULL,'C/M',1,'motivosreporte')

INSERT INTO Seguridad.ModuloPantalla VALUES(2,3)
INSERT INTO Seguridad.PantallaPermiso VALUES(3,1)
INSERT INTO Seguridad.PantallaPermiso VALUES(3,2)
INSERT INTO Seguridad.RolModuloPantallaPermiso VALUES(1,2,3,1)
INSERT INTO Seguridad.RolModuloPantallaPermiso VALUES(1,2,3,2)

--Catalogos
INSERT INTO Seguridad.Pantalla VALUES('Proyectos', NULL,'C/P',1,'proyecto')
INSERT INTO Seguridad.Pantalla VALUES('Motivos', NULL,'C/M',1,'motivo')

INSERT INTO Seguridad.ModuloPantalla VALUES(2,3)
INSERT INTO Seguridad.ModuloPantalla VALUES(2,4)

INSERT INTO Seguridad.PantallaPermiso VALUES(3,1)
INSERT INTO Seguridad.PantallaPermiso VALUES(3,2)
INSERT INTO Seguridad.PantallaPermiso VALUES(4,1)
INSERT INTO Seguridad.PantallaPermiso VALUES(4,2)

INSERT INTO Seguridad.RolModuloPantallaPermiso VALUES(1,2,3,1)
INSERT INTO Seguridad.RolModuloPantallaPermiso VALUES(1,2,3,2)
INSERT INTO Seguridad.RolModuloPantallaPermiso VALUES(1,2,4,1)
INSERT INTO Seguridad.RolModuloPantallaPermiso VALUES(1,2,4,2)

--Modulo de Reporte
INSERT INTO Seguridad.Modulo VALUES('Reportes',NULL,1,'reporte')

--Resetear el id del identity
DBCC CHECKIDENT ('Seguridad.Pantalla', RESEED, 4)

--Pantalla de Reportes

INSERT INTO Seguridad.Pantalla VALUES('Reportes', NULL,'C/R',1,'reporte')
INSERT INTO Seguridad.Pantalla VALUES('ReportesAdministrador', NULL,'C/A',1,'reporteadministrador')


INSERT INTO Seguridad.ModuloPantalla VALUES(3,5)
INSERT INTO Seguridad.ModuloPantalla VALUES(3,6)

INSERT INTO Seguridad.PantallaPermiso VALUES(5,1)
INSERT INTO Seguridad.PantallaPermiso VALUES(5,2)
INSERT INTO Seguridad.PantallaPermiso VALUES(6,1)
INSERT INTO Seguridad.PantallaPermiso VALUES(6,2)

INSERT INTO Seguridad.RolModuloPantallaPermiso VALUES(2,3,5,1)
INSERT INTO Seguridad.RolModuloPantallaPermiso VALUES(2,3,5,2)
INSERT INTO Seguridad.RolModuloPantallaPermiso VALUES(1,3,6,1)
INSERT INTO Seguridad.RolModuloPantallaPermiso VALUES(1,3,6,2)

