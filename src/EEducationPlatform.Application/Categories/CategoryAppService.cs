using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EEducationPlatform.Aggregates.Categories;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

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
    
    public async Task<Guid> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        var category = ObjectMapper.Map<CreateCategoryDto, Category>(createCategoryDto);
        
        var result = await _categoryManager.CreateAsync(category);
        
        return result.Id;
    }

    public async Task UpdateCategoryAsync(Guid id, UpdateCategoryDto updateCategoryDto)
    {
        updateCategoryDto.Id = id;
        var updatedCategory = ObjectMapper.Map<UpdateCategoryDto, Category>(updateCategoryDto);
        
        await _categoryManager.UpdateCategory(updatedCategory);    
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        await _categoryManager.DeleteCategoryAsync(id);
    }

    public async Task<CategoryDto> GetAsync(Guid id, GetCategoryQueryDto queryDto)
    {
        var category = await _categoryRepository.GetCategoryDetailsAsync(id);

        return ObjectMapper.Map<Category, CategoryDto>(category); 
    }
}