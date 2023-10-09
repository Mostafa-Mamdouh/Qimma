using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class IntegrationEditAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIdentity",
                table: "LegalRepresentativesProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "LegalRepresentativesProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanFacilityId",
                table: "FacilityProfile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LegalRepresentativesProfile_UserType",
                table: "LegalRepresentativesProfile",
                column: "UserType");

            migrationBuilder.AddForeignKey(
                name: "FK_LegalRepresentativesProfile_LookupSetTerm_UserType",
                table: "LegalRepresentativesProfile",
                column: "UserType",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LegalRepresentativesProfile_LookupSetTerm_UserType",
                table: "LegalRepresentativesProfile");

            migrationBuilder.DropIndex(
                name: "IX_LegalRepresentativesProfile_UserType",
                table: "LegalRepresentativesProfile");

            migrationBuilder.DropColumn(
                name: "UserIdentity",
                table: "LegalRepresentativesProfile");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "LegalRepresentativesProfile");

            migrationBuilder.DropColumn(
                name: "AmanFacilityId",
                table: "FacilityProfile");
        }
    }
}
