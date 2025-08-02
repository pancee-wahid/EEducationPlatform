using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Modularity;
using Xunit;

namespace EEducationPlatform.CategoryAppServiceTests;

public abstract partial class CategoryAppServiceTests<TStartupModule> where TStartupModule : IAbpModule
{
    [Fact]
    public async Task Create__Should_Create_Main_Category()
    {
        //Act
        var result = await _categoryAppService.CreateAsync(_firstCreateCategoryDto);

        //Assert
        result.ShouldNotBe(Guid.Empty);
        var createdCategory = await _categoryRepository.GetAsync(result);
        createdCategory.Name.ShouldBe(_firstCreateCategoryDto.Name);
        createdCategory.Description.ShouldBe(_firstCreateCategoryDto.Description);
        createdCategory.Code.ShouldBe(_firstCreateCategoryDto.Code);
        createdCategory.ParentCategoryId.ShouldBeNull();
        createdCategory.HasSubCategories.ShouldBeFalse();
        createdCategory.SubCategories.ShouldBeEmpty();
    }
    
    [Fact]
    public async Task Create__Should_Fail_On_Create_Category_With_Duplicate_Code()
    {
        //Arrange
        await _categoryAppService.CreateAsync(_firstCreateCategoryDto);
        _secondCreateCategoryDto.Code = _firstCreateCategoryDto.Code;
        
        //Act && Assert
        await Assert.ThrowsAnyAsync<Exception>(async () => 
            await _categoryAppService.CreateAsync(_secondCreateCategoryDto)
        ); 
    }
    
    [Fact]
    public async Task Create__Should_Create_Subcategory()
    {
        //Arrange
        var parentCategoryId = await _categoryAppService.CreateAsync(_firstCreateCategoryDto);
        _secondCreateCategoryDto.ParentCategoryId = parentCategoryId;
        
        //Act
        var subCategoryId = await _categoryAppService.CreateAsync(_secondCreateCategoryDto);
        
        //Assert
        // parent category
        var parentCategory = await _categoryRepository.GetAsync(parentCategoryId);
        parentCategory.Code.ShouldBe(_firstCreateCategoryDto.Code);
        parentCategory.HasSubCategories.ShouldBeTrue();
        parentCategory.SubCategories.Count().ShouldBe(1);
        parentCategory.SubCategories.ToList()[0].Id.ShouldBe(subCategoryId);
        // subcategory
        var subCategory = await _categoryRepository.GetAsync(subCategoryId);
        subCategory.Code.ShouldBe(_secondCreateCategoryDto.Code);
        subCategory.ParentCategoryId.ShouldBe(parentCategoryId);
        subCategory.HasSubCategories.ShouldBeFalse();
        subCategory.SubCategories.ShouldBeEmpty();
    }

    [Fact] public async Task Create__Should_Fail_On_Create_Subcategory_With_Unfound_Parent()
    {
        //Act && Assert
        _secondCreateCategoryDto.ParentCategoryId = Guid.NewGuid();

        await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await _categoryAppService.CreateAsync(_secondCreateCategoryDto)
        );
    }
}