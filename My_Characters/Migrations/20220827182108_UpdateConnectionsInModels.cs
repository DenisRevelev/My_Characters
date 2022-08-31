using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Characters.Migrations
{
    public partial class UpdateConnectionsInModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ranks_Biographies_BiographyId",
                table: "Ranks");

            migrationBuilder.DropIndex(
                name: "IX_Ranks_BiographyId",
                table: "Ranks");

            migrationBuilder.DropColumn(
                name: "BiographyId",
                table: "Ranks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BiographyId",
                table: "Ranks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ranks_BiographyId",
                table: "Ranks",
                column: "BiographyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ranks_Biographies_BiographyId",
                table: "Ranks",
                column: "BiographyId",
                principalTable: "Biographies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
