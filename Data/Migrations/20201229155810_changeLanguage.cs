using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes.Data.Migrations
{
    public partial class changeLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Language");

            migrationBuilder.AddColumn<string>(
                name: "LanguageName",
                table: "Language",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageName",
                table: "Language");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Language",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
