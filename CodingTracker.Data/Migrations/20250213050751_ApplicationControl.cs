using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationControl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoalMinutes",
                table: "CodingSessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoalMinutes",
                table: "CodingSessions");
        }
    }
}
