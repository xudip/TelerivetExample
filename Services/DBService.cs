using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TelerivetExample.Services
{
    public abstract class DBService
    {
        protected static IDbConnection OpenConnection()
        {
            IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ABCSystemConnectionString"].ConnectionString);
            connection.Open();
            return connection;
        }
    }
}