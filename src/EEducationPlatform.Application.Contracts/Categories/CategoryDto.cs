using System;
using System.Collections.Generic;

namespace EEducationPlatform.Categories;

public class CategoryDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Code { get; set; }
    public Guid? ParentCategoryId { get; set; } 
    public required bool HasSubCategories { get; set; }
    public List<CategoryDto> SubCategories { get; set; } = [];
}