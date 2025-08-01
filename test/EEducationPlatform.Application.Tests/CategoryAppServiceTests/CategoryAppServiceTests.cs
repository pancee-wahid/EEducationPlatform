using EEducationPlatform.Aggregates.Categories;
using EEducationPlatform.Categories;
using EEducationPlatform.Categories.Dtos;
using Volo.Abp.Modularity;

namespace EEducationPlatform.CategoryAppServiceTests;

 public abstract partial class CategoryAppServiceTests<TStartupModule> : EEducationPlatformApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ICategoryAppService _categoryAppService;
    
    private CreateCategoryDto _firstCreateCategoryDto = new CreateCategoryDto 
    {
        Code = "TestCode1",
        Description = "TestDescription1",
        Name = "TestName1"
    };

    private CreateCategoryDto _secondCreateCategoryDto = new CreateCategoryDto
    {
        Code = "TestCode2",
        Description = "TestDescription2",
        Name = "TestName2"
    };

    private CreateCategoryDto _thirdCreateCategoryDto = new CreateCategoryDto
    {
        Code = "TestCode3",
        Description = "TestDescription3",
        Name = "TestName3"
    };

    private CreateCategoryDto _fourthCreateCategoryDto = new CreateCategoryDto
    {
        Code = "TestCode4",
        Description = "TestDescription4",
        Name = "TestName4"
    };

    
    public CategoryAppServiceTests()
    {
        _categoryAppService = GetRequiredService<ICategoryAppService>();
    }

}