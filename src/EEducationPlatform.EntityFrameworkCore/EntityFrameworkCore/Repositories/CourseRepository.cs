using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<List<Course>> GetCoursesByCategoryIdAsync(Guid categoryId)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .Include(course => course.Categories)
            .Where(course => 
                course.Categories.Any(courseCategory => courseCategory.CategoryId == categoryId))
            .ToListAsync();
    }
    
    public async Task<bool> AreAnyCoursesBelongToCategory(Guid categoryId)
    {
        var dbContext = await GetDbContextAsync();
        var query = from course in dbContext.Set<Course>()
            join courseCategory in dbContext.Set<CourseCategory>() on course.Id equals courseCategory.CourseId
            where courseCategory.CategoryId == categoryId
            select course;
        
        return await query.AnyAsync();
    }
}