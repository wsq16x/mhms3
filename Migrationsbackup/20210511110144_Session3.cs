using Microsoft.EntityFrameworkCore.Migrations;

namespace mhms3.Data.Migrations
{
    public partial class Session3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Expressions",
                table: "Session",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expressions",
                table: "Session");
        }
    }
}
