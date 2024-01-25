using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml.Linq;
using CRUDLOG;
using System.Globalization;

namespace AdoNetConsoleApplication
{
    class Program
    {
        //private string fileName = @"C:\Users\tanishkaf\Desktop\dotnetprac\A2\logFile.txt";
        static void Main(string[] args)
        {
            Program program = new Program();         

            if (program.TestConnection())
            {
                int choice;
                do
                {
                    Console.WriteLine("Menu:");
                    Console.WriteLine("0 : EXIT");
                    Console.WriteLine("1 : Create Table");
                    Console.WriteLine("2 : Insert Data");
                    Console.WriteLine("3 : Update Data");
                    Console.WriteLine("4 : Delete Data");
                    Console.WriteLine("5 : Display Table");
                    Console.WriteLine("6 : Delete all Data of Table");
                    Console.WriteLine("7 : Delete Table");

                    Console.WriteLine("Please Enter Your Choice");
                    choice = Convert.ToInt32(Console.ReadLine());

                    // Console.WriteLine(choice);          

                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("Exiting the program. Goodbye!");
                            break;
                        case 1:
                            Console.WriteLine("Creating Table:");
                            program.CreateTable();
                            break;
                        case 2:
                            Console.WriteLine("Inserting Data in table:");
                            program.InsertData();
                            break;
                        case 3:
                            Console.WriteLine("Update Data in the table:");
                            program.UpdateData();
                            break;
                        case 4:
                            Console.WriteLine("Deleting Data in table:");
                            program.DeleteData();
                            break;
                        case 5:
                            Console.WriteLine("Displaying Data of table:");
                            program.DisplayTable();
                            break;
                        case 6:
                            Console.WriteLine("Deleting all data of table:");
                            program.DeleteAllData();
                            break;
                        case 7:
                            Console.WriteLine("Deleting the whole table:");
                            program.DropTable();
                            break;
                        default:
                            Console.WriteLine("Invalid Choice,Please Enter a " +
                                "correct choice:");
                            break;
                    }

                } while (choice != 0);
            }
            
            else
            {
                Console.WriteLine("Connection is not established properly");
                Console.ReadLine();
            }
        }

        public bool TestConnection()
        {
            SqlConnection connection = null;

            try
            {
                // Read connection string from app.config
                string connectionString = ConfigurationManager
                    .ConnectionStrings["MyConnectionString"].ConnectionString;

                connection = new SqlConnection(connectionString);

                connection.Open();

                Console.WriteLine("Connection established Successfully");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong in Testing Connection: " + e);
                Logger.AddData(e);
                return false;
            }
            finally
            {
                connection?.Close();
            }
        }

        public void CreateTable()
        {
            SqlConnection connection = null;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

                connection = new SqlConnection(connectionString);
               
                string query = "create table teacher(id int not null,name varchar(100), " +
                    "email varchar(50), join_date date)";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                command.ExecuteNonQuery();

                Console.WriteLine("Table created Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong in Table Creation." + e);
                Logger.AddData(e);
            }

            finally
            {
                connection.Close();
            }
        }

        public void DisplayTable()
        {
            SqlConnection connection = null;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                connection = new SqlConnection(connectionString);

                string query = "SELECT * FROM teacher";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader sqlDataReader = command.ExecuteReader();
                if (!sqlDataReader.HasRows)
                {
                    Console.WriteLine("No records found. Kindly insert data.");
                }
                else
                {
                    //Console.WriteLine("Display");
                    while (sqlDataReader.Read())
                    {
                        //Console.WriteLine("Display Inside Loop");
                        Console.WriteLine(sqlDataReader["id"] + " " + sqlDataReader["name"] + " " + sqlDataReader["email"] + " " + sqlDataReader["join_date"]);
                    }
                    Console.WriteLine("Displayed Data Successfully");
                }
            }catch (Exception e){
                Console.WriteLine("Something went wrong in displaying data." + e);
                Logger.AddData(e);
            }finally{
                connection.Close();
            }
        }

        public void InsertData()
        {
            SqlConnection connection = null;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                connection = new SqlConnection(connectionString);

                Console.WriteLine("Please Enter the Data to be Inserted in the Table:");

                Console.Write("Id:");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Name:");
                string name = Console.ReadLine();
                Console.Write("Email:");
                string email = Console.ReadLine();
                Console.Write("Date Of Joining in the format of 'yyyy-MM-dd':");
                string date = Console.ReadLine();

                string query = $"INSERT INTO teacher VALUES ({id}, '{name}', '{email}', '{date}')";
                //string query = "INSERT INTO teacher values(2,'aman','aman','1200-10-21')";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                command.ExecuteNonQuery();

                Console.WriteLine("Insertion Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong." + e);
                Logger.AddData(e);
            }finally
            {
                connection.Close();
            }
        }

        public void UpdateData()
        {
            SqlConnection connection = null;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                connection = new SqlConnection(connectionString);

                Console.WriteLine("Please Enter the Name and ID to be Updated in the Table:");

                Console.Write("Id:");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Name:");
                string name = Console.ReadLine();

                // string query = "UPDATE Teacher SET email='amanemail' WHERE id= 2";
                string query = $"UPDATE teacher SET name = '{name}'  WHERE id = {id}";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                command.ExecuteNonQuery();

                Console.WriteLine("Updation Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong in updating data." + e);
                Logger.AddData(e);
            }

            finally
            {
                connection.Close();
            }
        }

        public void DeleteData()
        {
            SqlConnection connection = null;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                connection = new SqlConnection(connectionString);

                Console.WriteLine("Please Enter the ID for whose  record to be deleted from Table:");

                Console.Write("Id:");
                int id = Convert.ToInt32(Console.ReadLine());

                // string query = "DELETE FROM teacher WHERE id=2";
                string query = $"DELETE FROM teacher WHERE id = {id}";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                command.ExecuteNonQuery();

                Console.WriteLine("Deletion Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong in deleting data." + e);
                Logger.AddData(e);
            }

            finally
            {
                connection.Close();
            }
        }

        public void DeleteAllData()
        {
            SqlConnection connection = null;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                connection = new SqlConnection(connectionString);

                Console.WriteLine("Please Enter 'YES' if you want to delete all data from Table:");

                //  Console.Write("I:");
                string id = Console.ReadLine();
                string consent = "YES";

                if (String.Equals(id, consent))
                {
                    string query = "TRUNCATE TABLE teacher";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.ExecuteNonQuery();

                    Console.WriteLine("Deletion of all data Successfully");
                }
                else
                {
                    Console.WriteLine("Choose from the menu:");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong in deleting all data." + e);
                Logger.AddData(e);
            }

            finally
            {
                connection.Close();
            }
        }

        public void DropTable()
        {
            SqlConnection connection = null;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                connection = new SqlConnection(connectionString);

                Console.WriteLine("Please Enter 'YES' if you want to delete the Table:");

                //  Console.Write("I:");
                string id = Console.ReadLine();
                string consent = "YES";

                if (String.Equals(id, consent))
                {
                    string query = "DROP TABLE teacher";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.ExecuteNonQuery();

                    Console.WriteLine("Deletion of Table Successfully");
                }
                else
                {
                    Console.WriteLine("Choose from the menu:");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong in dropping of table." + e);
                Logger.AddData(e);
            }

            finally
            {
                connection.Close();
            }
        }
    }
}