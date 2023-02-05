using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PiecykPolHurt.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class DictionaryValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DictionaryTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Order Status" },
                    { 2, "User Role" }
                });

            migrationBuilder.InsertData(
                table: "DictionaryValues",
                columns: new[] { "Id", "Description", "DictionaryTypeId", "Value" },
                values: new object[,]
                {
                    { 1, "Wysłane", 1, 0 },
                    { 2, "Odrzucone", 1, 1 },
                    { 3, "Zaakceptowane", 1, 2 },
                    { 4, "Anulowane", 1, 3 },
                    { 5, "Zakończone", 1, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DictionaryTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DictionaryValues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DictionaryValues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DictionaryValues",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DictionaryValues",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DictionaryValues",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DictionaryTypes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
