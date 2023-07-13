using MVCArchitecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Views
{
    public class VLocation
    {
        public void GetAll(List<Location> locations)
        {
            foreach (Location location in locations)
            {
                GetById(location);
            }
        }

        public void Menu()
        {
            Console.WriteLine("Basic Authentication Geri Marizki");
            Console.WriteLine("Pilih menu untuk masuk ke menunya");
            Console.WriteLine("1. Tambah location");
            Console.WriteLine("2. Update location");
            Console.WriteLine("3. Hapus location");
            Console.WriteLine("4. Search By Id location");
            Console.WriteLine("5. Get All location");
            Console.WriteLine("6. Main Menu");
            Console.WriteLine("Pilih: ");
        }

        public void GetById(Location location)
        {
            Console.WriteLine("Id: " + location.Id);
            Console.WriteLine("Street Address: " + location.Street_Address);
            Console.WriteLine("Postal Code: " + location.Postal_Code);
            Console.WriteLine("State Province:" + location.State_Province);
            Console.WriteLine("Country ID: " + location.Country_Id);
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

        public Location InsertMenu()
        {
            string inputStreetAddress = Console.ReadLine();
            Console.WriteLine("Tambah StreetAddress: ");
            string inputPostalCode = Console.ReadLine();
            Console.WriteLine("Tambah PostalCode: ");
            string inputCity = Console.ReadLine();
            Console.WriteLine("Tambah City: ");
            string inputStateProvince = Console.ReadLine();
            Console.WriteLine("Tambah inputStateProvince: ");
            return new Location
            {
                Id = 0,
                Street_Address = inputStreetAddress,
                Postal_Code = inputPostalCode,
                City = inputCity,
                State_Province = inputStateProvince

            };
        }

        public Location UpdateMenu()
        {
            int inputLocationId = Int32.Parse(Console.ReadLine());
            string inputStreetAddress = Console.ReadLine();
            Console.WriteLine("Update StreetAddress: ");
            string inputPostalCode = Console.ReadLine();
            Console.WriteLine("Update PostalCode: ");
            string inputCity = Console.ReadLine();
            Console.WriteLine("Update City: ");
            string inputStateProvince = Console.ReadLine();
            Console.WriteLine("Update StateProvince: ");

            return new Location
            {
                Id = inputLocationId,
                Street_Address = inputStreetAddress,
                Postal_Code = inputPostalCode,
                City = inputCity,
                State_Province = inputStateProvince
            };
        }

        public int DeleteMenu()
        {
            int inputId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Hapus Location Id: ");

            return inputId;
        }

        public int GetByIdMenu()
        {
            Console.WriteLine("Cari Location Id: ");
            int inputId = Convert.ToInt32(Console.ReadLine());


            return inputId;
        }
    }
}
