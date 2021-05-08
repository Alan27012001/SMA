USE smafacpya


SELECT * FROM Reporte.Reporte
SELECT * FROM Catalogo.Proyecto
SELECT * FROM Catalogo.Motivo
SELECT * FROM Reporte.Reporte
SELECT * FROM Configuracion.Estatus

INSERT INTO Configuracion.Estatus VALUES('Abierto','abierto')
INSERT INTO Configuracion.Estatus VALUES('Asignado','asignado')
INSERT INTO Configuracion.Estatus VALUES('Cerrado','cerrado')


INSERT INTO Reporte.Reporte VALUES('RP001',1,2,'2021-03-12','Error en la pantalla del login',1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)

INSERT INTO Reporte.Reporte VALUES('RP002',2,1,'2021-04-10','Cambio de regla de negocio',2,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)

INSERT INTO Reporte.Reporte VALUES('RP003',3,3,'2021-04-10','Nueva regla de negocio',3,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)