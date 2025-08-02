using System.Threading.Tasks;
using EEducationPlatform.Categories.Dtos;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace EEducationPlatform.CategoryAppServiceTests;

public abstract partial class CategoryAppServiceTests<TStartupModule> where TStartupModule : IAbpModule
{
    [Theory]
    [InlineData("Code")]
    [InlineData("Name")]
    [InlineData("X")]
    public async Task Get__Should_Get_List_With_Filter_And_Sorting(string sorting)
    {
        //Act
        var result = await _categoryAppService.GetListAsync(new GetCategoriesQueryDto
        {
            Filter = "APPl",
            ParentsOnly = false,
            Sorting = sorting
        });

        //Assert
        result.TotalCount.ShouldBe(2);
        result.Items.Count.ShouldBe(2);
        if (sorting == "Code")
        {
            result.Items[0].Id.ShouldBe(TestData.Category2Id);
            result.Items[1].Id.ShouldBe(TestData.Category4Id);
        }
        else
        {
            result.Items[0].Id.ShouldBe(TestData.Category4Id);
            result.Items[1].Id.ShouldBe(TestData.Category2Id);
        }
    }

    [Fact]
    public async Task Get__Should_Get_List_With_Filter_And_Parents_Only()
    {
        //Act
        var result = await _categoryAppService.GetListAsync(new GetCategoriesQueryDto
        {
            Filter = "APPl",
            ParentsOnly = true
        });

        //Assert
        result.TotalCount.ShouldBe(1);
        result.Items.Count.ShouldBe(1);
        result.Items[0].Id.ShouldBe(TestData.Category4Id);
    }
    
    [Fact]
    public async Task Get__Should_Get_List_With_Filter_Paging_Sorting()
    {
        //Act
        var result = await _categoryAppService.GetListAsync(new GetCategoriesQueryDto
        {
            Filter = "APPl",
            ParentsOnly = false,
            MaxResultCount = 1,
            SkipCount = 1
        });

        //Assert
        result.TotalCount.ShouldBe(1);
        result.Items.Count.ShouldBe(1);
        result.Items[0].Id.ShouldBe(TestData.Category2Id);
    }
}