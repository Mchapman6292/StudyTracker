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
                name: "StartTimeLocal",
                table: "CodingSessions",
                newName: "StartTimeUTC");

            migrationBuilder.RenameColumn(
                name: "StartDateLocal",
                table: "CodingSessions",
                newName: "StartDateUTC");

            migrationBuilder.RenameColumn(
                name: "EndTimeLocal",
                table: "CodingSessions",
                newName: "EndTimeUTC");

            migrationBuilder.RenameColumn(
                name: "EndDateLocal",
                table: "CodingSessions",
                newName: "EndDateUTC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTimeUTC",
                table: "CodingSessions",
                newName: "StartTimeLocal");

            migrationBuilder.RenameColumn(
                name: "StartDateUTC",
                table: "CodingSessions",
                newName: "StartDateLocal");

            migrationBuilder.RenameColumn(
                name: "EndTimeUTC",
                table: "CodingSessions",
                newName: "EndTimeLocal");

            migrationBuilder.RenameColumn(
                name: "EndDateUTC",
                table: "CodingSessions",
                newName: "EndDateLocal");
        }
    }
}
