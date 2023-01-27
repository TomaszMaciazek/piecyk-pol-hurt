using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PiecykPolHurt.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedCoordinations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "SendPoints",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Logitude",
                table: "SendPoints",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "SendPoints");

            migrationBuilder.DropColumn(
                name: "Logitude",
                table: "SendPoints");
        }
    }
}
