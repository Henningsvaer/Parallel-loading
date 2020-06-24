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
            
        }

        // TO DO: Запустить в отдельном потоке.
        // Выход из приложения.
        static void AppQuit()
        {
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey();

            } while (cki.Key != ConsoleKey.Escape);
        }
    }
}
