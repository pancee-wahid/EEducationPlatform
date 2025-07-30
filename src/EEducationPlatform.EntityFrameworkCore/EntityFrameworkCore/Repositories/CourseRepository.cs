using System;
using System.Threading.Tasks;
using EEducationPlatform.Aggregates.Courses;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EEducationPlatform.EntityFrameworkCore.Repositories;

public class CourseRepository : EfCoreRepository<EEducationPlatformDbContext, Course, Guid>, ICourseRepository
{
    public CourseRepository(IDbContextProvider<EEducationPlatformDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {

    }

    
    public async Task<Course?> GetCourseByCodeAsync(string code)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(c => c.Code == code);
    }
}