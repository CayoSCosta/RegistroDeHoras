using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroDeHoras.Api.Migrations
{
    /// <inheritdoc />
    public partial class HorasDePausa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "HorasDePausa",
                table: "Tarefas",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HorasDePausa",
                table: "Tarefas");
        }
    }
}
