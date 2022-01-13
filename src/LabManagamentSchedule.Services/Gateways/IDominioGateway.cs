using LabManagamentSchedule.Core.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Services.Gateways
{
    public interface IDominioGateway
    {
        Task<Dominio> GetDomain(string dominio);
        Task<IList<Dominio>> GetDomains();
    }
}
