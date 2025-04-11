using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.LookupTypes;

public class LookupType : FullAuditedAggregateRoot<long>
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string? Description { get; private set; }

    private readonly List<LookupValue> _lookupValues;
    public IEnumerable<LookupValue> LookupValues => _lookupValues.AsReadOnly();

    public LookupType(long id, string name, string code, string? description) : base(id)
    {
        Name = name;
        Code = code;
        Description = description;
        _lookupValues = [];
    }

    public LookupType Update(string name, string? description)
    {
        Name = name;
        Description = description;

        return this;
    }

    public void AddLookupValue(long id, string name, string code, string description)
    {
        _lookupValues.Add(new LookupValue(id, name, code, description, this.Id));
    }

    public void UpdateLookupValue(LookupValue updatedLookupValue)
    {
        var lookupValue = _lookupValues.Find(l => l.Id == updatedLookupValue.Id)
                          ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                              .WithData("EntityName", nameof(LookupValue))
                              .WithData("Id", updatedLookupValue.Id.ToString());

        lookupValue.Update(updatedLookupValue.Name, updatedLookupValue.Code, updatedLookupValue.Description);
    }
}