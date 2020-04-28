using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class InitialSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Klanten",
                columns: new[] { "KlantId", "Naam" },
                values: new object[,]
                {
                    { 1, "Marge" },
                    { 2, "Homer" },
                    { 3, "Lisa" },
                    { 4, "Maggie" },
                    { 5, "Bart" }
                });

            migrationBuilder.InsertData(
                table: "Rekeningen",
                columns: new[] { "RekeningNr", "KlantId", "Saldo" },
                values: new object[] { "BE68123456789012", 1, 1000m });

            migrationBuilder.InsertData(
                table: "Rekeningen",
                columns: new[] { "RekeningNr", "KlantId", "Saldo" },
                values: new object[] { "BE68234567890169", 1, 2000m });

            migrationBuilder.InsertData(
                table: "Rekeningen",
                columns: new[] { "RekeningNr", "KlantId", "Saldo" },
                values: new object[] { "BE68345678901212", 2, 500m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Klanten",
                keyColumn: "KlantId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Klanten",
                keyColumn: "KlantId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Klanten",
                keyColumn: "KlantId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rekeningen",
                keyColumn: "RekeningNr",
                keyValue: "BE68123456789012");

            migrationBuilder.DeleteData(
                table: "Rekeningen",
                keyColumn: "RekeningNr",
                keyValue: "BE68234567890169");

            migrationBuilder.DeleteData(
                table: "Rekeningen",
                keyColumn: "RekeningNr",
                keyValue: "BE68345678901212");

            migrationBuilder.DeleteData(
                table: "Klanten",
                keyColumn: "KlantId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Klanten",
                keyColumn: "KlantId",
                keyValue: 2);
        }
    }
}
