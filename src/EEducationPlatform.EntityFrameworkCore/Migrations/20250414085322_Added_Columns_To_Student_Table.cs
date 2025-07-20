using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EEducationPlatform.Migrations
{
    /// <inheritdoc />
    public partial class Added_Columns_To_Student_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Student",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnrollmentApproved",
                table: "Student",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NeedsEnrollmentApproval",
                table: "Student",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "IsEnrollmentApproved",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "NeedsEnrollmentApproval",
                table: "Student");
        }
    }
}
