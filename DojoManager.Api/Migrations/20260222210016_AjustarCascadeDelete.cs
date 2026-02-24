using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DojoManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class AjustarCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AlunoId1",
                table: "Graduacoes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Graduacoes_AlunoId1",
                table: "Graduacoes",
                column: "AlunoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Graduacoes_Alunos_AlunoId1",
                table: "Graduacoes",
                column: "AlunoId1",
                principalTable: "Alunos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Graduacoes_Alunos_AlunoId1",
                table: "Graduacoes");

            migrationBuilder.DropIndex(
                name: "IX_Graduacoes_AlunoId1",
                table: "Graduacoes");

            migrationBuilder.DropColumn(
                name: "AlunoId1",
                table: "Graduacoes");
        }
    }
}
