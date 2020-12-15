using org.mariuszgromada.math.mxparser;
using System.Collections.Generic;
using System;
using static Interpretator_Missile.Print;
using static Interpretator_Missile.Assignment;
using static Interpretator_Missile.Logical_Statement;
using static Interpretator_Missile.Conditional_Statements;

namespace Interpretator_Missile
{
    public static class Program
    {

        //variable containers (variable name, variable value)
        static List<(string, double)> numbers = new List<(string, double)>();
        static List<(string, string)> strings = new List<(string, string)>();

        static bool in_loop = false;
        static int current_tab = 0;


        static void Main(string[] args)
        {
            //Strip commenting
            //SWITCH: if, while, else, elif, for  ----> all have terminator ':'
            //and or statements to split on
            //
            /*
                Steps for Breaking down a line of code:
                1) strip commenting
                2) strip on key words
                3) 
             
             */


            string[] key_words = {"while", "if", "elif", "else", "for"};

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




                //TODO NEEDS TO COUNT TABS
                //CONDITIONAL STATEMENT*************
                //while, for, if, elif, else
                foreach (var s in key_words)
                {
                    if (ourString.Contains(s))
                    {
                        if (!(ourString[ourString.Length-1] == ':'))
                        {
                            //ERROR
                            Console.WriteLine("Missing :");
                        }
                        else
                        {
                            //Grab the keyword
                            string key_word = s;
                            int length = ourString.IndexOf(s) + s.Length;
                            //Split between keyword and : 
                            string comparison = ourString.Substring(length);
                            comparison = comparison.Substring(0, comparison.Length - 1);
                            bool logic_evaluated = Logical_Statment(comparison, s, 0, numbers, strings);

                            //Logic is evaluated, if it returns true, go into next block of code
                            if (logic_evaluated)
                            {
                                current_tab = 1;
                                in_loop = true;
                            }
                        }
                    }
                }

                Assignment_Statement(ourString, null, 0, strings, numbers);
                //Assignment_Operator(ourString, null, 0, strings, numbers);

                //EXIT*****************************
                if (ourString == "exit") break;
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
                if (ourString.Contains("print("))
                {
                    PrintFunction(ourString, numbers, strings);
                }
                //Print function
                //*************************************


                //MATH*********************************
                Expression e = new Expression(ourString);
                double x = e.calculate();
                
                
                if (double.IsNaN(x) == false)
                {
                    /*if (ourString.Contains("<") || ourString.Contains("<=") || ourString.Contains(">") || ourString.Contains(">=") || ourString.Contains("==") || ourString.Contains("!="))
                    {
                        if (x == 0) Console.WriteLine("False");
                        else if (x == 1) Console.WriteLine("True");
                        else Console.WriteLine(x.ToString());
                    }
                    else Console.WriteLine(x.ToString());*/
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


            }


        }


       
        //while and for Loops 
        //TODO
        static bool LOOP(string ourString, string key_word, int tabs)
        {
            return true;
        }

        
    }
}
