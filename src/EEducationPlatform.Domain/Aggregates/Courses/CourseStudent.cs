using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Courses;

public class CourseStudent : FullAuditedEntity<Guid>
{
    public Guid UserId { get; private set; }
    public Guid CourseId { get; private set; }
    public DateTime EnrollmentDate { get; private set; }
    public float Score { get; private set; }

    public CourseStudent(Guid id, Guid userId, Guid courseId, DateTime enrollmentDate, float score) : base(id)
    {
        UserId = userId;
        CourseId = courseId;
        EnrollmentDate = enrollmentDate;
        Score = score;
    }

    public CourseStudent Update(float score)
    {
        Score = score;

        return this;
    }
}