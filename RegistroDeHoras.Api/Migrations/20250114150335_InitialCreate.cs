using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroDeHoras.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataDeInicio = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    HoraDeInicio = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    HoraDeTermino = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    HorasUtilizadas = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    NumeroAtividade = table.Column<string>(type: "TEXT", nullable: true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: true),
                    Cliente = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataDeCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataDeModificacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarefas");
        }
    }
}
