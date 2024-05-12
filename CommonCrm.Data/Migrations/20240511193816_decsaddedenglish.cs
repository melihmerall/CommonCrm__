using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonCrm.Data.Migrations
{
    /// <inheritdoc />
    public partial class decsaddedenglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfferDescriptionEnglish",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferDescriptionEnglish",
                table: "Products");
        }
    }
}
