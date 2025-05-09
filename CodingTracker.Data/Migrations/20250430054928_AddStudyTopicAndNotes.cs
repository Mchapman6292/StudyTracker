using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStudyTopicAndNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudyNotes",
                table: "CodingSessions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudyProject",
                table: "CodingSessions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyNotes",
                table: "CodingSessions");

            migrationBuilder.DropColumn(
                name: "StudyProject",
                table: "CodingSessions");
        }
    }
}
