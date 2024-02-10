using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioWebsite.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixedImageUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageURL",
                value: "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/birds.jpg?updatedAt=1707594856304");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageURL",
                value: "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/sun.jpg?updatedAt=1707594855618");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageURL",
                value: "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/explosion.jpg?updatedAt=1707594855179");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageURL",
                value: "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/holy.jpg?updatedAt=1707594853722");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageURL",
                value: "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/Devotion%20Gray.jpg?updatedAt=1707594734206");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageURL",
                value: "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/No%20Tobaccoshirt2.png?updatedAt=1707594733986");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageURL",
                value: "https://ik.imagekit.io/valter/StefanPortfolio/Shop/Prints/tr:w-800/birds.jpg?updatedAt=1707594856304");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageURL",
                value: "https://ik.imagekit.io/valter/StefanPortfolio/Shop/Prints/tr:w-800/sun.jpg?updatedAt=1707594855618");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageURL",
                value: "https://ik.imagekit.io/valter/StefanPortfolio/Shop/Prints/tr:w-800/explosion.jpg?updatedAt=1707594855179");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageURL",
                value: "https://ik.imagekit.io/valter/StefanPortfolio/Shop/Prints/tr:w-800/holy.jpg?updatedAt=1707594853722");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageURL",
                value: "https://ik.imagekit.io/valter/StefanPortfolio/Shop/Prints/tr:w-800/Devotion%20Gray.jpg?updatedAt=1707594734206");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageURL",
                value: "https://ik.imagekit.io/valter/StefanPortfolio/Shop/Prints/tr:w-800/No%20Tobaccoshirt2.png?updatedAt=1707594733986");
        }
    }
}
