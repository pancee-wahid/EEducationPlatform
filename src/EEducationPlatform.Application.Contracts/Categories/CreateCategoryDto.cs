using System;
using Volo.Abp.Application.Dtos;

namespace EEducationPlatform.Categories;

public class CreateCategoryDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Code {get; set; }
    public Guid? ParentCategoryId { get; set; }
}