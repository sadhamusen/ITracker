using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITracker.Migrations
{
    /// <inheritdoc />
    public partial class Initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contributorTable",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    taskId = table.Column<int>(type: "int", nullable: false),
                    ideaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contributorTable", x => x.id);
                    table.ForeignKey(
                        name: "FK_contributorTable_ideaTable_ideaId",
                        column: x => x.ideaId,
                        principalTable: "ideaTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_contributorTable_ideaId",
                table: "contributorTable",
                column: "ideaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contributorTable");
        }
    }
}
