using System;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace EEducationPlatform.Categories;

public class UpdateCategoryDto
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Code {get; set; }
    public Guid? ParentCategoryId { get; set; }
    public bool HasSubCategories { get; set; }
}