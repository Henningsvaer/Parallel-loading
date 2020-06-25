using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteReadClassLib
{
    internal sealed class LogFileReport : IReport
    {
        private string _path = 
            Environment.CurrentDirectory.ToString() + @"\OutputData\outputLog.txt";
        public void Display(string message)
        {
            if(string.IsNullOrEmpty(_path))
            {
                return;
            }

            // Запись в файл.

        }
    }
}
