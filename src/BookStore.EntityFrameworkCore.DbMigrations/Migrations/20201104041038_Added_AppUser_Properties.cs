using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class Added_AppUser_Properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AbpUsers",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Introduction",
                table: "AbpUsers",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Introduction",
                table: "AbpUsers");
        }
    }
}
