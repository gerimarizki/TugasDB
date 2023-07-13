using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Models
{
    public class History
    {

        public DateTime Start_Date { get; set; }

        public int Employee_Id { get; set; }

        public DateTime? End_Date { get; set; }

        public int Department_Id { get; set; }

        public string Job_Id { get; set; }

        public List<History> GetAll()
        {
            var connection = Connection.Get();

            var histories = new List<History>();

            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = "SELECT * FROM histories";

            try
            {
                connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        History history = new History();

                        history.Start_Date = reader.GetDateTime(0);
                        history.Employee_Id = reader.GetInt32(1);
                        history.End_Date = reader.GetDateTime(2);
                        history.Department_Id = reader.GetInt32(3);
                        history.Job_Id = reader.GetString(4);

                        histories.Add(history);
                    }
                }
                else
                {
                    reader.Close();
                    connection.Close();
                }

                return histories;
            }
            catch
            {
                return new List<History>();
            }
        }


        public int Insert(History history)
        {
            var _connection = Connection.Get();


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT INTO histories VALUES (@start_Date), (@end_Date)";

            _connection.Open();
            using SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;


            try
            {
                SqlParameter pStartDate = new SqlParameter();
                pStartDate.ParameterName = "@start_date";
                pStartDate.SqlDbType = System.Data.SqlDbType.DateTime;
                pStartDate.Value = history.Start_Date;
                sqlCommand.Parameters.Add(pStartDate);

                SqlParameter pEndDate = new SqlParameter();
                pEndDate.ParameterName = "@end_date";
                pEndDate.SqlDbType = System.Data.SqlDbType.DateTime;
                pEndDate.Value = history.End_Date;
                sqlCommand.Parameters.Add(pEndDate);

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



        public int Update(History history)
        {
            var _connection = Connection.Get();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE Histories SET end_date = @end_date WHERE start_date = @start_date";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pStartDate = new SqlParameter();
                pStartDate.ParameterName = "@start_date";
                pStartDate.SqlDbType = System.Data.SqlDbType.DateTime;
                pStartDate.Value = history.Start_Date;
                sqlCommand.Parameters.Add(pStartDate);

                SqlParameter pEndDate = new SqlParameter();
                pEndDate.ParameterName = "@end_date";
                pEndDate.SqlDbType = System.Data.SqlDbType.DateTime;
                pEndDate.Value = history.End_Date;
                sqlCommand.Parameters.Add(pEndDate);

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



        public int Delete(DateTime start_date)
        {
            var _connection = Connection.Get();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM histories WHERE start_date = @start_date";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pStartDate = new SqlParameter();
                pStartDate.ParameterName = "@start_date";
                pStartDate.SqlDbType = System.Data.SqlDbType.DateTime;
                pStartDate.Value = start_date;
                sqlCommand.Parameters.Add(pStartDate);

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



        public History GetById(DateTime start_Date)
        {
            var history = new History();

            using (SqlConnection connection = Connection.Get())
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "SELECT * FROM histories WHERE start_Date = @start_Date";
                sqlCommand.Parameters.AddWithValue("@start_Date", start_Date);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            history.Start_Date = reader.GetDateTime(0);
                            history.Employee_Id = reader.GetInt32(1);
                            history.End_Date = reader.GetDateTime(2);
                            history.Department_Id = reader.GetInt32(3);
                            history.Job_Id = reader.GetString(4);
                        }
                    }
                }
                catch
                {
                    return new History();
                }
            }

            return history;
        }


    }
}
