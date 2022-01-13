using System.Data.SqlClient;

namespace ManagerExamsLabs.Domain.Interfaces
{
    public interface IRepository
    {
        void CreateConnection(string connectionString);
        SqlConnection Connection();
    }
}
