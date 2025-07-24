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
        var dbContext = await GetDbContextAsync();
        
        var query = dbContext.Set<Category>().AsQueryable();
        
        if (maxDepth > 0)
            query = dbContext.Set<Category>().AsQueryable().Include(x => x.SubCategories);
        
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
}