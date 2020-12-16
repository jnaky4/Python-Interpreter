using System;
using System.Collections.Generic;
using System.Text;

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

    }
}
