using MVCArchitecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MVCArchitecture.Views.VLinq;

namespace MVCArchitecture.Views
{
        public class VLinq
        {
            public void GetAll(List<Linq> employees)
            {
                Console.WriteLine("Data Employees Join:");
                Console.WriteLine("======================");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"Employee ID: {employee.EmployeeId}");
                    Console.WriteLine($"Full Name: {employee.FullName}");
                    Console.WriteLine($"Email: {employee.Email}");
                    Console.WriteLine($"Phone Number: {employee.PhoneNumber}");
                    Console.WriteLine($"Salary: {employee.Salary}");
                    Console.WriteLine($"Department Name: {employee.DepartmentName}");
                    Console.WriteLine($"Street Address: {employee.StreetAddress}");
                    Console.WriteLine($"Country Name: {employee.CountryName}");
                    Console.WriteLine($"Region Name: {employee.RegionName}");
                    Console.WriteLine("======================");
                }
            }


            public void EmployeeNotFound()
            {
                Console.WriteLine("Employee not found.");
            }
        public void AllDataEmployee()
        {
            Linq linq = new Linq();
            var joinData = linq.GetAll();

            if (joinData.Any())
            {
                Console.WriteLine("Data Join:");
                foreach (var data in joinData)
                {
                    Console.WriteLine($"Employee ID: {data.EmployeeId}");
                    Console.WriteLine($"Nama Lengkap: {data.FullName}");
                    Console.WriteLine($"Email: {data.Email}");
                    Console.WriteLine($"Nomor Telepon: {data.PhoneNumber}");
                    Console.WriteLine($"Gaji: {data.Salary}");
                    Console.WriteLine($"Nama Departemen: {data.DepartmentName}");
                    Console.WriteLine($"Alamat: {data.StreetAddress}");
                    Console.WriteLine($"Nama Negara: {data.CountryName}");
                    Console.WriteLine($"Nama Region: {data.RegionName}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Data Join tidak tersedia.");
            }
        }
    }




}
