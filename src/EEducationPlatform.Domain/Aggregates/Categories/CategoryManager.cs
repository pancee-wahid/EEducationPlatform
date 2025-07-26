using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace EEducationPlatform.Aggregates.Categories;

public class CategoryManager : DomainService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryManager(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> CreateAsync(Category category)
    {
        var createdCategory = new Category(
            id: GuidGenerator.Create(),
            name: category.Name,
            description: category.Description,
            code: category.Code,
            parentCategoryId: category.ParentCategoryId
        );

        if (createdCategory.ParentCategoryId.HasValue)
            await UpdateParentOnAddingChild((Guid)createdCategory.ParentCategoryId!);

        return await _categoryRepository.InsertAsync(createdCategory);
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

    public async Task UpdateCategory(Category updatedCategory)
    {
        var existingCategory = await _categoryRepository.GetAsync(updatedCategory.Id, false);
        var oldParentCategoryId = existingCategory.ParentCategoryId;
        var newParentCategoryId = updatedCategory.ParentCategoryId;
        
        existingCategory.Update(
            name: updatedCategory.Name,
            description: updatedCategory.Description,
            // code: updatedCategory.Code,
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
        var category = await _categoryRepository.GetAsync(id, includeDetails: false);
        if (category.HasSubCategories)
            throw new BusinessException(EEducationPlatformDomainErrorCodes.CategoryHasSubCategories);
        
        var parentCategoryId = category.ParentCategoryId;

        await _categoryRepository.DeleteAsync(category);

        if (parentCategoryId.HasValue) 
            await UpdateParentOnRemovingChild(parentCategoryId.Value, id);
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
}