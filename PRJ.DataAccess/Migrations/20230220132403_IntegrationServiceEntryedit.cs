using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class IntegrationServiceEntryedit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceEntryFees_LookupSetTerm_ProcessIdLookupSetTermId",
                table: "ServiceEntryFees");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceEntryFees_LookupSetTerm_StageIdLookupSetTermId",
                table: "ServiceEntryFees");

            migrationBuilder.RenameColumn(
                name: "StageIdLookupSetTermId",
                table: "ServiceEntryFees",
                newName: "StageId");

            migrationBuilder.RenameColumn(
                name: "ProcessIdLookupSetTermId",
                table: "ServiceEntryFees",
                newName: "ProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceEntryFees_StageIdLookupSetTermId",
                table: "ServiceEntryFees",
                newName: "IX_ServiceEntryFees_StageId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceEntryFees_ProcessIdLookupSetTermId",
                table: "ServiceEntryFees",
                newName: "IX_ServiceEntryFees_ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceEntryFees_LookupSetTerm_ProcessId",
                table: "ServiceEntryFees",
                column: "ProcessId",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceEntryFees_LookupSetTerm_StageId",
                table: "ServiceEntryFees",
                column: "StageId",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceEntryFees_LookupSetTerm_ProcessId",
                table: "ServiceEntryFees");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceEntryFees_LookupSetTerm_StageId",
                table: "ServiceEntryFees");

            migrationBuilder.RenameColumn(
                name: "StageId",
                table: "ServiceEntryFees",
                newName: "StageIdLookupSetTermId");

            migrationBuilder.RenameColumn(
                name: "ProcessId",
                table: "ServiceEntryFees",
                newName: "ProcessIdLookupSetTermId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceEntryFees_StageId",
                table: "ServiceEntryFees",
                newName: "IX_ServiceEntryFees_StageIdLookupSetTermId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceEntryFees_ProcessId",
                table: "ServiceEntryFees",
                newName: "IX_ServiceEntryFees_ProcessIdLookupSetTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceEntryFees_LookupSetTerm_ProcessIdLookupSetTermId",
                table: "ServiceEntryFees",
                column: "ProcessIdLookupSetTermId",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceEntryFees_LookupSetTerm_StageIdLookupSetTermId",
                table: "ServiceEntryFees",
                column: "StageIdLookupSetTermId",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");
        }
    }
}
