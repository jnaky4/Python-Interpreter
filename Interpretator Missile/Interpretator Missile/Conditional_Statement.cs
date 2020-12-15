using System;
using System.Collections.Generic;
using System.Text;

namespace Interpretator_Missile
{
    class Conditional_Statements
    {
        //Conditional statements (<, <=, >, >=, ==, !=)
        //split_at_condition is an array[2], split at condition
        public static bool Conditional_Statement(string[] split_at_condition, string condition)
        {
            split_at_condition[0] = split_at_condition[0].Trim();
            split_at_condition[1] = split_at_condition[1].Trim();
            //if (numbers.Contains(split_at_condition[0]))
            switch (condition)
            {
                case ("<"):
                    //TODO
                    break;
                case ("<="):
                    //TODO
                    break;
                case (">"):
                    //TODO
                    break;
                case (">="):
                    //TODO
                    //return split_at_condition[0] >= split_at_condition[1] ? true : false;
                    break;
                case ("=="):
                    //TODO check if works
                    return split_at_condition[0] == split_at_condition[1] ? true : false;
                case ("!="):
                    //TODO check if works
                    return split_at_condition[0] != split_at_condition[1] ? true : false;
            }


            //DEBUG PRINTING
            Console.WriteLine(condition);
            Console.WriteLine(split_at_condition[0]);
            Console.WriteLine(split_at_condition[1]);
            //DEBUG RETURN
            return true;
        }
    }
}
