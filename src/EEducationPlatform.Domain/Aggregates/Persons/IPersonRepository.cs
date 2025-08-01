using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EEducationPlatform.Aggregates.Persons;

public interface IPersonRepository : IBasicRepository<Person, Guid>
{
    Task<Person?> FindPersonByUserIdAsync(Guid userId);
}