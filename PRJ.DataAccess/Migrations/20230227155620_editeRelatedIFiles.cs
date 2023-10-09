using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class editeRelatedIFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileNum",
                table: "RelatedItemsFiles");

            migrationBuilder.DropColumn(
                name: "UploadType",
                table: "RelatedItemsFiles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileNum",
                table: "RelatedItemsFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UploadType",
                table: "RelatedItemsFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
