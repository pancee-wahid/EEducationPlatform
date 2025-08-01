using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Courses;

public class CourseCategory : FullAuditedEntity<Guid>
{
    public Guid CategoryId { get; private set; }
    public Guid CourseId { get; private set; }

    protected CourseCategory() {}
    public CourseCategory(Guid id, Guid categoryId, Guid courseId) : base(id)
    {
        CategoryId = categoryId;
        CourseId = courseId;
    }
}