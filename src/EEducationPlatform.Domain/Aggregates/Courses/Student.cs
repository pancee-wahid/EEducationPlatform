using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Courses;

public class Student : FullAuditedEntity<Guid>
{
    public Guid UserId { get; private set; }
    public Guid CourseId { get; private set; }
    public DateTime EnrollmentDate { get; private set; }
    public float Score { get; private set; } // calculated across all exams - may be deleted

    public Student(Guid id, Guid userId, Guid courseId, DateTime enrollmentDate, float score) : base(id)
    {
        UserId = userId;
        CourseId = courseId;
        EnrollmentDate = enrollmentDate;
        Score = score;
    }

    public Student Update(float score)
    {
        Score = score;

        return this;
    }
}