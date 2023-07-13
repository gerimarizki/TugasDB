using MVCArchitecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Views
{
    public class VCountry
    {
        public void GetAll(List<Country> countries)
        {
            foreach (Country country in countries)
            {
                GetById(country);
            }
        }

        public void Menu()
        {
            Console.WriteLine("Basic Authentication Geri Marizki");
            Console.WriteLine("Pilih menu untuk masuk ke menunya");
            Console.WriteLine("1. Tambah country");
            Console.WriteLine("2. Update country");
            Console.WriteLine("3. Hapus country");
            Console.WriteLine("4. Search By Id country");
            Console.WriteLine("5. Get All country");
            Console.WriteLine("6. Main Menu");
            Console.WriteLine("Pilih: ");
        }

        public void GetById(Country country)
        {
            Console.WriteLine("Id: " + country.Id);
            Console.WriteLine("Name: " + country.Name);
            Console.WriteLine("Region Id: " + country.Region_Id);
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

        public Country InsertMenu()
        {
            Console.WriteLine("Masukkan Nama: ");

            string inputName = Console.ReadLine();
            return new Country
            {
                Id = "",
                Name = inputName

            };
        }

        public Country UpdateMenu()
        {
            Console.WriteLine("Input Id untuk diubah: ");
            string id = Console.ReadLine();
            Console.WriteLine("Nama: ");
            string name = Console.ReadLine();

            return new Country
            {
                Id = id,
                Name = name
            };
        }

        public string DeleteMenu()
        {
            Console.WriteLine("Hapus region Id: ");
            string inputId = Console.ReadLine();

            return inputId;
        }

        public string GetByIdMenu()
        {
            Console.WriteLine("Cari region Id: ");
            string inputId = Console.ReadLine();


            return inputId;
        }
    }
}