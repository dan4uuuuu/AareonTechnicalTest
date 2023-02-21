using Microsoft.EntityFrameworkCore.Migrations;

namespace AareonTechnicalTest.Migrations
{
    public partial class RemoveNoteId_Tickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "Tickets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoteId",
                table: "Tickets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
