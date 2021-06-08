using Microsoft.EntityFrameworkCore.Migrations;

namespace mhms3.Migrations
{
    public partial class addCouselorIdToSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CousnelorID",
                table: "Session",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CousnelorID",
                table: "Session");
        }
    }
}
