﻿using MVCArchitecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Views
{
    public class VEmployee
    {
        private Employee _employeeModel;
        public void GetAll(List<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                GetById(employee);
            }
        }

        public void Menu()
        {
            Console.WriteLine("Basic Authentication Geri Marizki");
            Console.WriteLine("Pilih menu untuk masuk ke menunya");
            Console.WriteLine("1. Tambah employee");
            Console.WriteLine("2. Update employee");
            Console.WriteLine("3. Hapus employee");
            Console.WriteLine("4. Search By Id employee");
            Console.WriteLine("5. Get All employee");
            Console.WriteLine("6. Main Menu");
            Console.WriteLine("Pilih: ");
        }

        public void GetById(Employee employee)
        {
            Console.WriteLine("Id: " + employee.Id);
            Console.WriteLine("First Name: " + employee.First_Name);
            Console.WriteLine("Last Name: " + employee.Last_Name);
            Console.WriteLine("Email:" + employee.Email);
            Console.WriteLine("Phone Number: " + employee.Phone_Number);
            Console.WriteLine("Hire Date: " + employee.Hire_Date);
            Console.WriteLine("Salary: " + employee.Salary);
            Console.WriteLine("Comission PCT: " + employee.Comission_Pct);
            Console.WriteLine("Manager ID: " + employee.Manager_Id);
            Console.WriteLine("Job ID: " + employee.Job_Id);
            Console.WriteLine("Department ID: " + employee.Department_Id);

        }

        public void DataEmpty()
        {
            Console.WriteLine("Tidak ada data");
        }

        public void Fail()
        {
            Console.WriteLine("Gagal");
        }

        public void Success()
        {
            Console.WriteLine("Berhasil");
        }

        public Employee InsertMenu()
        {
            Console.WriteLine("Tambah First Name: ");
            string inputFirstName = Console.ReadLine();
            Console.WriteLine("Tambah Last Name: ");
            string inputLastName = Console.ReadLine();
            Console.WriteLine("Tambah Email: ");
            string inputEmail = Console.ReadLine();
            Console.WriteLine("Tambah Nomor HP: ");
            string inputPhone = Console.ReadLine();
            Console.Write("Tambah Hire Date: ");
            DateTime inputHireDate = Helper.DateValidation();
            Console.WriteLine("Tambah Salary: ");
            int inputSalary = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Tambah Commision PCT: ");
            decimal inputComission = decimal.Parse(Console.ReadLine());


            return new Employee
            {
                First_Name = inputFirstName,
                Last_Name = inputLastName,
                Email = inputEmail,
                Phone_Number = inputPhone,
                Hire_Date = inputHireDate,
                Salary = inputSalary,
                Comission_Pct = inputComission

            };
        }

        public Employee UpdateMenu()
        {
            Console.WriteLine("Update Id");
            int inputEmployeeId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Tambah First Name: ");
            string inputFirstName = Console.ReadLine();
            Console.WriteLine("Tambah Last Name: ");
            string inputLastName = Console.ReadLine();
            Console.WriteLine("Tambah Email: ");
            string inputEmail = Console.ReadLine();
            Console.WriteLine("Tambah Nomor HP: ");
            string inputPhone = Console.ReadLine();
            Console.Write("Tambah Hire Date: ");
            DateTime inputHireDate = Helper.DateValidation();
            Console.WriteLine("Tambah Salary: ");
            int inputSalary = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Tambah Commision PCT: ");
            decimal inputComission = decimal.Parse(Console.ReadLine());


            return new Employee
            {
                Id = inputEmployeeId,
                First_Name = inputFirstName,
                Last_Name = inputLastName,
                Email = inputEmail,
                Phone_Number = inputPhone,
                Hire_Date = inputHireDate,
                Salary = inputSalary,
                Comission_Pct = inputComission
            };
        }

        public int DeleteMenu()
        {
            Console.WriteLine("Hapus Employee Id: ");
            int inputId = Convert.ToInt32(Console.ReadLine());
    

            return inputId;
        }
        public int GetEmployeeId()
        {
            Console.WriteLine("Masukkan ID Employee: ");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

    }
}
