using System;
using Volo.Abp.Domain.Repositories;

namespace EEducationPlatform.Aggregates.Courses;

public interface ICourseRepository : IBasicRepository<Course, Guid>
{
    
}
