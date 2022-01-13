using Dapper;
using ManagerExamsLabs.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Repositories.SqlServer
{
    public class ResultRepository : BaseRepository, IResultRepository
    {
        public async Task<IEnumerable<int>> GetExamsCheckedsFromDomain()
        {
            using (var connection = Connection())
            {
                var sql = "select * from atendimento_exame_resultado where bol_ativo = 1 and dt_criacao >= '2020-09-01'";

                return await connection.QueryAsync<int>(sql);
            }
        }
    }
}
