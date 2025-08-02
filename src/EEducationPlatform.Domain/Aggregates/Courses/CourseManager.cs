using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EEducationPlatform.Aggregates.Categories;
using EEducationPlatform.Aggregates.Persons;
using EEducationPlatform.Localization;
using Microsoft.Extensions.Localization;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Users;

namespace EEducationPlatform.Aggregates.Courses;

public class CourseManager(
    ICourseRepository courseRepository,
    ICategoryRepository categoryRepository,
    IStringLocalizer<EEducationPlatformResource> localizer,
    IPersonRepository personRepository,
    ICurrentUser currentUser)
    : DomainService
{
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

        // Add course categories
        if (course.Categories.Any())
        {
            await ValidateCategoriesAreFound(course.Categories.Select(c => c.CategoryId).ToList());
        }

        foreach (var category in course.Categories)
        {
            createdCourse.AddCourseCategory(GuidGenerator.Create(), category.CategoryId);
        }

        // Add current user to course admins
        var person = await personRepository.FindPersonByUserIdAsync(currentUser.GetId())
                     ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.MissingPerson);

        createdCourse.AddAdmin(GuidGenerator, person.Id);

        return await courseRepository.InsertAsync(createdCourse);
    }
    
    public async Task UpdateAsync(Guid id, Course updatedCourse)
    {
        var existingCourse = await courseRepository.GetAsync(id, includeDetails: false);
        await courseRepository.EnsureCollectionLoadedAsync(existingCourse, c => c.Categories);
        
        await ValidateCurrentUserIsAdmin(existingCourse);

        if (existingCourse.Code != updatedCourse.Code)
        {
            await ValidateCourseCodeUniqueness(updatedCourse.Code);
        }

        existingCourse.UpdateCourseInfo(
            name: updatedCourse.Name,
            code: updatedCourse.Code,
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

        await courseRepository.UpdateAsync(existingCourse);
    }

    private async Task ValidateCourseCodeUniqueness(string code)
    {
        if (await courseRepository.GetCourseByCodeAsync(code) != null)
        {
            throw new BusinessException(EEducationPlatformDomainErrorCodes.CourseWithSameCodeExists);
        }
    }

    private async Task ValidateCategoriesAreFound(List<Guid> categoriesIds)
    {
        var unfoundCategories = await categoryRepository.GetUnfoundCategoriesIds(
            categoriesIds);

        if (!unfoundCategories.IsNullOrEmpty())
        {
            var unfoundIds = string.Join(", ", unfoundCategories);
            throw new BusinessException(EEducationPlatformDomainErrorCodes.CategoriesUnfound)
                .WithData("CategoriesIds", unfoundIds);
        }
    }

    public async Task ActivateAsync(Guid id, bool activate)
    {
        var course = await courseRepository.GetAsync(id, includeDetails: false);
        
        await ValidateCurrentUserIsAdmin(course);

        if (activate == course.IsActive)
        {
            throw new BusinessException(EEducationPlatformDomainErrorCodes.AlreadyInSpecifiedActivationState)
                .WithData("Status", activate ? localizer["Active"] : localizer["Inactive"]);
        }

        course.Activate(activate);

        await courseRepository.UpdateAsync(course);
    }
    
    private async Task ValidateCurrentUserIsAdmin(Course course)
    {
        await courseRepository.EnsureCollectionLoadedAsync(course, c => c.Admins);

        var currentPerson = await personRepository.FindPersonByUserIdAsync(currentUser.GetId())
                            ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.MissingPerson);
        
        if (course.Admins.All(a => a.PersonId != currentPerson.Id))
        {
            throw new BusinessException(EEducationPlatformDomainErrorCodes.AdminOnlyCanUpdateCourse);
        }
    }
}