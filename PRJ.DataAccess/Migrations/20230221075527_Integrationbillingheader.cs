using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class Integrationbillingheader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceEntryFees_AmanOrgId",
                table: "ServiceEntryFees");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "ServiceEntryFees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "ServiceEntryFees",
                type: "int",
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
