using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppForSEII2526.API.Migrations
{
    /// <inheritdoc />
    public partial class CreateIdentitySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    correoElectronico = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    numTelefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fabricante",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fabricante", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "alquilar",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    direccionEnvio = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fechaAlquiler = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    precioTotal = table.Column<float>(type: "real", maxLength: 30, nullable: false),
                    metodoPago = table.Column<int>(type: "int", nullable: false),
                    applicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alquilar", x => x.id);
                    table.ForeignKey(
                        name: "FK_alquilar_AspNetUsers_applicationUserId",
                        column: x => x.applicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    direccionEnvio = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    precioTotal = table.Column<float>(type: "real", nullable: true),
                    metodopago = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compra_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oferta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaOferta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    metodoPago = table.Column<int>(type: "int", nullable: false),
                    paraSocio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oferta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oferta_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reparacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaRecogida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    precioTotal = table.Column<float>(type: "real", nullable: false),
                    metodoPago = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reparacion_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Herramienta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    material = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    precio = table.Column<float>(type: "real", nullable: false),
                    tiempoReparacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fabricanteid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Herramienta", x => x.id);
                    table.ForeignKey(
                        name: "FK_Herramienta_fabricante_fabricanteid",
                        column: x => x.fabricanteid,
                        principalTable: "fabricante",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "alquilarItem",
                columns: table => new
                {
                    alquilarid = table.Column<int>(type: "int", nullable: false),
                    herramientaid = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    precio = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alquilarItem", x => new { x.alquilarid, x.herramientaid });
                    table.ForeignKey(
                        name: "FK_alquilarItem_Herramienta_herramientaid",
                        column: x => x.herramientaid,
                        principalTable: "Herramienta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_alquilarItem_alquilar_alquilarid",
                        column: x => x.alquilarid,
                        principalTable: "alquilar",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComprarItem",
                columns: table => new
                {
                    compraId = table.Column<int>(type: "int", nullable: false),
                    herramientaid = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    precio = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprarItem", x => new { x.compraId, x.herramientaid });
                    table.ForeignKey(
                        name: "FK_ComprarItem_Compra_compraId",
                        column: x => x.compraId,
                        principalTable: "Compra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComprarItem_Herramienta_herramientaid",
                        column: x => x.herramientaid,
                        principalTable: "Herramienta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfertaItem",
                columns: table => new
                {
                    ofertaId = table.Column<int>(type: "int", nullable: false),
                    herramientaid = table.Column<int>(type: "int", nullable: false),
                    porcentaje = table.Column<int>(type: "int", nullable: false),
                    precioFinal = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfertaItem", x => new { x.ofertaId, x.herramientaid });
                    table.ForeignKey(
                        name: "FK_OfertaItem_Herramienta_herramientaid",
                        column: x => x.herramientaid,
                        principalTable: "Herramienta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfertaItem_Oferta_ofertaId",
                        column: x => x.ofertaId,
                        principalTable: "Oferta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReparacionItem",
                columns: table => new
                {
                    Herramientaid = table.Column<int>(type: "int", nullable: false),
                    Reparacionid = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    precio = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReparacionItem", x => new { x.Herramientaid, x.Reparacionid });
                    table.ForeignKey(
                        name: "FK_ReparacionItem_Herramienta_Herramientaid",
                        column: x => x.Herramientaid,
                        principalTable: "Herramienta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReparacionItem_Reparacion_Reparacionid",
                        column: x => x.Reparacionid,
                        principalTable: "Reparacion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_alquilar_applicationUserId",
                table: "alquilar",
                column: "applicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_alquilarItem_herramientaid",
                table: "alquilarItem",
                column: "herramientaid");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_ApplicationUserId",
                table: "Compra",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ComprarItem_herramientaid",
                table: "ComprarItem",
                column: "herramientaid");

            migrationBuilder.CreateIndex(
                name: "IX_Herramienta_fabricanteid",
                table: "Herramienta",
                column: "fabricanteid");

            migrationBuilder.CreateIndex(
                name: "IX_Oferta_ApplicationUserId",
                table: "Oferta",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertaItem_herramientaid",
                table: "OfertaItem",
                column: "herramientaid");

            migrationBuilder.CreateIndex(
                name: "IX_Reparacion_ApplicationUserId",
                table: "Reparacion",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparacionItem_Reparacionid",
                table: "ReparacionItem",
                column: "Reparacionid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alquilarItem");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ComprarItem");

            migrationBuilder.DropTable(
                name: "OfertaItem");

            migrationBuilder.DropTable(
                name: "ReparacionItem");

            migrationBuilder.DropTable(
                name: "alquilar");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Oferta");

            migrationBuilder.DropTable(
                name: "Herramienta");

            migrationBuilder.DropTable(
                name: "Reparacion");

            migrationBuilder.DropTable(
                name: "fabricante");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
