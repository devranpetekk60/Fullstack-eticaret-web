using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETicaretweb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: true),
                    URL = table.Column<string>(type: "TEXT", nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "productImages",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productImages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    isActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    URL = table.Column<string>(type: "TEXT", nullable: true),
                    quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductImageId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_productImages_ProductImageId",
                        column: x => x.ProductImageId,
                        principalTable: "productImages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "Id", "PasswordHash", "Role", "Username" },
                values: new object[] { 1, "67CD3F79C3A2B26A5F2BFDF1610E31452DB4FD4F60381CB317FF5C6F5E996DEF", "Admin", "Devran" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "CategoryName", "ImageUrl", "URL" },
                values: new object[,]
                {
                    { 1, "Telefon", "telefon.webp", "akilli-telefon" },
                    { 2, "Kıyafetler", "kiyafet.webp", "kiyafet" },
                    { 3, "Aksesuar", "aksesuar.jpg", "aksesuar" }
                });

            migrationBuilder.InsertData(
                table: "productImages",
                columns: new[] { "id", "ProductId", "Url" },
                values: new object[,]
                {
                    { 1, 0, "iphone15.jpg" },
                    { 2, 0, "iphone16.jpg" },
                    { 3, 0, "samsung-s24.jpg" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "CategoryId", "Description", "Price", "ProductImageId", "ProductName", "URL", "isActive", "quantity" },
                values: new object[,]
                {
                    { 1, 1, null, 45000, 1, "İphone15", "iphone15", true, 1 },
                    { 2, 1, null, 55000, 2, "İphone16", "iphone16", true, 1 },
                    { 3, 1, null, 40000, 3, "Samsung Galaxy S24", "samsung-s24", true, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoryId",
                table: "products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_products_ProductImageId",
                table: "products",
                column: "ProductImageId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "productImages");
        }
    }
}
