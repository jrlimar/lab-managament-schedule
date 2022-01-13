using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagerExamsLabs.Domain.Interfaces
{
    public interface IResultRepository : IRepository
    {
        Task<IEnumerable<int>> GetExamsCheckedsFromDomain();
    }
}
