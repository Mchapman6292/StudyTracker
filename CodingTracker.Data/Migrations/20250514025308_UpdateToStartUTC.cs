using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable

namespace CodingTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToStartUTC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "CodingSessions",
                newName: "StartTimeUTC");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "CodingSessions",
                newName: "StartDateUTC");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "CodingSessions",
                newName: "EndTimeUTC");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "CodingSessions",
                newName: "EndDateUTC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTimeUTC",
                table: "CodingSessions",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "StartDateUTC",
                table: "CodingSessions",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndTimeUTC",
                table: "CodingSessions",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "EndDateUTC",
                table: "CodingSessions",
                newName: "EndDate");
        }
    }
}