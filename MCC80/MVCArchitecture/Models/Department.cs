﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MVCArchitecture.Models
{
    public class Department
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int Location_Id { get; set; }

        public int Manager_Id { get; set; }

        public List<Department> GetAll()
        {
            var connection = Connection.Get();

            var departments = new List<Department>();

            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = "SELECT * FROM departments";

            try
            {
                connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Department department = new Department();
                        department.Id = reader.GetInt32(0);
                        department.Name = reader.GetString(1);
                        department.Location_Id = reader.GetInt32(2);
                        department.Manager_Id = reader.GetInt32(3);

                        departments.Add(department);
                    }
                }
                else
                {
                    reader.Close();
                    connection.Close();
                }

                return departments;
            }
            catch
            {
                return new List<Department>();
            }
        }


        public int Insert(Department department)
        {
            var _connection = Connection.Get();


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT INTO departments VALUES (@name)";

            _connection.Open();
            using SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;


            try
            {

                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = System.Data.SqlDbType.VarChar;
                pName.Value = department.Name;
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



        public int Update(Department department)
        {
            var _connection = Connection.Get();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE departments SET name = @name WHERE id = @id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = System.Data.SqlDbType.VarChar;
                pName.Value = department.Name;
                sqlCommand.Parameters.Add(pName);

                SqlParameter pDepartmentId = new SqlParameter();
                pDepartmentId.ParameterName = "id";
                pDepartmentId.SqlDbType = System.Data.SqlDbType.Int;
                pDepartmentId.Value = department.Id;
                sqlCommand.Parameters.Add(pDepartmentId);

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
            sqlCommand.CommandText = "DELETE FROM departments WHERE id = @id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pDepartmentId = new SqlParameter();
                pDepartmentId.ParameterName = "@id";
                pDepartmentId.SqlDbType = System.Data.SqlDbType.Int;
                pDepartmentId.Value =id;
                sqlCommand.Parameters.Add(pDepartmentId);

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



        public Department GetById(int id)
        {
            var department = new Department();

            var _connection = Connection.Get();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM departments WHERE id = @id";
            sqlCommand.Parameters.AddWithValue("@id", id);

            try
            {
                _connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    department.Id = reader.GetInt32(0);
                    department.Name = reader.GetString(1);
                    department.Location_Id = reader.GetInt32(2);
                    department.Manager_Id = reader.GetInt32(2);

                }
                reader.Close();
                _connection.Close();

                return new Department();
            }
            catch
            {
                return new Department();
            }

        }


    }
}