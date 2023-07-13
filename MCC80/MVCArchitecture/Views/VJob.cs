using MVCArchitecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Views
{
    public class VJob
    {
        public void GetAll(List<Job> jobs)
        {
            foreach (Job job in jobs)
            {
                GetById(job);
            }
        }

        public void Menu()
        {
            Console.WriteLine("Basic Authentication Geri Marizki");
            Console.WriteLine("Pilih menu untuk masuk ke menunya");
            Console.WriteLine("1. Tambah job");
            Console.WriteLine("2. Update job");
            Console.WriteLine("3. Hapus job");
            Console.WriteLine("4. Search By Id job");
            Console.WriteLine("5. Get All job");
            Console.WriteLine("6. Main Menu");
            Console.WriteLine("Pilih: ");
        }

        public void GetById(Job job)
        {
            Console.WriteLine("Id: " + job.Id);
            Console.WriteLine("Title: " + job.Title);
            Console.WriteLine("Min Salary: " + job.Min_Salary);
            Console.WriteLine("Max Salary: " + job.Max_Salary);
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

        public Job InsertMenu()
        {
            string inputTitle = Console.ReadLine();
            Console.WriteLine("Tambah Title: ");
            int inputMinSalary = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Tambah Min Salary: ");
            int inputMaxSalary = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Tambah MaxSalary: ");
            return new Job
            {
                Id = "",
                Title = inputTitle,
                Min_Salary = inputMinSalary,
                Max_Salary = inputMaxSalary

            };
        }

        public Job UpdateMenu()
        {
            string inputJobId = Console.ReadLine();
            Console.WriteLine("Update Id");
            string inputTitle = Console.ReadLine();
            Console.WriteLine("Update Title: ");
            int inputMinSalary = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Update Min Salary: ");
            int inputMaxSalary = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Update MaxSalary: ");

            return new Job
            {
                Id = inputJobId,
                Title = inputTitle,
                Min_Salary = inputMinSalary,
                Max_Salary = inputMaxSalary
            };
        }

        public string DeleteMenu()
        {
            Console.WriteLine("Hapus Job Id: ");
            string inputId = Console.ReadLine();

            return inputId;
        }

        public string GetByIdMenu()
        {
            Console.WriteLine("Cari Job Id: ");
            string inputId = Console.ReadLine();


            return inputId;
        }

        public string GetJobId()
        {
            Console.WriteLine("Masukkan ID Job: ");
            string id = Console.ReadLine();
            return id;
        }
    }
}
