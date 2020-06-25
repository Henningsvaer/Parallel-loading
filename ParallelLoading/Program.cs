using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WriteReadClassLib;

namespace ParallelLoading
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            // Считаем количество потоков из CustomConfig.
            int threadCount = GetThreadCountFromConfig(
               Environment.CurrentDirectory.ToString() + @"\AppConfigure\customConfig.txt");

            // TO DO : запусти все потоки в background = true;
           DataManager.Log(DataManager.Logger.Console, "Привет");

            // Выход из приложения по ESC оставим в главном потоке.
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey();

            } while (cki.Key != ConsoleKey.Escape);

            // Закрытие всех потоков, работавших в фоновом режиме.
            Environment.Exit(Environment.ExitCode);
        }

        static int GetThreadCountFromConfig(string path)
        {
            int threadCount = 1;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    // Возвращает число потоков из первой строки файла конфигурации.
                    threadCount = int.Parse(sr.ReadLine().Split(':')[1]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return threadCount;
        }
    }
}
