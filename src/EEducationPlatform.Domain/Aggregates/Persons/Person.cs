using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Persons;

public class Person : FullAuditedAggregateRoot<Guid>
{
    public Guid UserId { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string? FullNameAr { get; private set; }
    public string? FullNameEn { get; private set; }
    public string? FirstNameAr { get; private set; }
    public string? FirstNameEn { get; private set; }
    public string? LastNameAr { get; private set; }
    public string? LastNameEn { get; private set; }

    public Person(Guid id, DateTime birthDate, string? fullNameAr, string? fullNameEn, string? firstNameAr,
        string? firstNameEn, string? lastNameAr, string? lastNameEn) : base(id)
    {
        BirthDate = birthDate;
        FullNameAr = fullNameAr;
        FullNameEn = fullNameEn;
        FirstNameAr = firstNameAr;
        FirstNameEn = firstNameEn;
        LastNameAr = lastNameAr;
        LastNameEn = lastNameEn;
    }
}