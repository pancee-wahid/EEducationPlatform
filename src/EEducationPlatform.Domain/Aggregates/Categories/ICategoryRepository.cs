using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EEducationPlatform.Aggregates.Categories;

public interface ICategoryRepository : IBasicRepository<Category, Guid>
{
    Task<Category> GetCategoryDetailsAsync(Guid id);
}