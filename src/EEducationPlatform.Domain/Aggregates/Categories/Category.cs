using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Categories;

public class Category : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string Code { get; private set; }
    public Guid? ParentCategoryId { get; private set; } // null only for main parent categories
    public bool HasSubCategories { get; private set; }


    public Category(Guid id, string name, string description, string code, Guid? parentCategoryId,
        bool hasSubCategories) : base(id)
    {
        Name = name;
        Description = description;
        Code = code;
        ParentCategoryId = parentCategoryId;
        HasSubCategories = hasSubCategories;
    }
}