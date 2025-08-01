using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EEducationPlatform.Aggregates.Courses;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace EEducationPlatform.Aggregates.Categories;

public class CategoryManager : DomainService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICourseRepository _courseRepository;

    public CategoryManager(ICategoryRepository categoryRepository, ICourseRepository courseRepository)
    {
        _categoryRepository = categoryRepository;
        _courseRepository = courseRepository;
    }

    public async Task<Category> CreateAsync(Category category)
    {
        if (await _categoryRepository.GetCategoryByCodeAsync(category.Code) != null)
        {
            throw new BusinessException(EEducationPlatformDomainErrorCodes.CategoryWithSameCodeExists);
        }

        var createdCategory = new Category(
            id: GuidGenerator.Create(),
            name: category.Name,
            description: category.Description,
            code: category.Code,
            parentCategoryId: category.ParentCategoryId
        );

        if (createdCategory.ParentCategoryId.HasValue)
        {
            await UpdateParentOnAddingChild((Guid)createdCategory.ParentCategoryId!);
        }

        return await _categoryRepository.InsertAsync(createdCategory);
    }

    public async Task UpdateCategory(Guid id, Category updatedCategory)
    {
        var existingCategory = await _categoryRepository.GetAsync(id, false);
        var oldParentCategoryId = existingCategory.ParentCategoryId;
        var newParentCategoryId = updatedCategory.ParentCategoryId;

        existingCategory.Update(
            name: updatedCategory.Name,
            description: updatedCategory.Description,
            parentCategoryId: updatedCategory.ParentCategoryId
        );

        if (newParentCategoryId.HasValue && !oldParentCategoryId.HasValue) // add child
        {
            await UpdateParentOnAddingChild(newParentCategoryId.Value);
        }
        else if (!newParentCategoryId.HasValue && oldParentCategoryId.HasValue) // remove child
        {
            await UpdateParentOnRemovingChild(oldParentCategoryId.Value, existingCategory.Id);
        }
        else if (newParentCategoryId.HasValue && oldParentCategoryId.HasValue) // add to new, remove from old
        {
            await UpdateParentOnRemovingChild(oldParentCategoryId.Value, existingCategory.Id);
            await UpdateParentOnAddingChild(newParentCategoryId.Value);
        }
        // case: null in new and old, no need to handle parents

        await _categoryRepository.UpdateAsync(existingCategory);
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var hasCourses = await _courseRepository.AreAnyCoursesBelongToCategory(id);
        if (hasCourses)
        {
            throw new BusinessException(EEducationPlatformDomainErrorCodes.CategoryHasCourses);
        }
        
        var category = await _categoryRepository.GetAsync(id, includeDetails: false);
        if (category.HasSubCategories)
        {
            throw new BusinessException(EEducationPlatformDomainErrorCodes.CategoryHasSubCategories);
        }

        var parentCategoryId = category.ParentCategoryId;

        await _categoryRepository.DeleteAsync(category);

        if (parentCategoryId.HasValue)
        {
            await UpdateParentOnRemovingChild(parentCategoryId.Value, id);
        }
    }

    private async Task UpdateParentOnRemovingChild(Guid parentCategoryId, Guid subCategoryId)
    {
        var parentCategory = await _categoryRepository.GetCategoryDetailsAsync((Guid)parentCategoryId!);
        if (parentCategory.SubCategories.Where(sc => sc.Id != subCategoryId).ToList().IsNullOrEmpty())
        {
            parentCategory.SetHasSubCategories(false);
            await _categoryRepository.UpdateAsync(parentCategory);
        }
    }
    
    private async Task UpdateParentOnAddingChild(Guid parentCategoryId)
    {
        var parentCategory = await _categoryRepository.GetAsync(parentCategoryId, false);

        if (!parentCategory.HasSubCategories)
        {
            parentCategory.SetHasSubCategories(true);
            await _categoryRepository.UpdateAsync(parentCategory);
        }
    }
}