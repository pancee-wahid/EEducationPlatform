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

    private readonly List<Instructor> _instructors;
    public IEnumerable<Instructor> Instructors => _instructors.AsReadOnly();

    private readonly List<Admin> _admins;
    public IEnumerable<Admin> Admins => _admins.AsReadOnly();

    private readonly List<Student> _students;
    public IEnumerable<Student> Students => _students.AsReadOnly();

    private readonly List<Lecture> _lectures;
    public IEnumerable<Lecture> Lectures => _lectures.AsReadOnly();

    private readonly List<Document> _documents;
    public IEnumerable<Document> Documents => _documents.AsReadOnly();
    
    private readonly List<Exam> _exams;
    public IEnumerable<Exam> Exams => _exams.AsReadOnly();
    
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
        _exams = [];
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

    public void AddInstructor(Guid id, Guid userId, Guid courseId, string? experience, string? bio)
    {
        _instructors.Add(new Instructor(
            id: id,
            userId: userId,
            courseId: courseId,
            experience: experience,
            bio: bio
        ));
    }

    public void UpdateInstructor(Instructor updatedInstructor)
    {
        var courseLecturer = _instructors.Find(l => l.Id == updatedInstructor.Id)
                             ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                                 .WithData("EntityName", nameof(Instructor))
                                 .WithData("Id", updatedInstructor.Id.ToString());

        courseLecturer.Update(
            experience: updatedInstructor.Experience,
            bio: updatedInstructor.Bio
        );
    }

    public void RemoveInstructors(IEnumerable<Instructor> instructors)
    {
        _instructors.RemoveAll(instructors);
    }

    #endregion

    #region Course admin

    public void AddAdmin(Guid id, Guid userId, Guid courseId, string? experience, string? bio)
    {
        _admins.Add(new Admin(
            id: id,
            userId: userId,
            courseId: courseId,
            experience: experience,
            bio: bio
        ));
    }

    public void UpdateAdmin(Admin updatedAdmin)
    {
        var admin = _admins.Find(l => l.Id == updatedAdmin.Id)
                          ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                              .WithData("EntityName", nameof(Admin))
                              .WithData("Id", updatedAdmin.Id.ToString());

        admin.Update(
            experience: updatedAdmin.Experience,
            bio: updatedAdmin.Bio
        );
    }

    public void RemoveAdmins(IEnumerable<Admin> admins)
    {
        _admins.RemoveAll(admins);
    }

    #endregion

    #region Course student

    public void AddStudent(Guid id, Guid userId, Guid courseId, DateTime enrollmentDate, float score)
    {
        _students.Add(new Student(
            id: id,
            userId: userId,
            courseId: courseId,
            enrollmentDate: enrollmentDate,
            score: score
        ));
    }

    public void UpdateStudent(Student updatedStudent)
    {
        var student = _students.Find(l => l.Id == updatedStudent.Id)
                            ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                                .WithData("EntityName", nameof(Student))
                                .WithData("Id", updatedStudent.Id.ToString());

        student.Update(score: updatedStudent.Score);
    }

    public void RemoveStudents(IEnumerable<Student> students)
    {
        _students.RemoveAll(students);
    }

    #endregion

    #region Course lecture

    public void AddLecture(Guid id, string name, string? description, int? length, DateTime? publishDate,
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

    public void UpdateLecture(Lecture updatedLecture)
    {
        var courseLecture = _lectures.Find(l => l.Id == updatedLecture.Id)
                            ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                                .WithData("EntityName", nameof(Lecture))
                                .WithData("Id", updatedLecture.Id.ToString());

        courseLecture.Update(
            name: updatedLecture.Name,
            description: updatedLecture.Description,
            length: updatedLecture.Length,
            publishDate: updatedLecture.PublishDate,
            youtubeLink: updatedLecture.YoutubeLink);
    }

    public void RemoveLectures(IEnumerable<Lecture> lectures)
    {
        _lectures.RemoveAll(lectures);
    }

    #endregion

    #region Course document

    public void AddDocument(Guid id, string name, DocumentType type, Guid lectureId, string path, int size,
        int pagesCount)
    {
        _documents.Add(new Document(
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

    public void RemoveDocuments(IEnumerable<Document> documents)
    {
        _documents.RemoveAll(documents);
    }

    #endregion
    
    #region Course exam

    public void AddExam(Guid id, string name, string? description, Guid? lectureId, ExamType type, float score)
    {
        _exams.Add(new Exam(
            id: id,
            name: name, 
            description: description,  
            courseId: this.Id, 
            lectureId: lectureId,
            type: type,
            score: score
        ));
    }

    public void UpdateExam(Exam updatedExam)
    {
        var exam = _exams.Find(l => l.Id == updatedExam.Id)
                             ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                                 .WithData("EntityName", nameof(Exam))
                                 .WithData("Id", updatedExam.Id.ToString());

        exam.Update(
            name: updatedExam.Name,
            description: updatedExam.Description,
            type: updatedExam.Type,
            score: updatedExam.Score
        );
    }

    public void RemoveExams(IEnumerable<Exam> exams)
    {
        _exams.RemoveAll(exams);
    }

    #endregion
}