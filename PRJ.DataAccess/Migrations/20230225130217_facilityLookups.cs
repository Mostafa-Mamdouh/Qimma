using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class facilityLookups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "FacilityProfile");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "ServiceEntryFees",
                newName: "CustomerNameEn");

            migrationBuilder.AddColumn<string>(
                name: "CustomerNameAr",
                table: "ServiceEntryFees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Province",
                table: "FacilityProfile",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "FacilityProfile",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacilityProfile_CityId",
                table: "FacilityProfile",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityProfile_Province",
                table: "FacilityProfile",
                column: "Province");

            migrationBuilder.AddForeignKey(
                name: "FK_FacilityProfile_LookupSetTerm_CityId",
                table: "FacilityProfile",
                column: "CityId",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_FacilityProfile_LookupSetTerm_Province",
                table: "FacilityProfile",
                column: "Province",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacilityProfile_LookupSetTerm_CityId",
                table: "FacilityProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_FacilityProfile_LookupSetTerm_Province",
                table: "FacilityProfile");

            migrationBuilder.DropIndex(
                name: "IX_FacilityProfile_CityId",
                table: "FacilityProfile");

            migrationBuilder.DropIndex(
                name: "IX_FacilityProfile_Province",
                table: "FacilityProfile");

            migrationBuilder.DropColumn(
                name: "CustomerNameAr",
                table: "ServiceEntryFees");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "FacilityProfile");

            migrationBuilder.RenameColumn(
                name: "CustomerNameEn",
                table: "ServiceEntryFees",
                newName: "CustomerName");

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "ServiceEntryFees",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Province",
                table: "FacilityProfile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "FacilityProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceEntryFees_AmanOrgId",
                table: "ServiceEntryFees",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");
        }
    }
}
