using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "InternalFieldPermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InternalFieldPermissions_RoleId",
                table: "InternalFieldPermissions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalFieldPermissions_InternalRoles_RoleId",
                table: "InternalFieldPermissions",
                column: "RoleId",
                principalTable: "InternalRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalFieldPermissions_InternalRoles_RoleId",
                table: "InternalFieldPermissions");

            migrationBuilder.DropIndex(
                name: "IX_InternalFieldPermissions_RoleId",
                table: "InternalFieldPermissions");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "InternalFieldPermissions");
        }
    }
}
