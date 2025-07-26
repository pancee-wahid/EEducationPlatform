using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Courses;

public class Admin : FullAuditedEntity<Guid>
{
    public Guid PersonId { get; private set; }
    public Guid CourseId { get; private set; }

    public Admin(Guid id, Guid personId, Guid courseId) : base(id)
    {
        PersonId = personId;
        CourseId = courseId;
    }
}