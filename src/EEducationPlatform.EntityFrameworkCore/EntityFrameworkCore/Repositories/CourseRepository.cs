using System;
using EEducationPlatform.Aggregates.Courses;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EEducationPlatform.EntityFrameworkCore.Repositories;

public class CourseRepository : EfCoreRepository<EEducationPlatformDbContext, Course, Guid>, ICourseRepository
{
    public CourseRepository(IDbContextProvider<EEducationPlatformDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {

    }

    // public async Task<Person> FindByNameAsync(string name)
    // {
    //     var dbContext = await GetDbContextAsync();
    //     return await dbContext.Set<Person>()
    //         .Where(p => p.Name == name)
    //         .FirstOrDefaultAsync();
    // }
}