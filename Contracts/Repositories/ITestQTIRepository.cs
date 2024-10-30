using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Models;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories;

public interface ITestQTIRepository
{
    public Task<IEnumerable<TestQTI>> GetAllTestQTIsAsync(bool trackChanges);

    public Task<TestQTI> GetTestQTIAsync(Guid id, bool trackChanges);

    public Task<IEnumerable<TestQTI>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);

    public void CreateTestQTI(TestQTI testQTI);

    public void DeleteTestQTI(TestQTI testQTI);
}