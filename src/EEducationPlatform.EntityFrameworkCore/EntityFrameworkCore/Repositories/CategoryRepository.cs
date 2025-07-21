using System;
using System.Linq;
using System.Threading;
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

    public async Task<Category> GetCategoryDetailsAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();
        var query = dbContext.Set<Category>().AsQueryable()
            .Include(x => x.SubCategories.Where(sc => !sc.IsDeleted));
        
        var category = await query
                           .FirstOrDefaultAsync(x => x.Id == id)
                       ?? throw new EntityNotFoundException(typeof(Category), id);

        return category;
    }
}