using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class editrelatedItemFiles1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LicenseProfilePractice_LicenseProfile_LicenseProfileId",
                table: "LicenseProfilePractice");

            migrationBuilder.DropColumn(
                name: "FacilityRelatedItemID",
                table: "RelatedItem");

            migrationBuilder.AddColumn<int>(
                name: "AttachmentType",
                table: "RelatedItemsFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "LicenseProfileId",
                table: "LicenseProfilePractice",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "FacilityProfile",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseProfilePractice_LicenseProfile_LicenseProfileId",
                table: "LicenseProfilePractice",
                column: "LicenseProfileId",
                principalTable: "LicenseProfile",
                principalColumn: "LicenseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LicenseProfilePractice_LicenseProfile_LicenseProfileId",
                table: "LicenseProfilePractice");

            migrationBuilder.DropColumn(
                name: "AttachmentType",
                table: "RelatedItemsFiles");

            migrationBuilder.AddColumn<string>(
                name: "FacilityRelatedItemID",
                table: "RelatedItem",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LicenseProfileId",
                table: "LicenseProfilePractice",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "FacilityProfile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseProfilePractice_LicenseProfile_LicenseProfileId",
                table: "LicenseProfilePractice",
                column: "LicenseProfileId",
                principalTable: "LicenseProfile",
                principalColumn: "LicenseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
