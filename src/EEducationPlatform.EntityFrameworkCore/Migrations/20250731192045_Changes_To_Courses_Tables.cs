using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EEducationPlatform.Migrations
{
    /// <inheritdoc />
    public partial class Changes_To_Courses_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAdmin_AbpUsers_UserId",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseInstructor_AbpUsers_UserId",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudent_AbpUsers_UserId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "NeedsEnrollmentApproval",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Admin");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Student",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_UserId",
                table: "Student",
                newName: "IX_Student_PersonId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Instructor",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_UserId",
                table: "Instructor",
                newName: "IX_Instructor_PersonId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Admin",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Admin_UserId",
                table: "Admin",
                newName: "IX_Admin_PersonId");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Exam",
                type: "varchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "Exam",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "NeedsEnrollmentApproval",
                table: "Course",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FullNameAr = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullNameEn = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstNameAr = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstNameEn = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastNameAr = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastNameEn = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Person_UserId",
                table: "Person",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_Person_PersonId",
                table: "Admin",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Person_PersonId",
                table: "Instructor",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Person_PersonId",
                table: "Student",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Person_PersonId",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Person_PersonId",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Person_PersonId",
                table: "Student");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "NeedsEnrollmentApproval",
                table: "Course");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Student",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_PersonId",
                table: "Student",
                newName: "IX_Student_UserId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Instructor",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_PersonId",
                table: "Instructor",
                newName: "IX_Instructor_UserId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Admin",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Admin_PersonId",
                table: "Admin",
                newName: "IX_Admin_UserId");

            migrationBuilder.AddColumn<bool>(
                name: "NeedsEnrollmentApproval",
                table: "Student",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Admin",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "Admin",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAdmin_AbpUsers_UserId",
                table: "Admin",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInstructor_AbpUsers_UserId",
                table: "Instructor",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudent_AbpUsers_UserId",
                table: "Student",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
