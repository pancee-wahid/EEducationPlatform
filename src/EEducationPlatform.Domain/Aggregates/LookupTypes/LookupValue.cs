using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.LookupTypes;

public class LookupValue : FullAuditedEntity<long>
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string? Description { get; private set; }
    public long LookupTypeId { get; private set; }

    public LookupValue(long id, string name, string code, string description, long lookupTypeId) : base(id)
    {
        Name = name;
        Code = code;
        Description = description;
        LookupTypeId = lookupTypeId;
    }

    public LookupValue Update(string name, string code, string description)
    {
        Name = name;
        Code = code;
        Description = description;

        return this;
    }
}