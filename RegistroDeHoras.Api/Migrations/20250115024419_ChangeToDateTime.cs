using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroDeHoras.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeToDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraDeInicio",
                table: "Tarefas");

            migrationBuilder.RenameColumn(
                name: "HoraDeTermino",
                table: "Tarefas",
                newName: "DataDeTermino");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataDeTermino",
                table: "Tarefas",
                newName: "HoraDeTermino");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraDeInicio",
                table: "Tarefas",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
