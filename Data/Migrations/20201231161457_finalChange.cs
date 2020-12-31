using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes.Data.Migrations
{
    public partial class finalChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Food");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Food",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
