using System.ComponentModel.DataAnnotations;

namespace EEducationPlatform.Categories;

public class GetCategoryQueryDto
{
    public int MaxDepth { get; set; } = 1;
}