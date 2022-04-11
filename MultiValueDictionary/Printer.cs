using System;

namespace MultiValueDictionary
{
    internal class Printer : IPrinter
    {
        public void Print(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
