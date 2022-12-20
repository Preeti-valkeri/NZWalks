using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class Uddingnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userRole_role_RoleId",
                table: "userRole");

            migrationBuilder.DropForeignKey(
                name: "FK_userRole_user_UserId",
                table: "userRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userRole",
                table: "userRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role",
                table: "role");

            migrationBuilder.RenameTable(
                name: "userRole",
                newName: "userRoles");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "role",
                newName: "roles");

            migrationBuilder.RenameIndex(
                name: "IX_userRole_UserId",
                table: "userRoles",
                newName: "IX_userRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_userRole_RoleId",
                table: "userRoles",
                newName: "IX_userRoles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userRoles",
                table: "userRoles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                table: "roles",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_userRoles_roles_RoleId",
                table: "userRoles",
                column: "RoleId",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userRoles_users_UserId",
                table: "userRoles",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userRoles_roles_RoleId",
                table: "userRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_userRoles_users_UserId",
                table: "userRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userRoles",
                table: "userRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                table: "roles");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "user");

            migrationBuilder.RenameTable(
                name: "userRoles",
                newName: "userRole");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "role");

            migrationBuilder.RenameIndex(
                name: "IX_userRoles_UserId",
                table: "userRole",
                newName: "IX_userRole_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_userRoles_RoleId",
                table: "userRole",
                newName: "IX_userRole_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userRole",
                table: "userRole",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role",
                table: "role",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_userRole_role_RoleId",
                table: "userRole",
                column: "RoleId",
                principalTable: "role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userRole_user_UserId",
                table: "userRole",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
