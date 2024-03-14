using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonCrm.Data.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class relationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Attribute_AttributeId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_AttributeId",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Attribute",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_ProductId",
                table: "Attribute",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_Product_ProductId",
                table: "Attribute",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Product_ProductId",
                table: "Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_ProductId",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Attribute");

            migrationBuilder.CreateIndex(
                name: "IX_Product_AttributeId",
                table: "Product",
                column: "AttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Attribute_AttributeId",
                table: "Product",
                column: "AttributeId",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
