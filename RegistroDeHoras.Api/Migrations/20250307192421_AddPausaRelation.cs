using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroDeHoras.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPausaRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pausa_Tarefas_TarefaId",
                table: "Pausa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pausa",
                table: "Pausa");

            migrationBuilder.RenameTable(
                name: "Pausa",
                newName: "Pausas");

            migrationBuilder.RenameIndex(
                name: "IX_Pausa_TarefaId",
                table: "Pausas",
                newName: "IX_Pausas_TarefaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pausas",
                table: "Pausas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pausas_Tarefas_TarefaId",
                table: "Pausas",
                column: "TarefaId",
                principalTable: "Tarefas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pausas_Tarefas_TarefaId",
                table: "Pausas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pausas",
                table: "Pausas");

            migrationBuilder.RenameTable(
                name: "Pausas",
                newName: "Pausa");

            migrationBuilder.RenameIndex(
                name: "IX_Pausas_TarefaId",
                table: "Pausa",
                newName: "IX_Pausa_TarefaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pausa",
                table: "Pausa",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pausa_Tarefas_TarefaId",
                table: "Pausa",
                column: "TarefaId",
                principalTable: "Tarefas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
