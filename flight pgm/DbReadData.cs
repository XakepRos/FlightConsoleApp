using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flight_pgm
{
    class DbReadData
    {
        public static void DbRead()
        {
            try
            {
                Console.WriteLine("Connect to SQL Server and perform Create, Read, Update and Delete operations :");

                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                //string a = ".sqlexpress";
                builder.DataSource = @"XAKEP\SQLEXPRESS";   // update me
                // builder.UserID = "sa";                             // update me
                // builder.Password = "your_password";                // update me
                builder.InitialCatalog = "FlightDb";
                builder.IntegratedSecurity = true;
                // Connect to SQL
                Console.Write("Connecting to SQL Server ....\n");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");

                    // Create a sample database
                    //Console.Write("Dropping and creating database 'FlightDb' ... ");
                    //String sql = "IF EXISTS (SELECT 1 FROM sys.databases WHERE NAME = 'FlightDb') DROP DATABASE [FlightDb]; CREATE DATABASE [FlightDb]";
                    //using (SqlCommand command = new SqlCommand(sql, connection))
                    //{
                    //    command.ExecuteNonQuery();
                    //    Console.WriteLine("Done.");
                    //}
                    //string sql = null;


                    string sql = null;
                    // READ demo
                    Console.WriteLine("Reading data from table, press any key to continue...\n");
                    Console.ReadKey(true);
                    sql = "SELECT * FROM CUSTOMERS";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("ID \t NAME \t AGE \t ADDRESS \t\t SALARY");
                            while (reader.Read())
                            {
                                //var r = reader.GetDecimal(4).ToString();
                                Console.WriteLine("{0}\t{1}  {2}\t{3}{4}", reader.GetInt32(0).ToString(), reader.GetString(1).ToString(), reader.GetInt32(2).ToString(), reader.GetString(3).ToString(), reader.GetDecimal(4).ToString());
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("All done. Press any key to finish...\n");
            Console.ReadKey(true);
        }
    }
}
