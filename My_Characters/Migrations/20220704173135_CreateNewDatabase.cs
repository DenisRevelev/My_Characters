using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Characters.Migrations
{
    public partial class CreateNewDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Biographies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biographies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Progresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    StatusProgress = table.Column<bool>(type: "bit", nullable: false),
                    Task = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Finish = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckTask = table.Column<bool>(type: "bit", nullable: false),
                    BiographyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progresses_Biographies_BiographyId",
                        column: x => x.BiographyId,
                        principalTable: "Biographies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    BiographyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                    table.ForeignKey(
                        name: "FK_References_Biographies_BiographyId",
                        column: x => x.BiographyId,
                        principalTable: "Biographies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Renders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenderImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    BiographyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Renders_Biographies_BiographyId",
                        column: x => x.BiographyId,
                        principalTable: "Biographies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SourceFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BiographyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SourceFiles_Biographies_BiographyId",
                        column: x => x.BiographyId,
                        principalTable: "Biographies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_BiographyId",
                table: "Progresses",
                column: "BiographyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_References_BiographyId",
                table: "References",
                column: "BiographyId");

            migrationBuilder.CreateIndex(
                name: "IX_Renders_BiographyId",
                table: "Renders",
                column: "BiographyId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceFiles_BiographyId",
                table: "SourceFiles",
                column: "BiographyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Progresses");

            migrationBuilder.DropTable(
                name: "References");

            migrationBuilder.DropTable(
                name: "Renders");

            migrationBuilder.DropTable(
                name: "SourceFiles");

            migrationBuilder.DropTable(
                name: "Biographies");
        }
    }
}
