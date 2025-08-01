using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EEducationPlatform.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsActive_To_Course : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Course",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Course");
        }
    }
}
