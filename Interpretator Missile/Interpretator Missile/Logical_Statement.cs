using System;
using System.Collections.Generic;
using System.Text;
using static Interpretator_Missile.Assignment;

namespace Interpretator_Missile
{
    class Logical_Statement
    {
        //Logical check (if, elif, else, while, for)
        public static bool Logical_Statment(string ourString, string key_word, int tabs, List<(string, double)> numbers, List<(string, string)> strings)
        {
            ourString = ourString.Trim();
            string[] conditional_statements = { "and", "or", "<=", "<", ">=", ">", "==", "!=" };
            string[] split_at_condition;

            foreach (var condition in conditional_statements)
            {

                if (ourString.Contains(condition))
                {
                    //Console.WriteLine("Logic Worked");
                    //split the string at the condition
                    ourString = ourString.Replace("(", "");
                    ourString = ourString.Replace(")", "");
                    ourString = ourString.Trim();
                    split_at_condition = ourString.Split(condition);
                    if (condition == "and")
                    {
                        //split ourString at condition and send both through this function again, if BOTH return true then return true
                        return (Logical_Statment(split_at_condition[0], key_word, tabs, numbers, strings) && Logical_Statment(split_at_condition[1], key_word, tabs, numbers, strings)) ? true : false;
                    }
                    if (condition == "or")
                    {
                        //split ourString at condition and send both through this function again, if EITHER return true then return true
                        return (Logical_Statment(split_at_condition[0], key_word, tabs, numbers, strings) || Logical_Statment(split_at_condition[1], key_word, tabs, numbers, strings)) ? true : false;

                    }
                    //all other statements get passed to conditional statement function, returns boolean from conditional statement
                    return Conditional_Statement(split_at_condition, condition, numbers, strings);
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
        public static bool Conditional_Statement(string[] split, string condition, List<(string, double)> numbers, List<(string, string)> strings)
        {
            bool statement = false;
            double x = 0, y = 0;

            if (double.IsNaN(double.Parse(split[0])) == false) x = double.Parse(split[0]);
            else
            {
                foreach (var s in numbers)
                {
                    if (split[0].Contains(s.Item1))
                    {
                        x = s.Item2;
                    }
                }
            }
            if (double.IsNaN(double.Parse(split[1])) == false) y = double.Parse(split[1]);
            else
            {
                foreach (var s in numbers)
                {
                    if (split[1].Contains(s.Item1))
                    {
                        y = s.Item2;
                    }
                }
            }

            switch(condition)
            {
                case "<":
                    statement = x < y;
                    break;
                case "<=":
                    statement = x <= y;
                    break;
                case ">":
                    statement = x > y;
                    break;
                case ">=":
                    statement = x > y;
                    break;
                case "==":
                    statement = x == y;
                    break;
                case "!=":
                    statement = x != y;
                    break;
                default:
                    break;
            }
            Console.WriteLine("x: " + x + "y: " + y + "condition: " + condition + statement);
            return statement;
        }
    }
}
