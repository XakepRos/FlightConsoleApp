using System;
using System.Text;
using System.Data.SqlClient;

namespace FlightDatabase
{
    class DatabaseConnection
    {
        public static void dbconn()
        {
            try
            {
                Console.WriteLine("Connect to SQL Server and perform Create, Read, Update and Delete operations :");

                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                //string a = ".sqlexpress";
                builder.DataSource = @"XAKEP\SQLEXPRESS";   // update me
                                                                      // builder.UserID = "sa";              // update me
                                                                      // builder.Password = "your_password";      // update me
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
                    sb.Append("ID INT NOT NULL PRIMARY KEY, ");
                    sb.Append("Flight_Number INT NOT NULL, ");
                    sb.Append("City_Name NVARCHAR(50), ");
                    sb.Append("Flight_Distance INT NOT NULL, ");
                    sb.Append("Flight_Price DECIMAL(10,3), ");
                    sb.Append("Discount_Price DECIMAL(10,3) ");
                    sb.Append("); ");

                    Console.Write("Table Created, press any key to Insert Data into Table ...\n");
                    Console.ReadKey(true);

                    sb.Append("INSERT INTO FlightDetails(ID, Flight_Number, City_Name, Flight_Distance, Flight_Price, Discount_Price) VALUES ");
                    sb.Append("(1, 1500, 'Jnk', 100, 500, 20);");
                    sb.Append("INSERT INTO FlightDetails(ID, Flight_Number, City_Name, Flight_Distance, Flight_Price, Discount_Price) VALUES ");
                    sb.Append("(2, 1600, 'Ktm', 200, 600, 20);");
                    sb.Append("INSERT INTO FlightDetails(ID, Flight_Number, City_Name, Flight_Distance, Flight_Price, Discount_Price) VALUES ");
                    sb.Append("(3, 1700, 'brj', 300, 700, 20)");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Done.");
                    }
                    Console.Write("Data Inserted, press any key to Read Data from Table ...\n");
                    Console.ReadKey(true);

                    // INSERT demo
                    //Console.Write("Inserting a new row into table, press any key to continue...");
                    //Console.ReadKey(true);
                    //sb.Clear();
                    //sb.Append("INSERT INTO FlightDetails (ID, Flight_Number, City_Name, Flight_Distance, Flight_Price, Discount_Price) ");
                    //sb.Append("VALUES (@ID, @Flight_Number, @City_Name, @Flight_Distance, @Flight_Price, @Discount_Price);");
                    //sql = sb.ToString();
                    //using (SqlCommand command = new SqlCommand(sql, connection))
                    //{
                    //    command.Parameters.AddWithValue("@ID", 1);
                    //    command.Parameters.AddWithValue("@Flight_Number", 1500);
                    //    command.Parameters.AddWithValue("@City_Name", "Janakpur");
                    //    command.Parameters.AddWithValue("@Flight_Distance", 100);
                    //    command.Parameters.AddWithValue("@Flight_Price", 500);
                    //    command.Parameters.AddWithValue("@Discount_Price", 10);
                    //    int rowsAffected = command.ExecuteNonQuery();
                    //    Console.WriteLine(rowsAffected + " row(s) inserted");
                    //}

                    //// UPDATE demo
                    //String userToUpdate = "Nikita";
                    //Console.Write("Updating 'Location' for user '" + userToUpdate + "', press any key to continue...");
                    //Console.ReadKey(true);
                    //sb.Clear();
                    //sb.Append("UPDATE Employees SET Location = N'United States' WHERE Name = @name");
                    //sql = sb.ToString();
                    //using (SqlCommand command = new SqlCommand(sql, connection))
                    //{
                    //    command.Parameters.AddWithValue("@name", userToUpdate);
                    //    int rowsAffected = command.ExecuteNonQuery();
                    //    Console.WriteLine(rowsAffected + " row(s) updated");
                    //}

                    //// DELETE demo
                    //String userToDelete = "Jared";
                    //Console.Write("Deleting user '" + userToDelete + "', press any key to continue...");
                    //Console.ReadKey(true);
                    //sb.Clear();
                    //sb.Append("DELETE FROM Employees WHERE Name = @name;");
                    //sql = sb.ToString();
                    //using (SqlCommand command = new SqlCommand(sql, connection))
                    //{
                    //    command.Parameters.AddWithValue("@name", userToDelete);
                    //    int rowsAffected = command.ExecuteNonQuery();
                    //    Console.WriteLine(rowsAffected + " row(s) deleted");
                    //}

                    // READ demo
                    Console.WriteLine("Reading data from table, press any key to continue...\n");
                    Console.ReadKey(true);
                    sql = "SELECT * FROM FlightDetails";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1} {2} {3} {4} {5}", reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3), reader.GetDecimal(4), reader.GetDecimal(5));
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

        //public static void dbconn()
        //{
        //    try
        //    {
        //        // Build connection string
        //        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        //        builder.DataSource = @"DESKTOP-V7JJNRK\SQLEXPRESS";   // update me
        //                                                              //builder.UserID = "sa";              // update me
        //                                                              //builder.Password = "your_password";      // update me

        //        builder.InitialCatalog = "Xakep";
        //        builder.IntegratedSecurity = true;

        //        // Connect to SQL
        //        Console.Write("Connecting to SQL Server ... ");
        //        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        //        {
        //            connection.Open();
        //            Console.WriteLine("Done.");
        //        }
        //    }

        //    catch (SqlException e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }

        //    Console.WriteLine("All done. Press any key to finish...");
        //    Console.ReadKey(true);
        //}
    }
}