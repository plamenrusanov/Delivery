using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.Infrastructure.Migrations
{
    public partial class AddedCutlery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cutlery",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cutlery",
                table: "Orders");
        }
    }
}
