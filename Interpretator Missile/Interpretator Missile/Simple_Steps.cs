using System;
using System.Collections.Generic;
using System.Text;
using org.mariuszgromada.math.mxparser;

namespace Interpretator_Missile
{
    class Simple_Steps
    {
        public static void Terminal_Ouput(int indentation)
        {
            //Write To Console****************
            Console.Write(">>>");
            for (int i = 0; i < indentation; i++)
            {
                Console.Write(".");
            }
            //formats the command line correctly for us
            //**********************************'
        }

        public static string Remove_Comments(string ourString)
        {
            //REMOVE COMMENTING***********************
            if (ourString.Contains("#"))
            {
                ourString = ourString.Substring(0, ourString.IndexOf("#"));
                
            }
            return ourString;
            //**********************************
        }
        public static double Math(string ourString, List<(string, double)> numbers)
        {

            foreach(var s in numbers)
            {
                if (ourString.Contains(s.Item1))
                {
                    ourString = ourString.Replace(s.Item1, s.Item2.ToString());
                }
            }

            Expression e = new Expression(ourString);
            double x = e.calculate();

            

            if (double.IsNaN(x) == false)
            {
                if (ourString.Contains("<") || ourString.Contains("<=") || ourString.Contains(">") || ourString.Contains(">=") || ourString.Contains("==") || ourString.Contains("!="))
                {
                    if (x == 0) Console.WriteLine("False");
                    else if (x == 1) Console.WriteLine("True");
                    else Console.WriteLine(x.ToString());
                }
                else Console.WriteLine(x.ToString());
            }
            else if (ourString.Contains("%"))
            {
                ourString = ourString.Replace('%', '#');
                Expression f = new Expression(ourString);
                x = f.calculate();
                if (double.IsNaN(x) == false)
                {
                    Console.WriteLine(x.ToString());
                }
            }
            return x;
        }

    }
}
