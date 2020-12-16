using System;
using System.Collections.Generic;
using static Interpretator_Missile.Logical_Statement;
using System.Text;

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
                default:
                    break;
            }
            //return true;
        }

        public static void ifCondition(List<(string, double)> numbers, List<(string, string)> strings, List<(string, int)> block_commands, List<(string, string, int)> loop_stored)
        {
            string condition = loop_stored[loop_stored.Count - 1].Item2;
            string keyword = loop_stored[loop_stored.Count - 1].Item1;
            int tabs = loop_stored[loop_stored.Count - 1].Item3;
            if (Logical_Statment(condition, keyword, tabs, numbers, strings) == true)
                Console.WriteLine("Hooray");
        }
        
        public static string inLoop(string ourString, string[] keywords)
        {
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
