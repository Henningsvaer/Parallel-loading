using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WriteReadClassLib.DataManagement;

namespace WriteReadClassLib
{
    // Управление вводом и выводом данных.
    public static class DataManager
    {
        public static int InputFileLineCount { get; private set; }
        public static string PathToInputFile { get; private set; }

        public enum Logger
        {
            Console = 1,
            File = 2
        }
        static DataManager()
        {
            PathToInputFile = Environment.CurrentDirectory.ToString() + @"\DataManagement\InputData\input.txt";
            InputFileLineCount = File.ReadAllLines(PathToInputFile).Length;
        }

        public static List<Person> ReadInputFileAsParallel()
        {
            List<Person> persons = new List<Person>();

            Parallel.ForEach(File.ReadLines(PathToInputFile), line =>
            {
                try
                {
                    persons.Add(JsonConvert.DeserializeObject<Person>(line));
                }
                catch(Exception e)
                {
                    throw e;
                }
            });

            return persons;
        }

        public static void Log(Logger logger, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            switch (logger)
            {
                case Logger.Console:
                    new ConsoleReport().Display(message);
                    break;

                case Logger.File:
                    new LogFileReport().Display(message);
                    break;

                default:
                    break;
            }
        }

        public static int GetThreadCountFromConfig(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return 1;
            }

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
                throw e;
            }
            return threadCount;
        }
    }
}
