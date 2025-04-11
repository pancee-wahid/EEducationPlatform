using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Courses;

public class CourseInstructor : FullAuditedEntity<Guid>
{
    public Guid UserId { get; private set; }
    public Guid CourseId { get; private set; }
    public string? Experience { get; private set; }
    public string? Bio { get; private set; }

    public CourseInstructor(Guid id, Guid userId, Guid courseId, string? experience, string? bio) : base(id)
    {
        UserId = userId;
        CourseId = courseId;
        Experience = experience;
        Bio = bio;
    }

    public CourseInstructor Update(string? experience, string? bio)
    {
        Experience = experience;
        Bio = bio;

        return this;
    }
}