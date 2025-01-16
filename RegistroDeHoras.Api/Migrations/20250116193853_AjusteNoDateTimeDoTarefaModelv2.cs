using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroDeHoras.Api.Migrations
{
    /// <inheritdoc />
    public partial class AjusteNoDateTimeDoTarefaModelv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataDeTermino",
                table: "Tarefas",
                newName: "Termino");

            migrationBuilder.RenameColumn(
                name: "DataDeInicio",
                table: "Tarefas",
                newName: "Reinicio");

            migrationBuilder.AddColumn<DateTime>(
                name: "Inicio",
                table: "Tarefas",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Pausa",
                table: "Tarefas",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inicio",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Pausa",
                table: "Tarefas");

            migrationBuilder.RenameColumn(
                name: "Termino",
                table: "Tarefas",
                newName: "DataDeTermino");

            migrationBuilder.RenameColumn(
                name: "Reinicio",
                table: "Tarefas",
                newName: "DataDeInicio");
        }
    }
}
