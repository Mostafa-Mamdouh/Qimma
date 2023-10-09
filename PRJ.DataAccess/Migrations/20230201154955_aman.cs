using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class aman : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LookupSetTerm_LookupSetId",
                table: "LookupSetTerm");

            migrationBuilder.CreateIndex(
                name: "IX_LookupSetTerm_LookupSetId_AmanOrgId",
                table: "LookupSetTerm",
                columns: new[] { "LookupSetId", "AmanOrgId" },
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LookupSetTerm_LookupSetId_AmanOrgId",
                table: "LookupSetTerm");

            migrationBuilder.CreateIndex(
                name: "IX_LookupSetTerm_LookupSetId",
                table: "LookupSetTerm",
                column: "LookupSetId");
        }
    }
}
