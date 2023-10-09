using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class nuclearMaterialUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityUnitId",
                table: "NuclearMaterials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Element",
                table: "NuclearMaterialRadionulcides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_QuantityUnitId",
                table: "NuclearMaterials",
                column: "QuantityUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_NuclearMaterials_LookupSetTerm_QuantityUnitId",
                table: "NuclearMaterials",
                column: "QuantityUnitId",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NuclearMaterials_LookupSetTerm_QuantityUnitId",
                table: "NuclearMaterials");

            migrationBuilder.DropIndex(
                name: "IX_NuclearMaterials_QuantityUnitId",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "QuantityUnitId",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "Element",
                table: "NuclearMaterialRadionulcides");
        }
    }
}
