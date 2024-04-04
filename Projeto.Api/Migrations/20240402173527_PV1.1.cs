using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto.Api.Migrations
{
    /// <inheritdoc />
    public partial class PV11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Credencial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credencial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(8)", maxLength: 8, nullable: false),
                    PrimeiroNome = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    UltimoSobrenome = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Codigo = table.Column<string>(type: "VARCHAR(6)", maxLength: 6, nullable: false),
                    LimiteValidacao = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false),
                    ValidacaoRealizada = table.Column<DateTime>(type: "SMALLDATETIME", nullable: true),
                    HashSenha = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioCredencial",
                columns: table => new
                {
                    CredencialId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "VARCHAR(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCredencial", x => new { x.CredencialId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_UsuarioCredencial_CredencialId",
                        column: x => x.CredencialId,
                        principalTable: "Credencial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioCredencial_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credencial_Titulo",
                table: "Credencial",
                column: "Titulo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Endereco",
                table: "Usuario",
                column: "Endereco",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCredencial_UsuarioId",
                table: "UsuarioCredencial",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioCredencial");

            migrationBuilder.DropTable(
                name: "Credencial");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
