using System;
using System.Collections.Generic;
using System.Text;
using static Interpretator_Missile.Assignment;

namespace Interpretator_Missile
{
    class Logical_Statement
    {
        //Logical check (if, elif, else, while, for)
        public static bool Logical_Statment(string ourString, string key_word, int tabs)
        {
            ourString = ourString.Trim();
            string[] conditional_statements = { "and", "or", "<", "<=", ">", ">=", "==", "!=" };
            string[] split_at_condition;

            foreach (var condition in conditional_statements)
            {

                if (ourString.Contains(condition))
                {
                    //Console.WriteLine("Logic Worked");
                    //split the string at the condition
                    split_at_condition = ourString.Split(condition);
                    if (condition == "and")
                    {
                        //split ourString at condition and send both through this function again, if BOTH return true then return true
                        return (Logical_Statment(split_at_condition[0], key_word, tabs) && Logical_Statment(split_at_condition[1], key_word, tabs)) ? true : false;
                    }
                    if (condition == "or")
                    {
                        //split ourString at condition and send both through this function again, if EITHER return true then return true
                        return (Logical_Statment(split_at_condition[0], key_word, tabs) || Logical_Statment(split_at_condition[1], key_word, tabs)) ? true : false;

                    }
                    //all other statements get passed to conditional statement function, returns boolean from conditional statement
                    //return Conditional_Statement(split_at_condition, condition);
                    return false;
                }
            }

            //DEBUG PRINTING
            //Console.WriteLine(key_word);
            //Console.WriteLine(ourString);
            //Console.WriteLine(tabs);

            //DEBUG RETURN
            //TODO check value of itself
            //No conditional operators, need to check value itself
            return true;
        }
    }
}
