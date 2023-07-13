using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITracker.Migrations
{
    /// <inheritdoc />
    public partial class Initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "approversTable",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    approverName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_approversTable", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rolesTable",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolesTable", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usersTable",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rId = table.Column<int>(type: "int", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersTable", x => x.id);
                    table.ForeignKey(
                        name: "FK_usersTable_rolesTable_UserType",
                        column: x => x.UserType,
                        principalTable: "rolesTable",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ideaTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    longDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    startDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    endDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    signOff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDelete = table.Column<int>(type: "int", nullable: true),
                    like = table.Column<int>(type: "int", nullable: true),
                    idOfOwner = table.Column<int>(type: "int", nullable: false),
                    IdOFUser = table.Column<int>(type: "int", nullable: true),
                    approverId = table.Column<int>(type: "int", nullable: false),
                    IdOfApprover = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ideaTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ideaTable_approversTable_IdOfApprover",
                        column: x => x.IdOfApprover,
                        principalTable: "approversTable",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ideaTable_usersTable_IdOFUser",
                        column: x => x.IdOFUser,
                        principalTable: "usersTable",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "commentsTable",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentsDateOnly = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentsTimeOnly = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taskid = table.Column<int>(type: "int", nullable: false),
                    IdOfIdea = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    Owner = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commentsTable", x => x.id);
                    table.ForeignKey(
                        name: "FK_commentsTable_ideaTable_IdOfIdea",
                        column: x => x.IdOfIdea,
                        principalTable: "ideaTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_commentsTable_usersTable_Owner",
                        column: x => x.Owner,
                        principalTable: "usersTable",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_commentsTable_IdOfIdea",
                table: "commentsTable",
                column: "IdOfIdea");

            migrationBuilder.CreateIndex(
                name: "IX_commentsTable_Owner",
                table: "commentsTable",
                column: "Owner");

            migrationBuilder.CreateIndex(
                name: "IX_ideaTable_IdOfApprover",
                table: "ideaTable",
                column: "IdOfApprover");

            migrationBuilder.CreateIndex(
                name: "IX_ideaTable_IdOFUser",
                table: "ideaTable",
                column: "IdOFUser");

            migrationBuilder.CreateIndex(
                name: "IX_usersTable_UserType",
                table: "usersTable",
                column: "UserType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "commentsTable");

            migrationBuilder.DropTable(
                name: "ideaTable");

            migrationBuilder.DropTable(
                name: "approversTable");

            migrationBuilder.DropTable(
                name: "usersTable");

            migrationBuilder.DropTable(
                name: "rolesTable");
        }
    }
}
