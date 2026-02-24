using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DojoManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarStatusAluno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "Alunos",
                newName: "EstaAtivo");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstaAtivo",
                table: "Alunos",
                newName: "Ativo");

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
        }
    }
}
