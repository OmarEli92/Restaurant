using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unicam.Paradigmi._118301.Infrastructure.Migrations
{
    public partial class Initialmod2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_OrderedByUserUserId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderedByUserUserId",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderedByUserUserId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "OrderedByUserUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                newName: "IX_Orders_OrderedByUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_OrderedByUserUserId",
                table: "Orders",
                column: "OrderedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
