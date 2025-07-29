using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EEducationPlatform.Aggregates.Categories;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EEducationPlatform.EntityFrameworkCore.Repositories;

public class CategoryRepository : EfCoreRepository<EEducationPlatformDbContext, Category, Guid>, ICategoryRepository
{
    public CategoryRepository(IDbContextProvider<EEducationPlatformDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Category> GetCategoryDetailsAsync(Guid id, int maxDepth = 1)
    {
        var dbSet = await GetDbSetAsync();

        var query = dbSet.AsQueryable();

        if (maxDepth > 0)
            query = dbSet.Include(x => x.SubCategories);

        var category = await query
                           .FirstOrDefaultAsync(x => x.Id == id)
                       ?? throw new EntityNotFoundException(typeof(Category), id);

        var currentCategories = category.SubCategories.ToList();
        for (int i = 1; i < maxDepth; i++)
        {
            List<Category> nextCategories = [];
            foreach (var subCategory in currentCategories)
            {
                await EnsureCollectionLoadedAsync(subCategory, x => x.SubCategories);
                nextCategories.AddRange(subCategory.SubCategories.ToList());
            }

            if (nextCategories.IsNullOrEmpty()) break;

            currentCategories = nextCategories;
        }

        return category;
    }

    public async Task<Category?> GetCategoryByCodeAsync(string code)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task<List<Category>> GetListAsync(string? filter, int maxResultCount, int skipCount,
        string? sorting, bool parentsOnly = false)
    {
        var dbSet = await GetDbSetAsync();

        return await dbSet
            .WhereIf(
                parentsOnly,
                category => category.ParentCategoryId == Guid.Empty || category.ParentCategoryId == null
            )
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                category => category.Name.ToLower().Contains(filter!.ToLower())
                            || category.Code.ToLower().Contains(filter!.ToLower())
                            || (category.Description ?? "").ToLower().Contains(filter!.ToLower())
                            || category.Id.ToString().ToLower().Contains(filter!.ToLower())
            )
            .OrderBy(x => sorting == "Code" ? x.Code : x.Name)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}