using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class Nuclear_Attachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NoCharacterizationCertificateFlag",
                table: "NuclearMaterials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoCustomImportPermitFlag",
                table: "NuclearMaterials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoImagesFlag",
                table: "NuclearMaterials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoManufacturerCertificateFlag",
                table: "NuclearMaterials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoShipperImportPermitFlag",
                table: "NuclearMaterials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoSourceTagImageFlag",
                table: "NuclearMaterials",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoCharacterizationCertificateFlag",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "NoCustomImportPermitFlag",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "NoImagesFlag",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "NoManufacturerCertificateFlag",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "NoShipperImportPermitFlag",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "NoSourceTagImageFlag",
                table: "NuclearMaterials");
        }
    }
}
