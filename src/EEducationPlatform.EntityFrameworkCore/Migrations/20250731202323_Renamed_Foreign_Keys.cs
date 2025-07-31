using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EEducationPlatform.Migrations
{
    /// <inheritdoc />
    public partial class Renamed_Foreign_Keys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAdmin_Course_CourseId",
                table: "Admin");
            
            migrationBuilder.DropForeignKey(
                name: "FK_CourseDocument_Course_CourseId",
                table: "Document");
            
            migrationBuilder.DropForeignKey(
                name: "FK_CourseInstructor_Course_CourseId",
                table: "Instructor");
            
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLecture_Course_CourseId",
                table: "Lecture");
            
            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudent_Course_CourseId",
                table: "Student");
            
            
            migrationBuilder.AddForeignKey(
                name: "FK_Admin_Course_CourseId",
                table: "Admin",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            
            migrationBuilder.AddForeignKey(
                name: "FK_Document_Course_CourseId",
                table: "Document",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            
            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Course_CourseId",
                table: "Instructor",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            
            migrationBuilder.AddForeignKey(
                name: "FK_Lecture_Course_CourseId",
                table: "Lecture",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            
            migrationBuilder.AddForeignKey(
                name: "FK_Student_Course_CourseId",
                table: "Student",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Course_CourseId",
                table: "Admin");
            
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Course_CourseId",
                table: "Document");
            
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Course_CourseId",
                table: "Instructor");
            
            migrationBuilder.DropForeignKey(
                name: "FK_Lecture_Course_CourseId",
                table: "Lecture");
            
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Course_CourseId",
                table: "Student");
            
            
            migrationBuilder.AddForeignKey(
                name: "FK_CourseAdmin_Course_CourseId",
                table: "Admin",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            
            migrationBuilder.AddForeignKey(
                name: "FK_CourseDocument_Course_CourseId",
                table: "Document",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            
            migrationBuilder.AddForeignKey(
                name: "FK_CourseInstructor_Course_CourseId",
                table: "Instructor",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            
            migrationBuilder.AddForeignKey(
                name: "FK_CourseLecture_Course_CourseId",
                table: "Lecture",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            
            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudent_Course_CourseId",
                table: "Student",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
