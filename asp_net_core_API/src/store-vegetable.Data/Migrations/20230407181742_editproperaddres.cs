using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace store_vegetable.Data.Migrations
{
    /// <inheritdoc />
    public partial class editproperaddres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adrress",
                table: "Order",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Order",
                newName: "Adrress");
        }
    }
}
