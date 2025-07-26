using System;
using System.Threading.Tasks;
using EEducationPlatform.Categories.Dtos;
using Volo.Abp.Application.Services;

namespace EEducationPlatform.Categories;

public interface ICategoryAppService : IApplicationService
{
    Task<Guid> CreateAsync(CreateCategoryDto createCategoryDto);
    Task UpdateAsync(Guid id, UpdateCategoryDto updateCategoryDto);
    Task DeleteAsync(Guid id);
    Task<CategoryDto> GetAsync(Guid id, GetCategoryQueryDto queryDto);
}