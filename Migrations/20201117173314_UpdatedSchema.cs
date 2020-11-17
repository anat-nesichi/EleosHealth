using Microsoft.EntityFrameworkCore.Migrations;

namespace EleosHealth.Migrations
{
    public partial class UpdatedSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParticipantsCount",
                table: "Meetings",
                newName: "Participants");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Participants",
                table: "Meetings",
                newName: "ParticipantsCount");
        }
    }
}
