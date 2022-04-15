using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.Infrastructure.Migrations
{
    public partial class Mig10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllergensProducts_Allergens_AllergenId",
                table: "AllergensProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Allergens",
                table: "Allergens");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Allergens",
                newName: "Allergen");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Category",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allergen",
                table: "Allergen",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllergensProducts_Allergen_AllergenId",
                table: "AllergensProducts",
                column: "AllergenId",
                principalTable: "Allergen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Category_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllergensProducts_Allergen_AllergenId",
                table: "AllergensProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Category_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Allergen",
                table: "Allergen");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Allergen",
                newName: "Allergens");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allergens",
                table: "Allergens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllergensProducts_Allergens_AllergenId",
                table: "AllergensProducts",
                column: "AllergenId",
                principalTable: "Allergens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
