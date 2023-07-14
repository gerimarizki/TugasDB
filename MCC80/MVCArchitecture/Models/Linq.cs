using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Models
{
 
    public class Linq
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Salary { get; set; }
        public string DepartmentName { get; set; }
        public string StreetAddress { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }

        public List<Linq> GetAll()
        {
            using (var context = new Connection.DBContext())
            {
                var employees = new List<Linq>();

                using (var connection = context.OpenConnection())
                {
                    var query = @"select e.id, concat(e.first_name,' ', e.last_name) as nama_lengkap, e.email, e.phone_number, e.salary, d.name as dep_name, l.street_address, c.name as country_name, r.name as reg_name
                                from employees e join departments d on e.department_id= d.id join locations l on d.location_id=l.id join countries c on l.country_id=c.id join regions r on c.region_id=r.id";

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var employee = new Linq
                                {
                                    

                                    EmployeeId = reader.GetInt32(0),
                                    FullName = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    PhoneNumber = reader.IsDBNull(3) ? "N/A" : reader.GetString(3),
                                    Salary = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                    DepartmentName = reader.GetString(5),
                                    StreetAddress = reader.GetString(6),
                                    CountryName = reader.GetString(7),
                                    RegionName = reader.GetString(8)
                                };

                                employees.Add(employee);
                            }
                        }
                    }
                }

                return employees;
            }
        }
    }

}

