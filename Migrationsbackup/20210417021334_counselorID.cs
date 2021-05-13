using Microsoft.EntityFrameworkCore.Migrations;

namespace mhms3.Data.Migrations
{
    public partial class counselorID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CounselorID",
                table: "Client",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CounselorID",
                table: "Client");
        }
    }
}
