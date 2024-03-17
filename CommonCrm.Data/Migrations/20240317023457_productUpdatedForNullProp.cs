using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonCrm.Data.Migrations
{
    /// <inheritdoc />
    public partial class productUpdatedForNullProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "KdvTL",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KdvEuro",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KdvDolar",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDolarDefault",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEuroDefault",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTlDefault",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnglishDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDolarDefault",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsEuroDefault",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsTlDefault",
                table: "Products");

            migrationBuilder.AlterColumn<decimal>(
                name: "KdvTL",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "KdvEuro",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "KdvDolar",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
