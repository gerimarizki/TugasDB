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
            Console.WriteLine("City " + location.City);
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
            Console.WriteLine("Tambah Id");
            int Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Tambah StreetAddress: ");
            string inputStreetAddress = Console.ReadLine();
            Console.WriteLine("Tambah PostalCode: ");
            string inputPostalCode = Console.ReadLine();
            Console.WriteLine("Tambah City: ");
            string inputCity = Console.ReadLine();
            Console.WriteLine("Tambah StateProvince: ");
            string inputStateProvince = Console.ReadLine();
            Console.WriteLine("Tambah Country Id: ");
            string inputCountryId = Console.ReadLine();


            return new Location
            {
                Id = Id,
                Street_Address = inputStreetAddress,
                Postal_Code = inputPostalCode,
                City = inputCity,
                State_Province = inputStateProvince,
                Country_Id = inputCountryId

            };
        }

        public Location UpdateMenu()
        {
            int inputLocationId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Update StreetAddress: ");
            string inputStreetAddress = Console.ReadLine();
            Console.WriteLine("Update PostalCode: ");
            string inputPostalCode = Console.ReadLine();
            Console.WriteLine("Update City: ");
            string inputCity = Console.ReadLine();
            Console.WriteLine("Update StateProvince: ");
            string inputStateProvince = Console.ReadLine();


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
            Console.WriteLine("Hapus Location Id: ");
            int inputId = Convert.ToInt32(Console.ReadLine());


            return inputId;
        }

        public int GetLocationId()
        {
            Console.WriteLine("Masukkan ID Location: ");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }
    }
}
