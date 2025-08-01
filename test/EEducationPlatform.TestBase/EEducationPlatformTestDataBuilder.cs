using System.Threading.Tasks;
using EEducationPlatform.Aggregates.Categories;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace EEducationPlatform;

public class EEducationPlatformTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;
    private readonly ICategoryRepository _categoryRepository;
    
    public EEducationPlatformTestDataSeedContributor(
        ICurrentTenant currentTenant,
        ICategoryRepository categoryRepository)
    {
        _currentTenant = currentTenant;
        _categoryRepository  = categoryRepository;
    }

    public Task SeedAsync(DataSeedContext context)
    {
        /* Seed additional test data... */
        
        _categoryRepository.InsertAsync(new Category(
            id: TestData.Category1Id, 
            name: "Mathematics", 
            description: "Mathematics related", 
            code: "Math",
            parentCategoryId: null,
            hasSubCategories:  true));
        
        _categoryRepository.InsertAsync(new Category(
            id: TestData.Category2Id, 
            name: "Applied Mathematics", 
            description: "Applied Mathematics related", 
            code: "AppliedMath",
            parentCategoryId: TestData.Category1Id,
            hasSubCategories: true));
        
        _categoryRepository.InsertAsync(new Category(
            id: TestData.Category3Id, 
            name: "Dynamics", 
            description: "Dynamics related", 
            code: "Dynamics",
            parentCategoryId: TestData.Category2Id,
            hasSubCategories:  false));
        
        _categoryRepository.InsertAsync(new Category(
            id: TestData.Category4Id, 
            name: "Applied Machine Learning", 
            description: "Applied Machine Learning related", 
            code: "AppliedML",
            parentCategoryId: null,
            hasSubCategories:  false));
        
        
        
        using (_currentTenant.Change(context?.TenantId))
        {
            return Task.CompletedTask;
        }
    }
}
