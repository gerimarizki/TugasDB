using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture
{
    public class Helper
    {
        public static DateTime DateValidation()
        {
            DateTime inputTanggal;
            Console.Write("Tanggal (mm/dd/yyyy) : ");
            bool cekValid = DateTime.TryParse(Console.ReadLine(), out inputTanggal);
            if (!cekValid)
            {
                Console.WriteLine("Tanggal salah, masukkan ulang!");
                DateValidation();
            }
            return inputTanggal;
        }
    }
}
