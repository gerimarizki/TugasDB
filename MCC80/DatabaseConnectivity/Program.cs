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
        //_connection = new SqlConnection(_ConnectionString);

        //try
        //{
        //    _connection.Open();
        //    Console.WriteLine("Koneksi berhasil");
        //    _connection.Close();
        //}
        //catch
        //{
        //    Console.WriteLine("gagal");
        //}

        MenuRegion();
    }

    #region
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

    public static void SearchById()
    {

    }
    #endregion



    public static void MenuRegion()
    {
        Console.WriteLine("Basic Authentication Geri Marizki");
        Console.WriteLine("Pilih menu untuk masuk ke menunya");
        Console.WriteLine("1. Tambah Region");
        Console.WriteLine("2. Update Region");
        Console.WriteLine("3. Hapus Region");
        Console.WriteLine("4. Search By Id Region");
        Console.WriteLine("5. Get All Regions");
        Console.WriteLine("6. Exit");
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
                    Console.WriteLine("4. Search By Region");
                    Console.Clear();

                    MenuRegion();
                    break;
                case 5:
                    Console.WriteLine("5. Get All Regions");
                    GetRegions();
                    break;
                case 6:
                    Console.WriteLine("Exit");
                    Environment.Exit(0);
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
                    Console.WriteLine("Id: " + reader.GetString(1));
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
        Console.WriteLine("Hapus region Id: ");
        DeleteCountry(inputId);
    }

    public static void SearchByIdCountry()
    {

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
        Console.WriteLine("6. Exit");
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
                    MenuRegion();
                    break;
                case 2:
                    Console.WriteLine("2. Update Country");
                    Console.Clear();
                    UpdateIntoCountry();
                    MenuRegion();
                    break;
                case 3:
                    Console.WriteLine("3. Hapus Country");
                    Console.Clear();
                    DeleteByCountry();
                    MenuRegion();
                    break;
                case 4:
                    Console.WriteLine("4. Search By Country");
                    Console.Clear();

                    MenuRegion();
                    break;
                case 5:
                    Console.WriteLine("5. Get All Country");
                    GetCountry();
                    break;
                case 6:
                    Console.WriteLine("Exit");
                    Environment.Exit(0);
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