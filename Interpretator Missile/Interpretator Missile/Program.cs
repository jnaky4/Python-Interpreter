using org.mariuszgromada.math.mxparser;
using System;

namespace Interpretator_Missile
{
    public static class Program
    {
        static void Main(string[] args)
        {
            //Strip commenting
            //if, while, else, elif terminator ':'
            //and or statements to split on
            //

            string[] key_words = { "print", "while", "if", "elif", "else", "and","or", "break", "for", "in"};

            Console.WriteLine("Hi! Please use this interpeter to write python code. Use the command \"exit\" to quit the program.");
            int indentation = 0;

            while (true)
            {



                //Write To Console****************
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
                //**********************************'


                //START READING*********************
                string ourString = Console.ReadLine();


                //COMMENTING***********************
                //gives us our string that the person wrote in
                if (ourString.Contains("#"))
                {
                    ourString = ourString.Substring(0, ourString.IndexOf("#"));
                }
                //Comment functinallity
                //**********************************









                //EXIT*****************************
                if (ourString.Contains("exit")) break;
                //Ability to exit from the program
                //**********************************



                //TAB COUNTING**********************
                foreach(var s in ourString)
                {
                    if (s.CompareTo('\t') == 0) indentation++;
                }
                if (ourString.Length == 0) indentation = 0;
                //Gives us correct indentation
                //**********************************


                //PRINTING*****************************
                if (ourString.Contains("print(\"") && ourString.Contains("\")"))
                {
                    ourString = ourString.Substring(ourString.IndexOf("print(\""), ourString.IndexOf("\")"));
                    ourString = ourString.Substring(ourString.IndexOf("\"") + 1);
                    Console.WriteLine(ourString);
                }
                //Print function
                //*************************************


                //MATH*********************************
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
                    double y = f.calculate();
                    if (double.IsNaN(y) == false)
                    {
                        Console.WriteLine(y.ToString());
                    }
                }
                //Most of arithmatic is working
                //******************************************


                //ASSIGNMENT OPERATORS********************
                string[] assignment_split;
                switch(ourString)
                {

                    case string a when a.Contains("=="):
                        Console.WriteLine("found ==");
                        assignment_split = ourString.Split("==");
                        break;
                    case string b when b.Contains("+="):
                        Console.WriteLine("found +=");
                        assignment_split = ourString.Split("+=");
                        break;
                    case string a when a.Contains("-="):
                        Console.WriteLine("found -=");
                        assignment_split = ourString.Split("-=");
                        break;
                    case string a when a.Contains("*="):
                        Console.WriteLine("found *=");
                        assignment_split = ourString.Split("+=");
                        break;
                    case string a when a.Contains("/="):
                        Console.WriteLine("found /=");
                        assignment_split = ourString.Split("+=");
                        break;
                    case string a when a.Contains("^="):
                        assignment_split = ourString.Split("+=");
                        Console.WriteLine("found ^=");
                        break;
                    case string a when a.Contains("%="):
                        assignment_split = ourString.Split("%=");
                        Console.WriteLine("found %=");
                        break;
                    case string a when a.Contains("="):
                        assignment_split = ourString.Split("=");
                        Console.WriteLine("found =");
                        break;
                    default: {} break;


                }
/*                if (ourString.Contains("=") || ourString.Contains("==") )
                {
                    split by spaces into array
                    string[] var_split = ourString.Split('=');
                    foreach(var s in var_split)
                    {

                       } 
                }*/

            }

        }
    }
}
