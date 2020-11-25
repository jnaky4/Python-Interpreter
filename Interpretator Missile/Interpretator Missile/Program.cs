using org.mariuszgromada.math.mxparser;
using System;

namespace Interpretator_Missile
{
    public static class Program
    {
        static float PlusFloat(string num1, string num2)
        {
            float numA = float.Parse(num1);
            float numB = float.Parse(num2);
            return numA + numB;
        }

        static int PlusInt(string num1, string num2)
        {
            return int.Parse(num1) + int.Parse(num2);
        }
        public static bool IsInteger(this string s)
        {
            if (String.IsNullOrEmpty(s))
                return false;

            int i;
            return Int32.TryParse(s, out i);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hi! Please use this interpeter to write python code. Use the command \"exit\" to quit the program.");
            int indentation = 0;
            while (true)
            {
                Console.Write(">>>");
                for (int i = 0; i < indentation; i++)
                {
                    Console.Write(".");
                }
                for (int i = 0; i < indentation; i++)
                {
                    Console.Write("\t");
                }
                Console.Write(" ");
                //formats the command line correctly for us
                
                string ourString = Console.ReadLine();
                //gives us our string that the person wrote in

                if (ourString.Contains("#"))
                {
                    ourString = ourString.Substring(0, ourString.IndexOf("#"));
                }
                //Comment functinallity

                if (ourString.Contains("exit")) break;
                //Ability to exit from the program

                foreach(var s in ourString)
                {
                    if (s.CompareTo('\t') == 0) indentation++;
                }
                if (ourString.Length == 0) indentation = 0;
                //Gives us correct indentation

                if (ourString.Contains("print(\"") && ourString.Contains("\")"))
                {
                    ourString = ourString.Substring(ourString.IndexOf("print(\""), ourString.IndexOf("\")"));
                    ourString = ourString.Substring(ourString.IndexOf("\"") + 1);
                    Console.WriteLine(ourString);
                }

                Expression e = new Expression(ourString);
                double x = e.calculate();
                if (double.IsNaN(x) == false)
                {
                    Console.WriteLine(x.ToString());
                }
                //Most of arithmatic is working, only one that is not is remainder.
            }
            
        }
    }
}
