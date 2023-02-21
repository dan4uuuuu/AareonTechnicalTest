using Microsoft.EntityFrameworkCore.Migrations;

namespace AareonTechnicalTest.Migrations
{
    public partial class AddSuspendedNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSuspended",
                table: "Notes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuspended",
                table: "Notes");
        }
    }
}
