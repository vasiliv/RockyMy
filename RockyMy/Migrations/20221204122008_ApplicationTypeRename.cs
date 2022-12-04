using Microsoft.EntityFrameworkCore.Migrations;

namespace RockyMy.Migrations
{
    public partial class ApplicationTypeRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ApplicationType_ApplicationTypeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationType",
                table: "ApplicationType");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "ApplicationType",
                newName: "ApplicationTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationTypes",
                table: "ApplicationTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ApplicationTypes_ApplicationTypeId",
                table: "Products",
                column: "ApplicationTypeId",
                principalTable: "ApplicationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ApplicationTypes_ApplicationTypeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationTypes",
                table: "ApplicationTypes");

            migrationBuilder.RenameTable(
                name: "ApplicationTypes",
                newName: "ApplicationType");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationType",
                table: "ApplicationType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ApplicationType_ApplicationTypeId",
                table: "Products",
                column: "ApplicationTypeId",
                principalTable: "ApplicationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
