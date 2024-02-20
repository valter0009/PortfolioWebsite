using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortfolioWebsite.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Shirts" },
                    { 2, "Stickers" },
                    { 3, "Prints" },
                    { 4, "Posters" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageURL", "Name", "Price", "Qty" },
                values: new object[,]
                {
                    { 1, 1, "Merch for my band called Suntari", "https://ik.imagekit.io/valter/StefanPortfolio/Shop/shirts/tr:w-800/suntarishirt2.jpg?updatedAt=1707594734174", "Suntari - Black T-Shirt", 25m, 1000 },
                    { 2, 1, "Merch for my other band called Inger Cowboy", "https://ik.imagekit.io/valter/StefanPortfolio/Shop/shirts/tr:w-800/INGERTR%C3%96JA.jpg?updatedAt=1707594733912", "Inger Cowboy - Black T-Shirt", 25m, 1000 },
                    { 3, 1, "Merch for my third band called Los Mucus", "https://ik.imagekit.io/valter/StefanPortfolio/Shop/shirts/tr:w-800/mucusshirt.jpg?updatedAt=1707594733635", "Los Mucus - Black T-Shirt", 25m, 1000 },
                    { 4, 2, "A cool sticker to stick on anything you want", "https://ik.imagekit.io/valter/StefanPortfolio/Gallery/Images/tr:w-800/GalleryImage%20(33).jpg?updatedAt=1705912745773", "Obeshimi - Sticker", 3m, 1000 },
                    { 5, 2, "A cool Los Mucus sticker to stick on anything you want", "https://ik.imagekit.io/valter/StefanPortfolio/Shop/stickers/tr:w-800/stickerredversion2.jpg?updatedAt=1707594734695", "Los Mucus - Sticker", 3m, 1000 },
                    { 6, 2, "A cool Los Mucus sticker to stick on anything you want", "https://ik.imagekit.io/valter/StefanPortfolio/Shop/stickers/tr:w-800/sticker.jpg?updatedAt=1707594734694", "Los Mucus - Sticker", 3m, 1000 },
                    { 7, 2, "A cool Los Mucus sticker to stick on anything you want", "https://ik.imagekit.io/valter/StefanPortfolio/Shop/stickers/tr:w-800/Mucusskull.jpg?updatedAt=1707594734689", "Los Mucus - Sticker", 3m, 1000 },
                    { 8, 3, "A 400x200 Print", "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/birds.jpg?updatedAt=1707594856304", "Birds - Print", 40m, 1000 },
                    { 9, 3, "A 400x200 Print", "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/sun.jpg?updatedAt=1707594855618", "Ascension - Print", 40m, 1000 },
                    { 10, 3, "A 400x200 Print", "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/explosion.jpg?updatedAt=1707594855179", "Explosion - Print", 40m, 1000 },
                    { 11, 3, "A 300x200 Print", "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/holy.jpg?updatedAt=1707594853722", "Mother - Print", 40m, 1000 },
                    { 12, 3, "A 400x200 Print", "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/Devotion%20Gray.jpg?updatedAt=1707594734206", "Devotion - Print", 40m, 1000 },
                    { 13, 4, "A poster from a gig at Flat Cap, Helsingborg", "https://ik.imagekit.io/valter/StefanPortfolio/Shop/posters/tr:w-800/Flatcap.jpg?updatedAt=1707594735646", "Los Mucus / Flat Cap - Poster", 20m, 1000 },
                    { 14, 4, "A poster from a gig at Grand, Malmö", "https://ik.imagekit.io/valter/StefanPortfolio/Shop/posters/tr:w-800/GRANDversion2.jpg?updatedAt=1707594734910", "Los Mucus / Grand - Poster", 20m, 1000 },
                    { 15, 4, "A poster from a gig at Flat Cap, Helsingborg", "https://ik.imagekit.io/valter/StefanPortfolio/Shop/posters/tr:w-800/Flatcap2.jpg?updatedAt=1707594734836", "Los Mucus / Flat Cap - Poster", 20m, 1000 },
                    { 16, 4, "A poster from a gig at Hemgården, Lund", "https://ik.imagekit.io/valter/StefanPortfolio/Shop/posters/tr:w-800/Posterklar.jpg?updatedAt=1707594734268", "Los Mucus / Hemgården - Poster", 20m, 1000 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "ProductCategories");
        }
    }
}
