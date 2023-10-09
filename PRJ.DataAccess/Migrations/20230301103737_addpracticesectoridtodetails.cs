using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class addpracticesectoridtodetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingServiceTrnDetails_BillingServiceTrnHeader_TransactionID",
                table: "BillingServiceTrnDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BillingServiceTrnHeader_ItemHierarchyStructure_InvoiceBU",
                table: "BillingServiceTrnHeader");

            migrationBuilder.DropIndex(
                name: "IX_BillingServiceTrnHeader_InvoiceBU",
                table: "BillingServiceTrnHeader");

            migrationBuilder.DropIndex(
                name: "IX_BillingServiceTrnDetails_TransactionID",
                table: "BillingServiceTrnDetails");

            migrationBuilder.AddColumn<int>(
                name: "BillingServiceTrnDetailsLineNum",
                table: "BillingServiceTrnHeader",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BillingServiceTrnDetailsTransactionID",
                table: "BillingServiceTrnHeader",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PracticeId",
                table: "BillingServiceTrnDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "BillingServiceTrnDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillingServiceTrnHeader_BillingServiceTrnDetailsLineNum_BillingServiceTrnDetailsTransactionID",
                table: "BillingServiceTrnHeader",
                columns: new[] { "BillingServiceTrnDetailsLineNum", "BillingServiceTrnDetailsTransactionID" });

            migrationBuilder.AddForeignKey(
                name: "FK_BillingServiceTrnHeader_BillingServiceTrnDetails_BillingServiceTrnDetailsLineNum_BillingServiceTrnDetailsTransactionID",
                table: "BillingServiceTrnHeader",
                columns: new[] { "BillingServiceTrnDetailsLineNum", "BillingServiceTrnDetailsTransactionID" },
                principalTable: "BillingServiceTrnDetails",
                principalColumns: new[] { "LineNum", "TransactionID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingServiceTrnHeader_BillingServiceTrnDetails_BillingServiceTrnDetailsLineNum_BillingServiceTrnDetailsTransactionID",
                table: "BillingServiceTrnHeader");

            migrationBuilder.DropIndex(
                name: "IX_BillingServiceTrnHeader_BillingServiceTrnDetailsLineNum_BillingServiceTrnDetailsTransactionID",
                table: "BillingServiceTrnHeader");

            migrationBuilder.DropColumn(
                name: "BillingServiceTrnDetailsLineNum",
                table: "BillingServiceTrnHeader");

            migrationBuilder.DropColumn(
                name: "BillingServiceTrnDetailsTransactionID",
                table: "BillingServiceTrnHeader");

            migrationBuilder.DropColumn(
                name: "PracticeId",
                table: "BillingServiceTrnDetails");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "BillingServiceTrnDetails");

            migrationBuilder.CreateIndex(
                name: "IX_BillingServiceTrnHeader_InvoiceBU",
                table: "BillingServiceTrnHeader",
                column: "InvoiceBU");

            migrationBuilder.CreateIndex(
                name: "IX_BillingServiceTrnDetails_TransactionID",
                table: "BillingServiceTrnDetails",
                column: "TransactionID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingServiceTrnDetails_BillingServiceTrnHeader_TransactionID",
                table: "BillingServiceTrnDetails",
                column: "TransactionID",
                principalTable: "BillingServiceTrnHeader",
                principalColumn: "TransactionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillingServiceTrnHeader_ItemHierarchyStructure_InvoiceBU",
                table: "BillingServiceTrnHeader",
                column: "InvoiceBU",
                principalTable: "ItemHierarchyStructure",
                principalColumn: "ItemStructureCode");
        }
    }
}
