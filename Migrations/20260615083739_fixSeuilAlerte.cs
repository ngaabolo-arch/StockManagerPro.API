using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManagerPro.API.Migrations
{
    /// <inheritdoc />
    public partial class fixSeuilAlerte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeuilleAlert",
                table: "Produits",
                newName: "SeuilAlerte");

            migrationBuilder.RenameColumn(
                name: "Destcription",
                table: "Produits",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeuilAlerte",
                table: "Produits",
                newName: "SeuilleAlert");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Produits",
                newName: "Destcription");
        }
    }
}
