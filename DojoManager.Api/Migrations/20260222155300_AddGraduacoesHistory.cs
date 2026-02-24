using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DojoManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddGraduacoesHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Graduacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlunoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FaixaConquistada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrauConquistado = table.Column<int>(type: "int", nullable: false),
                    DataConquista = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Graduacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Graduacoes_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Graduacoes_AlunoId",
                table: "Graduacoes",
                column: "AlunoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Graduacoes");
        }
    }
}
