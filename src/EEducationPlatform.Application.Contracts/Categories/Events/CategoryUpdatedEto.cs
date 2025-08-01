using System;

namespace EEducationPlatform.Categories.Events;

public class CategoryUpdatedEto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public Guid? ParentCategoryId { get; set; }
}