using Microsoft.EntityFrameworkCore.Migrations;

namespace RockyMy.Migrations
{
    public partial class AddApplicationTypeToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationTypeId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ApplicationTypeId",
                table: "Products",
                column: "ApplicationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ApplicationType_ApplicationTypeId",
                table: "Products",
                column: "ApplicationTypeId",
                principalTable: "ApplicationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ApplicationType_ApplicationTypeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ApplicationType");

            migrationBuilder.DropIndex(
                name: "IX_Products_ApplicationTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ApplicationTypeId",
                table: "Products");
        }
    }
}
