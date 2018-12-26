using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flight_pgm
{
    public class DbUser
    {
        public static void userconn(List<flightProperties> flightdetail)
        {
            try
            {
                Console.WriteLine("Connect to SQL Server and perform Create, Read, Update and Delete operations :");

                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                //string a = ".sqlexpress";
                builder.DataSource = @"XAKEP\SQLEXPRESS";   // update me
                //builder.UserID = "sa";                              // update me
                //builder.Password = "your_password";                 // update me
                builder.InitialCatalog = "FlightDb";
                builder.IntegratedSecurity = true;
                // Connect to SQL
                Console.Write("Connecting to SQL Server ....\n");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");

                    //string sql = null;
                    // Create a Table and insert some sample data
                    Console.Write("Creating FlightDetails table, press any key to continue...\n");
                    Console.ReadKey(true);
                    //string sql = null;

                    String sql = "IF OBJECT_ID('dbo.FlightDetails', 'U') IS NOT NULL DROP TABLE dbo.FlightDetails;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.Write("FlightDetails table already exists, press any key to DROP TABLE...\n");
                        Console.ReadKey(true);
                        Console.WriteLine("Done.");
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.Append("USE FlightDb; ");
                    sb.Append("CREATE TABLE FlightDetails ( ");
                    sb.Append("ID INT IDENTITY NOT NULL PRIMARY KEY, ");
                    sb.Append("FlightNumber INT NOT NULL, ");
                    sb.Append("CityName NVARCHAR(50), ");
                    sb.Append("FlightDistance INT NOT NULL, ");
                    sb.Append("FlightPrice DECIMAL(10,1), ");
                    sb.Append("DiscountPrice DECIMAL(10,1) ");
                    sb.Append("); ");

                    Console.Write("Table Created, press any key to Insert Data into Table ...\n");
                    Console.ReadKey(true);

                    //flightProperties userdata = new flightProperties();
                    //flightProperties userdata = new flightProperties();
                    foreach (var items in flightdetail)
                    {
                        sb.Append(string.Format("INSERT INTO FlightDetails(FlightNumber, CityName, FlightDistance, FlightPrice, DiscountPrice) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');", items.FlightNumber, items.FlightCity, items.FlightDistance, items.FlightPrice, items.DiscountPrice));
                        //sb.CommandType = CommandType.Text;
                        //sb.Connection = conn;
                    }
                                                                                                  

                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Done.");
                    }
                    Console.Write("Data Inserted, press any key to Read Data from Table ...\n");
                    Console.ReadKey(true);

                    // READ demo
                    Console.WriteLine("Reading data from table, press any key to continue...\n");
                    Console.ReadKey(true);
                    Console.WriteLine("ID  FlightNumber  CityName   FlightDistance   FlightPrice  PriceDiscount");
                    sql = "SELECT * FROM FlightDetails";
                    using (SqlCommand command1 = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0}\t{1}\t   {2} \t\t{3}\t\t{4}\t\t{5}", reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3), reader.GetDecimal(4), reader.GetDecimal(5));
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
