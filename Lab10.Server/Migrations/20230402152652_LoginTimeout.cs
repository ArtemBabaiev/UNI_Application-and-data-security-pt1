using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab10.Server.Migrations
{
    /// <inheritdoc />
    public partial class LoginTimeout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Timeouts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tries = table.Column<int>(type: "int", nullable: false),
                    UserUsername = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timeouts_Users_UserUsername",
                        column: x => x.UserUsername,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Timeouts_UserUsername",
                table: "Timeouts",
                column: "UserUsername");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timeouts");
        }
    }
}
