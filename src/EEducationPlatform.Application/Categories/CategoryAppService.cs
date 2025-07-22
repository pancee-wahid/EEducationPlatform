using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EEducationPlatform.Aggregates.Categories;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Validation;

namespace EEducationPlatform.Categories;

public class CategoryAppService: ApplicationService, ICategoryAppService
{
    private readonly CategoryManager _categoryManager;
    private readonly ICategoryRepository _categoryRepository;
    public CategoryAppService(CategoryManager categoryManager, ICategoryRepository categoryRepository)
    {
        _categoryManager = categoryManager;
        _categoryRepository = categoryRepository;
    }
    
    // all methods are virtual as FluentValidation doesn't work automatically while using Conventional Controllers
    // unless methods are all virtual
    // in case of switching to custom controllers, as we use dependency injection and inject IXAppService to it,
    // we can remove virtual from methods here
    public virtual async Task<Guid> CreateAsync(CreateCategoryDto createCategoryDto)
    {
        var category = ObjectMapper.Map<CreateCategoryDto, Category>(createCategoryDto);
        
        var result = await _categoryManager.CreateAsync(category);
        
        return result.Id;
    }

    public virtual async Task UpdateAsync(Guid id, UpdateCategoryDto updateCategoryDto)
    {
        updateCategoryDto.Id = id;
        var updatedCategory = ObjectMapper.Map<UpdateCategoryDto, Category>(updateCategoryDto);
        
        await _categoryManager.UpdateCategory(updatedCategory);    
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await _categoryManager.DeleteCategoryAsync(id);
    }

    public virtual async Task<CategoryDto> GetAsync(Guid id, GetCategoryQueryDto queryDto)
    {
        var category = await _categoryRepository.GetCategoryDetailsAsync(id);

        return ObjectMapper.Map<Category, CategoryDto>(category); 
    }
}