using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentalBook.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePinCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "PinCode",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "PinCode",
                keyValue: null,
                column: "PinCode",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "PinCode",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
    }
}
