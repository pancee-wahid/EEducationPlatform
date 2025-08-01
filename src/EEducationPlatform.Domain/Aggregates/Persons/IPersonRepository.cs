using System;
using Volo.Abp.Domain.Repositories;

namespace EEducationPlatform.Aggregates.Persons;

public interface IPersonRepository : IBasicRepository<Person, Guid>
{
    
}