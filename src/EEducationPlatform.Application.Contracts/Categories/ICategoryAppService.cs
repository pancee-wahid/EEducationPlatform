using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EEducationPlatform.Categories;

public interface ICategoryAppService : IApplicationService
{
    Task<Guid> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task UpdateCategoryAsync(Guid id, UpdateCategoryDto updateCategoryDto);
    Task DeleteCategoryAsync(Guid id);
    Task<CategoryDto> GetAsync(Guid id, GetCategoryQueryDto queryDto);
}