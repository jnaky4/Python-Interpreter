using System;
using System.Collections.Generic;
using org.mariuszgromada.math.mxparser;

namespace Interpretator_Missile
{
    internal class Assignment
    {
        //splits the statement based on assignment operator
        //Assignment operators (=, +=, -=, *=, /=, ^=, %=)
        public static void Assignment_Statement(string ourString, string keyword, int tabs, List <(string, string)> strings, List <(string, double)> numbers)
        {
            ourString = ourString.Trim();
            string[] assignment_operators = { "+=", "-=", "*=", "/=", "^=", "%=" };
            string[] split_at_operator;

            foreach (var assignment in assignment_operators)
            {
                if (ourString.Contains(assignment))
                {
                    split_at_operator = ourString.Split(assignment);
                    Assignment_Operator(split_at_operator, assignment, strings, numbers);
                    return;
                }
            }

            //special case for "=" operator
            if (ourString.Contains("=") && !ourString.Contains("==") && !ourString.Contains(">=") && !ourString.Contains("<=") && !ourString.Contains("!="))
            {
                split_at_operator = ourString.Split("=");
                Assignment_Operator(split_at_operator, "=", strings, numbers);
            }
        }

        //handles each of the assignment operators
        public static void Assignment_Operator(string[] split_at_operator, string assignment, List <(string, string)> strings, List <(string, double)> numbers)
        {
            string variable = split_at_operator[0].Trim();
            string value = split_at_operator[1].Trim();
            (string, int) type = getType(variable, strings, numbers);
            //Console.WriteLine("Variable type: " + type.Item1 + " index: " + type.Item2);

            switch (assignment)
            {
                case ("+="):
                    //TODO
                    break;
                case ("-="):
                    //TODO
                    break;
                case ("*="):
                    //TODO
                    break;
                case ("/="):
                    //Console.WriteLine("found /=");
                    //TODO
                    break;
                case ("^="):
                    //TODO
                    break;
                case ("%="):
                    //TODO
                    break;
                case ("="):
                    //TODO
                    //Console.WriteLine("found = ");
                    equals(variable, value, strings, numbers, type);
                    break;
            }
        }

        //gets the type of a variable and the index it is stored in
        public static (string, int) getType(string variable, List<(string, string)> strings, List<(string, double)> numbers)
        {
            int index;
            index = isString(strings, variable);
            if (index != -1)
                return ("string", index);

            index = isDouble(numbers, variable);
            if (index != -1)
                return ("number", index);
            else
                return (null, index);
        }

        //return -1 if the index isn't found, checks to see if variable is saved as a string
        public static int isString(List<(string, string)> strings, string name)
        {
            int index = 0;
            foreach (var variable in strings)
            {
                if (variable.Item1 == name)
                {
                    return index;
                }
                index++;
            }

            return -1;
        }

        //return -1 if the index isn't found, checks to see if variable is saved as a double
        public static int isDouble(List<(string, double)> numbers, string name)
        {
            int index = 0;
            foreach (var variable in numbers)
            {
                if (variable.Item1 == name)
                {
                    return index;
                }
                index++;
            }

            return -1;
        }

        //handles '=' operator
        public static void equals(string variable, string value, List<(string, string)> strings, List<(string, double)> numbers, (string, int) type)
        {
            Expression e = new Expression(value);
            double num = e.calculate();

            if (double.IsNaN(num) == true)
            {
                //new variable needs to be added to the list
                if (type.Item2 == -1)
                {
                    strings.Add((variable, value));
                    return;
                }
                else
                {
                    //matching types need to update old value
                    if (type.Item1 == "string")
                    {
                        strings[type.Item2] = (variable, value);
                        return;
                    }
                    if (type.Item1 == "number")
                    {
                        numbers.Remove(numbers[type.Item2]);
                        strings.Add((variable, value));
                        return;
                    }
                }

                strings.Add((variable, value));
                //Console.WriteLine("String " + variable + " = " + value);
                return;
            }
            else
            {
                //new variable needs to be added to the list
                if (type.Item2 == -1)
                {
                    numbers.Add((variable, num));
                    return;
                }
                else
                {
                    if (type.Item1 == "number")
                    {
                        numbers[type.Item2] = (variable, num);
                        return;
                    }
                    if (type.Item1 == "string")
                    {
                        strings.Remove(strings[type.Item2]);
                        numbers.Add((variable, num));
                        return;
                    }
                }
                //Console.WriteLine("Number " + variable + " = " + num);
                return;
            }
        }
    }
}
