using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class AttachmentCertificate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoCertificate",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "NoCertificate",
                table: "ItemSourceProfile");

            migrationBuilder.AddColumn<int>(
                name: "SourceActivityUnit",
                table: "TrnItemSource",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_SourceActivityUnit",
                table: "TrnItemSource",
                column: "SourceActivityUnit");

            migrationBuilder.AddForeignKey(
                name: "FK_TrnItemSource_LookupSetTerm_SourceActivityUnit",
                table: "TrnItemSource",
                column: "SourceActivityUnit",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrnItemSource_LookupSetTerm_SourceActivityUnit",
                table: "TrnItemSource");

            migrationBuilder.DropIndex(
                name: "IX_TrnItemSource_SourceActivityUnit",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "SourceActivityUnit",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "FileAvailableFlag",
                table: "TrnItemFiles");

            migrationBuilder.DropColumn(
                name: "FileAvailableFlag",
                table: "ItemSourceFiles");

            migrationBuilder.AddColumn<bool>(
                name: "NoCertificate",
                table: "TrnItemSource",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoCertificate",
                table: "ItemSourceProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
