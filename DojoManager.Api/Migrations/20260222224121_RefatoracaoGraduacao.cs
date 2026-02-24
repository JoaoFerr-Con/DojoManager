using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DojoManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class RefatoracaoGraduacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "FaixaConquistada",
                table: "Graduacoes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "Graduacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FaixaAtual",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "Modalidade",
                table: "Alunos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sexo",
                table: "Alunos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "Graduacoes");

            migrationBuilder.DropColumn(
                name: "Modalidade",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Alunos");

            migrationBuilder.AlterColumn<string>(
                name: "FaixaConquistada",
                table: "Graduacoes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<Guid>(
                name: "AlunoId1",
                table: "Graduacoes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Alunos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FaixaAtual",
                table: "Alunos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
