using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentalBook.Migrations
{
    /// <inheritdoc />
    public partial class UpdateD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e696a31-654d-4345-9b6d-d8584730cf24", "2", "Admin", "Admin" },
                    { "6a9828f7-b45e-46c6-a67e-c13c201f598f", "1", "SuperAdmin", "SuperAdmin" },
                    { "896f629c-9878-4533-abb6-9f5c5fdfa81d", "4", "User", "User" },
                    { "f2c35a60-c673-4108-ae1d-6a7debd266a0", "3", "Dealer", "Dealer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e696a31-654d-4345-9b6d-d8584730cf24");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a9828f7-b45e-46c6-a67e-c13c201f598f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "896f629c-9878-4533-abb6-9f5c5fdfa81d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2c35a60-c673-4108-ae1d-6a7debd266a0");

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
    }
}
