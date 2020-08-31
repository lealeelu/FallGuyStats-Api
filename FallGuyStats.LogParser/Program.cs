using System;
using System.Collections.Generic;
using System.Text;

namespace FallGuyStats.LogParser
{
    class Program
    {
        public static void Main()
        {
            LogParser.ReadLogData();
            Console.WriteLine("DONE");
            Console.ReadKey();
        }
    }
}
