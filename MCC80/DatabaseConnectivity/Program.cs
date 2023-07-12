using System.Data.SqlClient;
using System.Threading.Channels;

namespace DatabaseConnectivity;

public class Program
{
    private static string _ConnectionString = @"
            Data Source=GERIMARIZKI;
            Initial Catalog=Latihan;
            Integrated Security=True";

    private static SqlConnection _connection;

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
                    MenuEmployees();
                    break;
                case 2:
                    Console.WriteLine("2. Departments");
                    Console.Clear();
                    MenuDepartment();
                    MainMenu();
                    break;
                case 3:
                    Console.WriteLine("3. Jobs");
                    Console.Clear();
                    MenuJobs();
                    MainMenu();
                    break;
                case 4:
                    Console.WriteLine("4. Histories");
                    Console.Clear();
                    MenuHistories();
                    MainMenu();
                    break;
                case 5:
                    Console.WriteLine("5. Locations");
                    Console.Clear();
                    MenuLocation();
                    MainMenu();
                    break;
                case 6:
                    Console.WriteLine("6. Countries");
                    Console.Clear();
                    MenuCountry();
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



    public static void GetRegions()
    {
        _connection = new SqlConnection( _ConnectionString);
        

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM Regions";


        try
        {
            _connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                }
            }
            else
            {
                reader.Close();
                _connection.Close();

            }
        }
        catch
        {
            Console.WriteLine("Tidak ada Regions");
        }


    }

    public static void InsertRegions(string name)
    {
        _connection = new SqlConnection(_ConnectionString);


        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "INSERT INTO Regions VALUES (@name)";

        _connection.Open();
        using SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;


        try
        {

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = System.Data.SqlDbType.VarChar;
            pName.Value = name;
            sqlCommand.Parameters.Add(pName);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Berhasil");
            }
            else
            {
                Console.WriteLine("Gagal");
            }

            transaction.Commit();
            _connection.Close();

        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Tidak ada Regions");
        }


    }

    public static void InsertIntoRegion()
    {
        string inputName = Console.ReadLine();
        Console.WriteLine("Tambah Nama: ");
        InsertRegions(inputName);
    }
    
    public static void UpdateRegions(string name, int region_id)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "UPDATE regions SET name = @name WHERE region_id = @region_id";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = System.Data.SqlDbType.VarChar;
            pName.Value = name;
            sqlCommand.Parameters.Add(pName);

            SqlParameter pRegionId = new SqlParameter();
            pRegionId.ParameterName = "@region_id";
            pRegionId.SqlDbType = System.Data.SqlDbType.Int;
            pRegionId.Value = region_id;
            sqlCommand.Parameters.Add(pRegionId);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Update Success");
            }
            else
            {
                Console.WriteLine("Update Fail");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to the database");
        }
    }

    public static void UpdateIntoRegion()
    {
        string inputName = Console.ReadLine();
        Console.WriteLine("Ganti Nama: ");
        int inputId = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Ganti ID: ");
        UpdateRegions(inputName, inputId);
    }
    
    public static void DeleteRegions(int region_id)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "DELETE FROM regions WHERE region_id = @region_id";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pRegionId = new SqlParameter();
            pRegionId.ParameterName = "@region_id";
            pRegionId.SqlDbType = System.Data.SqlDbType.Int;
            pRegionId.Value = region_id;
            sqlCommand.Parameters.Add(pRegionId);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Delete Success");
            }
            else
            {
                Console.WriteLine("Delete Fail");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to the database");
        }
    }

    public static void DeleteByRegion()
    {
        int inputId = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Hapus region Id: ");
        DeleteRegions(inputId);
    }

    public static void GetRegionById(int region_id)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM regions WHERE region_id = @region_id";
        sqlCommand.Parameters.AddWithValue("@region_id", region_id);

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                }
            }
            else
            {
                Console.WriteLine("tidak ada region");
            }
            reader.Close();
            _connection.Close();
        }
        catch
        {
            Console.WriteLine("Error");
        }

    }

    public static void SearchRegionById()
    {
        int inputId = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Cari region Id: ");
        GetRegionById(inputId);
    }

    public static void MenuRegion()
    {
        Console.WriteLine("Basic Authentication Geri Marizki");
        Console.WriteLine("Pilih menu untuk masuk ke menunya");
        Console.WriteLine("1. Tambah Region");
        Console.WriteLine("2. Update Region");
        Console.WriteLine("3. Hapus Region");
        Console.WriteLine("4. Search By Id Region");
        Console.WriteLine("5. Get All Regions");
        Console.WriteLine("6. Main Menu");
        Console.WriteLine("Pilih: ");

        try
        {
            int pilihMenu = Int32.Parse(Console.ReadLine());

            switch (pilihMenu)
            {

                case 1:
                    Console.WriteLine("1. Tambah Region");
                    Console.Clear();
                    InsertIntoRegion();
                    MenuRegion();
                    break;
                case 2:
                    Console.WriteLine("2. Update Region");
                    Console.Clear();
                    UpdateIntoRegion();
                    MenuRegion();
                    break;
                case 3:
                    Console.WriteLine("3. Hapus Region");
                    Console.Clear();
                    DeleteByRegion();
                    MenuRegion();
                    break;
                case 4:
                    Console.WriteLine("4. Search By Region ID");
                    Console.Clear();
                    SearchRegionById();
                    MenuRegion();
                    break;
                case 5:
                    Console.WriteLine("5. Get All Regions");
                    GetRegions();
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





    public static void GetCountry()
    {
        _connection = new SqlConnection(_ConnectionString);


        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM countries";


        try
        {
            _connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetString(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                    Console.WriteLine("Region_Id: " + reader.GetInt32(2));
                }
            }
            else
            {
                reader.Close();
                _connection.Close();

            }
        }
        catch
        {
            Console.WriteLine("Tidak ada Country");
        }


    }

    public static void InsertCountry(string name)
    {
        _connection = new SqlConnection(_ConnectionString);


        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "INSERT INTO countries VALUES (@name)";

        _connection.Open();
        using SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;


        try
        {

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = System.Data.SqlDbType.VarChar;
            pName.Value = name;
            sqlCommand.Parameters.Add(pName);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Berhasil");
            }
            else
            {
                Console.WriteLine("Gagal");
            }

            transaction.Commit();
            _connection.Close();

        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Tidak ada Regions");
        }


    }

    public static void InsertIntoCountry()
    {
        string inputName = Console.ReadLine();
        Console.WriteLine("Tambah Nama: ");
        InsertCountry(inputName);
    }

    public static void UpdateCountry(string name, string country_id)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "UPDATE countries SET name = @name WHERE country_id = @country_id";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = System.Data.SqlDbType.VarChar;
            pName.Value = name;
            sqlCommand.Parameters.Add(pName);

            SqlParameter pCountryId = new SqlParameter();
            pCountryId.ParameterName = "@country_id";
            pCountryId.SqlDbType = System.Data.SqlDbType.Int;
            pCountryId.Value = country_id;
            sqlCommand.Parameters.Add(pCountryId);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Update Success");
            }
            else
            {
                Console.WriteLine("Update Fail");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to the database");
        }
    }

    public static void UpdateIntoCountry()
    {
        string inputName = Console.ReadLine();
        Console.WriteLine("Ganti Nama: ");
        string inputId = Console.ReadLine();
        Console.WriteLine("Ganti ID: ");
        UpdateCountry(inputName, inputId);
    }

    public static void DeleteCountry(string country_id)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "DELETE FROM countries WHERE country_id = @country_id";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pCountryId = new SqlParameter();
            pCountryId.ParameterName = "@country_id";
            pCountryId.SqlDbType = System.Data.SqlDbType.Int;
            pCountryId.Value = country_id;
            sqlCommand.Parameters.Add(pCountryId);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Delete Success");
            }
            else
            {
                Console.WriteLine("Delete Fail");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to the database");
        }
    }

    public static void DeleteByCountry()
    {
        string inputId = Console.ReadLine();
        Console.WriteLine("Hapus Country Id: ");
        DeleteCountry(inputId);
    }

    public static void GetCountryById(string country_id)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM countries WHERE region_id = @country_id";
        sqlCommand.Parameters.AddWithValue("@country_id", country_id);

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetString(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                    Console.WriteLine("Region Id: " + reader.GetString(2));
                }
            }
            else
            {
                Console.WriteLine("tidak ada country");
            }
            reader.Close();
            _connection.Close();
        }
        catch
        {
            Console.WriteLine("Error");
        }

    }

    public static void SearchCountryById()
    {
        string inputId = Console.ReadLine();
        Console.WriteLine("Cari Country Id: ");
        GetCountryById(inputId);
    }

    public static void MenuCountry()
    {
        Console.WriteLine("Basic Authentication Geri Marizki");
        Console.WriteLine("Pilih menu untuk masuk ke menunya");
        Console.WriteLine("1. Tambah Country");
        Console.WriteLine("2. Update Country");
        Console.WriteLine("3. Hapus Country");
        Console.WriteLine("4. Search By Id Country");
        Console.WriteLine("5. Get All Country");
        Console.WriteLine("6. MainMenu");
        Console.WriteLine("Pilih: ");

        try
        {
            int pilihMenu = Int32.Parse(Console.ReadLine());

            switch (pilihMenu)
            {

                case 1:
                    Console.WriteLine("1. Tambah Country");
                    Console.Clear();
                    InsertIntoCountry();
                    MenuCountry();
                    break;
                case 2:
                    Console.WriteLine("2. Update Country");
                    Console.Clear();
                    UpdateIntoCountry();
                    MenuCountry();
                    break;
                case 3:
                    Console.WriteLine("3. Hapus Country");
                    Console.Clear();
                    DeleteByCountry();
                    MenuCountry();
                    break;
                case 4:
                    Console.WriteLine("4. Search By Country");
                    Console.Clear();
                    SearchCountryById();
                    MenuCountry();
                    break;
                case 5:
                    Console.WriteLine("5. Get All Country");
                    GetCountry();
                    MenuCountry();
                    break;
                case 6:
                    Console.WriteLine("MainMenu");
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Silahkan Pilih Nomor 1-6");
                    MenuCountry();
                    break;
            }
        }
        catch
        {
            Console.WriteLine("Input Hanya diantara 1-6!");
            MenuCountry();
        }
    }



    public static void GetLocations()
    {
        _connection = new SqlConnection(_ConnectionString);


        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM locations";


        try
        {
            _connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Street Address: " + reader.GetString(1));
                    Console.WriteLine("Postal Code: " + reader.GetString(2));
                    Console.WriteLine("City: " + reader.GetString(3));
                    Console.WriteLine("State Province: " + reader.GetString(4));
                    Console.WriteLine("Country Id: " + reader.GetString(5));
                }
            }
            else
            {
                reader.Close();
                _connection.Close();

            }
        }
        catch
        {
            Console.WriteLine("Tidak ada Locations");
        }


    }

    public static void InsertLocations(string street_address, string postal_code, string city, string state_province)
    {
        _connection = new SqlConnection(_ConnectionString);


        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "INSERT INTO countries VALUES (@street_address), (@postal_code), (@city), (@state_province)";

        _connection.Open();
        using SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;


        try
        {

            SqlParameter pStreetAddress = new SqlParameter();
            pStreetAddress.ParameterName = "@street_address";
            pStreetAddress.SqlDbType = System.Data.SqlDbType.VarChar;
            pStreetAddress.Value = street_address;
            sqlCommand.Parameters.Add(pStreetAddress);

            SqlParameter pPostalCode = new SqlParameter();
            pPostalCode.ParameterName = "@postal_code";
            pPostalCode.SqlDbType = System.Data.SqlDbType.VarChar;
            pPostalCode.Value = postal_code;
            sqlCommand.Parameters.Add(pPostalCode);

            SqlParameter pCity = new SqlParameter();
            pCity.ParameterName = "@city";
            pCity.SqlDbType = System.Data.SqlDbType.VarChar;
            pCity.Value = city;
            sqlCommand.Parameters.Add(pCity);

            SqlParameter pStateProvince = new SqlParameter();
            pStateProvince.ParameterName = "@state_province";
            pStateProvince.SqlDbType = System.Data.SqlDbType.VarChar;
            pStateProvince.Value = state_province;
            sqlCommand.Parameters.Add(pStateProvince);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Berhasil");
            }
            else
            {
                Console.WriteLine("Gagal");
            }

            transaction.Commit();
            _connection.Close();

        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Tidak ada Regions");
        }


    }

    public static void InsertIntoLocations()
    {
        string inputStreetAddress = Console.ReadLine();
        Console.WriteLine("Tambah StreetAddress: ");
        string inputPostalCode = Console.ReadLine();
        Console.WriteLine("Tambah PostalCode: ");
        string inputCity = Console.ReadLine();
        Console.WriteLine("Tambah City: ");
        string inputStateProvince = Console.ReadLine();
        Console.WriteLine("Tambah inputStateProvince: ");
        InsertLocations(inputStreetAddress, inputPostalCode, inputCity, inputStateProvince);
    }

    public static void UpdateLocations(int location_id, string street_address, string postal_code, string city, string state_province)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "UPDATE countries SET street_address = @street_Address, postal_code = @postal_code, city = @city, state_province = @state_province WHERE location_id = @location_id";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pLocationId = new SqlParameter();
            pLocationId.ParameterName = "@location_id";
            pLocationId.SqlDbType = System.Data.SqlDbType.Int;
            pLocationId.Value = location_id;
            sqlCommand.Parameters.Add(pLocationId);

            SqlParameter pStreetAddress = new SqlParameter();
            pStreetAddress.ParameterName = "@street_address";
            pStreetAddress.SqlDbType = System.Data.SqlDbType.VarChar;
            pStreetAddress.Value = street_address;
            sqlCommand.Parameters.Add(pStreetAddress);

            SqlParameter pPostalCode = new SqlParameter();
            pPostalCode.ParameterName = "@postal_code";
            pPostalCode.SqlDbType = System.Data.SqlDbType.VarChar;
            pPostalCode.Value = postal_code;
            sqlCommand.Parameters.Add(pPostalCode);

            SqlParameter pCity = new SqlParameter();
            pCity.ParameterName = "@city";
            pCity.SqlDbType = System.Data.SqlDbType.VarChar;
            pCity.Value = city;
            sqlCommand.Parameters.Add(pCity);

            SqlParameter pStateProvince = new SqlParameter();
            pStateProvince.ParameterName = "@state_province";
            pStateProvince.SqlDbType = System.Data.SqlDbType.VarChar;
            pStateProvince.Value = state_province;
            sqlCommand.Parameters.Add(pStateProvince);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Update Success");
            }
            else
            {
                Console.WriteLine("Update Fail");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to the database");
        }
    }

    public static void UpdateIntoLocation()
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
        UpdateLocations(inputLocationId ,inputStreetAddress, inputPostalCode, inputCity, inputStateProvince);

    }

    public static void DeleteLocation(int location_id)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "DELETE FROM locations WHERE location_id = @location_id";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pLocationId = new SqlParameter();
            pLocationId.ParameterName = "@location_id";
            pLocationId.SqlDbType = System.Data.SqlDbType.Int;
            pLocationId.Value = location_id;
            sqlCommand.Parameters.Add(pLocationId);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Delete Success");
            }
            else
            {
                Console.WriteLine("Delete Fail");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to the database");
        }
    }

    public static void DeleteByLocation()
    {
        int inputId = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Hapus Locaation Id: ");
        DeleteLocation(inputId);
    }

    public static void GetLocationById(int location_id)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM locations WHERE location_id = @location_id";
        sqlCommand.Parameters.AddWithValue("@location_id", location_id);

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetString(0));
                    Console.WriteLine("Street Address: " + reader.GetString(1));
                    Console.WriteLine("Postal Code: " + reader.GetString(2));
                    Console.WriteLine("City: " + reader.GetString(3));
                    Console.WriteLine("State Province: " + reader.GetString(4));
                    Console.WriteLine("Country Id: " + reader.GetString(5));
                }
            }
            else
            {
                Console.WriteLine("tidak ada location");
            }
            reader.Close();
            _connection.Close();
        }
        catch
        {
            Console.WriteLine("Error");
        }

    }

    public static void SearchLocationById()
    {
        int inputId = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Cari Country Id: ");
        GetLocationById(inputId);
    }

    public static void MenuLocation()
    {
        Console.WriteLine("Basic Authentication Geri Marizki");
        Console.WriteLine("Pilih menu untuk masuk ke menunya");
        Console.WriteLine("1. Tambah Location");
        Console.WriteLine("2. Update Location");
        Console.WriteLine("3. Hapus Location");
        Console.WriteLine("4. Search By Id Location");
        Console.WriteLine("5. Get All Location");
        Console.WriteLine("6. Main Menu");
        Console.WriteLine("Pilih: ");

        try
        {
            int pilihMenu = Int32.Parse(Console.ReadLine());

            switch (pilihMenu)
            {

                case 1:
                    Console.WriteLine("1. Tambah Location");
                    Console.Clear();
                    InsertIntoLocations();
                    MenuLocation();
                    break;
                case 2:
                    Console.WriteLine("2. Update Location");
                    Console.Clear();
                    UpdateIntoLocation();
                    MenuLocation();
                    break;
                case 3:
                    Console.WriteLine("3. Hapus Location");
                    Console.Clear();
                    DeleteByLocation();
                    MenuLocation();
                    break;
                case 4:
                    Console.WriteLine("4. Search By Location");
                    Console.Clear();
                    SearchLocationById();
                    MenuLocation();
                    break;
                case 5:
                    Console.WriteLine("5. Get All Location");
                    GetLocations();
                    MenuLocation();
                    break;
                case 6:
                    Console.WriteLine("MainMenu");
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Silahkan Pilih Nomor 1-6");
                    MenuLocation();
                    break;
            }
        }
        catch
        {
            Console.WriteLine("Input Hanya diantara 1-6!");
            MenuLocation();
        }
    }



    public static void GetDepartments()
    {
        _connection = new SqlConnection(_ConnectionString);


        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM departments";


        try
        {
            _connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                    Console.WriteLine("Location Id: " + reader.GetInt32(2));
                    Console.WriteLine("Manager Id: " + reader.GetInt32(3));
                }
            }
            else
            {
                reader.Close();
                _connection.Close();

            }
        }
        catch
        {
            Console.WriteLine("Tidak ada Departments");
        }


    }

    public static void InsertDepartments(string name)
    {
        _connection = new SqlConnection(_ConnectionString);


        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "INSERT INTO departments VALUES (@name)";

        _connection.Open();
        using SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;


        try
        {

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = System.Data.SqlDbType.VarChar;
            pName.Value = name;
            sqlCommand.Parameters.Add(pName);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Berhasil");
            }
            else
            {
                Console.WriteLine("Gagal");
            }

            transaction.Commit();
            _connection.Close();

        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Tidak ada Regions");
        }


    }

    public static void InsertIntoDepartments()
    {
        string inputName = Console.ReadLine();
        Console.WriteLine("Tambah Department: ");
        InsertDepartments(inputName);
    }

    public static void UpdateDepartment(string name, int department_id)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "UPDATE department SET name = @name WHERE department_id = @department_id";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = System.Data.SqlDbType.VarChar;
            pName.Value = name;
            sqlCommand.Parameters.Add(pName);

            SqlParameter pDepartmentId = new SqlParameter();
            pDepartmentId.ParameterName = "@department_id";
            pDepartmentId.SqlDbType = System.Data.SqlDbType.Int;
            pDepartmentId.Value = department_id;
            sqlCommand.Parameters.Add(pDepartmentId);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Update Success");
            }
            else
            {
                Console.WriteLine("Update Fail");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to the database");
        }
    }

    public static void UpdateIntoDepartment()
    {
        string inputName = Console.ReadLine();
        Console.WriteLine("Ganti Nama: ");
        int inputId = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Ganti ID: ");
        UpdateDepartment(inputName, inputId);
    }

    public static void DeleteDepartment(int department_id)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "DELETE FROM departments WHERE department_id = @department_id";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pDepartmentId = new SqlParameter();
            pDepartmentId.ParameterName = "@department_id";
            pDepartmentId.SqlDbType = System.Data.SqlDbType.Int;
            pDepartmentId.Value = department_id;
            sqlCommand.Parameters.Add(pDepartmentId);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Delete Success");
            }
            else
            {
                Console.WriteLine("Delete Fail");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to the database");
        }
    }

    public static void DeleteByDepartment()
    {
        int inputId = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Hapus Department Id: ");
        DeleteDepartment(inputId);
    }

    public static void GetDepartmentById(int department_id)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM departments WHERE department_id = @department_id";
        sqlCommand.Parameters.AddWithValue("@department_id", department_id);

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                    Console.WriteLine("Location Id: " + reader.GetInt32(2));
                    Console.WriteLine("Manager Id: " + reader.GetInt32(3));
                }
            }
            else
            {
                Console.WriteLine("tidak ada department");
            }
            reader.Close();
            _connection.Close();
        }
        catch
        {
            Console.WriteLine("Error");
        }

    }

    public static void SearchDepartmentById()
    {
        int inputId = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Cari Department Id: ");
        GetDepartmentById(inputId);
    }

    public static void MenuDepartment()
    {
        Console.WriteLine("Basic Authentication Geri Marizki");
        Console.WriteLine("Pilih menu untuk masuk ke menunya");
        Console.WriteLine("1. Tambah Department");
        Console.WriteLine("2. Update Department");
        Console.WriteLine("3. Hapus Department");
        Console.WriteLine("4. Search By Id Department");
        Console.WriteLine("5. Get All Department");
        Console.WriteLine("6. Main Menu");
        Console.WriteLine("Pilih: ");

        try
        {
            int pilihMenu = Int32.Parse(Console.ReadLine());

            switch (pilihMenu)
            {

                case 1:
                    Console.WriteLine("1. Tambah Department");
                    Console.Clear();
                    InsertIntoDepartments();
                    MenuDepartment();
                    break;
                case 2:
                    Console.WriteLine("2. Update Department");
                    Console.Clear();
                    UpdateIntoDepartment();
                    MenuDepartment();
                    break;
                case 3:
                    Console.WriteLine("3. Hapus Department");
                    Console.Clear();
                    DeleteByDepartment();
                    MenuDepartment();
                    break;
                case 4:
                    Console.WriteLine("4. Search By Department");
                    Console.Clear();
                    SearchDepartmentById();
                    MenuDepartment();
                    break;
                case 5:
                    Console.WriteLine("5. Get All Department");
                    GetDepartments();
                    MenuDepartment();
                    break;
                case 6:
                    Console.WriteLine("MainMenu");
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Silahkan Pilih Nomor 1-6");
                    MenuDepartment();
                    break;
            }
        }
        catch
        {
            Console.WriteLine("Input Hanya diantara 1-6!");
            MenuDepartment();
        }
    }


    public static void GetJobs()
    {
        _connection = new SqlConnection(_ConnectionString);


        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM jobs";


        try
        {
            _connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetString(0));
                    Console.WriteLine("Title: " + reader.GetString(1));
                    Console.WriteLine("Min Salary: " + reader.GetInt32(2));
                    Console.WriteLine("Max Salary: " + reader.GetInt32(3));
                }
            }
            else
            {
                reader.Close();
                _connection.Close();

            }
        }
        catch
        {
            Console.WriteLine("Tidak ada Jobs");
        }


    }

    public static void InsertJobs(string title, int min_salary, int max_salary)
    {
        _connection = new SqlConnection(_ConnectionString);


        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "INSERT INTO jobs VALUES (@title), (@min_salary), (@max_salary)";

        _connection.Open();
        using SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;


        try
        {

            SqlParameter pTitle = new SqlParameter();
            pTitle.ParameterName = "@title";
            pTitle.SqlDbType = System.Data.SqlDbType.VarChar;
            pTitle.Value = title;
            sqlCommand.Parameters.Add(pTitle);

            SqlParameter pMinSalary = new SqlParameter();
            pMinSalary.ParameterName = "@min_salary";
            pMinSalary.SqlDbType = System.Data.SqlDbType.Int;
            pMinSalary.Value = min_salary;
            sqlCommand.Parameters.Add(pMinSalary);

            SqlParameter pMaxSalary = new SqlParameter();
            pMaxSalary.ParameterName = "@max_salary";
            pMaxSalary.SqlDbType = System.Data.SqlDbType.Int;
            pMaxSalary.Value = max_salary;
            sqlCommand.Parameters.Add(pMaxSalary);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Berhasil");
            }
            else
            {
                Console.WriteLine("Gagal");
            }

            transaction.Commit();
            _connection.Close();

        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Tidak ada Regions");
        }


    }

    public static void InsertIntoJobs()
    {
        string inputTitle = Console.ReadLine();
        Console.WriteLine("Tambah Title: ");
        int inputMinSalary = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Tambah Min Salary: ");
        int inputMaxSalary = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Tambah MaxSalary: ");
        InsertJobs(inputTitle, inputMinSalary, inputMaxSalary);
    }

    public static void UpdateJobs(string job_id, string title, int min_salary, int max_salary)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "UPDATE jobs SET title = @title, min_salary = @min_salary, max_salary = @max_salary WHERE job_id = @job_id";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pJobId = new SqlParameter();
            pJobId.ParameterName = "@location_id";
            pJobId.SqlDbType = System.Data.SqlDbType.Char;
            pJobId.Value = job_id;
            sqlCommand.Parameters.Add(job_id);

            SqlParameter pTitle = new SqlParameter();
            pTitle.ParameterName = "@title";
            pTitle.SqlDbType = System.Data.SqlDbType.VarChar;
            pTitle.Value = title;
            sqlCommand.Parameters.Add(pTitle);

            SqlParameter pMinSalary = new SqlParameter();
            pMinSalary.ParameterName = "@min_salary";
            pMinSalary.SqlDbType = System.Data.SqlDbType.Int;
            pMinSalary.Value = min_salary;
            sqlCommand.Parameters.Add(pMinSalary);

            SqlParameter pMaxSalary = new SqlParameter();
            pMaxSalary.ParameterName = "@max_salary";
            pMaxSalary.SqlDbType = System.Data.SqlDbType.Int;
            pMaxSalary.Value = max_salary;
            sqlCommand.Parameters.Add(pMaxSalary);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Update Success");
            }
            else
            {
                Console.WriteLine("Update Fail");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to the database");
        }
    }

    public static void UpdateIntoJob()
    {
        string inputJobId = Console.ReadLine();
        Console.WriteLine("Update Id");
        string inputTitle = Console.ReadLine();
        Console.WriteLine("Update Title: ");
        int inputMinSalary = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Update Min Salary: ");
        int inputMaxSalary = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Update MaxSalary: ");
        UpdateJobs(inputJobId, inputTitle, inputMinSalary, inputMaxSalary);

    }

    public static void DeleteJob(string job_id)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "DELETE FROM jobs WHERE job_id = @job_id";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pJobId = new SqlParameter();
            pJobId.ParameterName = "@job_id";
            pJobId.SqlDbType = System.Data.SqlDbType.Char;
            pJobId.Value = job_id;
            sqlCommand.Parameters.Add(pJobId);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Delete Success");
            }
            else
            {
                Console.WriteLine("Delete Fail");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to the database");
        }
    }

    public static void DeleteByJob()
    {
        int inputId = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Hapus Job Id: ");
        DeleteDepartment(inputId);
    }

    public static void GetJobById(string job_id)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM jobs WHERE job_id = @job_id";
        sqlCommand.Parameters.AddWithValue("@job_id", job_id);

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetString(0));
                    Console.WriteLine("Title: " + reader.GetString(1));
                    Console.WriteLine("Min Salary: " + reader.GetInt32(2));
                    Console.WriteLine("Max Salary: " + reader.GetInt32(3));
                }
            }
            else
            {
                Console.WriteLine("tidak ada job");
            }
            reader.Close();
            _connection.Close();
        }
        catch
        {
            Console.WriteLine("Error");
        }

    }

    public static void SearchJobById()
    {
        string inputId = Console.ReadLine();
        Console.WriteLine("Cari job Id: ");
        GetJobById(inputId);
    }

    public static void MenuJobs()
    {
        Console.WriteLine("Basic Authentication Geri Marizki");
        Console.WriteLine("Pilih menu untuk masuk ke menunya");
        Console.WriteLine("1. Tambah Job");
        Console.WriteLine("2. Update Job");
        Console.WriteLine("3. Hapus Job");
        Console.WriteLine("4. Search By Id Job");
        Console.WriteLine("5. Get All Job");
        Console.WriteLine("6. Main Menu");
        Console.WriteLine("Pilih: ");

        try
        {
            int pilihMenu = Int32.Parse(Console.ReadLine());

            switch (pilihMenu)
            {

                case 1:
                    Console.WriteLine("1. Tambah Job");
                    Console.Clear();
                    InsertIntoJobs();
                    MenuJobs();
                    break;
                case 2:
                    Console.WriteLine("2. Update Job");
                    Console.Clear();
                    UpdateIntoJob();
                    MenuJobs();
                    break;
                case 3:
                    Console.WriteLine("3. Hapus Job");
                    Console.Clear();
                    DeleteByJob();
                    MenuJobs();
                    break;
                case 4:
                    Console.WriteLine("4. Search By Job");
                    Console.Clear();
                    SearchJobById();
                    MenuJobs();
                    break;
                case 5:
                    Console.WriteLine("5. Get All Job");
                    GetJobs();
                    MenuJobs();
                    break;
                case 6:
                    Console.WriteLine("MainMenu");
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Silahkan Pilih Nomor 1-6");
                    MenuJobs();
                    break;
            }
        }
        catch
        {
            Console.WriteLine("Input Hanya diantara 1-6!");
            MenuJobs();
        }
    }


    public static void GetHistories()
    {
        _connection = new SqlConnection(_ConnectionString);


        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM histories";


        try
        {
            _connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Start Date: " + reader.GetDateTime(0));
                    Console.WriteLine("Employee Id: " + reader.GetInt32(1));
                    Console.WriteLine("End Date: " + reader.GetDateTime(2));
                    Console.WriteLine("Department Id: " + reader.GetInt32(3));
                    Console.WriteLine("Job Id: " + reader.GetInt32(4));
                }
            }
            else
            {
                reader.Close();
                _connection.Close();

            }
        }
        catch
        {
            Console.WriteLine("Tidak ada Jobs");
        }


    }

    public static void InsertHistories(DateTime start_date, DateTime end_date)
    {
        _connection = new SqlConnection(_ConnectionString);


        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "INSERT INTO histories VALUES (@start_Date), (@end_Date)";

        _connection.Open();
        using SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;


        try
        {

            SqlParameter pStartDate = new SqlParameter();
            pStartDate.ParameterName = "@start_date";
            pStartDate.SqlDbType = System.Data.SqlDbType.DateTime;
            pStartDate.Value = start_date;
            sqlCommand.Parameters.Add(pStartDate);

            SqlParameter pEndDate = new SqlParameter();
            pEndDate.ParameterName = "@end_date";
            pEndDate.SqlDbType = System.Data.SqlDbType.DateTime;
            pEndDate.Value = end_date;
            sqlCommand.Parameters.Add(pEndDate);


            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Berhasil");
            }
            else
            {
                Console.WriteLine("Gagal");
            }

            transaction.Commit();
            _connection.Close();

        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Tidak ada Regions");
        }


    }

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

    public static void InsertIntoHistories()
    {
        DateTime inputStartDate = DateValidation();
        Console.Write("StartDate : ");
        DateTime inputEndDate = DateValidation();
        Console.Write("EndDate : ");
        InsertHistories(inputStartDate, inputEndDate);
    }

    public static void UpdateHistories(DateTime start_date, DateTime end_date)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "UPDATE Histories SET end_date = @end_date WHERE start_date = @start_date";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pStartDate = new SqlParameter();
            pStartDate.ParameterName = "@start_date";
            pStartDate.SqlDbType = System.Data.SqlDbType.DateTime;
            pStartDate.Value = start_date;
            sqlCommand.Parameters.Add(pStartDate);

            SqlParameter pEndDate = new SqlParameter();
            pEndDate.ParameterName = "@end_date";
            pEndDate.SqlDbType = System.Data.SqlDbType.DateTime;
            pEndDate.Value = end_date;
            sqlCommand.Parameters.Add(pEndDate);



            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Update Success");
            }
            else
            {
                Console.WriteLine("Update Fail");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to the database");
        }
    }

    public static void UpdateIntoHistories()
    {
        DateTime inputStartDate = DateValidation();
        Console.Write("StartDate : ");
        DateTime inputEndDate = DateValidation();
        Console.Write("EndDate : ");
        UpdateHistories(inputStartDate, inputEndDate);

    }

    public static void DeleteHistories(DateTime start_date)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "DELETE FROM histories WHERE start_date = @start_date";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pStartDate = new SqlParameter();
            pStartDate.ParameterName = "@start_date";
            pStartDate.SqlDbType = System.Data.SqlDbType.DateTime;
            pStartDate.Value = start_date;
            sqlCommand.Parameters.Add(pStartDate);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Delete Success");
            }
            else
            {
                Console.WriteLine("Delete Fail");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to the database");
        }
    }

    public static void DeleteByHistories()
    {
        DateTime inputStartDate = DateValidation();
        Console.Write("Hapus History Start Date: ");
        DeleteHistories(inputStartDate);
    }

    public static void GetHistoriesById(DateTime start_date)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM histories WHERE start_date = @start_date";
        sqlCommand.Parameters.AddWithValue("@start_date", start_date);

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Start Date: " + reader.GetDateTime(0));
                    Console.WriteLine("Employee Id: " + reader.GetInt32(1));
                    Console.WriteLine("End Date: " + reader.GetDateTime(2));
                    Console.WriteLine("Department Id: " + reader.GetInt32(3));
                    Console.WriteLine("Job Id: " + reader.GetInt32(4));
                }
            }
            else
            {
                Console.WriteLine("tidak ada job");
            }
            reader.Close();
            _connection.Close();
        }
        catch
        {
            Console.WriteLine("Error");
        }

    }

    public static void SearchHistoriesById()
    {
        DateTime inputStartDate = DateValidation();
        Console.Write("Cari Histories berdasarkan StartDate : ");
        GetHistoriesById(inputStartDate);
    }

    public static void MenuHistories()
    {
        Console.WriteLine("Basic Authentication Geri Marizki");
        Console.WriteLine("Pilih menu untuk masuk ke menunya");
        Console.WriteLine("1. Tambah Histories");
        Console.WriteLine("2. Update Histories");
        Console.WriteLine("3. Hapus Histories");
        Console.WriteLine("4. Search By Id Histories");
        Console.WriteLine("5. Get All Histories");
        Console.WriteLine("6. Main Menu");
        Console.WriteLine("Pilih: ");

        try
        {
            int pilihMenu = Int32.Parse(Console.ReadLine());

            switch (pilihMenu)
            {

                case 1:
                    Console.WriteLine("1. Tambah Histories");
                    Console.Clear();
                    InsertIntoJobs();
                    MenuHistories();
                    break;
                case 2:
                    Console.WriteLine("2. Update Histories");
                    Console.Clear();
                    UpdateIntoJob();
                    MenuHistories();
                    break;
                case 3:
                    Console.WriteLine("3. Hapus Histories");
                    Console.Clear();
                    DeleteByJob();
                    MenuHistories();
                    break;
                case 4:
                    Console.WriteLine("4. Search By Histories");
                    Console.Clear();
                    SearchJobById();
                    MenuHistories();
                    break;
                case 5:
                    Console.WriteLine("5. Get All Histories");
                    GetJobs();
                    MenuHistories();
                    break;
                case 6:
                    Console.WriteLine("MainMenu");
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Silahkan Pilih Nomor 1-6");
                    MenuHistories();
                    break;
            }
        }
        catch
        {
            Console.WriteLine("Input Hanya diantara 1-6!");
            MenuHistories();
        }
    }


    public static void GetEmployees()
    {
        _connection = new SqlConnection(_ConnectionString);


        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM employees";


        try
        {
            _connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string phone_number = reader.IsDBNull(4) ? "N/A" : reader.GetString(4);
                    int salary = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                    decimal comission_pct = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7);

                    Console.WriteLine("ID: " + reader.GetInt32(0));
                    Console.WriteLine("First Name: " + reader.GetString(1));
                    Console.WriteLine("Last Name: " + reader.GetString(2));
                    Console.WriteLine("Email: " + reader.GetString(3));
                    Console.WriteLine("Phone Number: " + phone_number);
                    Console.WriteLine("Hire Date: " + reader.GetDateTime(5));
                    Console.WriteLine("Salary: " + salary);
                    Console.WriteLine("Comission PCT: " + comission_pct);

                }
            }
            else
            {
                reader.Close();
                _connection.Close();

            }
        }
        catch
        {
            Console.WriteLine("Tidak ada Employees");
        }


    }

    public static void InsertEmployees(string first_name, string last_name, string email, string phone_number, DateTime hire_date, int salary, decimal comission_pct)
    {
        _connection = new SqlConnection(_ConnectionString);


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
            pFName.Value = first_name;
            sqlCommand.Parameters.Add(pFName);

            SqlParameter pLName = new SqlParameter();
            pLName.ParameterName = "@last_name";
            pLName.SqlDbType = System.Data.SqlDbType.Int;
            pLName.Value = last_name;
            sqlCommand.Parameters.Add(pLName);

            SqlParameter pEmail = new SqlParameter();
            pEmail.ParameterName = "@email";
            pEmail.SqlDbType = System.Data.SqlDbType.VarChar;
            pEmail.Value = email;
            sqlCommand.Parameters.Add(pEmail);

            SqlParameter pPhoneNumber = new SqlParameter();
            pPhoneNumber.ParameterName = "@phone_number";
            pPhoneNumber.SqlDbType = System.Data.SqlDbType.VarChar;
            pPhoneNumber.Value = phone_number;
            sqlCommand.Parameters.Add(pPhoneNumber);

            SqlParameter pHireDate = new SqlParameter();
            pHireDate.ParameterName = "@hire_date";
            pHireDate.SqlDbType = System.Data.SqlDbType.DateTime;
            pHireDate.Value = hire_date;
            sqlCommand.Parameters.Add(pHireDate);

            SqlParameter pSalary = new SqlParameter();
            pSalary.ParameterName = "@salary";
            pSalary.SqlDbType = System.Data.SqlDbType.Int;
            pSalary.Value = salary;
            sqlCommand.Parameters.Add(pSalary);

            SqlParameter pComissionPCT = new SqlParameter();
            pComissionPCT.ParameterName = "@comission_pct";
            pComissionPCT.SqlDbType = System.Data.SqlDbType.Decimal;
            pComissionPCT.Value = comission_pct;
            sqlCommand.Parameters.Add(pComissionPCT);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Berhasil");
            }
            else
            {
                Console.WriteLine("Gagal");
            }

            transaction.Commit();
            _connection.Close();

        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Tidak ada Regions");
        }


    }

    public static void InsertIntoEmployees()
    {
        string inputFirstName = Console.ReadLine();
        Console.WriteLine("Tambah First Name: ");
        string inputLastName = Console.ReadLine();
        Console.WriteLine("Tambah Last Name: ");
        string inputEmail = Console.ReadLine();
        Console.WriteLine("Tambah Email: ");
        string inputPhone = Console.ReadLine();
        Console.WriteLine("Tambah Nomor HP: ");
        DateTime inputHireDate = DateValidation();
        Console.Write("Tambah Hire Date: ");
        int inputSalary = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Tambah Salary: ");
        decimal inputComission = decimal.Parse(Console.ReadLine());
        Console.WriteLine("Tambah Commision PCT: ");
        InsertEmployees(inputFirstName, inputLastName, inputEmail, inputPhone, inputHireDate, inputSalary, inputComission);
    }

    public static void UpdateEmployees(int employee_id, string first_name, string last_name, string email, string phone_number, DateTime hire_date, int salary, decimal comission_pct)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "UPDATE employees SET first_name = @first_name, last_name = @last_name, email = @email, phone_number = @phone_number, hire_date = @hire_date, salary = @salary, comission_pct = @comission_pct WHERE employee_id = @employee_id";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pEmployeeId = new SqlParameter();
            pEmployeeId.ParameterName = "@employee_id";
            pEmployeeId.SqlDbType = System.Data.SqlDbType.Int;
            pEmployeeId.Value = employee_id;
            sqlCommand.Parameters.Add(employee_id);

            SqlParameter pFName = new SqlParameter();
            pFName.ParameterName = "@first_name";
            pFName.SqlDbType = System.Data.SqlDbType.VarChar;
            pFName.Value = first_name;
            sqlCommand.Parameters.Add(pFName);

            SqlParameter pLName = new SqlParameter();
            pLName.ParameterName = "@last_name";
            pLName.SqlDbType = System.Data.SqlDbType.Int;
            pLName.Value = last_name;
            sqlCommand.Parameters.Add(pLName);

            SqlParameter pEmail = new SqlParameter();
            pEmail.ParameterName = "@email";
            pEmail.SqlDbType = System.Data.SqlDbType.VarChar;
            pEmail.Value = email;
            sqlCommand.Parameters.Add(pEmail);

            SqlParameter pPhoneNumber = new SqlParameter();
            pPhoneNumber.ParameterName = "@phone_number";
            pPhoneNumber.SqlDbType = System.Data.SqlDbType.VarChar;
            pPhoneNumber.Value = phone_number;
            sqlCommand.Parameters.Add(pPhoneNumber);

            SqlParameter pHireDate = new SqlParameter();
            pHireDate.ParameterName = "@hire_date";
            pHireDate.SqlDbType = System.Data.SqlDbType.DateTime;
            pHireDate.Value = hire_date;
            sqlCommand.Parameters.Add(pHireDate);

            SqlParameter pSalary = new SqlParameter();
            pSalary.ParameterName = "@salary";
            pSalary.SqlDbType = System.Data.SqlDbType.Int;
            pSalary.Value = salary;
            sqlCommand.Parameters.Add(pSalary);

            SqlParameter pComissionPCT = new SqlParameter();
            pComissionPCT.ParameterName = "@comission_pct";
            pComissionPCT.SqlDbType = System.Data.SqlDbType.Decimal;
            pComissionPCT.Value = comission_pct;
            sqlCommand.Parameters.Add(pComissionPCT);


            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Update Success");
            }
            else
            {
                Console.WriteLine("Update Fail");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to the database");
        }
    }

    public static void UpdateIntoEmployees()
    {
        int inputEmployeeId = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Update Id");
        string inputFirstName = Console.ReadLine();
        Console.WriteLine("Tambah First Name: ");
        string inputLastName = Console.ReadLine();
        Console.WriteLine("Tambah Last Name: ");
        string inputEmail = Console.ReadLine();
        Console.WriteLine("Tambah Email: ");
        string inputPhone = Console.ReadLine();
        Console.WriteLine("Tambah Nomor HP: ");
        DateTime inputHireDate = DateValidation();
        Console.Write("Tambah Hire Date: ");
        int inputSalary = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Tambah Salary: ");
        decimal inputComission = decimal.Parse(Console.ReadLine());
        Console.WriteLine("Tambah Commision PCT: ");
        UpdateEmployees(inputEmployeeId, inputFirstName, inputLastName, inputEmail, inputPhone, inputHireDate, inputSalary, inputComission);

    }

    public static void DeleteEmployee(int employee_id)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "DELETE FROM employees WHERE employee_id = @employee_id";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pEmployeeId = new SqlParameter();
            pEmployeeId.ParameterName = "@employee_id";
            pEmployeeId.SqlDbType = System.Data.SqlDbType.Int;
            pEmployeeId.Value = employee_id;
            sqlCommand.Parameters.Add(pEmployeeId);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Delete Success");
            }
            else
            {
                Console.WriteLine("Delete Fail");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to the database");
        }
    }

    public static void DeleteByEmployee()
    {
        int inputId = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Hapus Employee Id: ");
        DeleteEmployee(inputId);
    }

    public static void GetEmployeeById(int employee_id)
    {
        _connection = new SqlConnection(_ConnectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM employees WHERE employee_id = @employee_id";
        sqlCommand.Parameters.AddWithValue("@employee_id", employee_id);

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string phone_number = reader.IsDBNull(4) ? "N/A" : reader.GetString(4);
                    int salary = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                    decimal comission_pct = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7);

                    Console.WriteLine("ID: " + reader.GetInt32(0));
                    Console.WriteLine("First Name: " + reader.GetString(1));
                    Console.WriteLine("Last Name: " + reader.GetString(2));
                    Console.WriteLine("Email: " + reader.GetString(3));
                    Console.WriteLine("Phone Number: " + phone_number);
                    Console.WriteLine("Hire Date: " + reader.GetDateTime(5));
                    Console.WriteLine("Salary: " + salary);
                    Console.WriteLine("Comission PCT: " + comission_pct);
                }
            }
            else
            {
                Console.WriteLine("tidak ada Employee");
            }
            reader.Close();
            _connection.Close();
        }
        catch
        {
            Console.WriteLine("Error");
        }

    }

    public static void SearchEmployeeById()
    {
        int inputId = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Cari Employee Id: ");
        GetEmployeeById(inputId);
    }

    public static void MenuEmployees()
    {
        Console.WriteLine("Basic Authentication Geri Marizki");
        Console.WriteLine("Pilih menu untuk masuk ke menunya");
        Console.WriteLine("1. Tambah Employee");
        Console.WriteLine("2. Update Employee");
        Console.WriteLine("3. Hapus Employee");
        Console.WriteLine("4. Search By Id Employee");
        Console.WriteLine("5. Get All Employee");
        Console.WriteLine("6. Main Menu");
        Console.WriteLine("Pilih: ");

        try
        {
            int pilihMenu = Int32.Parse(Console.ReadLine());

            switch (pilihMenu)
            {

                case 1:
                    Console.WriteLine("1. Tambah Employee");
                    Console.Clear();
                    InsertIntoEmployees();
                    MenuEmployees();
                    break;
                case 2:
                    Console.WriteLine("2. Update Employee");
                    Console.Clear();
                    UpdateIntoEmployees();
                    MenuEmployees();
                    break;
                case 3:
                    Console.WriteLine("3. Hapus Employee");
                    Console.Clear();
                    DeleteByEmployee();
                    MenuEmployees();
                    break;
                case 4:
                    Console.WriteLine("4. Search By Employee");
                    Console.Clear();
                    SearchEmployeeById();
                    MenuEmployees();
                    break;
                case 5:
                    Console.WriteLine("5. Get All Employees");
                    GetEmployees();
                    MenuEmployees();
                    break;
                case 6:
                    Console.WriteLine("MainMenu");
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Silahkan Pilih Nomor 1-6");
                    MenuEmployees();
                    break;
            }
        }
        catch
        {
            Console.WriteLine("Input Hanya diantara 1-6!");
            MenuEmployees();
        }
    }

}