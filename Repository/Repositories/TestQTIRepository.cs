using Contracts.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories;

public class TestQTIRepository : RepositoryBase<TestQTI>, ITestQTIRepository
{
    public TestQTIRepository(RepositoryContext context) : base(context)
    {
    }

    public void CreateTestQTI(TestQTI testQTI) => Create(testQTI);

    public void DeleteTestQTI(TestQTI testQTI) => Delete(testQTI);

    public async Task<IEnumerable<TestQTI>> GetAllTestQTIsAsync(bool trackChanges)
    {
        return await FindAll(trackChanges).OrderBy(t => t.CreationDate).ToListAsync();
    }

    public async Task<IEnumerable<TestQTI>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public async Task<TestQTI> GetTestQTIAsync(Guid guid, bool trackChanges)
    {
        throw new NotImplementedException();
    }
}
