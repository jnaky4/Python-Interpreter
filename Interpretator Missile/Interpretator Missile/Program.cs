using org.mariuszgromada.math.mxparser;
using System.Collections.Generic;
using System;
using static Interpretator_Missile.Print;
using static Interpretator_Missile.Assignment;
using static Interpretator_Missile.Logical_Statements;
using static Interpretator_Missile.Conditional_Statements;
using static Interpretator_Missile.Loops;
using static Interpretator_Missile.Simple_Steps;
using System.Collections;

namespace Interpretator_Missile
{
    public static class Program
    {

        //variable containers (variable name, variable value)
        static List<(string, double)> numbers = new List<(string, double)>();
        static List<(string, string)> strings = new List<(string, string)>();
        
        //static ArrayList block_commands = new ArrayList();//Stores block commands

        static List<(string, int)> block_commands = new List<(string, int)>();
        //1: Loop command: ie while, for, if    2: conditional logic    3: tabs
        static List<(string, string, int)> loop_stored = new List<(string, string, int)>();


        static bool in_loop = false;
        static int current_tab = 0;
        static int loop_tab = 0;
        static string loop_type;


        static void Main()
        {

            string[] key_words = { "while", "if", "elif", "else", "for" };
            

            Console.WriteLine("Hi! Please use this interpeter to write python code. Use the command \"exit\" to quit the program.");
            int indentation = 0;

            while (true)
            {
                
                string ourString;
                current_tab = 0;


                //print to terminal
                Terminal_Ouput(indentation);

                ourString = Console.ReadLine();
                
                //Ability to exit from the program
                if (ourString == "exit") break;

                //remove comments from string
                ourString = Remove_Comments(ourString);

                //Count leading tabs and strip them
                while (ourString.Length > 0 && ourString.Substring(0, 1) == "\t")
                {
                    current_tab++;
                    ourString = ourString.Substring(1);
                }

                //if current tab has same # of tabs or less, not in loop
                if ((current_tab <= loop_tab) == true)
                {
                    
                    in_loop = false;

                    //if there is no previous loop data stored
                    if (loop_stored.Count == 0)
                    {

                        //if we are not in a loop
                        if ((loop_type = inLoop(ourString, key_words)) == "false")
                        {
                            in_loop = false;
                            //evaluate the line

                            //check for print

                            if (ourString.Contains("print("))
                            {
                                PrintFunction(ourString, numbers, strings);
                            }

                            Assignment_Statement(ourString, null, 0, strings, numbers);
                            foreach(var s in strings)
                            {
                                Console.WriteLine(s);
                            }
                            foreach (var t in numbers)
                            {
                                Console.WriteLine(t);
                            }
                            if (!ourString.Contains("print("))
                            {
                                var x = Math(ourString, numbers);
                                if (!double.IsNaN(x)) Console.WriteLine(x);
                            }
                        }
                        //we are in a new loop
                        else
                        {
                            int length = ourString.IndexOf(loop_type) + loop_type.Length;
                            //Split between keyword and : 
                            string comparison = ourString.Substring(length);
                            comparison = comparison.Substring(0, comparison.Length - 1).Trim();
                            
                            bool logic_evaluated = Logical_Statement(comparison, loop_type, 0, numbers, strings);

                            //Logic is evaluated, if it returns true, go into next block of code
                            if (logic_evaluated)
                            {

                                loop_stored.Add((loop_type, comparison, current_tab));
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
                        }
                    }
                    //we were just in a loop
                    else
                    {

                        //bool logic_evaluated = Logical_Statement(loop_command, loop_type, loop_tab, numbers, strings);
                        LOOP(numbers, strings, block_commands, loop_stored);                              
                    }                  
                }
                //we are in loop
                else
                {
                    //if loop_evaluated to true store commands
                    if (in_loop)
                    {
                        block_commands.Add((ourString, current_tab));
                    }
                }            
               
            }


        }

        public static void Main2(string input, int tabs)
        { 

            string[] key_words = { "while", "if", "elif", "else", "for" };
            int indentation = 0;

            //while (true)
            {
                int current_tabs = 0;
                string ourString = "print(\"I fucked up\")";
                current_tabs = 0;


                //print to terminal
                Terminal_Ouput(indentation);

                if (input != "") ourString = input;
                //else break;

                //Ability to exit from the program
                //if (ourString == "exit") break;

                //remove comments from string
                ourString = Remove_Comments(ourString);

                //Count leading tabs and strip them
                current_tabs = tabs;

                if (ourString.Contains("print("))
                {
                    PrintFunction(ourString, numbers, strings);
                }


                Assignment_Statement(ourString, null, 0, strings, numbers);
                foreach (var s in strings)
                {
                    Console.WriteLine(s);
                }
                foreach (var t in numbers)
                {
                    Console.WriteLine(t);
                }
                if (!ourString.Contains("print("))
                {
                    var x = Math(ourString, numbers);
                    if (!double.IsNaN(x)) Console.WriteLine(x);
                }

                //if current tab has same # of tabs or less, not in loop
                //if (current_tab == 0) return;
                if ((current_tabs <= loop_tab) == true )
                {

                    in_loop = false;

                    //if there is no previous loop data stored
                    if (loop_stored.Count == 0)
                    {

                        //if we are not in a loop
                        if ((loop_type = inLoop(ourString, key_words)) == "false")
                        {
                            in_loop = false;
                            //evaluate the line

                            //check for print
                            if (ourString.Contains("print("))
                            {
                                PrintFunction(ourString, numbers, strings);
                            }


                            Assignment_Statement(ourString, null, 0, strings, numbers);
                            foreach (var s in strings)
                            {
                                Console.WriteLine(s);
                            }
                            foreach (var t in numbers)
                            {
                                Console.WriteLine(t);
                            }
                            if (!ourString.Contains("print("))
                            {
                                var x = Math(ourString, numbers);
                                if (!double.IsNaN(x)) Console.WriteLine(x);
                            }
                        }
                        //we are in a new loop
                        else
                        {
                            int length = ourString.IndexOf(loop_type) + loop_type.Length;
                            //Split between keyword and : 
                            string comparison = ourString.Substring(length);
                            comparison = comparison.Substring(0, comparison.Length - 1).Trim();

                            bool logic_evaluated = Logical_Statement(comparison, loop_type, 0, numbers, strings);

                            //Logic is evaluated, if it returns true, go into next block of code
                            if (logic_evaluated)
                            {

                                loop_stored.Add((loop_type, comparison, current_tabs));
                                loop_tab = current_tabs;
                                current_tabs = 0;
                                in_loop = true;
                            }
                            else
                            {
                                in_loop = false;
                                current_tabs = 0;
                                loop_tab = 0;
                            }
                        }
                    }
                    //we were just in a loop
                    else
                    {

                        //bool logic_evaluated = Logical_Statement(loop_command, loop_type, loop_tab, numbers, strings);
                        LOOP(numbers, strings, block_commands, loop_stored);
                    }
                }
                //we are in loop
                else
                {
                    //if loop_evaluated to true store commands
                    if (in_loop)
                    {
                        block_commands.Add((ourString, current_tab));
                    }
                }
            }
        }
    }
}
