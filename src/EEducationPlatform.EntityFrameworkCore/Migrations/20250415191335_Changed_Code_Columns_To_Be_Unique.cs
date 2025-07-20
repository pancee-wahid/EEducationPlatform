using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EEducationPlatform.Migrations
{
    /// <inheritdoc />
    public partial class Changed_Code_Columns_To_Be_Unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LookupValue_Code",
                table: "LookupValue",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LookupType_Code",
                table: "LookupType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_Code",
                table: "Course",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_Code",
                table: "Category",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LookupValue_Code",
                table: "LookupValue");

            migrationBuilder.DropIndex(
                name: "IX_LookupType_Code",
                table: "LookupType");

            migrationBuilder.DropIndex(
                name: "IX_Course_Code",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Category_Code",
                table: "Category");
        }
    }
}
