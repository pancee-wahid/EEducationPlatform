using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EEducationPlatform.Aggregates.Courses;

public interface ICourseRepository : IBasicRepository<Course, Guid>
{
    Task<Course?> GetCourseByCodeAsync(string code);
}
