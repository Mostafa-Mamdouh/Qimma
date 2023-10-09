using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class permiteAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermitDetailsDescAr",
                table: "PermitDetailsProfile");

            migrationBuilder.DropColumn(
                name: "PermitDetailsDescEn",
                table: "PermitDetailsProfile");

            migrationBuilder.AlterColumn<string>(
                name: "PermitNumber",
                table: "PermitDetailsProfile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacilityId",
                table: "PermitDetailsProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PermitType",
                table: "PermitDetailsProfile",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermitDetailsProfile_FacilityId",
                table: "PermitDetailsProfile",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitDetailsProfile_PermitType",
                table: "PermitDetailsProfile",
                column: "PermitType");

            migrationBuilder.AddForeignKey(
                name: "FK_PermitDetailsProfile_FacilityProfile_FacilityId",
                table: "PermitDetailsProfile",
                column: "FacilityId",
                principalTable: "FacilityProfile",
                principalColumn: "FacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermitDetailsProfile_LookupSetTerm_PermitType",
                table: "PermitDetailsProfile",
                column: "PermitType",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermitDetailsProfile_FacilityProfile_FacilityId",
                table: "PermitDetailsProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_PermitDetailsProfile_LookupSetTerm_PermitType",
                table: "PermitDetailsProfile");

            migrationBuilder.DropIndex(
                name: "IX_PermitDetailsProfile_FacilityId",
                table: "PermitDetailsProfile");

            migrationBuilder.DropIndex(
                name: "IX_PermitDetailsProfile_PermitType",
                table: "PermitDetailsProfile");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "PermitDetailsProfile");

            migrationBuilder.DropColumn(
                name: "PermitType",
                table: "PermitDetailsProfile");

            migrationBuilder.AlterColumn<string>(
                name: "PermitNumber",
                table: "PermitDetailsProfile",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermitDetailsDescAr",
                table: "PermitDetailsProfile",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermitDetailsDescEn",
                table: "PermitDetailsProfile",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);
        }
    }
}
