using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changesentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Items",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Items",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Bids",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Bids",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirebaseToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "FirebaseToken",
                table: "AspNetUsers");
        }
    }
}
