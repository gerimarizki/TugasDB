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

        public class DBContext : IDisposable
        {
            private readonly string _connectionString;
            private SqlConnection _connection;

            public DBContext()
            {
                _connectionString = @"
            Data Source=GERIMARIZKI;
            Initial Catalog=Latihan;
            Integrated Security=True";
            }

            public SqlConnection OpenConnection()
            {
                try
                {
                    _connection = new SqlConnection(_connectionString);
                    _connection.Open();
                    return _connection;
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            public void CloseConnection()
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }

            public void Dispose()
            {
                CloseConnection();
            }
        }
    }

     
}
