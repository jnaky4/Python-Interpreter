using org.mariuszgromada.math.mxparser;
using System.Collections.Generic;
using System;
using static Interpretator_Missile.Print;
using static Interpretator_Missile.Assignment;
using static Interpretator_Missile.Logical_Statement;
using static Interpretator_Missile.Conditional_Statements;
using static Interpretator_Missile.Loops;
using System.Collections;

namespace Interpretator_Missile
{
    public static class Program
    {

        //variable containers (variable name, variable value)
        static List<(string, double)> numbers = new List<(string, double)>();
        static List<(string, string)> strings = new List<(string, string)>();
        
        //static ArrayList al = new ArrayList();//Stores block commands
        static List<(string, int)> al = new List<(string, int)>();
        static List<(string, int)> loop_stored = new List<(string, int)>();


        static bool in_loop = false;
        static int current_tab = 0;
        static int loop_tab = 0;
        static string loop_command;


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

            

            string[] key_words = { "while", "if", "elif", "else", "for" };
            

            Console.WriteLine("Hi! Please use this interpeter to write python code. Use the command \"exit\" to quit the program.");
            int indentation = 0;

            while (true)
            {
                string ourString;
                current_tab = 0;
                //Write To Console****************
                Console.Write(">>>");
                for (int i = 0; i < indentation; i++)
                {
                    Console.Write(".");
                }
                //formats the command line correctly for us
                //**********************************'



                //in_loop set to true, never grabs block data
                if (!in_loop)
                {
                    ourString = Console.ReadLine();


                    while (ourString.Substring(0, 1) == "\t")
                    {
                        current_tab++;
                        ourString = ourString.Substring(1);
                    }
                }



                //if current tab has same # of tabs or less, not in loop
                if((current_tab <= loop_tab) == true)
                {
                    //if not in previous loop
                    if(loop_stored.Count == 0)
                    {

                    }
                    //we were just in a loop
                    else
                    {
                        //evaluate loop condition
                        loop_command = loop_stored[loop_stored.Count-1].Item1;
                        loop_tab = loop_stored[loop_stored.Count - 1].Item2;
                        foreach (var s in key_words)
                        {
                            if (loop_command.Contains(s))
                            {
                                if (!(ourString[ourString.Length - 1] == ':'))
                                {
                                    //ERROR
                                    Console.WriteLine("Missing :");
                                }
                                else
                                {
                                    string key_word = s;
                                    int length = loop_command.IndexOf(s) + s.Length;
                                    //Split between keyword and : 
                                    string comparison = loop_command.Substring(length);
                                    comparison = comparison.Substring(0, comparison.Length - 1);
                                    bool logic_evaluated = Logical_Statment(comparison, s, 0, numbers, strings);
                                    if (logic_evaluated)
                                    {
                                        in_loop = true;
                                        //evaluate commands in array

                                    }
                                    else
                                    {
                                        in_loop = false;
                                        loop_tab = 0;
                                        foreach(var command in al)
                                        {
                                            if(command.Item2 > loop_tab)
                                            {
                                                al.Remove(command);
                                            }
                                        }
                                    }
                                }


                }
                //we are in loop
                else
                {      
                    //if loop_evaluated to true store commands
                    if (in_loop)
                    {
                        al.Add((ourString, current_tab));
                    }
                }







                //START READING*********************
                if (al.Count == 0) ourString = Console.ReadLine();
                else
                {
                    ourString = al[0].ToString();
                    al.RemoveAt(0);
                }



                //COMMENTING***********************
                //gives us our string that the person wrote in
                if (ourString.Contains("#"))
                {
                    ourString = ourString.Substring(0, ourString.IndexOf("#"));
                }
                //Comment functinallity
                //**********************************




                Console.WriteLine(ourString);
                if (ourString.Contains("\t"))
                {
                    Console.WriteLine("FOUND TAB");
                }



                //TODO NEEDS TO COUNT TABS
                //CONDITIONAL STATEMENT*************
                //while, for, if, elif, else
                foreach (var s in key_words)
                {
                    if (ourString.Contains(s))
                    {
                        if (!(ourString[ourString.Length - 1] == ':'))
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
                                current_tab = 0;
                                in_loop = true;
                            }
                        }

                    }
                }

                if (!in_loop)
                {
                    Console.WriteLine("Not in LOOP");
                    Assignment_Statement(ourString, null, 0, strings, numbers);
                    //Assignment_Operator(ourString, null, 0, strings, numbers);
                }

                //LOOP Logic**********************
                while (in_loop)
                {
                    string temp_string;
                    //Console.WriteLine("STRIPPING TABS");
                    if ((temp_string = Console.ReadLine()).Substring(0, 1) == "\t")
                    {
                        //temp_string = Console.ReadLine();
                        int temp_tabs = 0;
                        //Console.WriteLine(temp_string.Substring(0, 1) + "hello");

                        while (temp_string.Substring(0, 1) == "\t")
                        {

                            temp_tabs++;
                            //Console.WriteLine("Contains Tabs");
                            temp_string = temp_string.Substring(1);
                            //Console.WriteLine(temp_string);
                            //al.Add(temp_string);
                        }
                        Console.WriteLine(temp_tabs);
                    }
                    else
                    {
                        //IF, ELIF, ELSE, end loop
                        al.Add(temp_string);
                        in_loop = false;
                        Console.WriteLine("EXIT LOOP");
                        ourString = temp_string;
                        //current line is still stored
                        //check what kind of loop we are in
                    }

                    //string comparison has all the logic that needs to be reevaluated at each loop

                    //read all lines that have indentation greater than current until we find line
                    //that is of same number of tabs to indicate end of loop block
                    //all of this needs to be fed into the loop
                    //LOOP()
                    // neeed comparison, tabs

                }
                //********************************


                //EXIT*****************************
                if (ourString == "exit") break;
                //Ability to exit from the program
                //**********************************



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


            }


        }





    }
}
