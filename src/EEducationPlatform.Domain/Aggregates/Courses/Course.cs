using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Guids;

namespace EEducationPlatform.Aggregates.Courses;

public class Course : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string? Description { get; private set; }
    public bool IsPaid { get; private set; }
    public float? SubscriptionFees { get; private set; } // null only when IsPaid is false
    public bool IsActive { get; private set; } 

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
        IsActive = true; // course is created active initially
        _categories = [];
        _instructors = [];
        _admins = [];
        _students = [];
        _lectures = [];
        _documents = [];
        _exams = [];
    }

    public Course Activate(bool isActive)
    {
        IsActive = isActive;
        
        return this;
    }

    public Course UpdateCourseInfo(string name, string code, string? description)
    {
        Name = name;
        Code = code;
        Description = description;

        return this;
    }

    public Course UpdateCourseFeesInfo(bool isPaid, float? subscriptionFees)
    {
        IsPaid = isPaid;
        SubscriptionFees = subscriptionFees;

        return this;
    }
    
    #region Course category

    public void AddCourseCategory(IGuidGenerator guidGenerator, Guid categoryId)
    {
        _categories.Add(new CourseCategory(
            id: guidGenerator.Create(),
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

    public void AddInstructor(IGuidGenerator guidGenerator, Guid userId, Guid courseId, string? experience, string? bio)
    {
        _instructors.Add(new Instructor(
            id: guidGenerator.Create(),
            personId: userId,
            courseId: courseId,
            experience: experience,
            bio: bio
        ));
    }

    public void UpdateInstructor(Guid id, string? experience, string? bio)
    {
        var instructor = _instructors.Find(l => l.Id == id)
                             ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                                 .WithData("EntityName", nameof(Instructor))
                                 .WithData("Id", id.ToString());

        instructor.Update(
            experience: experience,
            bio: bio
        );
    }

    public void RemoveInstructors(IEnumerable<Instructor> instructors)
    {
        _instructors.RemoveAll(instructors);
    }

    #endregion

    #region Course admin

    public void AddAdmin(IGuidGenerator guidGenerator, Guid userId, Guid courseId)
    {
        _admins.Add(new Admin(
            id: guidGenerator.Create(),
            personId: userId,
            courseId: courseId
        ));
    }
    
    public void RemoveAdmins(IEnumerable<Admin> admins)
    {
        _admins.RemoveAll(admins);
    }

    #endregion

    #region Course student

    public void AddStudent(IGuidGenerator guidGenerator, Guid userId, Guid courseId, DateTime enrollmentDate,
        float score, bool needsEnrollmentApproval, bool isEnrollmentApproved, bool isActive)
    {
        _students.Add(new Student(
            id: guidGenerator.Create(),
            personId: userId,
            courseId: courseId,
            enrollmentDate: enrollmentDate,
            score: score,
            needsEnrollmentApproval: needsEnrollmentApproval,
            isEnrollmentApproved: isEnrollmentApproved,
            isActive: isActive));
    }

    public void UpdateStudent(Student updatedStudent)
    {
        var student = _students.Find(l => l.Id == updatedStudent.Id)
                      ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                          .WithData("EntityName", nameof(Student))
                          .WithData("Id", updatedStudent.Id.ToString());

        student.Update(
            score: updatedStudent.Score,
            needsEnrollmentApproval: updatedStudent.NeedsEnrollmentApproval,
            isEnrollmentApproved: updatedStudent.IsEnrollmentApproved,
            isActive: updatedStudent.IsActive);
    }

    public void RemoveStudents(IEnumerable<Student> students)
    {
        _students.RemoveAll(students);
    }

    #endregion

    #region Course lecture

    public void AddLecture(IGuidGenerator guidGenerator, string name, string? description, int? length,
        DateTime? publishDate,
        string? youtubeLink)
    {
        _lectures.Add(new(id: guidGenerator.Create(),
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

    public void AddDocument(IGuidGenerator guidGenerator, string name, DocumentType type, Guid lectureId, string path,
        int size,
        int pagesCount)
    {
        _documents.Add(new Document(
            id: guidGenerator.Create(),
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

    public void AddExam(IGuidGenerator guidGenerator, string name, string? description, Guid? lectureId, ExamType type,
        float score, List<Question> questions)
    {
        var newExam = new Exam(
            id: guidGenerator.Create(),
            name: name,
            description: description,
            courseId: this.Id,
            lectureId: lectureId,
            type: type,
            score: score
        );

        _exams.Add(newExam);

        foreach (var question in questions)
        {
            newExam.AddQuestion(
                id: guidGenerator.Create(),
                examId: newExam.Id,
                content: question.Content,
                type: question.Type,
                correctAnswer: question.CorrectAnswer,
                needsManualChecking: question.NeedsManualChecking,
                score: question.Score
            );
        }
    }

    public void UpdateExam(IGuidGenerator guidGenerator, Exam updatedExam)
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
        
        exam.UpdateAllQuestions(guidGenerator, updatedExam.Questions.ToList());
    }

    public void RemoveExams(IEnumerable<Exam> exams)
    {
        _exams.RemoveAll(exams);
    }

    #endregion
}