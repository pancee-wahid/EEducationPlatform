using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Academies;

public class AcademyCourse : FullAuditedEntity<Guid>
{
    public Guid AcademyId { get; private set; }
    public Guid CourseId { get; private set; }

    public AcademyCourse(Guid id, Guid academyId, Guid courseId) : base(id)
    {
        AcademyId = academyId;
        CourseId = courseId;
    }
}