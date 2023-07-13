using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace MVCArchitecture
{
    public class Connection
    {
        private static string _ConnectionString = @"
            Data Source=GERIMARIZKI;
            Initial Catalog=Latihan;
            Integrated Security=True";

        private static SqlConnection _connection;

        public static SqlConnection Get()
        {
            try
            {
                _connection = new SqlConnection(_ConnectionString);
                return _connection;
            }
            catch
            {
                Console.WriteLine("");
                throw;
            }
        }
    }
}
