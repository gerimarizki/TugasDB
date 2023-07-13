using MVCArchitecture.Controllers;
using MVCArchitecture.Models;
using MVCArchitecture.Views;
using System;

public class Program
{

    public static void Main()
    {
        MainMenu();
    }

    public static void MainMenu()
    {

        Console.WriteLine("Pilih menu untuk masuk ke menunya");
        Console.WriteLine("1. Employees");
        Console.WriteLine("2. Departments");
        Console.WriteLine("3. Jobs");
        Console.WriteLine("4. Histories");
        Console.WriteLine("5. Locations");
        Console.WriteLine("6. Countries");
        Console.WriteLine("7. Regions");
        Console.WriteLine("8. Exit");
        Console.WriteLine("Pilih: ");

        try
        {
            int pilihMenu = Int32.Parse(Console.ReadLine());

            switch (pilihMenu)
            {

                case 1:
                    Console.WriteLine("1. Employees");
                    Console.Clear();
                    //MenuEmployees();
                    break;
                case 2:
                    Console.WriteLine("2. Departments");
                    Console.Clear();
                    //MenuDepartment();
                    MainMenu();
                    break;
                case 3:
                    Console.WriteLine("3. Jobs");
                    Console.Clear();
                    //MenuJobs();
                    MainMenu();
                    break;
                case 4:
                    Console.WriteLine("4. Histories");
                    Console.Clear();
                    //MenuHistories();
                    MainMenu();
                    break;
                case 5:
                    Console.WriteLine("5. Locations");
                    Console.Clear();
                    //MenuLocation();
                    MainMenu();
                    break;
                case 6:
                    Console.WriteLine("6. Countries");
                    Console.Clear();
                    //MenuCountry();
                    MainMenu();
                    break;
                case 7:
                    Console.WriteLine("7. Regions");
                    Console.Clear();
                    MenuRegion();
                    MainMenu();
                    break;
                case 8:
                    Console.WriteLine("8. Exit");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Silahkan Pilih Nomor 1-7");
                    MainMenu();
                    break;
            }
        }
        catch
        {
            Console.WriteLine("Input Hanya diantara 1-7!");
            MainMenu();
        }
    }


    public static void MenuRegion()
    {
        Region region = new Region();
        VRegion vRegion = new VRegion();
        RegionController regionController = new RegionController(region, vRegion);



        try
        {
            vRegion.Menu();
            int pilihMenu = Int32.Parse(Console.ReadLine());

            switch (pilihMenu)
            {

                case 1:
                    Console.WriteLine("1. Tambah Region");
                    regionController.Insert();
                    MenuRegion();
                    break;
                case 2:
                    Console.WriteLine("2. Update Region");
                    regionController.Update();
                    MenuRegion();
                    break;
                case 3:
                    Console.WriteLine("3. Hapus Region");
                    regionController.Delete();
                    MenuRegion();
                    break;
                case 4:
                    Console.WriteLine("4. Search By Region ID");
                    regionController.Search(); ;
                    MenuRegion();
                    break;
                case 5:
                    Console.WriteLine("5. Get All Regions");
                    regionController.GetAll();
                    MenuRegion();
                    break;
                case 6:
                    Console.WriteLine("MainMenu");
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Silahkan Pilih Nomor 1-6");
                    MenuRegion();
                    break;
            }
        }
        catch
        {
            Console.WriteLine("Input Hanya diantara 1-6!");
            MenuRegion();
        }
    }


    public static void MenuCountry()
    {
        Region region = new Region();
        VRegion vRegion = new VRegion();
        RegionController regionController = new RegionController(region, vRegion);



        try
        {
            vRegion.Menu();
            int pilihMenu = Int32.Parse(Console.ReadLine());

            switch (pilihMenu)
            {

                case 1:
                    Console.WriteLine("1. Tambah Country");
                    Console.Clear();
                    regionController.Insert();
                    MenuRegion();
                    break;
                case 2:
                    Console.WriteLine("2. Update Country");
                    Console.Clear();
                    regionController.Update();
                    MenuRegion();
                    break;
                case 3:
                    Console.WriteLine("3. Hapus Country");
                    Console.Clear();
                    //DeleteByRegion();
                    MenuRegion();
                    break;
                case 4:
                    Console.WriteLine("4. Search By Country ID");
                    Console.Clear();
                    //SearchRegionById();
                    MenuRegion();
                    break;
                case 5:
                    Console.WriteLine("5. Get All Country");
                    regionController.GetAll();
                    MenuRegion();
                    break;
                case 6:
                    Console.WriteLine("MainMenu");
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Silahkan Pilih Nomor 1-6");
                    MenuRegion();
                    break;
            }
        }
        catch
        {
            Console.WriteLine("Input Hanya diantara 1-6!");
            MenuRegion();
        }
    }
}