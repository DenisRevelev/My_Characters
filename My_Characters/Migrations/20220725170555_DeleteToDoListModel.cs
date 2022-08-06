using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Characters.Migrations
{
    public partial class DeleteToDoListModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoLists");

            migrationBuilder.DropIndex(
                name: "IX_Progresses_BiographyId",
                table: "Progresses");

            migrationBuilder.AddColumn<bool>(
                name: "CheckTask",
                table: "Progresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Finish",
                table: "Progresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Progresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Task",
                table: "Progresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_BiographyId",
                table: "Progresses",
                column: "BiographyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Progresses_BiographyId",
                table: "Progresses");

            migrationBuilder.DropColumn(
                name: "CheckTask",
                table: "Progresses");

            migrationBuilder.DropColumn(
                name: "Finish",
                table: "Progresses");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Progresses");

            migrationBuilder.DropColumn(
                name: "Task",
                table: "Progresses");

            migrationBuilder.CreateTable(
                name: "ToDoLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgressId = table.Column<int>(type: "int", nullable: false),
                    CheckTask = table.Column<bool>(type: "bit", nullable: false),
                    Finish = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Task = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDoLists_Progresses_ProgressId",
                        column: x => x.ProgressId,
                        principalTable: "Progresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_BiographyId",
                table: "Progresses",
                column: "BiographyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoLists_ProgressId",
                table: "ToDoLists",
                column: "ProgressId");
        }
    }
}
