using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ptuswphbackend.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Movies",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Movies");
        }
    }
}
