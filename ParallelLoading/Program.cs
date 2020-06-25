using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WriteReadClassLib;
using WriteReadClassLib.DataManagement;

namespace ParallelLoading
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            // Считаем количество потоков из CustomConfig.
            int threadCount = DataManager.GetThreadCountFromConfig(
               Environment.CurrentDirectory.ToString() + @"\AppConfigure\customConfig.txt");

            // Параллельно считываем input файл.
            List<Person> people = DataManager.ReadInputFileAsParallel();           
            
            // Запускаем потоки для обработки списка записей и записи в бд.
            List<Task> tasks = new List<Task>();

            // Разделяем "людей" по потокам.
            int integerPart = people.Count / threadCount;
            int remainderPart = people.Count % threadCount;

            for (int i = 0; i < threadCount ; i++)
            {
                TaskArgs arg = new TaskArgs()
                {
                    From = integerPart * i,
                    To = integerPart * i + integerPart - 1
                };

                // Все оставшиеся строки отдаем в последний поток.
                if (i == threadCount - 1)
                {
                    arg.To = integerPart * i + integerPart - 1 + remainderPart;
                }

                var task = new TaskFactory().StartNew(new Action(() =>
                {
                    ReadJsonAndWriteDb(arg.From, arg.To);
                }));
            }

            // Выход из приложения по ESC оставим в главном потоке.
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey();

            } while (cki.Key != ConsoleKey.Escape);

            // Закрытие всех потоков, работавших в фоновом режиме.
            Environment.Exit(Environment.ExitCode);
        }

        static void ReadJsonAndWriteDb(int from, int to)
        {
            Console.WriteLine($"from {from} to {to}");
        }

        public sealed class TaskArgs
        {
            public int From { get; set; }
            public int To { get; set; }
        }

    }
}
