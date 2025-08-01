using Volo.Abp.Application.Dtos;

namespace EEducationPlatform.Categories.Dtos;

public class GetCategoriesQueryDto : PagedAndSortedResultRequestDto
{
    public string? Filter {get; set;}
    public bool ParentsOnly { get; set; } = false;
}