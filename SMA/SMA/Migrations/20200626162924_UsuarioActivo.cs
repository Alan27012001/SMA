using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SMA.Migrations
{
    public partial class UsuarioActivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalogo");

            migrationBuilder.EnsureSchema(
                name: "Seguridad");

            migrationBuilder.CreateTable(
                name: "TipoArticulo",
                schema: "Catalogo",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoArticulo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modulo",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pantalla",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Ruta = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pantalla", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permiso",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Llave = table.Column<byte[]>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permiso", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    ApellidoPaterno = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    ApellidoMaterno = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioCreacion = table.Column<int>(nullable: true),
                    FechaEdicion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioEdicion = table.Column<int>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioEliminacion = table.Column<int>(nullable: true),
                    Correo = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    Contraseña = table.Column<byte[]>(nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_UsuarioCreacion",
                        column: x => x.UsuarioCreacion,
                        principalSchema: "Seguridad",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuario_UsuarioEdicion",
                        column: x => x.UsuarioEdicion,
                        principalSchema: "Seguridad",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuario_UsuarioEliminacion",
                        column: x => x.UsuarioEliminacion,
                        principalSchema: "Seguridad",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Articulo",
                schema: "Catalogo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Precio = table.Column<decimal>(type: "money", nullable: false),
                    IdTipoArticulo = table.Column<short>(nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articulo_TipoArticulo",
                        column: x => x.IdTipoArticulo,
                        principalSchema: "Catalogo",
                        principalTable: "TipoArticulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModuloPantalla",
                schema: "Seguridad",
                columns: table => new
                {
                    IdModulo = table.Column<short>(nullable: false),
                    IdPantalla = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguridad.ModuloPantalla", x => new { x.IdModulo, x.IdPantalla });
                    table.ForeignKey(
                        name: "FK_ModuloPantalla_Modulo",
                        column: x => x.IdModulo,
                        principalSchema: "Seguridad",
                        principalTable: "Modulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModuloPantalla_Pantalla",
                        column: x => x.IdPantalla,
                        principalSchema: "Seguridad",
                        principalTable: "Pantalla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PantallaPermiso",
                schema: "Seguridad",
                columns: table => new
                {
                    IdPantalla = table.Column<short>(nullable: false),
                    IdPermiso = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PantallaPermiso", x => new { x.IdPantalla, x.IdPermiso });
                    table.ForeignKey(
                        name: "FK_PantallaPermiso_Pantalla",
                        column: x => x.IdPantalla,
                        principalSchema: "Seguridad",
                        principalTable: "Pantalla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PantallaPermiso_Permiso",
                        column: x => x.IdPermiso,
                        principalSchema: "Seguridad",
                        principalTable: "Permiso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioCreacion = table.Column<int>(nullable: true),
                    FechaEdicion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioEdicion = table.Column<int>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioEliminacion = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rol_UsuarioCreacion",
                        column: x => x.UsuarioCreacion,
                        principalSchema: "Seguridad",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rol_UsuarioEdicion",
                        column: x => x.UsuarioEdicion,
                        principalSchema: "Seguridad",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rol_UsuarioEliminacion",
                        column: x => x.UsuarioEliminacion,
                        principalSchema: "Seguridad",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolModuloPantallaPermiso",
                schema: "Seguridad",
                columns: table => new
                {
                    IdRol = table.Column<short>(nullable: false),
                    IdModulo = table.Column<short>(nullable: false),
                    IdPantalla = table.Column<short>(nullable: false),
                    IdPermiso = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolModuloPantallaPermiso", x => new { x.IdRol, x.IdModulo, x.IdPantalla, x.IdPermiso });
                    table.ForeignKey(
                        name: "FK_RolModuloPantallaPermiso_Modulo",
                        column: x => x.IdModulo,
                        principalSchema: "Seguridad",
                        principalTable: "Modulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolModuloPantallaPermiso_Pantalla",
                        column: x => x.IdPantalla,
                        principalSchema: "Seguridad",
                        principalTable: "Pantalla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolModuloPantallaPermiso_Permiso",
                        column: x => x.IdPermiso,
                        principalSchema: "Seguridad",
                        principalTable: "Permiso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolModuloPantallaPermiso_Rol",
                        column: x => x.IdRol,
                        principalSchema: "Seguridad",
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                schema: "Seguridad",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false),
                    IdRol = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRol", x => new { x.IdUsuario, x.IdRol });
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Rol",
                        column: x => x.IdRol,
                        principalSchema: "Seguridad",
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Usuario",
                        column: x => x.IdUsuario,
                        principalSchema: "Seguridad",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_IdTipoArticulo",
                schema: "Catalogo",
                table: "Articulo",
                column: "IdTipoArticulo");

            migrationBuilder.CreateIndex(
                name: "UK_Modulo",
                schema: "Seguridad",
                table: "Modulo",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModuloPantalla_IdPantalla",
                schema: "Seguridad",
                table: "ModuloPantalla",
                column: "IdPantalla");

            migrationBuilder.CreateIndex(
                name: "UK_Pantalla",
                schema: "Seguridad",
                table: "Pantalla",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PantallaPermiso_IdPermiso",
                schema: "Seguridad",
                table: "PantallaPermiso",
                column: "IdPermiso");

            migrationBuilder.CreateIndex(
                name: "UK_Permiso",
                schema: "Seguridad",
                table: "Permiso",
                column: "Llave",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Rol_UsuarioCreacion",
                schema: "Seguridad",
                table: "Rol",
                column: "UsuarioCreacion");

            migrationBuilder.CreateIndex(
                name: "IX_Rol_UsuarioEdicion",
                schema: "Seguridad",
                table: "Rol",
                column: "UsuarioEdicion");

            migrationBuilder.CreateIndex(
                name: "IX_Rol_UsuarioEliminacion",
                schema: "Seguridad",
                table: "Rol",
                column: "UsuarioEliminacion");

            migrationBuilder.CreateIndex(
                name: "IX_RolModuloPantallaPermiso_IdModulo",
                schema: "Seguridad",
                table: "RolModuloPantallaPermiso",
                column: "IdModulo");

            migrationBuilder.CreateIndex(
                name: "IX_RolModuloPantallaPermiso_IdPantalla",
                schema: "Seguridad",
                table: "RolModuloPantallaPermiso",
                column: "IdPantalla");

            migrationBuilder.CreateIndex(
                name: "IX_RolModuloPantallaPermiso_IdPermiso",
                schema: "Seguridad",
                table: "RolModuloPantallaPermiso",
                column: "IdPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UsuarioCreacion",
                schema: "Seguridad",
                table: "Usuario",
                column: "UsuarioCreacion");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UsuarioEdicion",
                schema: "Seguridad",
                table: "Usuario",
                column: "UsuarioEdicion");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UsuarioEliminacion",
                schema: "Seguridad",
                table: "Usuario",
                column: "UsuarioEliminacion");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_IdRol",
                schema: "Seguridad",
                table: "UsuarioRol",
                column: "IdRol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articulo",
                schema: "Catalogo");

            migrationBuilder.DropTable(
                name: "ModuloPantalla",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "PantallaPermiso",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "RolModuloPantallaPermiso",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "UsuarioRol",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "TipoArticulo",
                schema: "Catalogo");

            migrationBuilder.DropTable(
                name: "Modulo",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Pantalla",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Permiso",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Rol",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "Seguridad");
        }
    }
}
