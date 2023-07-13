using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Models
{
    public class Job
    {

        public string Id { get; set; }

        public string Title { get; set; }

        public int Min_Salary { get; set; }

        public int Max_Salary { get; set; }

        public List<Job> GetAll()
        {
            var connection = Connection.Get();

            var jobs = new List<Job>();

            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = "SELECT * FROM jobs";

            try
            {
                connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Job job = new Job();

                        job.Id = reader.GetString(0);
                        job.Title = reader.GetString(1);
                        job.Min_Salary = reader.GetInt32(2);
                        job.Max_Salary = reader.GetInt32(3);

                        jobs.Add(job);
                    }
                }
                else
                {
                    reader.Close();
                    connection.Close();
                }

                return jobs;
            }
            catch
            {
                return new List<Job>();
            }
        }


        public int Insert(Job job)
        {
            var _connection = Connection.Get();


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT INTO jobs VALUES (@title), (@min_salary), (@max_salary)";

            _connection.Open();
            using SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;


            try
            {
                SqlParameter pTitle = new SqlParameter();
                pTitle.ParameterName = "@title";
                pTitle.SqlDbType = System.Data.SqlDbType.VarChar;
                pTitle.Value = job.Title;
                sqlCommand.Parameters.Add(pTitle);

                SqlParameter pMinSalary = new SqlParameter();
                pMinSalary.ParameterName = "@min_salary";
                pMinSalary.SqlDbType = System.Data.SqlDbType.Int;
                pMinSalary.Value = job.Min_Salary;
                sqlCommand.Parameters.Add(pMinSalary);

                SqlParameter pMaxSalary = new SqlParameter();
                pMaxSalary.ParameterName = "@max_salary";
                pMaxSalary.SqlDbType = System.Data.SqlDbType.Int;
                pMaxSalary.Value = job.Max_Salary;
                sqlCommand.Parameters.Add(pMaxSalary);

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



        public int Update(Job job)
        {
            var _connection = Connection.Get();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE jobs SET title = @title, min_salary = @min_salary, max_salary = @max_salary WHERE id = @id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pJobId = new SqlParameter();
                pJobId.ParameterName = "@id";
                pJobId.SqlDbType = System.Data.SqlDbType.Char;
                pJobId.Value = job.Id;
                sqlCommand.Parameters.Add(Id);

                SqlParameter pTitle = new SqlParameter();
                pTitle.ParameterName = "@title";
                pTitle.SqlDbType = System.Data.SqlDbType.VarChar;
                pTitle.Value = job.Title;
                sqlCommand.Parameters.Add(pTitle);

                SqlParameter pMinSalary = new SqlParameter();
                pMinSalary.ParameterName = "@min_salary";
                pMinSalary.SqlDbType = System.Data.SqlDbType.Int;
                pMinSalary.Value = job.Min_Salary;
                sqlCommand.Parameters.Add(pMinSalary);

                SqlParameter pMaxSalary = new SqlParameter();
                pMaxSalary.ParameterName = "@max_salary";
                pMaxSalary.SqlDbType = System.Data.SqlDbType.Int;
                pMaxSalary.Value = job.Max_Salary;
                sqlCommand.Parameters.Add(pMaxSalary);

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



        public int Delete(string id)
        {
            var _connection = Connection.Get();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM jobs WHERE id = @id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pJobId = new SqlParameter();
                pJobId.ParameterName = "@id";
                pJobId.SqlDbType = System.Data.SqlDbType.Char;
                pJobId.Value = id;
                sqlCommand.Parameters.Add(pJobId);

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



        public Job GetById(string id)
        {
            var job = new Job();

            using (SqlConnection connection = Connection.Get())
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "SELECT * FROM jobs WHERE id = @id";
                sqlCommand.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            job.Id = reader.GetString(0);
                            job.Title = reader.GetString(1);
                            job.Min_Salary = reader.GetInt32(2);
                            job.Max_Salary = reader.GetInt32(3);
                        }
                    }
                }
                catch
                {
                    return new Job();
                }
            }

            return job;
        }


    }
}
