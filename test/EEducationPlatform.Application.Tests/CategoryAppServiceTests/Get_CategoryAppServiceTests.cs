using System;
using System.Threading.Tasks;
using EEducationPlatform.Categories;
using EEducationPlatform.Categories.Dtos;
using Shouldly;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Xunit;

namespace EEducationPlatform.CategoryAppServiceTests;

public abstract partial class CategoryAppServiceTests<TStartupModule> : EEducationPlatformApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    [Fact]
    public async Task Get__Should_Get_Category_With_Max_Depth_0()
    {
        //Arrange
        var mainCategoryId = await _categoryAppService.CreateAsync(_firstCreateCategoryDto);
        _secondCreateCategoryDto.ParentCategoryId = mainCategoryId;
        var firstSubCategoryId = await _categoryAppService.CreateAsync(_secondCreateCategoryDto);
        _thirdCreateCategoryDto.ParentCategoryId = firstSubCategoryId;
        var secondSubCategoryId = await _categoryAppService.CreateAsync(_thirdCreateCategoryDto);
        _fourthCreateCategoryDto.ParentCategoryId = secondSubCategoryId;
        var thirdSubCategoryId = await _categoryAppService.CreateAsync(_fourthCreateCategoryDto);
        
        //Act
        var mainCategory = await _categoryAppService.GetAsync(mainCategoryId,  new GetCategoryQueryDto { MaxDepth = 0});

        //Assert
        mainCategory.Id.ShouldBe(mainCategoryId);
        mainCategory.Name.ShouldBe(_firstCreateCategoryDto.Name);
        mainCategory.Code.ShouldBe(_firstCreateCategoryDto.Code);
        mainCategory.Description.ShouldBe(_firstCreateCategoryDto.Description);
        mainCategory.HasSubCategories.ShouldBeTrue();
        mainCategory.SubCategories.ShouldBeEmpty();
    }
    
    [Fact]
    public async Task Get__Should_Get_Category_With_Max_Depth_2()
    {
        //Arrange
        var mainCategoryId = await _categoryAppService.CreateAsync(_firstCreateCategoryDto);
        _secondCreateCategoryDto.ParentCategoryId = mainCategoryId;
        var firstSubCategoryId = await _categoryAppService.CreateAsync(_secondCreateCategoryDto);
        _thirdCreateCategoryDto.ParentCategoryId = firstSubCategoryId;
        var secondSubCategoryId = await _categoryAppService.CreateAsync(_thirdCreateCategoryDto);
        _fourthCreateCategoryDto.ParentCategoryId = secondSubCategoryId;
        var thirdSubCategoryId = await _categoryAppService.CreateAsync(_fourthCreateCategoryDto);
        
        //Act
        var mainCategory = await _categoryAppService.GetAsync(mainCategoryId,  new GetCategoryQueryDto { MaxDepth = 2});

        //Assert
        mainCategory.Id.ShouldBe(mainCategoryId);
        mainCategory.Name.ShouldBe(_firstCreateCategoryDto.Name);
        mainCategory.Code.ShouldBe(_firstCreateCategoryDto.Code);
        mainCategory.Description.ShouldBe(_firstCreateCategoryDto.Description);
        mainCategory.HasSubCategories.ShouldBeTrue();
        mainCategory.SubCategories.Count.ShouldBe(1);
        mainCategory.SubCategories.ShouldContain(x => x.Id == firstSubCategoryId);

        var firstSubCategory = mainCategory.SubCategories[0];
        firstSubCategory.Id.ShouldBe(firstSubCategoryId);
        firstSubCategory.Name.ShouldBe(_secondCreateCategoryDto.Name);
        firstSubCategory.Code.ShouldBe(_secondCreateCategoryDto.Code);
        firstSubCategory.Description.ShouldBe(_secondCreateCategoryDto.Description);
        firstSubCategory.ParentCategoryId.ShouldBe(mainCategoryId);
        firstSubCategory.HasSubCategories.ShouldBeTrue();
        firstSubCategory.SubCategories.Count.ShouldBe(1);
        firstSubCategory.SubCategories.ShouldContain(x => x.Id == secondSubCategoryId);
        
        var secondSubCategory = firstSubCategory.SubCategories[0];
        secondSubCategory.Id.ShouldBe(secondSubCategoryId);
        secondSubCategory.Name.ShouldBe(_thirdCreateCategoryDto.Name);
        secondSubCategory.Code.ShouldBe(_thirdCreateCategoryDto.Code);
        secondSubCategory.Description.ShouldBe(_thirdCreateCategoryDto.Description);
        secondSubCategory.ParentCategoryId.ShouldBe(firstSubCategoryId);
        secondSubCategory.HasSubCategories.ShouldBeTrue();
        secondSubCategory.SubCategories.Count.ShouldBe(0);
    }
    
    [Fact]
    public async Task Get__Should_Get_Category_With_Max_Depth_3()
    {
        //Arrange
        var mainCategoryId = await _categoryAppService.CreateAsync(_firstCreateCategoryDto);
        _secondCreateCategoryDto.ParentCategoryId = mainCategoryId;
        var firstSubCategoryId = await _categoryAppService.CreateAsync(_secondCreateCategoryDto);
        _thirdCreateCategoryDto.ParentCategoryId = firstSubCategoryId;
        var secondSubCategoryId = await _categoryAppService.CreateAsync(_thirdCreateCategoryDto);
        _fourthCreateCategoryDto.ParentCategoryId = secondSubCategoryId;
        var thirdSubCategoryId = await _categoryAppService.CreateAsync(_fourthCreateCategoryDto);
        
        //Act
        var mainCategory = await _categoryAppService.GetAsync(mainCategoryId,  new GetCategoryQueryDto { MaxDepth = 3});

        //Assert
        mainCategory.Id.ShouldBe(mainCategoryId);
        mainCategory.Name.ShouldBe(_firstCreateCategoryDto.Name);
        mainCategory.Code.ShouldBe(_firstCreateCategoryDto.Code);
        mainCategory.Description.ShouldBe(_firstCreateCategoryDto.Description);
        mainCategory.HasSubCategories.ShouldBeTrue();
        mainCategory.SubCategories.Count.ShouldBe(1);
        mainCategory.SubCategories.ShouldContain(x => x.Id == firstSubCategoryId);

        var firstSubCategory = mainCategory.SubCategories[0];
        firstSubCategory.Id.ShouldBe(firstSubCategoryId);
        firstSubCategory.Name.ShouldBe(_secondCreateCategoryDto.Name);
        firstSubCategory.Code.ShouldBe(_secondCreateCategoryDto.Code);
        firstSubCategory.Description.ShouldBe(_secondCreateCategoryDto.Description);
        firstSubCategory.ParentCategoryId.ShouldBe(mainCategoryId);
        firstSubCategory.HasSubCategories.ShouldBeTrue();
        firstSubCategory.SubCategories.Count.ShouldBe(1);
        firstSubCategory.SubCategories.ShouldContain(x => x.Id == secondSubCategoryId);
        
        var secondSubCategory = firstSubCategory.SubCategories[0];
        secondSubCategory.Id.ShouldBe(secondSubCategoryId);
        secondSubCategory.Name.ShouldBe(_thirdCreateCategoryDto.Name);
        secondSubCategory.Code.ShouldBe(_thirdCreateCategoryDto.Code);
        secondSubCategory.Description.ShouldBe(_thirdCreateCategoryDto.Description);
        secondSubCategory.ParentCategoryId.ShouldBe(firstSubCategoryId);
        secondSubCategory.HasSubCategories.ShouldBeTrue();
        secondSubCategory.SubCategories.Count.ShouldBe(1);
        secondSubCategory.SubCategories.ShouldContain(x => x.Id == thirdSubCategoryId);
        
        var thirdSubCategory = secondSubCategory.SubCategories[0];
        thirdSubCategory.Id.ShouldBe(thirdSubCategoryId);
        thirdSubCategory.Name.ShouldBe(_fourthCreateCategoryDto.Name);
        thirdSubCategory.Code.ShouldBe(_fourthCreateCategoryDto.Code);
        thirdSubCategory.Description.ShouldBe(_fourthCreateCategoryDto.Description);
        thirdSubCategory.ParentCategoryId.ShouldBe(secondSubCategoryId);
        thirdSubCategory.HasSubCategories.ShouldBeFalse();
        thirdSubCategory.SubCategories.Count.ShouldBe(0);
    }
    
    [Fact]
    public async Task Get__Should_Fail_On_Get_Category_With_Max_Depth_Negative()
    {
        //Arrange
        var mainCategoryId = await _categoryAppService.CreateAsync(_firstCreateCategoryDto);
        
        //Act && Assert
        await Assert.ThrowsAsync<AbpValidationException>(async () => 
            await _categoryAppService.GetAsync(mainCategoryId,  new GetCategoryQueryDto { MaxDepth = -1})
        );
    }
    
    [Fact]
    public async Task Get__Should_Fail_On_Get_Category_With_Max_Depth_More_Than_Five()
    {
        //Arrange
        var mainCategoryId = await _categoryAppService.CreateAsync(_firstCreateCategoryDto);
        
        //Act && Assert
        await Assert.ThrowsAsync<AbpValidationException>(async () => 
            await _categoryAppService.GetAsync(mainCategoryId,  new GetCategoryQueryDto { MaxDepth = 6})
        );
    }
}