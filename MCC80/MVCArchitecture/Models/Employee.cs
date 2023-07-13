using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Models
{
    public class Employee
    {

        public int Id { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Email { get; set; }

        public string? Phone_Number { get; set; }

        public DateTime Hire_Date { get; set; }

        public int? Salary { get; set; }

        public decimal? Comission_Pct { get; set; }

        public int Manager_Id { get; set; }

        public string Job_Id { get; set; }

        public int Department_Id { get; set; }

        public List<Employee> GetAll()
        {
            var connection = Connection.Get();

            var employees = new List<Employee>();

            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = "SELECT * FROM employees";

            try
            {
                connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();

                        string phone_number = reader.IsDBNull(4) ? "N/A" : reader.GetString(4);
                        int salary = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                        decimal comission_pct = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7);

                        employee.Id = reader.GetInt32(0);
                        employee.First_Name = reader.GetString(1);
                        employee.Last_Name = reader.GetString(2);
                        employee.Email = reader.GetString(3);
                        employee.Phone_Number = phone_number;
                        employee.Hire_Date = reader.GetDateTime(5);
                        employee.Salary = salary;
                        employee.Comission_Pct = comission_pct;



                        employees.Add(employee);
                    }
                }
                else
                {
                    reader.Close();
                    connection.Close();
                }

                return employees;
            }
            catch
            {
                return new List<Employee>();
            }
        }


        public int Insert(Employee employee)
        {
            var _connection = Connection.Get();


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT INTO employees VALUES (@first_name), (@last_name), (@phone_number), (@hire_date), (@salary), (@comission_pct)";

            _connection.Open();
            using SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;


            try
            {

                SqlParameter pFName = new SqlParameter();
                pFName.ParameterName = "@first_name";
                pFName.SqlDbType = System.Data.SqlDbType.VarChar;
                pFName.Value = employee.First_Name;
                sqlCommand.Parameters.Add(pFName);

                SqlParameter pLName = new SqlParameter();
                pLName.ParameterName = "@last_name";
                pLName.SqlDbType = System.Data.SqlDbType.Int;
                pLName.Value = employee.Last_Name;
                sqlCommand.Parameters.Add(pLName);

                SqlParameter pEmail = new SqlParameter();
                pEmail.ParameterName = "@email";
                pEmail.SqlDbType = System.Data.SqlDbType.VarChar;
                pEmail.Value = employee.Email;
                sqlCommand.Parameters.Add(pEmail);

                SqlParameter pPhoneNumber = new SqlParameter();
                pPhoneNumber.ParameterName = "@phone_number";
                pPhoneNumber.SqlDbType = System.Data.SqlDbType.VarChar;
                pPhoneNumber.Value = employee.Phone_Number;
                sqlCommand.Parameters.Add(pPhoneNumber);

                SqlParameter pHireDate = new SqlParameter();
                pHireDate.ParameterName = "@hire_date";
                pHireDate.SqlDbType = System.Data.SqlDbType.DateTime;
                pHireDate.Value = employee.Hire_Date;
                sqlCommand.Parameters.Add(pHireDate);

                SqlParameter pSalary = new SqlParameter();
                pSalary.ParameterName = "@salary";
                pSalary.SqlDbType = System.Data.SqlDbType.Int;
                pSalary.Value = employee.Salary;
                sqlCommand.Parameters.Add(pSalary);

                SqlParameter pComissionPCT = new SqlParameter();
                pComissionPCT.ParameterName = "@comission_pct";
                pComissionPCT.SqlDbType = System.Data.SqlDbType.Decimal;
                pComissionPCT.Value = employee.Comission_Pct;
                sqlCommand.Parameters.Add(pComissionPCT);

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



        public int Update(Employee employee)
        {
            var _connection = Connection.Get();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE employees SET first_name = @first_name, last_name = @last_name, email = @email, phone_number = @phone_number, hire_date = @hire_date, salary = @salary, comission_pct = @comission_pct WHERE id = @id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pEmployeeId = new SqlParameter();
                pEmployeeId.ParameterName = "@employee_id";
                pEmployeeId.SqlDbType = System.Data.SqlDbType.Int;
                pEmployeeId.Value = employee.Id;
                sqlCommand.Parameters.Add(Id);

                SqlParameter pFName = new SqlParameter();
                pFName.ParameterName = "@first_name";
                pFName.SqlDbType = System.Data.SqlDbType.VarChar;
                pFName.Value = employee.First_Name;
                sqlCommand.Parameters.Add(pFName);

                SqlParameter pLName = new SqlParameter();
                pLName.ParameterName = "@last_name";
                pLName.SqlDbType = System.Data.SqlDbType.Int;
                pLName.Value = employee.Last_Name;
                sqlCommand.Parameters.Add(pLName);

                SqlParameter pEmail = new SqlParameter();
                pEmail.ParameterName = "@email";
                pEmail.SqlDbType = System.Data.SqlDbType.VarChar;
                pEmail.Value = employee.Email;
                sqlCommand.Parameters.Add(pEmail);

                SqlParameter pPhoneNumber = new SqlParameter();
                pPhoneNumber.ParameterName = "@phone_number";
                pPhoneNumber.SqlDbType = System.Data.SqlDbType.VarChar;
                pPhoneNumber.Value = employee.Phone_Number;
                sqlCommand.Parameters.Add(pPhoneNumber);

                SqlParameter pHireDate = new SqlParameter();
                pHireDate.ParameterName = "@hire_date";
                pHireDate.SqlDbType = System.Data.SqlDbType.DateTime;
                pHireDate.Value = employee.Hire_Date;
                sqlCommand.Parameters.Add(pHireDate);

                SqlParameter pSalary = new SqlParameter();
                pSalary.ParameterName = "@salary";
                pSalary.SqlDbType = System.Data.SqlDbType.Int;
                pSalary.Value = employee.Salary;
                sqlCommand.Parameters.Add(pSalary);

                SqlParameter pComissionPCT = new SqlParameter();
                pComissionPCT.ParameterName = "@comission_pct";
                pComissionPCT.SqlDbType = System.Data.SqlDbType.Decimal;
                pComissionPCT.Value = employee.Comission_Pct;
                sqlCommand.Parameters.Add(pComissionPCT);
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
            sqlCommand.CommandText = "DELETE FROM employees WHERE id = @id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pEmployeeId = new SqlParameter();
                pEmployeeId.ParameterName = "@id";
                pEmployeeId.SqlDbType = System.Data.SqlDbType.Int;
                pEmployeeId.Value = id;
                sqlCommand.Parameters.Add(pEmployeeId);

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



        public Employee GetById(int id)
        {
            var employee = new Employee();

            var _connection = Connection.Get();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM employees WHERE id = @id";
            sqlCommand.Parameters.AddWithValue("@id", id);

            try
            {
                _connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    string phone_number = reader.IsDBNull(4) ? "N/A" : reader.GetString(4);
                    int salary = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                    decimal comission_pct = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7);

                    employee.Id = reader.GetInt32(0);
                    employee.First_Name = reader.GetString(1);
                    employee.Last_Name = reader.GetString(2);
                    employee.Email = reader.GetString(3);
                    employee.Phone_Number = phone_number;
                    employee.Hire_Date = reader.GetDateTime(5);
                    employee.Salary = salary;
                    employee.Comission_Pct = comission_pct;

                }
                reader.Close();
                _connection.Close();

                return new Employee();
            }
            catch
            {
                return new Employee();
            }

        }


    }
}
