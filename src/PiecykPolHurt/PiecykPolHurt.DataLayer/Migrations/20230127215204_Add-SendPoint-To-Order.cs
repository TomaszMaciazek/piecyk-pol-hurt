using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PiecykPolHurt.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddSendPointToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SendPointId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SendPointId",
                table: "Orders",
                column: "SendPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SendPoints_SendPointId",
                table: "Orders",
                column: "SendPointId",
                principalTable: "SendPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SendPoints_SendPointId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SendPointId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SendPointId",
                table: "Orders");
        }
    }
}
