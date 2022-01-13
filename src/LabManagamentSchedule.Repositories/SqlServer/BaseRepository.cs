using ManagerExamsLabs.Domain.Interfaces;
using System.Data.SqlClient;

namespace LabManagamentSchedule.Repositories.SqlServer
{
    public class BaseRepository : IRepository
    {
        private string ConnectionString;

        public void CreateConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public SqlConnection Connection()
        {
            return new SqlConnection(this.ConnectionString);
        }
    }
}
