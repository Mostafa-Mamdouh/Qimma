using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class IntegrationServiceEntryFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceEntryFees",
                columns: table => new
                {
                    ServiceEntryFeesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTerms = table.Column<int>(type: "int", nullable: false),
                    StageIdLookupSetTermId = table.Column<int>(type: "int", nullable: true),
                    ProcessIdLookupSetTermId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmanOrgId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceEntryFees", x => x.ServiceEntryFeesId);
                    table.ForeignKey(
                        name: "FK_ServiceEntryFees_LookupSetTerm_ProcessIdLookupSetTermId",
                        column: x => x.ProcessIdLookupSetTermId,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_ServiceEntryFees_LookupSetTerm_StageIdLookupSetTermId",
                        column: x => x.StageIdLookupSetTermId,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceEntryFees_AmanOrgId",
                table: "ServiceEntryFees",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceEntryFees_ProcessIdLookupSetTermId",
                table: "ServiceEntryFees",
                column: "ProcessIdLookupSetTermId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceEntryFees_StageIdLookupSetTermId",
                table: "ServiceEntryFees",
                column: "StageIdLookupSetTermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceEntryFees");
        }
    }
}
