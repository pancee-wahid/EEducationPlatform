using System;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace EEducationPlatform.Categories.Dtos;

public class UpdateCategoryDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}