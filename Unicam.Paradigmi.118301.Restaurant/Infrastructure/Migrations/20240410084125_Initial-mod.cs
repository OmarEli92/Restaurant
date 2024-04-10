using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unicam.Paradigmi._118301.Infrastructure.Migrations
{
    public partial class Initialmod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Orders_OrderID",
                table: "Dishes");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "Dishes",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Dishes_OrderID",
                table: "Dishes",
                newName: "IX_Dishes_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Orders_OrderId",
                table: "Dishes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Orders_OrderId",
                table: "Dishes");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Dishes",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_Dishes_OrderId",
                table: "Dishes",
                newName: "IX_Dishes_OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Orders_OrderID",
                table: "Dishes",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
