using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonCrm.Data.Migrations
{
    /// <inheritdoc />
    public partial class offerProductsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferProduct_Offers_OfferId",
                table: "OfferProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferProduct",
                table: "OfferProduct");

            migrationBuilder.RenameTable(
                name: "OfferProduct",
                newName: "OfferProducts");

            migrationBuilder.RenameIndex(
                name: "IX_OfferProduct_OfferId",
                table: "OfferProducts",
                newName: "IX_OfferProducts_OfferId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferProducts",
                table: "OfferProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferProducts_Offers_OfferId",
                table: "OfferProducts",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferProducts_Offers_OfferId",
                table: "OfferProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferProducts",
                table: "OfferProducts");

            migrationBuilder.RenameTable(
                name: "OfferProducts",
                newName: "OfferProduct");

            migrationBuilder.RenameIndex(
                name: "IX_OfferProducts_OfferId",
                table: "OfferProduct",
                newName: "IX_OfferProduct_OfferId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferProduct",
                table: "OfferProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferProduct_Offers_OfferId",
                table: "OfferProduct",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id");
        }
    }
}
