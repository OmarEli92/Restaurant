using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unicam.Paradigmi._118301.Infrastructure.Migrations
{
    public partial class Initia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalCheck",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCheck",
                table: "Orders");
        }
    }
}
