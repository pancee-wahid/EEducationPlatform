using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Academies;

public class Academy : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string Code { get; private set; }
    public bool HasSubscriptionFees { get; private set; }
    public float? SubscriptionFees { get; private set; }
    
    private readonly List<AcademyCourse> _academyCourses;
    public IEnumerable<AcademyCourse> AcademyCourses => _academyCourses.AsReadOnly();

    public Academy(Guid id, string name, string description, string code, bool hasSubscriptionFees, float? subscriptionFees) : base(id)
    {
        Name = name;
        Description = description;
        Code = code;
        HasSubscriptionFees = hasSubscriptionFees;
        SubscriptionFees = subscriptionFees;
        _academyCourses = [];
    }

    public void AddAcademyCourse(Guid id, Guid courseId)
    {
        _academyCourses.Add(new AcademyCourse(id, this.Id, courseId));
    }

    public void RemoveAcademyCourses(IEnumerable<AcademyCourse> academyCourses)
    {
        _academyCourses.RemoveAll(academyCourses);
    }
}