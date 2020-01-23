using Microsoft.EntityFrameworkCore.Migrations;

namespace TestUser.Data.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "UserBook",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaken",
                table: "Book",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "UserBook");

            migrationBuilder.DropColumn(
                name: "IsTaken",
                table: "Book");
        }
    }
}
