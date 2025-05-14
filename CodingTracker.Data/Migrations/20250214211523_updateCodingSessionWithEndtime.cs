using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateCodingSessionWithEndtime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoalHHMM",
                table: "CodingSessions");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDateLocal",
                table: "CodingSessions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "GoalReached",
                table: "CodingSessions",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDateLocal",
                table: "CodingSessions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DurationSeconds",
                table: "CodingSessions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DurationHHMM",
                table: "CodingSessions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTimeLocal",
                table: "CodingSessions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "GoalSet",
                table: "CodingSessions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTimeLocal",
                table: "CodingSessions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CodingSessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTimeLocal",
                table: "CodingSessions");

            migrationBuilder.DropColumn(
                name: "GoalSet",
                table: "CodingSessions");

            migrationBuilder.DropColumn(
                name: "StartTimeLocal",
                table: "CodingSessions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CodingSessions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDateLocal",
                table: "CodingSessions",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "GoalReached",
                table: "CodingSessions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDateLocal",
                table: "CodingSessions",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<double>(
                name: "DurationSeconds",
                table: "CodingSessions",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<string>(
                name: "DurationHHMM",
                table: "CodingSessions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "GoalHHMM",
                table: "CodingSessions",
                type: "text",
                nullable: true);
        }
    }
}
