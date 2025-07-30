using System;
using EEducationPlatform.Aggregates.Persons;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EEducationPlatform.EntityFrameworkCore.Repositories;

public class PersonRepository: EfCoreRepository<EEducationPlatformDbContext, Person, Guid>, IPersonRepository
{
    public PersonRepository(IDbContextProvider<EEducationPlatformDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
        
    }
    
}