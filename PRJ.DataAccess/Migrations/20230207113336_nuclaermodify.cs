using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class nuclaermodify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NuclearMaterialRadionulcides_NuclearMaterials_MaterialId",
                table: "NuclearMaterialRadionulcides");

            migrationBuilder.DropForeignKey(
                name: "FK_NuclearMaterials_LookupSetTerm_InitialMassUnit",
                table: "NuclearMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_NuclearMaterials_LookupSetTerm_NuclearMaterialType",
                table: "NuclearMaterials");

            migrationBuilder.DropIndex(
                name: "IX_NuclearMaterials_InitialMassUnit",
                table: "NuclearMaterials");

            migrationBuilder.DropIndex(
                name: "IX_NuclearMaterials_NuclearMaterialType",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "InitialActivityDate",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "InitialMass",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "InitialMassUnit",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "NuclearMaterialType",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "NuclearMaterials");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "NuclearMaterialRadionulcides",
                newName: "NuclearMaterialElementId");

            migrationBuilder.RenameIndex(
                name: "IX_NuclearMaterialRadionulcides_MaterialId",
                table: "NuclearMaterialRadionulcides",
                newName: "IX_NuclearMaterialRadionulcides_NuclearMaterialElementId");

            migrationBuilder.AlterColumn<string>(
                name: "ManufacturerSerialNo",
                table: "NuclearMaterials",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "NuclearMaterials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NuclearMaterialElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElementMass = table.Column<double>(type: "float", nullable: true),
                    NuclearMaterialType = table.Column<int>(type: "int", nullable: true),
                    ElementMassUnit = table.Column<int>(type: "int", nullable: true),
                    InitialMassUnit = table.Column<int>(type: "int", nullable: true),
                    NuclearMaterialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NuclearMaterialElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NuclearMaterialElements_LookupSetTerm_InitialMassUnit",
                        column: x => x.InitialMassUnit,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_NuclearMaterialElements_LookupSetTerm_NuclearMaterialType",
                        column: x => x.NuclearMaterialType,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_NuclearMaterialElements_NuclearMaterials_NuclearMaterialId",
                        column: x => x.NuclearMaterialId,
                        principalTable: "NuclearMaterials",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_ManufacturerSerialNo",
                table: "NuclearMaterials",
                column: "ManufacturerSerialNo",
                unique: true,
                filter: "[ManufacturerSerialNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterialElements_InitialMassUnit",
                table: "NuclearMaterialElements",
                column: "InitialMassUnit");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterialElements_NuclearMaterialId",
                table: "NuclearMaterialElements",
                column: "NuclearMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterialElements_NuclearMaterialType",
                table: "NuclearMaterialElements",
                column: "NuclearMaterialType");

            migrationBuilder.AddForeignKey(
                name: "FK_NuclearMaterialRadionulcides_NuclearMaterialElements_NuclearMaterialElementId",
                table: "NuclearMaterialRadionulcides",
                column: "NuclearMaterialElementId",
                principalTable: "NuclearMaterialElements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NuclearMaterialRadionulcides_NuclearMaterialElements_NuclearMaterialElementId",
                table: "NuclearMaterialRadionulcides");

            migrationBuilder.DropTable(
                name: "NuclearMaterialElements");

            migrationBuilder.DropIndex(
                name: "IX_NuclearMaterials_ManufacturerSerialNo",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "NuclearMaterials");

            migrationBuilder.RenameColumn(
                name: "NuclearMaterialElementId",
                table: "NuclearMaterialRadionulcides",
                newName: "MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_NuclearMaterialRadionulcides_NuclearMaterialElementId",
                table: "NuclearMaterialRadionulcides",
                newName: "IX_NuclearMaterialRadionulcides_MaterialId");

            migrationBuilder.AlterColumn<string>(
                name: "ManufacturerSerialNo",
                table: "NuclearMaterials",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InitialActivityDate",
                table: "NuclearMaterials",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InitialMass",
                table: "NuclearMaterials",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InitialMassUnit",
                table: "NuclearMaterials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NuclearMaterialType",
                table: "NuclearMaterials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "NuclearMaterials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_InitialMassUnit",
                table: "NuclearMaterials",
                column: "InitialMassUnit");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_NuclearMaterialType",
                table: "NuclearMaterials",
                column: "NuclearMaterialType");

            migrationBuilder.AddForeignKey(
                name: "FK_NuclearMaterialRadionulcides_NuclearMaterials_MaterialId",
                table: "NuclearMaterialRadionulcides",
                column: "MaterialId",
                principalTable: "NuclearMaterials",
                principalColumn: "SourceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NuclearMaterials_LookupSetTerm_InitialMassUnit",
                table: "NuclearMaterials",
                column: "InitialMassUnit",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_NuclearMaterials_LookupSetTerm_NuclearMaterialType",
                table: "NuclearMaterials",
                column: "NuclearMaterialType",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");
        }
    }
}
