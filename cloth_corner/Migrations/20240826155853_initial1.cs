using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cloth_corner.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_CartDetails_CartDetailsId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_CartDetailsId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "CartDetailsId",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "CartDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_CartId",
                table: "CartDetails",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_Cart_CartId",
                table: "CartDetails",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_Cart_CartId",
                table: "CartDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartDetails_CartId",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "CartDetails");

            migrationBuilder.AddColumn<int>(
                name: "CartDetailsId",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CartDetailsId",
                table: "Cart",
                column: "CartDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_CartDetails_CartDetailsId",
                table: "Cart",
                column: "CartDetailsId",
                principalTable: "CartDetails",
                principalColumn: "CartDetailsId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
