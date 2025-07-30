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
    public bool NeedsEnrollmentApproval { get; private set; }

    private readonly List<CourseCategory> _categories = [];
    public IEnumerable<CourseCategory> Categories => _categories.AsReadOnly();

    private readonly List<Instructor> _instructors = [];
    public IEnumerable<Instructor> Instructors => _instructors.AsReadOnly();

    private readonly List<Admin> _admins = [];
    public IEnumerable<Admin> Admins => _admins.AsReadOnly();

    private readonly List<Student> _students = [];
    public IEnumerable<Student> Students => _students.AsReadOnly();

    private readonly List<Lecture> _lectures = [];
    public IEnumerable<Lecture> Lectures => _lectures.AsReadOnly();

    private readonly List<Document> _documents = [];
    public IEnumerable<Document> Documents => _documents.AsReadOnly();

    private readonly List<Exam> _exams = [];
    public IEnumerable<Exam> Exams => _exams.AsReadOnly();

    protected Course() {}
    
    public Course(Guid id, string name, string code, string? description, bool isPaid,
        float? subscriptionFees, bool needsEnrollmentApproval) : base(id)
    {
        Name = name;
        Code = code;
        Description = description;
        IsPaid = isPaid;
        SubscriptionFees = subscriptionFees;
        IsActive = true; // course is created active initially
        NeedsEnrollmentApproval = needsEnrollmentApproval;
    }

    public Course Activate(bool isActive)
    {
        IsActive = isActive;
        
        return this;
    }

    public Course UpdateCourseInfo(string name, string code, string? description, bool isPaid, float? subscriptionFees, bool needsEnrollmentApproval)
    {
        Name = name;
        Code = code;
        Description = description;
        IsPaid = isPaid;
        SubscriptionFees = subscriptionFees;
        NeedsEnrollmentApproval = needsEnrollmentApproval;
        
        return this;
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

    public void UpdateAllCourseCategories(IGuidGenerator guidGenerator, List<CourseCategory> updatedCourseCategories)
    {
        var categoriesToRemove = _categories.Where(c => 
            updatedCourseCategories.All(uc => uc.CategoryId != c.CategoryId)).ToList();
        
        RemoveCourseCategories(categoriesToRemove);

        var categoriesToAdd = updatedCourseCategories.Where(uc => 
            _categories.All(c => c.CategoryId != uc.CategoryId)).ToList();

        foreach (var courseCategory in categoriesToAdd)
        {
            AddCourseCategory(guidGenerator.Create(), courseCategory.CategoryId);
        }
    }

    public void RemoveCourseCategories(List<CourseCategory> courseCategories)
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
    
    public void RemoveAdmins(List<Admin> admins)
    {
        _admins.RemoveAll(admins);
    }

    #endregion

    #region Course student

    public void AddStudent(IGuidGenerator guidGenerator, Guid userId, Guid courseId, DateTime enrollmentDate,
        float score, bool isEnrollmentApproved, bool isActive)
    {
        _students.Add(new Student(
            id: guidGenerator.Create(),
            personId: userId,
            courseId: courseId,
            enrollmentDate: enrollmentDate,
            score: score,
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
            isEnrollmentApproved: updatedStudent.IsEnrollmentApproved,
            isActive: updatedStudent.IsActive);
    }

    public void RemoveStudents(List<Student> students)
    {
        _students.RemoveAll(students);
    }

    #endregion

    #region Course lecture

    public void AddLecture(IGuidGenerator guidGenerator, string name, string? description, int? length,
        DateTime? publishDate, string? youtubeLink)
    {
        _lectures.Add(new Lecture(
            id: guidGenerator.Create(),
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

    public void RemoveLectures(List<Lecture> lectures)
    {
        _lectures.RemoveAll(lectures);
    }

    #endregion

    #region Course document

    public void AddDocument(IGuidGenerator guidGenerator, string name, DocumentType type, Guid lectureId, string path,
        int size, int pagesCount)
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

    public void RemoveDocuments(List<Document> documents)
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

        foreach (var question in questions)
        {
            newExam.AddQuestion(
                guidGenerator: guidGenerator,
                examId: newExam.Id,
                content: question.Content,
                type: question.Type,
                correctAnswer: question.CorrectAnswer,
                needsManualChecking: question.NeedsManualChecking,
                score: question.Score,
                choices:  question.Choices.ToList()
            );
        }
        
        _exams.Add(newExam);
    }

    public void UpdateExam(IGuidGenerator guidGenerator, Exam updatedExam)
    {
        var exam = _exams.Find(l => l.Id == updatedExam.Id)
                   ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                       .WithData("EntityName", nameof(Exam))
                       .WithData("Id", updatedExam.Id.ToString());

        exam.Update(
            guidGenerator: guidGenerator,
            name: updatedExam.Name,
            description: updatedExam.Description,
            type: updatedExam.Type,
            score: updatedExam.Score,
            questions: updatedExam.Questions.ToList()
        );
        
    }

    public void RemoveExams(IEnumerable<Exam> exams)
    {
        _exams.RemoveAll(exams);
    }

    #endregion
}