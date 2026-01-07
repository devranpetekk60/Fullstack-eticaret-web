using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretweb.Migrations
{
    /// <inheritdoc />
    public partial class cokabir : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productImages_ProductImageId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_ProductImageId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ProductImageId",
                table: "products");

            migrationBuilder.UpdateData(
                table: "productImages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ProductId", "Url" },
                values: new object[] { 1, "/images/iphone15.jpg" });

            migrationBuilder.UpdateData(
                table: "productImages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ProductId", "Url" },
                values: new object[] { 2, "/images/iphone16.jpg" });

            migrationBuilder.UpdateData(
                table: "productImages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ProductId", "Url" },
                values: new object[] { 3, "/images/samsung-s24.jpg" });

            migrationBuilder.CreateIndex(
                name: "IX_productImages_ProductId",
                table: "productImages",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_productImages_products_ProductId",
                table: "productImages",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productImages_products_ProductId",
                table: "productImages");

            migrationBuilder.DropIndex(
                name: "IX_productImages_ProductId",
                table: "productImages");

            migrationBuilder.AddColumn<int>(
                name: "ProductImageId",
                table: "products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "productImages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ProductId", "Url" },
                values: new object[] { 0, "iphone15.jpg" });

            migrationBuilder.UpdateData(
                table: "productImages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ProductId", "Url" },
                values: new object[] { 0, "iphone16.jpg" });

            migrationBuilder.UpdateData(
                table: "productImages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ProductId", "Url" },
                values: new object[] { 0, "samsung-s24.jpg" });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "id",
                keyValue: 1,
                column: "ProductImageId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "id",
                keyValue: 2,
                column: "ProductImageId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "id",
                keyValue: 3,
                column: "ProductImageId",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_products_ProductImageId",
                table: "products",
                column: "ProductImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_products_productImages_ProductImageId",
                table: "products",
                column: "ProductImageId",
                principalTable: "productImages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
