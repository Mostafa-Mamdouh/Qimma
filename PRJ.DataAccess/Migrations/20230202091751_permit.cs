using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class permit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "PermitDetailsProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PermitDetailsProfile",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PermitDetailsProfile",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "PermitDetailsProfile",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "PermitDetailsProfile",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermitDetailsProfile_AmanOrgId",
                table: "PermitDetailsProfile",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PermitDetailsProfile_AmanOrgId",
                table: "PermitDetailsProfile");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "PermitDetailsProfile");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PermitDetailsProfile");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PermitDetailsProfile");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PermitDetailsProfile");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "PermitDetailsProfile");
        }
    }
}
