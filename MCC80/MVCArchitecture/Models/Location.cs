using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Models
{
    public class Location
    {

        public int Id { get; set; }

        public string Street_Address { get; set; }

        public string Postal_Code { get; set; }

        public string City { get; set; }

        public string State_Province { get; set; }

        public string Country_Id { get; set; }

        public List<Location> GetAll()
        {
            var connection = Connection.Get();

            var locations = new List<Location>();

            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = "SELECT * FROM locations";

            try
            {
                connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Location location = new Location();
                        location.Id = reader.GetInt32(0);
                        location.Street_Address = reader.GetString(1);
                        location.Postal_Code = reader.GetString(2);
                        location.City = reader.GetString(3);
                        location.State_Province = reader.GetString(4);
                        location.Country_Id = reader.GetString(5);

                        locations.Add(location);
                    }
                }
                else
                {
                    reader.Close();
                    connection.Close();
                }

                return locations;
            }
            catch
            {
                return new List<Location>();
            }
        }


        public int Insert(Location location)
        {
            var _connection = Connection.Get();


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT INTO countries VALUES (@street_address), (@postal_code), (@city), (@state_province)";

            _connection.Open();
            using SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;


            try
            {

                SqlParameter pStreetAddress = new SqlParameter();
                pStreetAddress.ParameterName = "@street_address";
                pStreetAddress.SqlDbType = System.Data.SqlDbType.VarChar;
                pStreetAddress.Value = location.Street_Address;
                sqlCommand.Parameters.Add(pStreetAddress);

                SqlParameter pPostalCode = new SqlParameter();
                pPostalCode.ParameterName = "@postal_code";
                pPostalCode.SqlDbType = System.Data.SqlDbType.VarChar;
                pPostalCode.Value = location.Postal_Code;
                sqlCommand.Parameters.Add(pPostalCode);

                SqlParameter pCity = new SqlParameter();
                pCity.ParameterName = "@city";
                pCity.SqlDbType = System.Data.SqlDbType.VarChar;
                pCity.Value = location.City ;
                sqlCommand.Parameters.Add(pCity);

                SqlParameter pStateProvince = new SqlParameter();
                pStateProvince.ParameterName = "@state_province";
                pStateProvince.SqlDbType = System.Data.SqlDbType.VarChar;
                pStateProvince.Value = location.State_Province;
                sqlCommand.Parameters.Add(pStateProvince);

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



        public int Update(Location location)
        {
            var _connection = Connection.Get();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE countries SET street_address = @street_Address, postal_code = @postal_code, city = @city, state_province = @state_province WHERE id = @id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pStreetAddress = new SqlParameter();
                pStreetAddress.ParameterName = "@street_address";
                pStreetAddress.SqlDbType = System.Data.SqlDbType.VarChar;
                pStreetAddress.Value = location.Street_Address;
                sqlCommand.Parameters.Add(pStreetAddress);

                SqlParameter pPostalCode = new SqlParameter();
                pPostalCode.ParameterName = "@postal_code";
                pPostalCode.SqlDbType = System.Data.SqlDbType.VarChar;
                pPostalCode.Value = location.Postal_Code;
                sqlCommand.Parameters.Add(pPostalCode);

                SqlParameter pCity = new SqlParameter();
                pCity.ParameterName = "@city";
                pCity.SqlDbType = System.Data.SqlDbType.VarChar;
                pCity.Value = location.City;
                sqlCommand.Parameters.Add(pCity);

                SqlParameter pStateProvince = new SqlParameter();
                pStateProvince.ParameterName = "@state_province";
                pStateProvince.SqlDbType = System.Data.SqlDbType.VarChar;
                pStateProvince.Value = location.State_Province;
                sqlCommand.Parameters.Add(pStateProvince);

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
            sqlCommand.CommandText = "DELETE FROM locations WHERE id = @id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pLocationId = new SqlParameter();
                pLocationId.ParameterName = "@id";
                pLocationId.SqlDbType = System.Data.SqlDbType.Int;
                pLocationId.Value = id;
                sqlCommand.Parameters.Add(pLocationId);

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



        public Location GetById(int id)
        {
            var location = new Location();

            var _connection = Connection.Get();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM locations WHERE id = @id";
            sqlCommand.Parameters.AddWithValue("@id", id);

            try
            {
                _connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    location.Id = reader.GetInt32(0);
                    location.Street_Address = reader.GetString(1);
                    location.Postal_Code = reader.GetString(2);
                    location.City = reader.GetString(3);
                    location.State_Province = reader.GetString(4);
                    location.Country_Id = reader.GetString(5);

                }
                reader.Close();
                _connection.Close();

                return new Location();
            }
            catch
            {
                return new Location();
            }

        }


    }
}
