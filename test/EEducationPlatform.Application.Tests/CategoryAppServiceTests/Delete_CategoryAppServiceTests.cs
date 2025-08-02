using System.Linq;
using System.Threading.Tasks;
using EEducationPlatform.Categories.Dtos;
using Shouldly;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Modularity;
using Xunit;

namespace EEducationPlatform.CategoryAppServiceTests;

public abstract partial class CategoryAppServiceTests<TStartupModule> where TStartupModule : IAbpModule
{
    [Fact]
    public async Task Delete__Should_Delete_Category_With_No_Subcategories()
    {
        //Arrange
        var categoryId = await _categoryAppService.CreateAsync(_firstCreateCategoryDto);
        
        //Act
        await _categoryAppService.DeleteAsync(categoryId);
        
        //Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => 
            _categoryAppService.GetAsync(categoryId, new GetCategoryQueryDto { })
        );  
    }
    
    [Fact]
    public async Task Delete__Should_Delete_Category_With_No_Parent_Category_And_Siblings()
    {
        //Arrange
        var parentCategoryId = await _categoryAppService.CreateAsync(_firstCreateCategoryDto);
        _secondCreateCategoryDto.ParentCategoryId = parentCategoryId;
        var firstSubCategoryId = await _categoryAppService.CreateAsync(_secondCreateCategoryDto);
        _thirdCreateCategoryDto.ParentCategoryId = parentCategoryId;
        var secondSubCategoryId = await _categoryAppService.CreateAsync(_thirdCreateCategoryDto);
        
        //Act
        await _categoryAppService.DeleteAsync(firstSubCategoryId);
        
        //Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => 
            _categoryAppService.DeleteAsync(firstSubCategoryId)
        );
        var parentCategory = await _categoryRepository.GetAsync(parentCategoryId);
        parentCategory.HasSubCategories.ShouldBeTrue();
        parentCategory.SubCategories.Count().ShouldBe(1);
        parentCategory.SubCategories.ToList()[0].Id.ShouldBe(secondSubCategoryId);
    }
    
    [Fact]
    public async Task Delete__Should_Delete_Category_With_No_Parent_Category_No_Siblings()
    {
        //Arrange
        var parentCategoryId = await _categoryAppService.CreateAsync(_firstCreateCategoryDto);
        _secondCreateCategoryDto.ParentCategoryId = parentCategoryId;
        var firstSubCategoryId = await _categoryAppService.CreateAsync(_secondCreateCategoryDto);
        
        //Act
        await _categoryAppService.DeleteAsync(firstSubCategoryId);
        
        //Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => 
            _categoryAppService.DeleteAsync(firstSubCategoryId)
        );
        var parentCategory = await _categoryRepository.GetAsync(parentCategoryId);
        parentCategory.HasSubCategories.ShouldBeFalse();
        parentCategory.SubCategories.ShouldBeEmpty();
    }
    
    [Fact]
    public async Task Delete__Should_Fail_On_Delete_Category_With_Subcategories()
    {
        //Arrange
        var parentCategoryId = await _categoryAppService.CreateAsync(_firstCreateCategoryDto);
        _secondCreateCategoryDto.ParentCategoryId = parentCategoryId;
        var firstSubCategoryId = await _categoryAppService.CreateAsync(_secondCreateCategoryDto);
        
        //Act && Assert
        var exception = await Assert.ThrowsAsync<BusinessException>(() => 
            _categoryAppService.DeleteAsync(parentCategoryId)
        );
        
        exception.Code.ShouldBe(EEducationPlatformDomainErrorCodes.CategoryHasSubCategories);
    }
}