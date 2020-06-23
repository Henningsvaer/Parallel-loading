using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelLoading
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo cki; 
            Start();
            // Переделать в отдельном потоке.
            do
            {
                cki = Console.ReadKey();

            } while (cki.Key != ConsoleKey.Escape);
        }

        static void Start()
        {

        }
    }
}
