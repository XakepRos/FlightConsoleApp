using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flight_pgm
{
    class DbPlay
    {
        public static void DBplay()
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
                builder.InitialCatalog = "DbPlay";
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

                    String sql = "IF OBJECT_ID('dbo.FlightDetails', 'U') IS NOT NULL DROP TABLE dbo.FlightDetails; IF OBJECT_ID('dbo.CUSTOMERS', 'U') IS NOT NULL DROP TABLE dbo.CUSTOMERS;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.Write("FlightDetails & CUSTOMERS table  are already exists, press any key to DROP TABLE...\n");
                        Console.ReadKey(true);
                        Console.WriteLine("Done.");
                    }
                    
                    StringBuilder sb = new StringBuilder();
                    sb.Append("USE DBplay; ");
                    sb.Append("CREATE TABLE FlightDetails ( ");
                    sb.Append("ID INT NOT NULL PRIMARY KEY, ");
                    sb.Append("Flight_Number INT NOT NULL, ");
                    sb.Append("City_Name NVARCHAR(50), ");
                    sb.Append("Flight_Distance INT NOT NULL, ");
                    sb.Append("Flight_Price DECIMAL(10,3), ");
                    sb.Append("Discount_Price DECIMAL(10,3) ");
                    sb.Append("); ");
                   
                    sb.Append("USE DBplay; ");
                    sb.Append("CREATE TABLE CUSTOMERS ( ");
                    sb.Append("ID   INT             NOT NULL, ");
                    sb.Append("NAME VARCHAR (20)    NOT NULL, ");
                    sb.Append("AGE  INT             NOT NULL, ");
                    sb.Append("ADDRESS  CHAR (25), ");
                    sb.Append("SALARY   DECIMAL (18, 2),  ");
                    sb.Append("PRIMARY KEY (ID) ");
                    sb.Append("); ");


                    Console.Write("Table Created, press any key to Insert Data into Table ...\n");
                    Console.ReadKey(true);

                    sb.Append("INSERT INTO FlightDetails(ID, Flight_Number, City_Name, Flight_Distance, Flight_Price, Discount_Price) VALUES ");
                    sb.Append("(1, 1500, 'Jnk', 100, 500, 20);");
                    sb.Append("INSERT INTO FlightDetails(ID, Flight_Number, City_Name, Flight_Distance, Flight_Price, Discount_Price) VALUES ");
                    sb.Append("(2, 1600, 'Ktm', 200, 600, 20);");
                    sb.Append("INSERT INTO FlightDetails(ID, Flight_Number, City_Name, Flight_Distance, Flight_Price, Discount_Price) VALUES ");
                    sb.Append("(3, 1700, 'brj', 300, 700, 20)");

                    sb.Append("INSERT INTO CUSTOMERS (ID,NAME,AGE,ADDRESS,SALARY) VALUES (1, 'Ramesh', 32, 'Ahmedabad', 2000.00 );");
                    sb.Append("INSERT INTO CUSTOMERS (ID,NAME,AGE,ADDRESS,SALARY) VALUES (2, 'Khilan', 25, 'Delhi', 1500.00); ");
                    sb.Append("INSERT INTO CUSTOMERS (ID,NAME,AGE,ADDRESS,SALARY) VALUES (3, 'kaushi', 23, 'Kota', 2000.00);");
                    sb.Append("INSERT INTO CUSTOMERS (ID,NAME,AGE,ADDRESS,SALARY) VALUES (4, 'Chaita', 25, 'Mumbai', 6500.00  );");
                    sb.Append("INSERT INTO CUSTOMERS (ID,NAME,AGE,ADDRESS,SALARY) VALUES (5, 'Hardik', 27, 'Bhopal', 8500.00);");
                    sb.Append("INSERT INTO CUSTOMERS (ID,NAME,AGE,ADDRESS,SALARY) VALUES (6, 'Komali', 22, 'MP', 4500.00  );");

                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Done.");
                    }
                    Console.Write("Data Inserted, press any key to Read Data from Table ...\n");
                    Console.ReadKey(true);


                    // INSERT demo : FlightDetails
                    Console.Write("Inserting a new row into table, press any key to continue...");
                    Console.ReadKey(true);
                    sb.Clear();
                    sb.Append("INSERT INTO FlightDetails (ID, Flight_Number, City_Name, Flight_Distance, Flight_Price, Discount_Price) ");
                    sb.Append("VALUES (@ID, @Flight_Number, @City_Name, @Flight_Distance, @Flight_Price, @Discount_Price);");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", 4);
                        command.Parameters.AddWithValue("@Flight_Number", 1800);
                        command.Parameters.AddWithValue("@City_Name", "Bang");
                        command.Parameters.AddWithValue("@Flight_Distance", 1800);
                        command.Parameters.AddWithValue("@Flight_Price", 800);
                        command.Parameters.AddWithValue("@Discount_Price", 80);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row(s) inserted");
                    }

                    // INSERT demo : CUSTOMERS
                    Console.Write("Inserting a new row into table, press any key to continue...");
                    Console.ReadKey(true);
                    sb.Clear();
                    sb.Append("INSERT INTO CUSTOMERS (ID, NAME, AGE, ADDRESS, SALARY)");
                    sb.Append("VALUES (@ID, @NAME, @AGE, @ADDRESS, @SALARY);");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", 7);
                        command.Parameters.AddWithValue("@NAME", "Patelu");
                        command.Parameters.AddWithValue("@AGE", 60);
                        command.Parameters.AddWithValue("@ADDRESS", "TamilNadu");
                        command.Parameters.AddWithValue("@SALARY", 8000);                     
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row(s) inserted");
                    }

                    // UPDATE demo : CUSTOMERS
                    String userToUpdate = "Komali";
                    Console.Write("Updating 'ADDRESS' for user '" + userToUpdate + "', press any key to continue...");
                    Console.ReadKey(true);
                    sb.Clear();
                    sb.Append("UPDATE CUSTOMERS SET ADDRESS = N'Birgunj' WHERE Name = @name");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userToUpdate);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row(s) updated");
                    }
                    // UPDATE demo : CUSTOMERS
                    String userToUpdate1 = "Komali";
                    Console.Write("Updating 'ADDRESS' for user '" + userToUpdate1 + "', press any key to continue...");
                    Console.ReadKey(true);
                    sb.Clear();
                    sb.Append("UPDATE CUSTOMERS SET SALARY = '50000' WHERE Name = @name");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userToUpdate);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row(s) updated");
                    }

                    // DELETE demo : CUSTOMERS
                    String userToDelete = "Patelu";
                    Console.Write("Deleting user '" + userToDelete + "', press any key to continue...");
                    Console.ReadKey(true);
                    sb.Clear();
                    sb.Append("DELETE FROM CUSTOMERS WHERE Name = @name;");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userToDelete);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row(s) deleted");
                    }

                    //READ demo : FlightDetails
                    Console.WriteLine("Reading data from table, press any key to continue...\n");
                    Console.ReadKey(true);
                    sql = "SELECT * FROM FlightDetails";                    
                    using (SqlCommand command1 = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            Console.WriteLine("TABLE 1 : FlightDetails");
                            Console.WriteLine("ID  FlightNumber  CityName   FlightDistance   FlightPrice  PriceDiscount");
                            while (reader.Read())
                            {
                                Console.WriteLine("{0}\t{1}\t {2} \t\t{3}\t\t{4}\t\t{5}", reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3), reader.GetDecimal(4), reader.GetDecimal(5));
                            }
                        }
                    }

                    //READ demo : CUSTOMERS
                    sql = "SELECT * FROM CUSTOMERS";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {                         
                            Console.WriteLine("\nTABLE 2 : CUSTOMERS");
                            Console.WriteLine("ID \t NAME  \t  AGE \t ADDRESS \t   \t SALARY");
                            while (reader.Read())
                            {
                                //var r = reader.GetDecimal(4).ToString();
                                Console.WriteLine("{0}\t {1}   {2} \t {3}{4}", reader.GetInt32(0).ToString(), reader.GetString(1).ToString(), reader.GetInt32(2).ToString(), reader.GetString(3).ToString(), reader.GetDecimal(4).ToString());
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
