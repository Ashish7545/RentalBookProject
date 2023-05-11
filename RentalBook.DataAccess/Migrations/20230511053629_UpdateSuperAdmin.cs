using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalBook.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d8b44f70-7644-4244-b2cb-d8289356c761", "1", "SuperAdmin", "SuperAdmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8b44f70-7644-4244-b2cb-d8289356c761");
        }
    }
}
