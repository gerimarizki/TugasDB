using MVCArchitecture;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Models
{
    public class Region
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Region> GetAll()
        {
            var connection = Connection.Get();

            var regions = new List<Region>();

            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = "SELECT * FROM Regions";

            try
            {
                connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Region region = new Region();
                        region.Id = reader.GetInt32(0);
                        region.Name = reader.GetString(1);

                        regions.Add(region);
                    }
                }
                else
                {
                    reader.Close();
                    connection.Close();
                }

                return regions;
            }
            catch
            {
                return new List<Region>();
            }
        }


        public int Insert(Region region)
        {
            var _connection = Connection.Get();


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT INTO Regions VALUES (@name)";

            _connection.Open();
            using SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;


            try
            {

                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = System.Data.SqlDbType.VarChar;
                pName.Value = region.Name;
                sqlCommand.Parameters.Add(pName);

                int result = sqlCommand.ExecuteNonQuery();


                transaction.Commit();
                _connection.Close();
                return result;

            }
            catch
            {
                transaction.Rollback();
                return -1;
            }


        }



        public int Update(Region region)
        {
            var _connection = Connection.Get();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE regions SET name = @name WHERE id = @id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = System.Data.SqlDbType.VarChar;
                pName.Value = region.Name;
                sqlCommand.Parameters.Add(pName);

                SqlParameter pRegionId = new SqlParameter();
                pRegionId.ParameterName = "@id";
                pRegionId.SqlDbType = System.Data.SqlDbType.Int;
                pRegionId.Value = region.Id;
                sqlCommand.Parameters.Add(pRegionId);

                int result = sqlCommand.ExecuteNonQuery();

                transaction.Commit();
                _connection.Close();


                return result;

            }
            catch
            {
                transaction.Rollback();
                return -1;
            }
        }



        public int Delete(int id)
        {
            var _connection = Connection.Get();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM regions WHERE id = @id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pRegionId = new SqlParameter();
                pRegionId.ParameterName = "@id";
                pRegionId.SqlDbType = System.Data.SqlDbType.Int;
                pRegionId.Value = id;
                sqlCommand.Parameters.Add(pRegionId);

                int result = sqlCommand.ExecuteNonQuery();


                transaction.Commit();
                _connection.Close();

                return result;
            }
            catch
            {
                transaction.Rollback();
                return -1;
            }
        }



        public Region GetById(int id)
        {
            var region = new Region();

            var _connection = Connection.Get();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM regions WHERE id = @id";
            sqlCommand.Parameters.AddWithValue("@id", id);

            try
            {
                _connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    region.Id = reader.GetInt32(0);
                    region.Name = reader.GetString(1);

                }
                reader.Close();
                _connection.Close();

                return new Region();
            }
            catch
            {
                return new Region();
            }

        }


    }
}
