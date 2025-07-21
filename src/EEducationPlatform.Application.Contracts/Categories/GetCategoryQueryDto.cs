using System.ComponentModel.DataAnnotations;

namespace EEducationPlatform.Categories;

public class GetCategoryQueryDto
{
    [Range(minimum: 1, maximum: 10, ErrorMessage = "MaxDepth should be inclusive between 1 and 10.")]
    public int MaxDepth { get; set; } = 3;
}