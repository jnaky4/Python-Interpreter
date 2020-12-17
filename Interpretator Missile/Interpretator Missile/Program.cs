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
        static string loop_command;
        static string loop_type;


        static void Main(string[] args)
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
                                Console.WriteLine("Loop is true! Storing Variables");

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
                        Console.WriteLine("Exited Loop");
                        Console.WriteLine("Evaluating previous loop");
                        //evaluate loop condition
                        //grab loop command
                        loop_command = loop_stored[loop_stored.Count - 1].Item2;
                        Console.WriteLine(loop_command);
                        //grab tab_count from loop
                        loop_tab = loop_stored[loop_stored.Count - 1].Item3;
                        Console.WriteLine(loop_tab);
                        loop_type = loop_stored[loop_stored.Count - 1].Item1;
                        Console.WriteLine(loop_type);


                        //bool logic_evaluated = Logical_Statement(loop_command, loop_type, loop_tab, numbers, strings);
                        LOOP(numbers, strings, block_commands, loop_stored);
                        bool logic_evaluated = false;
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


                            //remove blocks stored in block_commands that have more tabs than loop
                            List<(string, int)> temp_block = new List<(string, int)>();
                            temp_block = block_commands;
                            foreach (var command in temp_block)
                            {
                                if (command.Item2 > loop_tab)
                                {
                                    //remove the 
                                    block_commands.Remove(command);
                                }
                            }
                            


                            //dont remove if or elif, since we expect else
                            if(loop_command != "if" || loop_command != "elif")
                            {
                                //remove loop data from loop_stored
                                foreach (var loops in loop_stored)
                                {
                                    if (loop_command == loops.Item1)
                                    {
                                        loop_stored.Remove(loops);
                                    }
                                    //if we reach else, there is an if and maybe elif, remove them
                                    if (loop_command == "else")
                                    {
                                        //if we find an else or if with same # of tabs, remove
                                        if(loops.Item1 == "if" && loops.Item3 == loop_tab || loops.Item1 == "elif" && loops.Item3 == loop_tab)
                                        {
                                            loop_stored.Remove(loops);
                                        }
                                    }

                                }
                            }
                            loop_tab = 0;

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
                        block_commands.Add((ourString, current_tab));
                    }
                }



                    /*



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
                                //block_commands.Add(temp_string);
                            }
                            Console.WriteLine(temp_tabs);
                        }
                        else
                        {
                            //IF, ELIF, ELSE, end loop
                            block_commands.Add(temp_string);
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








                    */
                       
                    
               
            }


        }





    }
}
