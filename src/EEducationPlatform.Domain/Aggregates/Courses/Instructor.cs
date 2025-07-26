using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Courses;

public class Instructor : FullAuditedEntity<Guid>
{
    public Guid PersonId { get; private set; }
    public Guid CourseId { get; private set; }
    public string? Experience { get; private set; }
    public string? Bio { get; private set; }

    public Instructor(Guid id, Guid personId, Guid courseId, string? experience, string? bio) : base(id)
    {
        PersonId = personId;
        CourseId = courseId;
        Experience = experience;
        Bio = bio;
    }

    public Instructor Update(string? experience, string? bio)
    {
        Experience = experience;
        Bio = bio;

        return this;
    }
}