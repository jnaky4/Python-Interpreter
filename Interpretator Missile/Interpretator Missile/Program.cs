using System;

namespace Interpretator_Missile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hi! Please use this interpeter to write python code. Use the command \"exit\" to quit the program.");
            while (true)
            {
                string ourString = Console.ReadLine();
                
                if (ourString.Length > 3 && ourString.Substring(0, 4).CompareTo("exit") == 0) break;
            }
            
        }
    }
}
