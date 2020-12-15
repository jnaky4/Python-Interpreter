using System;
using System.Collections.Generic;

namespace Interpretator_Missile
{
    class Print
    {
        public static void PrintFunction(string ourString, List<(string, double)> numbers, List<(string, string)> strings)
        {
            string temp;
            foreach(var s in numbers)
            {
                if (ourString.Contains(s.Item1))
                {
                    if (ourString.Contains("str("))
                    {
                        temp = ConvertToString(s.Item2);

                        ourString = ourString.Replace("str(", "");
                        ourString = ourString.Replace(s.Item1, temp);
                        break;
                    }
                }
            }
            foreach (var s in strings)
            {
                if (ourString.Contains(s.Item1))
                {
                    temp = s.Item2;
                    ourString = ourString.Replace(s.Item1, temp);
                    break;
                }
            }
            ourString = ourString.Replace("print(", "");
            ourString = ourString.Replace("\"", "");
            ourString = ourString.Replace("print(", "");
            ourString = ourString.Replace(")", "");
            ourString = ourString.Replace("+", "");
            Console.WriteLine(ourString); 
        }

        public static string ConvertToString(double number)
        {
            return number.ToString();
        }
    }
}