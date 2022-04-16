using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MerchantApi.Migrations
{
    public partial class MyThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Merchants_MerchantId",
                table: "Stores");

            migrationBuilder.AlterColumn<int>(
                name: "MerchantId",
                table: "Stores",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "MerchantCode",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Merchants_MerchantId",
                table: "Stores",
                column: "MerchantId",
                principalTable: "Merchants",
                principalColumn: "MerchantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Merchants_MerchantId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "MerchantCode",
                table: "Stores");

            migrationBuilder.AlterColumn<int>(
                name: "MerchantId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Merchants_MerchantId",
                table: "Stores",
                column: "MerchantId",
                principalTable: "Merchants",
                principalColumn: "MerchantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
