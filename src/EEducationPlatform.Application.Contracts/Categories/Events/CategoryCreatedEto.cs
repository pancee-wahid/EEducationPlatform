using System;

namespace EEducationPlatform.Categories.Events;

public class CategoryCreatedEto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public Guid? ParentCategoryId { get; set; }
}