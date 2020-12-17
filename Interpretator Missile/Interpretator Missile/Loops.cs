using System;
using System.Collections.Generic;
using static Interpretator_Missile.Logical_Statements;
using System.Text;
using static Interpretator_Missile.Program;

namespace Interpretator_Missile
{
    class Loops
    {

        //while and for Loops 
        ////1: Loop command: ie while, for, if    2: conditional logic    3: tabs
        public static void LOOP(List<(string, double)> numbers, List<(string, string)> strings, List<(string, int)> block_commands, List<(string, string, int)> loop_stored)
        {
            string keyword = loop_stored[loop_stored.Count - 1].Item1;
            switch (keyword)
            {
                case ("if"):
                    ifCondition(numbers, strings, block_commands, loop_stored);
                    break;
                case ("elif"):

                    break;
                case ("else"):
                    break;
                case ("while"):
                    break;
                case ("for"):
                    break;
                default:
                    break;
            }

            string loop_type = loop_stored[loop_stored.Count - 1].Item1;
            Console.WriteLine(loop_type);
            string loop_command = loop_stored[loop_stored.Count - 1].Item2;
            Console.WriteLine(loop_command);
            int loop_tab = loop_stored[loop_stored.Count - 1].Item3;
            Console.WriteLine(loop_tab);


            //return true;
            bool logic_evaluated = false;
            //logic evaluated to true
            if (logic_evaluated)
            {

                //

            }
            //remove blocks stored in block_commands that have more tabs than loop
            List<(string, int)> temp_block = new List<(string, int)>();
            foreach (var c in block_commands)
            {
                temp_block.Add(c);
            }
            foreach (var command in temp_block)
            {
                if (command.Item2 > loop_tab)
                {
                    //remove the 
                    block_commands.Remove(command);
                }
            }
            //dont remove if or elif, since we expect else
            if (loop_command != "if" || loop_command != "elif")
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
                        if (loops.Item1 == "if" && loops.Item3 == loop_tab || loops.Item1 == "elif" && loops.Item3 == loop_tab)
                        {
                            loop_stored.Remove(loops);
                        }
                    }

                }
            }
        }

        public static void ifCondition(List<(string, double)> numbers, List<(string, string)> strings, List<(string, int)> block_commands, List<(string, string, int)> loop_stored)
        {
            string condition = loop_stored[loop_stored.Count - 1].Item2;
            string keyword = loop_stored[loop_stored.Count - 1].Item1;
            int tabs = loop_stored[loop_stored.Count - 1].Item3;
            if (Logical_Statement(condition, keyword, tabs, numbers, strings) == true)
            {
                //grab next tab commands
                foreach (var command in block_commands)
                {
                    
                    Main2(command.Item1, command.Item2);
                }

            }
            


        }
        
        public static string inLoop(string ourString, string[] keywords)
        {
            if (ourString.Length == 0) return "false";
            if (!(ourString[ourString.Length - 1] == ':'))
            {
                Console.WriteLine("Error, missing :");
                return "false";
            }

            foreach (var keyword in keywords)
            {
                if (ourString.Contains(keyword))
                    return keyword;
            }

            return "false";
        }
    }
}
