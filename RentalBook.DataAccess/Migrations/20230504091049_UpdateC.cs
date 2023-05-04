using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentalBook.Migrations
{
    /// <inheritdoc />
    public partial class UpdateC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "098a68c8-5af0-4534-a952-fba2a5d16445");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61a1af7d-fdb3-462b-8e2f-631661c9c32e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62e1c069-be20-4237-af93-eb7461ab3ade");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "debe93b1-17c0-4648-a56b-0c39c4a574c7");

            migrationBuilder.AddColumn<int>(
                name: "RentDuration",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RentDuration",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "18f07cc1-fe95-4179-8ef3-c8c1519bff20", "2", "Admin", "Admin" },
                    { "4622f749-257a-430a-823a-ef558abb9ca8", "4", "User", "User" },
                    { "62dd02e2-afd3-4093-ab1b-fac7b1974724", "1", "SuperAdmin", "SuperAdmin" },
                    { "b621cfeb-1f68-410d-b0b8-3be8cbda4ea8", "3", "Dealer", "Dealer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18f07cc1-fe95-4179-8ef3-c8c1519bff20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4622f749-257a-430a-823a-ef558abb9ca8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62dd02e2-afd3-4093-ab1b-fac7b1974724");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b621cfeb-1f68-410d-b0b8-3be8cbda4ea8");

            migrationBuilder.DropColumn(
                name: "RentDuration",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "RentDuration",
                table: "OrderDetails");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "098a68c8-5af0-4534-a952-fba2a5d16445", "1", "SuperAdmin", "SuperAdmin" },
                    { "61a1af7d-fdb3-462b-8e2f-631661c9c32e", "2", "Admin", "Admin" },
                    { "62e1c069-be20-4237-af93-eb7461ab3ade", "3", "Dealer", "Dealer" },
                    { "debe93b1-17c0-4648-a56b-0c39c4a574c7", "4", "User", "User" }
                });
        }
    }
}
