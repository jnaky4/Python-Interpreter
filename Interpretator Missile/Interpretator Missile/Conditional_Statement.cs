using System;
using System.Collections.Generic;
using System.Text;

namespace Interpretator_Missile
{
    class Conditional_Statements
    {
        //Conditional statements (<, <=, >, >=, ==, !=)
        //split_at_condition is an array[2], split at condition
        public static bool Conditional_Statement(string[] split, string condition, List<(string, double)> numbers, List<(string, string)> strings)
        {
            bool statement = false;
            double x = double.NaN, y = double.NaN;
            bool check = false;
            check = double.TryParse(split[0], out x);
            if (double.IsNaN(x) == false && check == true) x = double.Parse(split[0]);
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
            check = false;
            check = double.TryParse(split[1], out y);
            if (double.IsNaN(y) == false && check == true) y = double.Parse(split[1]);
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

            switch (condition)
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
                    statement = x >= y;
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
            return statement;
        }
    }
}
