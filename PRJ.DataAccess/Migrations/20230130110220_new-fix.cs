using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class newfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnrichmentOfUranium",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "InitialActivityDate",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "NumberOfAcquiredSources",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "NumberOfConsumedSources",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "ShieldChemicalForm",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "ShieldPhysicalForm",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "EnrichmentOfUranium",
                table: "ItemSourceProfile");

            migrationBuilder.DropColumn(
                name: "InitialActivityDate",
                table: "ItemSourceProfile");

            migrationBuilder.DropColumn(
                name: "NumberOfAcquiredSources",
                table: "ItemSourceProfile");

            migrationBuilder.DropColumn(
                name: "NumberOfConsumedSources",
                table: "ItemSourceProfile");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "ItemSourceProfile");

            migrationBuilder.DropColumn(
                name: "ShieldChemicalForm",
                table: "ItemSourceProfile");

            migrationBuilder.DropColumn(
                name: "ShieldPhysicalForm",
                table: "ItemSourceProfile");

            migrationBuilder.AlterColumn<string>(
                name: "ManufacturerSerialNo",
                table: "TrnItemSource",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ManufacturerSerialNo",
                table: "ItemSourceProfile",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_ManufacturerSerialNo",
                table: "TrnItemSource",
                column: "ManufacturerSerialNo",
                unique: true,
                filter: "[ManufacturerSerialNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_ManufacturerSerialNo",
                table: "ItemSourceProfile",
                column: "ManufacturerSerialNo",
                unique: true,
                filter: "[ManufacturerSerialNo] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TrnItemSource_ManufacturerSerialNo",
                table: "TrnItemSource");

            migrationBuilder.DropIndex(
                name: "IX_ItemSourceProfile_ManufacturerSerialNo",
                table: "ItemSourceProfile");

            migrationBuilder.AlterColumn<string>(
                name: "ManufacturerSerialNo",
                table: "TrnItemSource",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnrichmentOfUranium",
                table: "TrnItemSource",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InitialActivityDate",
                table: "TrnItemSource",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAcquiredSources",
                table: "TrnItemSource",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfConsumedSources",
                table: "TrnItemSource",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "TrnItemSource",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShieldChemicalForm",
                table: "TrnItemSource",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShieldPhysicalForm",
                table: "TrnItemSource",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ManufacturerSerialNo",
                table: "ItemSourceProfile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnrichmentOfUranium",
                table: "ItemSourceProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InitialActivityDate",
                table: "ItemSourceProfile",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAcquiredSources",
                table: "ItemSourceProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfConsumedSources",
                table: "ItemSourceProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "ItemSourceProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShieldChemicalForm",
                table: "ItemSourceProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShieldPhysicalForm",
                table: "ItemSourceProfile",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
