using System;
using System.Collections.Generic;

namespace EEducationPlatform.Courses.Dtos;

public class CourseRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsPaid { get; set; }
    public float? SubscriptionFees  { get; set; }
    public bool NeedsEnrollmentApproval { get; set; }
    public List<Guid> Categories { get; set; } = [];
}