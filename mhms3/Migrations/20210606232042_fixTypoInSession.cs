using Microsoft.EntityFrameworkCore.Migrations;

namespace mhms3.Migrations
{
    public partial class fixTypoInSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CousnelorID",
                table: "Session",
                newName: "CounselorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CounselorID",
                table: "Session",
                newName: "CousnelorID");
        }
    }
}
