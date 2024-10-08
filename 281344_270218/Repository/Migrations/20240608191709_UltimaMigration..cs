﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class UltimaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Depositos",
                columns: table => new
                {
                    DepositoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tamanio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Climatizacion = table.Column<bool>(type: "bit", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depositos", x => x.DepositoId);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreYApellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.PersonaId);
                    table.UniqueConstraint("AK_Persona_Mail", x => x.Mail);
                });

            migrationBuilder.CreateTable(
                name: "Promociones",
                columns: table => new
                {
                    PromocionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Etiqueta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PorcentajeDescuento = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promociones", x => x.PromocionId);
                });

            migrationBuilder.CreateTable(
                name: "FechasNoDisponibles",
                columns: table => new
                {
                    FechasNoDisponibleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositoId = table.Column<int>(type: "int", nullable: false),
                    FechaDesde = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaHasta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FechasNoDisponibles", x => x.FechasNoDisponibleId);
                    table.ForeignKey(
                        name: "FK_FechasNoDisponibles_Depositos_DepositoId",
                        column: x => x.DepositoId,
                        principalTable: "Depositos",
                        principalColumn: "DepositoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepositoPromocions",
                columns: table => new
                {
                    DepositoId = table.Column<int>(type: "int", nullable: false),
                    PromocionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositoPromocions", x => new { x.PromocionId, x.DepositoId });
                    table.ForeignKey(
                        name: "FK_DepositoPromocions_Depositos_DepositoId",
                        column: x => x.DepositoId,
                        principalTable: "Depositos",
                        principalColumn: "DepositoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepositoPromocions_Promociones_PromocionId",
                        column: x => x.PromocionId,
                        principalTable: "Promociones",
                        principalColumn: "PromocionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    ReservaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaDesde = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaHasta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepositoId = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JustificacionRechazo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    PromocionId = table.Column<int>(type: "int", nullable: true),
                    PagoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.ReservaId);
                    table.ForeignKey(
                        name: "FK_Reservas_Depositos_DepositoId",
                        column: x => x.DepositoId,
                        principalTable: "Depositos",
                        principalColumn: "DepositoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Persona_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Promociones_PromocionId",
                        column: x => x.PromocionId,
                        principalTable: "Promociones",
                        principalColumn: "PromocionId");
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    PagoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservaId = table.Column<int>(type: "int", nullable: false),
                    EstadoPago = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.PagoId);
                    table.ForeignKey(
                        name: "FK_Pagos_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "ReservaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepositoPromocions_DepositoId",
                table: "DepositoPromocions",
                column: "DepositoId");

            migrationBuilder.CreateIndex(
                name: "IX_FechasNoDisponibles_DepositoId",
                table: "FechasNoDisponibles",
                column: "DepositoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_ReservaId",
                table: "Pagos",
                column: "ReservaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ClienteId",
                table: "Reservas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_DepositoId",
                table: "Reservas",
                column: "DepositoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_PromocionId",
                table: "Reservas",
                column: "PromocionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepositoPromocions");

            migrationBuilder.DropTable(
                name: "FechasNoDisponibles");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Depositos");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Promociones");
        }
    }
}
