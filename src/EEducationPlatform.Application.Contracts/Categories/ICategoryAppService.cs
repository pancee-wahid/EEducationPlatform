using System;
using System.Threading.Tasks;
using EEducationPlatform.Categories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EEducationPlatform.Categories;

public interface ICategoryAppService : IApplicationService
{
    Task<Guid> CreateAsync(CreateCategoryDto dto);
    Task UpdateAsync(Guid id, UpdateCategoryDto dto);
    Task DeleteAsync(Guid id);
    Task<SpecificCategoryDto> GetAsync(Guid id, GetCategoryQueryDto queryDto);
    Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoriesQueryDto queryDto);
}