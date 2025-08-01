using System;
using System.Threading.Tasks;
using EEducationPlatform.Aggregates.Persons;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EEducationPlatform.EntityFrameworkCore.Repositories;

public class PersonRepository: EfCoreRepository<EEducationPlatformDbContext, Person, Guid>, IPersonRepository
{
    public PersonRepository(IDbContextProvider<EEducationPlatformDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
        
    }

    public async Task<Person?> FindPersonByUserIdAsync(Guid userId)
    {
        var dbSet = await GetDbSetAsync();

        return await dbSet.FirstOrDefaultAsync(p => p.UserId == userId);
    }
}