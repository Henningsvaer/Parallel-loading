using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WriteReadClassLib;
using WriteReadClassLib.DataManagement;

namespace ParallelLoading
{
    internal sealed class Program
    {
        static List<Person> people;
        static void Main(string[] args)
        {
            // Считаем количество потоков из CustomConfig.
            int threadCount = DataManager.GetThreadCountFromConfig(
               Environment.CurrentDirectory.ToString() + @"\AppConfigure\customConfig.txt");

            // Параллельно считываем input файл.
            people = DataManager.ReadInputFileAsParallel();           
            
            // Запускаем потоки для обработки списка записей и записи в бд.
            List<Task> tasks = new List<Task>();

            // Разделяем записи по потокам.
            int integerPart = people.Count / threadCount;
            int remainderPart = people.Count % threadCount;


            Console.WriteLine($"People count {people.Count}");

            for (int i = 0; i < threadCount ; i++)
            {
                TaskArgs arg = new TaskArgs()
                {
                    From = integerPart * i,
                    To = integerPart * i + integerPart - 1
                };

                Console.WriteLine($"from {arg.From} to {arg.To}");
                // Все оставшиеся строки отдаем в последний поток.
                if (i == threadCount - 1)
                {
                    arg.To = integerPart * i + integerPart - 1 + remainderPart;
                }

                // Записываем данные в таблицу бд.
                var task = new TaskFactory().StartNew(new Action(() =>
                {
                    Parallel.For(arg.From, arg.To + 1, count => 
                    {
                        var db = new DbWriter();
                        string log = string.Empty;
                        db.WriteToDbRow(people[count], ref log);

                        // Логирование.
                        DataManager.Log(DataManager.Logger.Console, log);
                        DataManager.Log(DataManager.Logger.File, log);
                    });
                }));
            }

            // Выход из приложения по ESC оставим в главном потоке.
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey();

            } while (cki.Key != ConsoleKey.Escape);

            // Закрытие всех потоков.
            Environment.Exit(Environment.ExitCode);
        }

        public sealed class TaskArgs
        {
            public int From { get; set; }
            public int To { get; set; }
        }

    }
}
