using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITracker.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ideaTable_approversTable_IdOfApprover",
                table: "ideaTable");

            migrationBuilder.DropForeignKey(
                name: "FK_taskApproversTable_approversTable_IdOfApprover",
                table: "taskApproversTable");

            migrationBuilder.DropIndex(
                name: "IX_ideaTable_IdOfApprover",
                table: "ideaTable");

            migrationBuilder.DropColumn(
                name: "IdOfApprover",
                table: "ideaTable");

            migrationBuilder.DropColumn(
                name: "approverName",
                table: "approversTable");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "taskApproversTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "approversTable",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_approversTable_userid",
                table: "approversTable",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_approversTable_usersTable_userid",
                table: "approversTable",
                column: "userid",
                principalTable: "usersTable",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_taskApproversTable_usersTable_IdOfApprover",
                table: "taskApproversTable",
                column: "IdOfApprover",
                principalTable: "usersTable",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_approversTable_usersTable_userid",
                table: "approversTable");

            migrationBuilder.DropForeignKey(
                name: "FK_taskApproversTable_usersTable_IdOfApprover",
                table: "taskApproversTable");

            migrationBuilder.DropIndex(
                name: "IX_approversTable_userid",
                table: "approversTable");

            migrationBuilder.DropColumn(
                name: "status",
                table: "taskApproversTable");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "approversTable");

            migrationBuilder.AddColumn<int>(
                name: "IdOfApprover",
                table: "ideaTable",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "approverName",
                table: "approversTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ideaTable_IdOfApprover",
                table: "ideaTable",
                column: "IdOfApprover");

            migrationBuilder.AddForeignKey(
                name: "FK_ideaTable_approversTable_IdOfApprover",
                table: "ideaTable",
                column: "IdOfApprover",
                principalTable: "approversTable",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_taskApproversTable_approversTable_IdOfApprover",
                table: "taskApproversTable",
                column: "IdOfApprover",
                principalTable: "approversTable",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
