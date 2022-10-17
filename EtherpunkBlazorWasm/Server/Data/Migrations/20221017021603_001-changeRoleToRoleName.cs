using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EtherpunkBlazorWasm.Server.Data.Migrations
{
    public partial class _001changeRoleToRoleName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a228fa83-e5b6-4d1d-9636-55228302b22f"), new Guid("e39dfd1f-85b8-4586-8a98-037611b60558") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("4e375925-a166-4924-8289-c80718f83c29"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("dc09e4be-9631-4adf-b65e-6a396859b946"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a228fa83-e5b6-4d1d-9636-55228302b22f"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("e39dfd1f-85b8-4586-8a98-037611b60558"));

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "AppRoles",
                newName: "RoleName");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { new Guid("fae64def-afb0-4669-9a0a-c37c839c6578"), "Admin" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreatedOn", "Email", "PasswordHash" },
                values: new object[] { new Guid("69d4fdad-c18f-48e2-8fb3-2f2a5926ada3"), new DateTime(2022, 10, 17, 2, 16, 3, 75, DateTimeKind.Utc).AddTicks(1293), "regularuser2@user.com", "$2b$12$k87L/MF28Q673VKh8/cPi.pNe6atmnYcCQXfH80DwODOUtvUgB1NK" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreatedOn", "Email", "PasswordHash" },
                values: new object[] { new Guid("99cd8f1d-9d6a-4e36-938e-63f8336393ee"), new DateTime(2022, 10, 17, 2, 16, 2, 555, DateTimeKind.Utc).AddTicks(6406), "admin@admin.com", "$2b$12$k87L/MF28Q673VKh8/cPi.pNe6atmnYcCQXfH80DwODOUtvUgB1NK" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreatedOn", "Email", "PasswordHash" },
                values: new object[] { new Guid("c98d8b02-2064-44da-baa3-69be0de43295"), new DateTime(2022, 10, 17, 2, 16, 2, 815, DateTimeKind.Utc).AddTicks(2652), "regularuser1@user.com", "$2b$12$k87L/MF28Q673VKh8/cPi.pNe6atmnYcCQXfH80DwODOUtvUgB1NK" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("fae64def-afb0-4669-9a0a-c37c839c6578"), new Guid("99cd8f1d-9d6a-4e36-938e-63f8336393ee") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("fae64def-afb0-4669-9a0a-c37c839c6578"), new Guid("99cd8f1d-9d6a-4e36-938e-63f8336393ee") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69d4fdad-c18f-48e2-8fb3-2f2a5926ada3"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("c98d8b02-2064-44da-baa3-69be0de43295"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("fae64def-afb0-4669-9a0a-c37c839c6578"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("99cd8f1d-9d6a-4e36-938e-63f8336393ee"));

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "AppRoles",
                newName: "Role");

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
        }
    }
}
