using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes.Data.Migrations
{
    public partial class WorldCuisinesChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Birth",
                table: "Chef",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "Chef",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorldCuisinesId",
                table: "Chef",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WorldCuisines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorldCuisines", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chef_WorldCuisinesId",
                table: "Chef",
                column: "WorldCuisinesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chef_WorldCuisines_WorldCuisinesId",
                table: "Chef",
                column: "WorldCuisinesId",
                principalTable: "WorldCuisines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chef_WorldCuisines_WorldCuisinesId",
                table: "Chef");

            migrationBuilder.DropTable(
                name: "WorldCuisines");

            migrationBuilder.DropIndex(
                name: "IX_Chef_WorldCuisinesId",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "Birth",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "Information",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "WorldCuisinesId",
                table: "Chef");
        }
    }
}
