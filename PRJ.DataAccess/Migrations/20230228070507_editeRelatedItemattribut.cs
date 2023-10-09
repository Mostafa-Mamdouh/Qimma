using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class editeRelatedItemattribut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NoCharacterizationCertificateFlag",
                table: "RelatedItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoCustomImportPermitFlag",
                table: "RelatedItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoManufacturerCertificateFlag",
                table: "RelatedItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoShipperImportPermitFlag",
                table: "RelatedItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoSourceTagImageFlag",
                table: "RelatedItem",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoCharacterizationCertificateFlag",
                table: "RelatedItem");

            migrationBuilder.DropColumn(
                name: "NoCustomImportPermitFlag",
                table: "RelatedItem");

            migrationBuilder.DropColumn(
                name: "NoManufacturerCertificateFlag",
                table: "RelatedItem");

            migrationBuilder.DropColumn(
                name: "NoShipperImportPermitFlag",
                table: "RelatedItem");

            migrationBuilder.DropColumn(
                name: "NoSourceTagImageFlag",
                table: "RelatedItem");
        }
    }
}
