using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteReadClassLib
{
    // Управление вводом и выводом данных.
    public static class DataManager
    {
        public enum Logger
        {
            Console = 1,
            File = 2
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
    }
}
