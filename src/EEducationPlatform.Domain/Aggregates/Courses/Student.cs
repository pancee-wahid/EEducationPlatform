using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Courses;

public class Student : FullAuditedEntity<Guid>
{
    public Guid PersonId { get; private set; }
    public Guid CourseId { get; private set; }
    public bool IsEnrollmentApproved { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime EnrollmentDate { get; private set; }
    public float Score { get; private set; } // calculated across all exams - may be deleted

    public Student(Guid id, Guid personId, Guid courseId, DateTime enrollmentDate, float score,
        bool isEnrollmentApproved, bool isActive) : base(id)
    {
        PersonId = personId;
        CourseId = courseId;
        EnrollmentDate = enrollmentDate;
        Score = score;
        IsEnrollmentApproved = isEnrollmentApproved;
        IsActive = isActive;
    }

    public Student Update(float score, bool isEnrollmentApproved, bool isActive)
    {
        Score = score;
        IsEnrollmentApproved = isEnrollmentApproved;
        IsActive = isActive;
        
        return this;
    }
    
    
    public Student Activate(bool isActive)
    {
        IsActive = isActive;
        
        return this;
    }

}