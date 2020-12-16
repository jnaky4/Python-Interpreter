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
        static List<(string, string, int)> loop_stored = new List<(string, string, int)>();


        static bool in_loop = false;
        static int current_tab = 0;
        static int loop_tab = 0;
        static string loop_command;
        static string loop_type;


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




                
                ourString = Console.ReadLine();

                //Count leading tabs and strip them
                while (ourString.Substring(0, 1) == "\t")
                {
                    current_tab++;
                    ourString = ourString.Substring(1);
                }

                
                //REMOVE COMMENTING***********************
                if (ourString.Contains("#"))
                {
                    ourString = ourString.Substring(0, ourString.IndexOf("#"));
                    Console.WriteLine("Found Commenting, removed it");
                    Console.WriteLine(ourString);
                }
                //**********************************

                Console.WriteLine("COMPARING TABS" + current_tab + " " + loop_tab);

                //if current tab has same # of tabs or less, not in loop
                if ((current_tab <= loop_tab) == true)
                {
                    Console.WriteLine("No new tabs");
                    in_loop = false;

                    //if there is no previous loop data stored
                    if (loop_stored.Count == 0)
                    {
                        Console.WriteLine("We were not previously in a loop");

                        //if we are not in a loop
                        if ((loop_type = inLoop(ourString, key_words)) == "false")
                        {
                            in_loop = false;
                            //evaluate the line

                        }
                        //we are in a new loop
                        else
                        {



                            int length = ourString.IndexOf(loop_type) + loop_type.Length;
                            //Split between keyword and : 
                            string comparison = ourString.Substring(length);
                            comparison = comparison.Substring(0, comparison.Length - 1);
                            bool logic_evaluated = Logical_Statment(comparison, loop_type, 0, numbers, strings);

                            //Logic is evaluated, if it returns true, go into next block of code
                            if (logic_evaluated)
                            {
                                Console.WriteLine("Loop is true! Storing Variables");
                                loop_stored.Add((loop_type, ourString, current_tab));
                                loop_tab = current_tab;
                                current_tab = 0;
                                in_loop = true;
                            }
                            else
                            {
                                in_loop = false;
                                current_tab = 0;
                                loop_tab = 0;
                            }
                            //else evaluate 
                        }



                    }

                    //we were just in a loop
                    else
                    {
                        Console.WriteLine("Exited Loop");
                        Console.WriteLine("Evaluating previous loop");
                        //evaluate loop condition
                        //grab loop command
                        loop_command = loop_stored[loop_stored.Count - 1].Item2;
                        //grab tab_count from loop
                        loop_tab = loop_stored[loop_stored.Count - 1].Item3;
                        loop_type = loop_stored[loop_stored.Count - 1].Item1;
                        //grab keyword from loop, split into logical_statements to be evaluated
                        foreach (var s in key_words)
                        {
                            if (loop_command.Contains(s))
                            {


                                string key_word = s;
                                int length = loop_command.IndexOf(s) + s.Length;
                                //Split between keyword and : 
                                string comparison = loop_command.Substring(length);
                                comparison = comparison.Substring(0, comparison.Length - 1);
                                bool logic_evaluated = Logical_Statment(comparison, s, 0, numbers, strings);

                                //logic evaluated to true
                                if (logic_evaluated)
                                {
                                    in_loop = true;
                                    //evaluate commands in array

                                }
                                //loop is no longer true, exit loop
                                else
                                {
                                    //clear loop variables
                                    in_loop = false;
                                    loop_tab = 0;

                                    //remove blocks stored in al that have more tabs than loop
                                    foreach (var command in al)
                                    {
                                        if (command.Item2 > loop_tab)
                                        {
                                            //remove the 
                                            al.Remove(command);
                                        }
                                    }
                                    //remove loop data from loop_stored
                                    foreach (var loops in loop_stored)
                                    {
                                        if (loop_command == loops.Item1)
                                        {
                                            loop_stored.Remove(loops);
                                        }

                                    }
                                }
                                


                            }
                        }
                    }
                    
                }
                //we are in loop
                else
                {
                    Console.WriteLine("IN LOOP");
                    //if loop_evaluated to true store commands
                    if (in_loop)
                    {
                        Console.WriteLine("adding to Block Data Array");
                        al.Add((ourString, current_tab));
                    }
                }







                    /*//START READING*********************
                    if (al.Count == 0) ourString = Console.ReadLine();
                    else
                    {
                        ourString = al[0].ToString();
                        al.RemoveAt(0);
                    }







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
                    //*******************************************/
                       
                    
               
            }


        }





    }
}
