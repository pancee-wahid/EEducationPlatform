using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        {
            var parentCategory = await _categoryRepository.GetAsync((Guid)createdCategory.ParentCategoryId!, false);
            parentCategory.SetHasSubCategories(true);
            
            await _categoryRepository.UpdateAsync(parentCategory);
        }

        return await _categoryRepository.InsertAsync(createdCategory);
    }

    public async Task UpdateCategory(Category category)
    {
        var existingCategory = await _categoryRepository.GetAsync(category.Id, false);

        existingCategory.Update(
            name: category.Name,
            description: category.Description,
            code: category.Code,
            parentCategoryId: category.ParentCategoryId
        );
        
        await _categoryRepository.UpdateAsync(existingCategory);
    }
    
    public async Task DeleteCategoryAsync(Guid id)
    {
        var category = await _categoryRepository.GetAsync(id, includeDetails: false);
        var parentCategoryId = category.ParentCategoryId;
        
        await _categoryRepository.DeleteAsync(category);
        
        if (!parentCategoryId.HasValue) return;
        
        var parentCategory = await _categoryRepository.GetCategoryDetailsAsync((Guid)parentCategoryId!);
        if (parentCategory.SubCategories.Where(sc => sc.Id != id).ToList().IsNullOrEmpty())
        {
            parentCategory.SetHasSubCategories(false);
            await _categoryRepository.UpdateAsync(parentCategory);
        }
    }
}