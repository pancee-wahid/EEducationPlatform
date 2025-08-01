using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EEducationPlatform.Aggregates.Categories;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace EEducationPlatform.Aggregates.Courses;

public class CourseManager : DomainService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICategoryRepository _categoryRepository;
    
    public CourseManager(ICourseRepository courseRepository,  ICategoryRepository categoryRepository)
    {
        _courseRepository  = courseRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Course> CreateAsync(Course course)
    {
        await ValidateCourseCodeUniqueness(course.Code);
        
        var createdCourse = new Course(
            id: GuidGenerator.Create(),
            name: course.Name,
            code: course.Code,
            description: course.Description,
            isPaid: course.IsPaid,
            subscriptionFees: course.SubscriptionFees,
            needsEnrollmentApproval: course.NeedsEnrollmentApproval
        );
        
        if (course.Categories.Any())
        {
            await ValidateCategoriesAreFound(course.Categories.Select(c => c.CategoryId).ToList());
        }
        
        foreach (var category in course.Categories)
        {
            createdCourse.AddCourseCategory(GuidGenerator.Create(), category.CategoryId);
        }
        
        return await _courseRepository.InsertAsync(createdCourse);
    }
    
    public async Task UpdateAsync(Guid id, Course updatedCourse)
    {
        var existingCourse = await _courseRepository.GetAsync(id);
        await _courseRepository.EnsureCollectionLoadedAsync(existingCourse, c => c.Categories);
            
        if (existingCourse.Code != updatedCourse.Code)
        {
            await ValidateCourseCodeUniqueness(updatedCourse.Code);
        }
        
        existingCourse.UpdateCourseInfo(
            name:  updatedCourse.Name,
            code:  updatedCourse.Code,
            description: updatedCourse.Description,
            isPaid: updatedCourse.IsPaid,
            subscriptionFees: updatedCourse.SubscriptionFees,
            needsEnrollmentApproval: updatedCourse.NeedsEnrollmentApproval
        );
        
        if (updatedCourse.Categories.Any())
        {
            await ValidateCategoriesAreFound(updatedCourse.Categories.Select(c => c.CategoryId).ToList());
        }

        existingCourse.UpdateAllCourseCategories(GuidGenerator, updatedCourse.Categories.ToList());
        
        await _courseRepository.UpdateAsync(existingCourse);
    }
    
    private async Task ValidateCourseCodeUniqueness(string code)
    {
        if (await _courseRepository.GetCourseByCodeAsync(code) != null)
        {
            throw new BusinessException(EEducationPlatformDomainErrorCodes.CourseWithSameCodeExists);
        }    
    }

    private async Task ValidateCategoriesAreFound(List<Guid> categoriesIds)
    {
        var unfoundCategories = await _categoryRepository.GetUnfoundCategoriesIds(
            categoriesIds);

        if (!unfoundCategories.IsNullOrEmpty())
        {
            var unfoundIds = string.Join(", ",  unfoundCategories);
            throw new BusinessException(EEducationPlatformDomainErrorCodes.CategoriesUnfound)
                .WithData("CategoriesIds",  unfoundIds);
        }
    }
}