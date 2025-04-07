using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroDeHoras.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Termino = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroDaTarefa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Solucao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusDaTarefa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HorasUtilizadasRaw = table.Column<long>(type: "bigint", nullable: false),
                    HorasDePausaRaw = table.Column<long>(type: "bigint", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataDeCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDeModificacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pausas",
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
                    table.PrimaryKey("PK_Pausas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pausas_Tarefas_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "Tarefas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pausas_TarefaId",
                table: "Pausas",
                column: "TarefaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pausas");

            migrationBuilder.DropTable(
                name: "Tarefas");
        }
    }
}
