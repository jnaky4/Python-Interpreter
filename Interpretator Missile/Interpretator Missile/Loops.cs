using System;
using System.Collections.Generic;
using System.Text;

namespace Interpretator_Missile
{
    class Loops
    {

        //while and for Loops 
        //TODO
        public static bool LOOP(string ourString, string key_word, int tabs)
        {
            return true;
        }
        
        public static string inLoop(string ourString, string[] keywords)
        {
            if (!(ourString[ourString.Length - 1] == ':'))
            {
                Console.WriteLine("Error, missing :");
                return null;
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
