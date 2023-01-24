using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PiecykPolHurt.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToSendPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SendPoints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SendPoints");
        }
    }
}
