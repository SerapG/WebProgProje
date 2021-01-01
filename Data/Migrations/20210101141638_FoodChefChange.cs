using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes.Data.Migrations
{
    public partial class FoodChefChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Chef_ChefId",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_ChefId",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "Food");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChefId",
                table: "Food",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Food_ChefId",
                table: "Food",
                column: "ChefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Chef_ChefId",
                table: "Food",
                column: "ChefId",
                principalTable: "Chef",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
