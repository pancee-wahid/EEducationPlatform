using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EEducationPlatform.Aggregates.Categories;

public interface ICategoryRepository : IBasicRepository<Category, Guid>
{
    Task<Category> GetCategoryDetailsAsync(Guid id, int maxDepth = 1);
    Task<Category?> GetCategoryByCodeAsync(string code);

    Task<List<Category>> GetListAsync(string? filter, int maxResultCount, int skipCount,
        string sorting = "Name", bool parentsOnly = false);
    
    Task<List<Guid>> GetUnfoundCategoriesIds(List<Guid> categoriesIds);
}