using System;
using System.Threading.Tasks;
using EEducationPlatform.Categories;
using EEducationPlatform.Categories.Dtos;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace EEducationPlatform.CategoryAppServiceTests;

public abstract partial class CategoryAppServiceTests<TStartupModule> : EEducationPlatformApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    [Fact]
    public async Task Update__Should_Update_Category()
    {
        //Arrange
        var categoryId = await _categoryAppService.CreateAsync(_firstCreateCategoryDto);

        var updateCategoryDto = new UpdateCategoryDto
        {
            Description = "TestDescriptionUpdated",
            Name = "TestNameUpdated"
        };

        //Act
        await _categoryAppService.UpdateAsync(categoryId,  updateCategoryDto);

        //Assert
        categoryId.ShouldNotBe(Guid.Empty);
        var createdCategory = await _categoryAppService.GetAsync(categoryId, new GetCategoryQueryDto { });
        createdCategory.Name.ShouldBe(updateCategoryDto.Name);
        createdCategory.Description.ShouldBe(updateCategoryDto.Description);
        createdCategory.Code.ShouldBe(_firstCreateCategoryDto.Code);
        createdCategory.ParentCategoryId.ShouldBeNull();
        createdCategory.HasSubCategories.ShouldBeFalse();
        createdCategory.SubCategories.ShouldBeEmpty();
    }
    
    [Fact]
    public async Task Update__Should_Update_Category_With_Adding_Parent()
    {
        //Arrange
        var categoryId = await _categoryAppService.CreateAsync(_firstCreateCategoryDto);
        var parentCategoryId = await _categoryAppService.CreateAsync(_secondCreateCategoryDto);

        var updateCategoryDto = new UpdateCategoryDto
        {
            Description = "TestDescriptionUpdated",
            Name = "TestNameUpdated",
            ParentCategoryId = parentCategoryId
        };

        //Act
        await _categoryAppService.UpdateAsync(categoryId,  updateCategoryDto);

        //Assert
        categoryId.ShouldNotBe(Guid.Empty);
        var createdCategory = await _categoryAppService.GetAsync(categoryId, new GetCategoryQueryDto { });
        createdCategory.Name.ShouldBe(updateCategoryDto.Name);
        createdCategory.Description.ShouldBe(updateCategoryDto.Description);
        createdCategory.Code.ShouldBe(_firstCreateCategoryDto.Code);
        createdCategory.ParentCategoryId.ShouldBe(parentCategoryId);
        createdCategory.HasSubCategories.ShouldBeFalse();
        createdCategory.SubCategories.ShouldBeEmpty();
        
        var parentCategory = await _categoryAppService.GetAsync(parentCategoryId, new GetCategoryQueryDto { });
        parentCategory.HasSubCategories.ShouldBeTrue();
        parentCategory.SubCategories.ShouldContain(x => x.Id == categoryId);
    }
    
    [Fact]
    public async Task Update__Should_Update_Category_With_Replacing_Parent()
    {
        //Arrange
        var oldParentCategoryId = await _categoryAppService.CreateAsync(_firstCreateCategoryDto);
        var newParentCategoryId = await _categoryAppService.CreateAsync(_secondCreateCategoryDto);
        _thirdCreateCategoryDto.ParentCategoryId = oldParentCategoryId;
        var categoryId = await _categoryAppService.CreateAsync(_thirdCreateCategoryDto);

        var updateCategoryDto = new UpdateCategoryDto
        {
            Description = "TestDescriptionUpdated",
            Name = "TestNameUpdated",
            ParentCategoryId = newParentCategoryId
        };
        
        //Act
        await _categoryAppService.UpdateAsync(categoryId,  updateCategoryDto);

        //Assert
        categoryId.ShouldNotBe(Guid.Empty);
        var createdCategory = await _categoryAppService.GetAsync(categoryId, new GetCategoryQueryDto { });
        createdCategory.Name.ShouldBe(updateCategoryDto.Name);
        createdCategory.Description.ShouldBe(updateCategoryDto.Description);
        createdCategory.Code.ShouldBe(_thirdCreateCategoryDto.Code);
        createdCategory.ParentCategoryId.ShouldBe(newParentCategoryId);
        createdCategory.HasSubCategories.ShouldBeFalse();
        createdCategory.SubCategories.ShouldBeEmpty();
        
        var oldParentCategory = await _categoryAppService.GetAsync(oldParentCategoryId, new GetCategoryQueryDto { });
        oldParentCategory.HasSubCategories.ShouldBeFalse();
        oldParentCategory.SubCategories.ShouldBeEmpty();
        
        var newParentCategory = await _categoryAppService.GetAsync(newParentCategoryId, new GetCategoryQueryDto { });
        newParentCategory.HasSubCategories.ShouldBeTrue();
        newParentCategory.SubCategories.ShouldContain(x => x.Id == categoryId);
    }
    
    [Fact]
    public async Task Update__Should_Update_Category_With_Removing_Parent()
    {
        //Arrange
        var oldParentCategoryId = await _categoryAppService.CreateAsync(_firstCreateCategoryDto);
        _thirdCreateCategoryDto.ParentCategoryId = oldParentCategoryId;
        var categoryId = await _categoryAppService.CreateAsync(_thirdCreateCategoryDto);

        var updateCategoryDto = new UpdateCategoryDto
        {
            Description = "TestDescriptionUpdated",
            Name = "TestNameUpdated"
        };
        
        //Act
        await _categoryAppService.UpdateAsync(categoryId,  updateCategoryDto);

        //Assert
        categoryId.ShouldNotBe(Guid.Empty);
        var createdCategory = await _categoryAppService.GetAsync(categoryId, new GetCategoryQueryDto { });
        createdCategory.Name.ShouldBe(updateCategoryDto.Name);
        createdCategory.Description.ShouldBe(updateCategoryDto.Description);
        createdCategory.Code.ShouldBe(_thirdCreateCategoryDto.Code);
        createdCategory.ParentCategoryId.ShouldBeNull();
        createdCategory.HasSubCategories.ShouldBeFalse();
        createdCategory.SubCategories.ShouldBeEmpty();
        
        var oldParentCategory = await _categoryAppService.GetAsync(oldParentCategoryId, new GetCategoryQueryDto { });
        oldParentCategory.HasSubCategories.ShouldBeFalse();
        oldParentCategory.SubCategories.ShouldBeEmpty();
    }
}