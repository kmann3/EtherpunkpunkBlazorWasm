using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EtherpunkBlazorWasm.Server.Data.Migrations
{
    public partial class _000init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AppUserRoles_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserRoles_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "Role" },
                values: new object[] { new Guid("a228fa83-e5b6-4d1d-9636-55228302b22f"), "Admin" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreatedOn", "Email", "PasswordHash" },
                values: new object[] { new Guid("4e375925-a166-4924-8289-c80718f83c29"), new DateTime(2022, 10, 17, 1, 40, 44, 425, DateTimeKind.Utc).AddTicks(1649), "regularuser2@user.com", "$2b$12$k87L/MF28Q673VKh8/cPi.pNe6atmnYcCQXfH80DwODOUtvUgB1NK" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreatedOn", "Email", "PasswordHash" },
                values: new object[] { new Guid("dc09e4be-9631-4adf-b65e-6a396859b946"), new DateTime(2022, 10, 17, 1, 40, 44, 170, DateTimeKind.Utc).AddTicks(4167), "regularuser1@user.com", "$2b$12$k87L/MF28Q673VKh8/cPi.pNe6atmnYcCQXfH80DwODOUtvUgB1NK" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreatedOn", "Email", "PasswordHash" },
                values: new object[] { new Guid("e39dfd1f-85b8-4586-8a98-037611b60558"), new DateTime(2022, 10, 17, 1, 40, 43, 908, DateTimeKind.Utc).AddTicks(3242), "admin@admin.com", "$2b$12$k87L/MF28Q673VKh8/cPi.pNe6atmnYcCQXfH80DwODOUtvUgB1NK" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("a228fa83-e5b6-4d1d-9636-55228302b22f"), new Guid("e39dfd1f-85b8-4586-8a98-037611b60558") });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_UserId",
                table: "AppUserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
