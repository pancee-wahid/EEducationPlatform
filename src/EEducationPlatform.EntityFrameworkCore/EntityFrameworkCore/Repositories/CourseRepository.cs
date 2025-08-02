using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EEducationPlatform.Aggregates.Categories;
using EEducationPlatform.Aggregates.Courses;
using EEducationPlatform.ViewModels;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EEducationPlatform.EntityFrameworkCore.Repositories;

public class CourseRepository : EfCoreRepository<EEducationPlatformDbContext, Course, Guid>, ICourseRepository
{
    public CourseRepository(IDbContextProvider<EEducationPlatformDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {

    }

    public override async Task<IQueryable<Course>> WithDetailsAsync()
    {
        return (await base.WithDetailsAsync())
            .Include(c => c.Categories)
            .Include(c => c.Admins)
            .Include(c => c.Instructors)
            .Include(c => c.Students)
            .Include(c => c.Lectures)
            .Include(c => c.Documents)
            .Include(c => c.Exams);
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

    public async Task<CourseViewModel> GetSpecificCourseDetails(Guid id)
    {
        var dbContext = await GetDbContextAsync();
        
        var query = from course in dbContext.Set<Course>()
            where course.Id == id
            select new  CourseViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Code = course.Code,
                Description = course.Description,
                IsActive = course.IsActive,
                IsPaid = course.IsPaid,
                SubscriptionFees = course.SubscriptionFees,
                NeedsEnrollmentApproval = course.NeedsEnrollmentApproval,
                Categories = (from courseCategory in dbContext.Set<CourseCategory>() 
                    join category in dbContext.Set<Category>() on courseCategory.CategoryId equals category.Id
                    where courseCategory.CourseId == id
                    select new  CourseCategoryViewModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Code = category.Code,
                        Description = category.Description
                    }).ToList()
            };
        
        return await query.FirstOrDefaultAsync() 
               ?? throw new EntityNotFoundException(typeof(Course), id);
    }
}