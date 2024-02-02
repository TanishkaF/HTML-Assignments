using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolCRUD.UtilityLayer
{

    public class ValidateData
    {
        /* public static T GetInput<T>(string s)
         {
             while (true)
             {
                 try
                 {
                     if (typeof(T) == typeof(int) && int.TryParse(s, out _))
                     {
                        // Console.Write("Correct Input value:");
                         return default;
                     }
                     else
                     {
                         Console.WriteLine("Please Enter the correct Input valuet:");
                         string input = Console.ReadLine();
                         return (T)Convert.ChangeType(input, typeof(T));
                     }
                 }
                 catch (FormatException ex)
                 {
                     Program.AddData(ex);
                     //Console.WriteLine("Invalid input. Please enter a valid value.");
                 }
                 catch (Exception ex)
                 {
                     Program.AddData(ex);
                     // Console.WriteLine($"An error occurred: {ex.Message}");
                 }
             }
         }
         */

        public static int GetValidIntegerInput(string m)
        {
            while (true)
            {
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine($"Please enter a valid {m}: ");
                }
            }
        }
        public static double GetValidDoubleInput(string m)
        {
            while (true)
            {
                string userInput = Console.ReadLine();
                if (double.TryParse(userInput, out double resultDouble))
                {
                    return resultDouble;
                }
                else
                {
                    Console.WriteLine($"Please enter a valid {m}: ");
                }
            }
        }
    }
}
