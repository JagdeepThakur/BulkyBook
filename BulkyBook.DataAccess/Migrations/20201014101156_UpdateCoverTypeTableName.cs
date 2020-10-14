using Microsoft.EntityFrameworkCore.Migrations;

namespace BulkyBook.DataAccess.Migrations
{
    public partial class UpdateCoverTypeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CoverType",
                table: "CoverType");

            migrationBuilder.RenameTable(
                name: "CoverType",
                newName: "CoverTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoverTypes",
                table: "CoverTypes",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CoverTypes",
                table: "CoverTypes");

            migrationBuilder.RenameTable(
                name: "CoverTypes",
                newName: "CoverType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoverType",
                table: "CoverType",
                column: "ID");
        }
    }
}
