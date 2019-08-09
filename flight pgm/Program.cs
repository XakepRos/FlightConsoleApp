using FlightDatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flight_pgm
{
    public class flightDetails
    {
        static int N = 2;
        static List<flightProperties> detail = new List<flightProperties>();
        
        public static void Main(string[] args)
        {
            Console.WriteLine("=========================================================================");
            Console.WriteLine("||                       Flight Information System                     ||");
            Console.WriteLine("=========================================================================");
            while (true)
            {
                menu();
                try
                {
                    var result = Console.ReadLine();
                    int results = Convert.ToInt32(result);
                    int choice = results;
                    if (choice >= 1 && choice <= 12)
                    {
                        switch (choice)
                        {
                            case 1:
                                flightInputs();
                                break;
                            case 2:
                                calculateDiscount();
                                break;
                            case 3:
                                displayAllRecords();
                                break;
                            case 4:
                                searchFlight();
                                break;
                            case 5:
                                lowestFlightPrice();
                                break;
                            case 6:
                                sortcityName();
                                break;
                            case 7:
                                textFile();
                                break;
                            case 8:
                                DbUserconn();
                                break;                         
                            case 9:
                                SqlConnection();
                                break;
                            case 10:
                                ReadDb();
                                break;
                            case 11:
                                DBPLAY();
                                break;
                            case 12:
                                Environment.Exit(choice);
                                break;
                            default:
                                return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n!! Please Enter Correct Choice From Menu List !!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }               
            }
        }

        public static void menu()
        {
            Console.WriteLine("\n*************************************************************************");
            Console.WriteLine("$                                MENU                                  $");
            Console.WriteLine("*************************************************************************");
            Console.WriteLine("** 1. Read, validate and store discounted price for all flights        **");
            Console.WriteLine("** 2. Calculate and store discounted price for all flights             **");
            Console.WriteLine("** 3. Display all flights                                              **");
            Console.WriteLine("** 4. Search a flight by number                                        **");
            Console.WriteLine("** 5. Display all flights with the lowest flight price                 **");
            Console.WriteLine("** 6. Sort and Display sorted flights                                  **");
            Console.WriteLine("** 7. Create Text File, Add Data & Display Data                        **");          
            Console.WriteLine("** 8. Connect to SQL Server : Retrive User Input Data                  **");
            Console.WriteLine("** 9. Connect to SQL Server : Create,Read,Update and Delete operations **");
            Console.WriteLine("** 10.Read all Data directly from Database                             **");
            Console.WriteLine("** 11.Play with Database from code                                     **");
            Console.WriteLine("** 12.Exit from the application                                        **");
            Console.WriteLine("*************************************************************************");
            Console.Write("\nSelect your choice from Menu [1-12] : ");
        }

        public static void flightInputs()
        {

            for (int i = 0; i < N; i++)
            {
                float Price;
                Console.WriteLine("\nEnter the following details for the flight: " + (i + 1));
                flightProperties data = new flightProperties();

                Console.Write("Flight Departure City: ");
                // var city = Console.ReadLine();   // set{}
                data.FlightCity = Console.ReadLine(); //city;

                Console.Write("Flight Number: ");
                //var number = Console.ReadLine();
                data.FlightNumber = int.Parse(Console.ReadLine()); //( number);
                while (data.FlightNumber < 1111 || data.FlightNumber > 9999)
                {
                    Console.WriteLine("Error !");
                    Console.WriteLine("Flight number should be in between 1111 and 9999.");
                    Console.Write("Enter a new Flight Number :");
                    data.FlightNumber = int.Parse(Console.ReadLine());   // set{}
                    //Console.WriteLine(data.FlightNumber);    //get{}                   
                }

                Console.Write("Flight Distance : ");
                data.FlightDistance = Convert.ToInt32(Console.ReadLine());   // set{}                      
                //Console.WriteLine(data.FlightDistance);    //get{}  

                Console.Write("Flight Price: ");
                data.FlightPrice = Convert.ToInt32(Console.ReadLine());
                while (data.FlightPrice < 9 || data.FlightPrice > 900)
                {
                    Console.WriteLine("Flight price should be in between 9 and 900.");
                    Console.Write("Enter a new Price :");
                    data.FlightPrice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(data.FlightPrice);
                }
                detail.Add(data);
            }
        }
        
        public static void calculateDiscount()
        {
            float discountpercentage;
            Console.Write("\nEnter the discount percentage between 0 and 100 :");
            discountpercentage = Convert.ToInt32(Console.ReadLine());
            foreach (flightProperties value in detail)
            {
                value.DiscountPrice = (value.FlightPrice - ((value.FlightPrice * discountpercentage) / 100));
            }
        }

        public static void displayAllRecords()
        {
            Console.WriteLine("Flight Number  City Name   Flight Distance   Flight Price  Price Discount");
            foreach (var item in detail)
            {
                Console.WriteLine(item.FlightNumber + "\t\t" + item.FlightCity + "\t\t" + item.FlightDistance + "\t\t" + item.FlightPrice + "\t\t" + item.DiscountPrice);
            }
        }

        public static void searchFlight()
        {
            try
            {
                int flightno;
                Console.WriteLine("Search flight details according to Flight Number :");
                flightno = Convert.ToInt32(Console.ReadLine());
                foreach (flightProperties item in detail)
                {
                    if (flightno == item.FlightNumber)
                    {
                        Console.WriteLine("Flight Number  City Name   Flight Distance   Flight Price  Price Discount");
                        Console.WriteLine(item.FlightNumber + "\t\t" + item.FlightCity + "\t\t" + item.FlightDistance + "\t\t" + item.FlightPrice + "\t\t" + item.DiscountPrice);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }

        public static void lowestFlightPrice()
        {
            try
            {
                Console.WriteLine("\nAll Flight details Sorted By Lowest Flight Price :");
                Console.WriteLine("Flight Number  City Name   Flight Distance   Flight Price  Price Discount");
                float min;
                foreach (flightProperties item in detail)
                {
                    foreach (flightProperties items in detail)
                    {

                        if (item.FlightPrice < items.FlightPrice)
                        {
                            min = Math.Min(item.FlightPrice, items.FlightPrice);
                            Console.WriteLine(item.FlightNumber + "\t\t" + item.FlightCity + "\t\t" + item.FlightDistance + "\t\t" + item.FlightPrice + "\t\t" + item.DiscountPrice);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void sortcityName()
        {
            try
            {
                Console.WriteLine("\nAll Flight details Sorted By Departure City Name in descending order :");
                Console.WriteLine("Flight Number  City Name   Flight Distance   Flight Price  Price Discount");

                foreach (flightProperties item in detail)
                {
                    foreach (flightProperties items in detail)
                    {
                        if (item.FlightCity.CompareTo(items.FlightCity) == 0)
                        {
                            Console.WriteLine(item.FlightNumber + "\t\t" + item.FlightCity + "\t\t" + item.FlightDistance + "\t\t" + item.FlightPrice + "\t\t" + item.DiscountPrice);
                        }


                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void textFile()
        {           
            string fileName = @"G:\Raushan\flight pgm\flight pgm\flightdata.txt";
            int count = -1;
            try
            {              
                // Check if file already exists. If yes, delete it. 
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                // Create a new file 
                using (FileStream fs = File.Create(fileName))
                {
                    //Add some text to file                   
                    Byte[] title1 = new UTF8Encoding(true).GetBytes(" FlightNumber   : ");
                    fs.Write(title1, 0 , title1.Length);
                    foreach (flightProperties items in detail)
                    {                        
                        Byte[] title6 = new UTF8Encoding(true).GetBytes(items.FlightNumber.ToString()+((count==detail.Count)?"":" : "));
                        fs.Write(title6, 0, title6.Length);
                        count++;
                    }
                    Byte[] title2 = new UTF8Encoding(true).GetBytes("\n CityName       : ");
                    fs.Write(title2, 0, title2.Length);
                    foreach (flightProperties items in detail)
                    {
                        Byte[] title7 = new UTF8Encoding(true).GetBytes(items.FlightCity+((count == detail.Count) ? "" : " : "));
                        fs.Write(title7, 0, title7.Length);
                    }
                    Byte[] title3 = new UTF8Encoding(true).GetBytes("\n FlightDistance : ");
                    fs.Write(title3, 0, title3.Length);
                    foreach (flightProperties items in detail)
                    {
                        Byte[] title8 = new UTF8Encoding(true).GetBytes(items.FlightDistance.ToString() + ((count == detail.Count) ? "" : " : "));
                        fs.Write(title8, 0, title8.Length);
                    }
                    Byte[] title4 = new UTF8Encoding(true).GetBytes("\n FlightPrice    : ");
                    fs.Write(title4, 0, title4.Length);
                    foreach (flightProperties items in detail)
                    {
                        Byte[] title9 = new UTF8Encoding(true).GetBytes(items.FlightPrice.ToString() + ((count == detail.Count) ? "" : " : "));
                        fs.Write(title9, 0, title9.Length);
                    }
                    Byte[] title5 = new UTF8Encoding(true).GetBytes("\n PriceDiscount  : ");
                    fs.Write(title5, 0, title5.Length);
                    foreach (flightProperties items in detail)
                    {
                        Byte[] title10 = new UTF8Encoding(true).GetBytes(items.DiscountPrice.ToString() + ((count == detail.Count) ? "" : " : "));
                        fs.Write(title10, 0, title10.Length);
                    }
                    //Byte[] title1 = new UTF8Encoding(true).GetBytes(" FlightNumber");
                    //fs.Write(title1, 0, title1.Length);
                    //Byte[] title2 = new UTF8Encoding(true).GetBytes("\tCityName");
                    //fs.Write(title2, 0, title2.Length);
                    //Byte[] title3 = new UTF8Encoding(true).GetBytes("  FlightDistance");
                    //fs.Write(title3, 0, title3.Length);
                    //Byte[] title4 = new UTF8Encoding(true).GetBytes("  FlightPrice");
                    //fs.Write(title4, 0, title4.Length);
                    //Byte[] title5 = new UTF8Encoding(true).GetBytes("\tPriceDiscount");
                    //fs.Write(title5, 0, title5.Length);
                    //Console.WriteLine("\n");
                    //foreach (flightProperties items in detail)
                    //{
                    //    Console.Write("\n");
                    //    Byte[] title6 = new UTF8Encoding(true).GetBytes(items.FlightNumber.ToString());
                    //    fs.Write(title6, 0, title6.Length);
                    //    Byte[] title7 = new UTF8Encoding(true).GetBytes(items.FlightCity);
                    //    fs.Write(title7, 0, title7.Length);
                    //    Byte[] title8 = new UTF8Encoding(true).GetBytes(items.FlightDistance.ToString());
                    //    fs.Write(title8, 0, title8.Length);
                    //    Byte[] title9 = new UTF8Encoding(true).GetBytes(items.FlightPrice.ToString());
                    //    fs.Write(title9, 0, title9.Length);
                    //    Byte[] title10 = new UTF8Encoding(true).GetBytes(items.DiscountPrice.ToString());
                    //    fs.Write(title10, 0, title10.Length);
                    //byte[] author = new UTF8Encoding(true).GetBytes(items.FlightDistance.ToString());
                    //fs.Write(author, 0, author.Length);                 
                }
                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        public static void DbUserconn()
        {
            //UserDatabaseConnection dc = new DatabaseConnection();
            //flightInputs();
            DbUser.userconn(detail);
        }

        public static void SqlConnection()
        {
            DatabaseConnection dc = new DatabaseConnection();
            DatabaseConnection.dbconn();
        }

        public static void ReadDb()
        {
            //DatabaseConnection dc = new DatabaseConnection();
            DbReadData.DbRead();
        }

        public static void DBPLAY()
        {
            //DatabaseConnection dc = new DatabaseConnection();
            DbPlay.DBplay();
        }

        public static void Exit()
       {
          Environment.Exit(0);
       }
    }
}
