using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Method Started......");

            SomeMethod();

            Console.WriteLine("Main Method End");
            Console.ReadKey();
        }

       // public static async void SomeMethod()
        public  static void SomeMethod()
        {
            Console.WriteLine("Some Method Started......");

           Thread.Sleep(TimeSpan.FromSeconds(10));
//await Task.Delay(TimeSpan.FromSeconds(10));
            Console.WriteLine("\n");
            Console.WriteLine("Some Method End");
        }
    }
}