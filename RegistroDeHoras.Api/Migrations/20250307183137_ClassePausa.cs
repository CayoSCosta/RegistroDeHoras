using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroDeHoras.Api.Migrations
{
    /// <inheritdoc />
    public partial class ClassePausa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pausa",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Reinicio",
                table: "Tarefas");

            migrationBuilder.RenameColumn(
                name: "HorasUtilizadas",
                table: "Tarefas",
                newName: "HorasDePausaRaw");

            migrationBuilder.AddColumn<string>(
                name: "Solucao",
                table: "Tarefas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pausa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Termino = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TarefaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataDeCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDeModificacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pausa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pausa_Tarefas_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "Tarefas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pausa_TarefaId",
                table: "Pausa",
                column: "TarefaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pausa");

            migrationBuilder.DropColumn(
                name: "Solucao",
                table: "Tarefas");

            migrationBuilder.RenameColumn(
                name: "HorasDePausaRaw",
                table: "Tarefas",
                newName: "HorasUtilizadas");

            migrationBuilder.AddColumn<DateTime>(
                name: "Pausa",
                table: "Tarefas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Reinicio",
                table: "Tarefas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
