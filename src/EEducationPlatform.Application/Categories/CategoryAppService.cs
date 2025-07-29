using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EEducationPlatform.Aggregates.Categories;
using EEducationPlatform.Categories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

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
    public virtual async Task<Guid> CreateAsync(CreateCategoryDto dto)
    {
        var category = ObjectMapper.Map<CreateCategoryDto, Category>(dto);
        
        var result = await _categoryManager.CreateAsync(category);
        
        return result.Id;
    }

    public virtual async Task UpdateAsync(Guid id, UpdateCategoryDto dto)
    {
        dto.Id = id;
        var updatedCategory = ObjectMapper.Map<UpdateCategoryDto, Category>(dto);
        
        await _categoryManager.UpdateCategory(updatedCategory);    
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await _categoryManager.DeleteCategoryAsync(id);
    }

    /// <summary>
    /// Retrieves a specific category with its children and grandchildren to the specified depth in the request, or depth 1 as default.
    /// </summary>
    public virtual async Task<SpecificCategoryDto> GetAsync(Guid id, GetCategoryQueryDto queryDto)
    {
        var category = await _categoryRepository.GetCategoryDetailsAsync(id, queryDto.MaxDepth);

        return ObjectMapper.Map<Category, SpecificCategoryDto>(category); 
    }
    
    public virtual async Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoriesQueryDto queryDto)
    {
        var categories = await _categoryRepository.GetListAsync(
            queryDto.Filter,
            queryDto.MaxResultCount,
            queryDto.SkipCount,
            queryDto.Sorting,
            queryDto.ParentsOnly
        );
        
        var categoryDtos = ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories);

        return new PagedResultDto<CategoryDto>
        {
            TotalCount = categories.Count,
            Items = categoryDtos
        };
    }
}