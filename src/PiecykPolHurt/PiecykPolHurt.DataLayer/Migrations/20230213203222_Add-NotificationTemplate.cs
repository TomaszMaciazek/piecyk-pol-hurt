using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PiecykPolHurt.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "NotificationTemplates",
                columns: new[] { "Id", "Body", "Subject", "Type" },
                values: new object[,]
                {
                    { 1, "Zamówienie o numerze {0} zostało zaakceptowane", "Zamówienie nr. {0} zostło zaakceptowane", 1 },
                    { 2, "Zamówienie o numerze {0} zostało odrzucone", "Zamówienie nr. {0} zostło odrzucone", 2 },
                    { 3, "Zamówienie o numerze {0} zostało anulowane", "Zamówienie nr. {0} zostło anulowane", 4 },
                    { 4, "Zamówienie o numerze {0} zostało zrealizowane", "Zamówienie nr. {0} zostło zrealizowane", 3 },
                    { 5, "Utworzono nowe zamówienie. Jego numer to {0}", "Utworzono nowe zamówienie", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationTemplates");
        }
    }
}
