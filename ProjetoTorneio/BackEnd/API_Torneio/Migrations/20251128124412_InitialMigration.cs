using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Torneio.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lutadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    ArtesMarciais = table.Column<int>(type: "int", nullable: false),
                    TotalLutas = table.Column<int>(type: "int", nullable: false),
                    Derrotas = table.Column<int>(type: "int", nullable: false),
                    Vitorias = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lutadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Torneios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VencedorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Torneios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Torneios_Lutadores_VencedorId",
                        column: x => x.VencedorId,
                        principalTable: "Lutadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TorneioLutador",
                columns: table => new
                {
                    ParticipantesId = table.Column<int>(type: "int", nullable: false),
                    TorneiosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TorneioLutador", x => new { x.ParticipantesId, x.TorneiosId });
                    table.ForeignKey(
                        name: "FK_TorneioLutador_Lutadores_ParticipantesId",
                        column: x => x.ParticipantesId,
                        principalTable: "Lutadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TorneioLutador_Torneios_TorneiosId",
                        column: x => x.TorneiosId,
                        principalTable: "Torneios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TorneioLutador_TorneiosId",
                table: "TorneioLutador",
                column: "TorneiosId");

            migrationBuilder.CreateIndex(
                name: "IX_Torneios_VencedorId",
                table: "Torneios",
                column: "VencedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TorneioLutador");

            migrationBuilder.DropTable(
                name: "Torneios");

            migrationBuilder.DropTable(
                name: "Lutadores");
        }
    }
}
