using System;

namespace MultiValueDictionary
{
    internal class Printer : IPrinter
    {
        /// <summary>
        /// Print the message
        /// </summary>
        /// <param name="msg"></param>
        public void Print(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
