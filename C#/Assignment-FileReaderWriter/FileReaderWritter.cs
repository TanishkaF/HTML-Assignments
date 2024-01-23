using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignmnet_File_Reader_Writer
{
    internal class FileReaderWritter
    {
        static void Main(string[] args)
        {
            // Store the path of the textfile in your system 
             string FILE_PATH = @"C:\Users\tanishkaf\Desktop\dotnetprac\A1\file.txt";

            int choice;

            do
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("0 : EXIT");
                Console.WriteLine("1 : Read File");
                Console.WriteLine("2 : Write File");

                Console.WriteLine("Please Enter Your Choice");
                choice = Convert.ToInt32(Console.ReadLine());

                // Console.WriteLine(choice);          

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Exiting the program. Goodbye!");
                        break;
                    case 1:
                        Console.WriteLine("Reading File:");
                        //fileReaderCall;
                        ReadFromFile(FILE_PATH);
                        break;
                    case 2:
                        Console.WriteLine("Writting in File:");
                        WriteToFile(FILE_PATH);
                        //fileWritterCall;
                        break;
                    default:
                        Console.WriteLine("Invalid Choice,Please Enter a correct choice:");
                        break;
                }

            } while (choice != 0);

        }

        static void WriteToFile(string FILE_PATH)
        {
            
            string file = FILE_PATH;

            try
            {
                Console.Write("Enter the text to write to the file: ");
                string inputText = Console.ReadLine();

                /* StreamWriter writer1 = new StreamWriter(file);
                 writer1.WriteLine(inputText);
                 Console.WriteLine("Success");
                 writer1.Close(); */

                using (StreamWriter writer = new StreamWriter(file, true))
                {
                    writer.WriteLine(inputText);
                    Console.WriteLine("Success");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }

        static void ReadFromFile(string FILE_PATH)
        {
            try
            {
                string file = FILE_PATH;

                if (File.Exists(file))
                {
                    Console.WriteLine("Reading File Data");
                    string[] lines = File.ReadAllLines(file);

                    foreach (string ln in lines)
                        Console.WriteLine(ln);
                }
                else
                {                    
                    Console.WriteLine($"Error Reading from file: File not found at {file}");
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Error Reading from file: {ex.Message}");
            }
        }


    }
}
