using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class updates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingServiceTrnHeader_LookupSetTerm_StatusFlag",
                table: "BillingServiceTrnHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_NuclearMaterials_ItemSourceProfile_ItemSourceProfileId",
                table: "NuclearMaterials");

            migrationBuilder.DropIndex(
                name: "IX_NuclearMaterials_ItemSourceProfileId",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "ItemSourceProfileId",
                table: "NuclearMaterials");

            migrationBuilder.AddColumn<bool>(
                name: "NoSourceTagImageFlag",
                table: "TrnItemSource",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoSourceTagImageFlag",
                table: "ItemSourceProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "StatusFlag",
                table: "BillingServiceTrnHeader",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillingServiceTrnHeader_InvoiceSource",
                table: "BillingServiceTrnHeader",
                column: "InvoiceSource");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingServiceTrnHeader_LookupSetTerm_InvoiceSource",
                table: "BillingServiceTrnHeader",
                column: "InvoiceSource",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingServiceTrnHeader_LookupSetTerm_StatusFlag",
                table: "BillingServiceTrnHeader",
                column: "StatusFlag",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingServiceTrnHeader_LookupSetTerm_InvoiceSource",
                table: "BillingServiceTrnHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_BillingServiceTrnHeader_LookupSetTerm_StatusFlag",
                table: "BillingServiceTrnHeader");

            migrationBuilder.DropIndex(
                name: "IX_BillingServiceTrnHeader_InvoiceSource",
                table: "BillingServiceTrnHeader");

            migrationBuilder.DropColumn(
                name: "NoSourceTagImageFlag",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "NoSourceTagImageFlag",
                table: "ItemSourceProfile");

            migrationBuilder.AddColumn<int>(
                name: "ItemSourceProfileId",
                table: "NuclearMaterials",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusFlag",
                table: "BillingServiceTrnHeader",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_ItemSourceProfileId",
                table: "NuclearMaterials",
                column: "ItemSourceProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingServiceTrnHeader_LookupSetTerm_StatusFlag",
                table: "BillingServiceTrnHeader",
                column: "StatusFlag",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_NuclearMaterials_ItemSourceProfile_ItemSourceProfileId",
                table: "NuclearMaterials",
                column: "ItemSourceProfileId",
                principalTable: "ItemSourceProfile",
                principalColumn: "SourceId");
        }
    }
}
