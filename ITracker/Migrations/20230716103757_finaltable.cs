using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITracker.Migrations
{
    /// <inheritdoc />
    public partial class finaltable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bio",
                table: "usersTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "blood_grop",
                table: "usersTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "created_time",
                table: "usersTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "dob",
                table: "usersTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "usersTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "instagram",
                table: "usersTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "linkedin",
                table: "usersTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mobile_number",
                table: "usersTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "rating",
                table: "usersTable",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "secondary_email",
                table: "usersTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "feedback",
                table: "taskApproversTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ideaCreatedDate",
                table: "ideaTable",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bio",
                table: "usersTable");

            migrationBuilder.DropColumn(
                name: "blood_grop",
                table: "usersTable");

            migrationBuilder.DropColumn(
                name: "created_time",
                table: "usersTable");

            migrationBuilder.DropColumn(
                name: "dob",
                table: "usersTable");

            migrationBuilder.DropColumn(
                name: "image",
                table: "usersTable");

            migrationBuilder.DropColumn(
                name: "instagram",
                table: "usersTable");

            migrationBuilder.DropColumn(
                name: "linkedin",
                table: "usersTable");

            migrationBuilder.DropColumn(
                name: "mobile_number",
                table: "usersTable");

            migrationBuilder.DropColumn(
                name: "rating",
                table: "usersTable");

            migrationBuilder.DropColumn(
                name: "secondary_email",
                table: "usersTable");

            migrationBuilder.DropColumn(
                name: "feedback",
                table: "taskApproversTable");

            migrationBuilder.DropColumn(
                name: "ideaCreatedDate",
                table: "ideaTable");
        }
    }
}
