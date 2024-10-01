using Domain.Helpers;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class Repository
    {
        public SqlConnection connection { get; set; }

        public Repository()
        {
            this.connection = new SqlConnection(Configuration.Connection);
        }
    }
}
