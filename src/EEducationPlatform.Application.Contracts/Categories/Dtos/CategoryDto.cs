using System;

namespace EEducationPlatform.Categories.Dtos;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Code { get; set; }
    public Guid? ParentCategoryId { get; set; } 
    public bool HasSubCategories { get; set; }
}