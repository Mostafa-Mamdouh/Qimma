using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class editlicensepractice5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LicenseProfilePractice_LicenseProfile_LicenseId",
                table: "LicenseProfilePractice");

            migrationBuilder.DropIndex(
                name: "IX_LicenseProfilePractice_LicenseId",
                table: "LicenseProfilePractice");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "LicenseProfilePractice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LicenseId",
                table: "LicenseProfilePractice",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LicenseProfilePractice_LicenseId",
                table: "LicenseProfilePractice",
                column: "LicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseProfilePractice_LicenseProfile_LicenseId",
                table: "LicenseProfilePractice",
                column: "LicenseId",
                principalTable: "LicenseProfile",
                principalColumn: "LicenseId");
        }
    }
}
