namespace LabManagamentSchedule.Core.ValueObjects
{
    public class Dominio
    {
        public string Id { get; private set; }
        public string ConnectionString { get; private set; }

        public Dominio(string id, string connectionString)
        {
            this.Id = id;
            this.ConnectionString = connectionString;
        }
    }
}
