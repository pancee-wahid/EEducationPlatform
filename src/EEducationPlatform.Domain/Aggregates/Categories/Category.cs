using System;
using System.Collections;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Categories;

public class Category : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string Code { get; private set; }
    public Guid? ParentCategoryId { get; private set; } // null only for main parent categories
    public bool HasSubCategories { get; private set; }

    private readonly List<Category> _subCategories = [];
    public IEnumerable<Category> SubCategories => _subCategories.AsReadOnly();
    
    protected Category(){}
    
    public Category(Guid id, string name, string? description, string code, Guid? parentCategoryId,
        bool hasSubCategories = false) : base(id)
    {
        Name = name;
        Description = description;
        Code = code;
        ParentCategoryId = parentCategoryId;
        HasSubCategories = hasSubCategories;
        _subCategories = [];
    }

    public Category Update(string name, string? description, Guid? parentCategoryId)
    {
        Name = name;
        Description = description;
        // Code = code;
        ParentCategoryId = parentCategoryId;

        return this;
    }

    public Category SetHasSubCategories(bool hasSubCategories)
    {
        HasSubCategories = hasSubCategories;

        return this;
    }
}