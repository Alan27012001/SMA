SELECT * FROM Reporte.Reporte
--Eliminar los reportes
DELETE FROM Reporte.Reporte

SELECT * FROM Catalogo.Proyecto
--Eliminar los proyectos
DELETE FROM Catalogo.Proyecto

SELECT * FROM Catalogo.Motivo
--Eliminar los motivos
DELETE FROM Catalogo.Motivo

SELECT * FROM Seguridad.Usuario

SELECT * FROM Seguridad.UsuarioRol
--Eliminar usuario roles
DELETE FROM Seguridad.UsuarioRol
WHERE IdUsuario > 2

SELECT * FROM Seguridad.Rol
--Eliminar rol
DELETE FROM Seguridad.Rol
WHERE Id = 3

SELECT * FROM Seguridad.UsuarioLogin
--Eliminar usuarios login
DELETE FROM Seguridad.UsuarioLogin

SELECT * FROM Seguridad.PantallaPermiso

SELECT * FROM Seguridad.RolModuloPantallaPermiso
--Eliminar RolModuloPantallaPermiso
DELETE FROM Seguridad.RolModuloPantallaPermiso
WHERE IdRol = 3