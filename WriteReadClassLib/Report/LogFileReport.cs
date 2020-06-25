using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteReadClassLib
{
    internal sealed class LogFileReport : IReport
    {
        private string _path = 
            Environment.CurrentDirectory.ToString() + @"\DataManagement\OutputData\outputLog.txt";
        public void Display(string message)
        {
            if(string.IsNullOrEmpty(_path))
            {
                return;
            }

            // Запись в файл.
            try
            {
                using (StreamWriter sw = new StreamWriter(_path, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(message);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
