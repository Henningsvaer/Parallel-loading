using System;

namespace WriteReadClassLib
{
    internal sealed class ConsoleReport : IReport
    {
        public void Display(string message)
        {
            Console.WriteLine(message);
        }
    }
}
