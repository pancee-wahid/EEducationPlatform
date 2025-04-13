using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EEducationPlatform.Migrations
{
    /// <inheritdoc />
    public partial class Renamed_Some_Course_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop foreign keys that reference the tables being renamed
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_CourseLecture_LectureId",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_CourseStudent_StudentId",
                table: "Submission");

            // Note: FK_Document_CourseLecture_LectureId exists in the schema (seen in the Down method)
            // We need to drop it because it references CourseLecture, which will be renamed to Lecture
            migrationBuilder.DropForeignKey(
                name: "FK_CourseDocument_CourseLecture_LectureId",
                table: "CourseDocument");

            // Step 2: Rename the tables
            migrationBuilder.RenameTable(
                name: "CourseAdmin",
                newName: "Admin");

            migrationBuilder.RenameTable(
                name: "CourseDocument",
                newName: "Document");

            migrationBuilder.RenameTable(
                name: "CourseInstructor",
                newName: "Instructor");

            migrationBuilder.RenameTable(
                name: "CourseStudent",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "CourseLecture",
                newName: "Lecture");

            // Step 3: Rename the indexes (since they were renamed automatically by MySQL)
            // Original migration tries to create new indexes, which would fail since they already exist
            migrationBuilder.RenameIndex(
                name: "IX_CourseAdmin_CourseId",
                table: "Admin",
                newName: "IX_Admin_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseAdmin_UserId",
                table: "Admin",
                newName: "IX_Admin_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseDocument_CourseId",
                table: "Document",
                newName: "IX_Document_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseDocument_LectureId",
                table: "Document",
                newName: "IX_Document_LectureId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseInstructor_CourseId",
                table: "Instructor",
                newName: "IX_Instructor_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseInstructor_UserId",
                table: "Instructor",
                newName: "IX_Instructor_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseLecture_CourseId",
                table: "Lecture",
                newName: "IX_Lecture_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseStudent_CourseId",
                table: "Student",
                newName: "IX_Student_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseStudent_UserId",
                table: "Student",
                newName: "IX_Student_UserId");

            // Step 4: Recreate the foreign keys with updated names
            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Lecture_LectureId",
                table: "Exam",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Student_StudentId",
                table: "Submission",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Lecture_LectureId",
                table: "Document",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop the renamed foreign keys
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Lecture_LectureId",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Student_StudentId",
                table: "Submission");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Lecture_LectureId",
                table: "Document");

            // Step 2: Rename the indexes back to their original names
            migrationBuilder.RenameIndex(
                name: "IX_Admin_CourseId",
                table: "Admin",
                newName: "IX_CourseAdmin_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Admin_UserId",
                table: "Admin",
                newName: "IX_CourseAdmin_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Document_CourseId",
                table: "Document",
                newName: "IX_CourseDocument_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Document_LectureId",
                table: "Document",
                newName: "IX_CourseDocument_LectureId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_CourseId",
                table: "Instructor",
                newName: "IX_CourseInstructor_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_UserId",
                table: "Instructor",
                newName: "IX_CourseInstructor_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Lecture_CourseId",
                table: "Lecture",
                newName: "IX_CourseLecture_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_CourseId",
                table: "Student",
                newName: "IX_CourseStudent_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_UserId",
                table: "Student",
                newName: "IX_CourseStudent_UserId");

            // Step 3: Rename the tables back to their original names
            migrationBuilder.RenameTable(
                name: "Admin",
                newName: "CourseAdmin");

            migrationBuilder.RenameTable(
                name: "Document",
                newName: "CourseDocument");

            migrationBuilder.RenameTable(
                name: "Instructor",
                newName: "CourseInstructor");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "CourseStudent");

            migrationBuilder.RenameTable(
                name: "Lecture",
                newName: "CourseLecture");

            // Step 4: Recreate the foreign keys with their original names
            migrationBuilder.AddForeignKey(
                name: "FK_Exam_CourseLecture_LectureId",
                table: "Exam",
                column: "LectureId",
                principalTable: "CourseLecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_CourseStudent_StudentId",
                table: "Submission",
                column: "StudentId",
                principalTable: "CourseStudent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseDocument_CourseLecture_LectureId",
                table: "CourseDocument",
                column: "LectureId",
                principalTable: "CourseLecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
