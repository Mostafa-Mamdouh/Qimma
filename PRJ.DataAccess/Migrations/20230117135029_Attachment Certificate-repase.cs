using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class AttachmentCertificaterepase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileAvailableFlag",
                table: "TrnItemFiles");

            migrationBuilder.DropColumn(
                name: "FileAvailableFlag",
                table: "ItemSourceFiles");

            migrationBuilder.AddColumn<bool>(
                name: "NoCharacterizationCertificateFlag",
                table: "TrnItemSource",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoCustomImportPermitFlag",
                table: "TrnItemSource",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoImagesFlag",
                table: "TrnItemSource",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoManufacturerCertificateFlag",
                table: "TrnItemSource",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoShipperImportPermitFlag",
                table: "TrnItemSource",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoCharacterizationCertificateFlag",
                table: "ItemSourceProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoCustomImportPermitFlag",
                table: "ItemSourceProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoImagesFlag",
                table: "ItemSourceProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoManufacturerCertificateFlag",
                table: "ItemSourceProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoShipperImportPermitFlag",
                table: "ItemSourceProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SourceActivityUnit",
                table: "ItemSourceProfile",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_SourceActivityUnit",
                table: "ItemSourceProfile",
                column: "SourceActivityUnit");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSourceProfile_LookupSetTerm_SourceActivityUnit",
                table: "ItemSourceProfile",
                column: "SourceActivityUnit",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemSourceProfile_LookupSetTerm_SourceActivityUnit",
                table: "ItemSourceProfile");

            migrationBuilder.DropIndex(
                name: "IX_ItemSourceProfile_SourceActivityUnit",
                table: "ItemSourceProfile");

            migrationBuilder.DropColumn(
                name: "NoCharacterizationCertificateFlag",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "NoCustomImportPermitFlag",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "NoImagesFlag",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "NoManufacturerCertificateFlag",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "NoShipperImportPermitFlag",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "NoCharacterizationCertificateFlag",
                table: "ItemSourceProfile");

            migrationBuilder.DropColumn(
                name: "NoCustomImportPermitFlag",
                table: "ItemSourceProfile");

            migrationBuilder.DropColumn(
                name: "NoImagesFlag",
                table: "ItemSourceProfile");

            migrationBuilder.DropColumn(
                name: "NoManufacturerCertificateFlag",
                table: "ItemSourceProfile");

            migrationBuilder.DropColumn(
                name: "NoShipperImportPermitFlag",
                table: "ItemSourceProfile");

            migrationBuilder.DropColumn(
                name: "SourceActivityUnit",
                table: "ItemSourceProfile");

            migrationBuilder.AddColumn<bool>(
                name: "FileAvailableFlag",
                table: "TrnItemFiles",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FileAvailableFlag",
                table: "ItemSourceFiles",
                type: "bit",
                nullable: true);
        }
    }
}
