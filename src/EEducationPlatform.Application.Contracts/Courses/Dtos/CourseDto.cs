using System;
using System.Collections.Generic;

namespace EEducationPlatform.Courses.Dtos;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsPaid { get; set; }
    public float? SubscriptionFees  { get; set; }
    public bool NeedsEnrollmentApproval { get; set; }
    public List<CourseCategoryDto> Categories { get; set; } = [];
}

public class CourseCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
}