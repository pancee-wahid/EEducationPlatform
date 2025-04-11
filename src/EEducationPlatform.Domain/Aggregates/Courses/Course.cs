using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Courses;

public class Course : FullAuditedAggregateRoot<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string? Description { get; private set; }
    public bool IsPaid { get; private set; }
    public float? SubscriptionFees { get; private set; } // null only when IsPaid is false

    private readonly List<CourseCategory> _categories;
    public IEnumerable<CourseCategory> Categories => _categories.AsReadOnly();

    private readonly List<CourseInstructor> _instructors;
    public IEnumerable<CourseInstructor> Instructors => _instructors.AsReadOnly();

    private readonly List<CourseAdmin> _admins;
    public IEnumerable<CourseAdmin> Admins => _admins.AsReadOnly();

    private readonly List<CourseStudent> _students;
    public IEnumerable<CourseStudent> Students => _students.AsReadOnly();

    private readonly List<CourseLecture> _lectures;
    public IEnumerable<CourseLecture> Lectures => _lectures.AsReadOnly();

    private readonly List<CourseDocument> _documents;
    public IEnumerable<CourseDocument> Documents => _documents.AsReadOnly();

    // List of Exams

    public Course(Guid id, string name, string code, string? description, bool isPaid,
        float? subscriptionFees) : base(id)
    {
        Name = name;
        Code = code;
        Description = description;
        IsPaid = isPaid;
        SubscriptionFees = subscriptionFees;
        _categories = [];
        _instructors = [];
        _admins = [];
        _students = [];
        _lectures = [];
        _documents = [];
    }

    #region Course category

    public void AddCourseCategory(Guid id, Guid categoryId)
    {
        _categories.Add(new CourseCategory(
            id: id,
            categoryId: categoryId,
            courseId: this.Id)
        );
    }

    public void RemoveCourseCategories(IEnumerable<CourseCategory> courseCategories)
    {
        _categories.RemoveAll(courseCategories);
    }

    #endregion

    #region Course instructor

    public void AddCourseInstructor(Guid id, Guid userId, Guid courseId, string? experience, string? bio)
    {
        _instructors.Add(new CourseInstructor(
            id: id,
            userId: userId,
            courseId: courseId,
            experience: experience,
            bio: bio
        ));
    }

    public void UpdateCourseInstructor(CourseInstructor updatedCourseInstructor)
    {
        var courseLecturer = _instructors.Find(l => l.Id == updatedCourseInstructor.Id)
                             ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                                 .WithData("EntityName", nameof(CourseInstructor))
                                 .WithData("Id", updatedCourseInstructor.Id.ToString());

        courseLecturer.Update(
            experience: updatedCourseInstructor.Experience,
            bio: updatedCourseInstructor.Bio
        );
    }

    public void RemoveCourseInstructors(IEnumerable<CourseInstructor> courseInstructors)
    {
        _instructors.RemoveAll(courseInstructors);
    }

    #endregion

    #region Course admin

    public void AddCourseAdmin(Guid id, Guid userId, Guid courseId, string? experience, string? bio)
    {
        _admins.Add(new CourseAdmin(
            id: id,
            userId: userId,
            courseId: courseId,
            experience: experience,
            bio: bio
        ));
    }

    public void UpdateCourseAdmin(CourseAdmin updatedCourseAdmin)
    {
        var courseAdmin = _admins.Find(l => l.Id == updatedCourseAdmin.Id)
                          ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                              .WithData("EntityName", nameof(CourseAdmin))
                              .WithData("Id", updatedCourseAdmin.Id.ToString());

        courseAdmin.Update(
            experience: updatedCourseAdmin.Experience,
            bio: updatedCourseAdmin.Bio
        );
    }

    public void RemoveCourseAdmins(IEnumerable<CourseAdmin> courseAdmins)
    {
        _admins.RemoveAll(courseAdmins);
    }

    #endregion

    #region Course student

    public void AddCourseStudent(Guid id, Guid userId, Guid courseId, DateTime enrollmentDate, float score)
    {
        _students.Add(new CourseStudent(
            id: id,
            userId: userId,
            courseId: courseId,
            enrollmentDate: enrollmentDate,
            score: score
        ));
    }

    public void UpdateCourseStudent(CourseStudent updatedCourseStudent)
    {
        var courseStudent = _students.Find(l => l.Id == updatedCourseStudent.Id)
                            ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                                .WithData("EntityName", nameof(CourseStudent))
                                .WithData("Id", updatedCourseStudent.Id.ToString());

        courseStudent.Update(score: updatedCourseStudent.Score);
    }

    public void RemoveCourseStudents(IEnumerable<CourseStudent> courseStudents)
    {
        _students.RemoveAll(courseStudents);
    }

    #endregion

    #region Course lecture

    public void AddCourseLecture(Guid id, string name, string? description, int? length, DateTime? publishDate,
        string? youtubeLink)
    {
        _lectures.Add(new(id: id,
            name: name,
            description: description,
            courseId: this.Id,
            length: length,
            publishDate: publishDate,
            youtubeLink: youtubeLink
        ));
    }

    public void UpdateCourseLecture(CourseLecture updatedCourseLecture)
    {
        var courseLecture = _lectures.Find(l => l.Id == updatedCourseLecture.Id)
                            ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                                .WithData("EntityName", nameof(CourseLecture))
                                .WithData("Id", updatedCourseLecture.Id.ToString());

        courseLecture.Update(
            name: updatedCourseLecture.Name,
            description: updatedCourseLecture.Description,
            length: updatedCourseLecture.Length,
            publishDate: updatedCourseLecture.PublishDate,
            youtubeLink: updatedCourseLecture.YoutubeLink);
    }

    public void RemoveCourseLectures(IEnumerable<CourseLecture> courseLectures)
    {
        _lectures.RemoveAll(courseLectures);
    }

    #endregion

    #region Course document

    public void AddCourseDocument(Guid id, string name, DocumentType type, Guid lectureId, string path, int size,
        int pagesCount)
    {
        _documents.Add(new CourseDocument(
            id: id,
            name: name,
            type: type,
            courseId: this.Id,
            lectureId: lectureId,
            path: path,
            size: size,
            pagesCount: pagesCount
        ));
    }

    public void RemoveCourseDocuments(IEnumerable<CourseDocument> courseDocuments)
    {
        _documents.RemoveAll(courseDocuments);
    }

    #endregion
}