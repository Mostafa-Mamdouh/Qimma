using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class IntegrationServicePaymentTerm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PaymentTerms",
                table: "ServiceEntryFees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceEntryFees_PaymentTerms",
                table: "ServiceEntryFees",
                column: "PaymentTerms");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceEntryFees_LookupSetTerm_PaymentTerms",
                table: "ServiceEntryFees",
                column: "PaymentTerms",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceEntryFees_LookupSetTerm_PaymentTerms",
                table: "ServiceEntryFees");

            migrationBuilder.DropIndex(
                name: "IX_ServiceEntryFees_PaymentTerms",
                table: "ServiceEntryFees");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentTerms",
                table: "ServiceEntryFees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
