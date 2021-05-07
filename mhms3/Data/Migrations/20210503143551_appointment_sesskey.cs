using Microsoft.EntityFrameworkCore.Migrations;

namespace mhms3.Data.Migrations
{
    public partial class appointment_sesskey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SessionKey",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionKey",
                table: "Appointment");
        }
    }
}
