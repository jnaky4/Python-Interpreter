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
            Expression e = new Expression(value);
            double num = e.calculate();
            //Console.WriteLine("Variable type: " + type.Item1 + " index: " + type.Item2);

            switch (assignment)
            {
                case ("+="):
                    PlusEquals(variable, value, num, strings, numbers, type);
                    break;
                case ("-="):
                    MinusEquals(variable, value, num, strings, numbers, type);
                    break;
                case ("*="):
                    MultiplyEquals(variable, value, num, strings, numbers, type);
                    break;
                case ("/="):
                    DivideEquals(variable, value, num, strings, numbers, type);
                    break;
                case ("^="):
                    XOREquals(variable, value, num, strings, numbers, type);
                    break;
                case ("%="):
                    RemainderEquals(variable, value, num, strings, numbers, type);
                    break;
                case ("="):
                    Equals(variable, value, num, strings, numbers, type);
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

        public static void RemainderEquals(string variable, string value, double num, List<(string, string)> strings, List<(string, double)> numbers, (string, int) type)
        {
            if (double.IsNaN(num) == true)
            {
                Console.WriteLine("Error: unsuported operand type for %=");
                return;
            }
            else
            {
                if (type.Item1 == "number")
                {
                    num = numbers[type.Item2].Item2 % num;
                    Equals(variable, value, num, strings, numbers, type);
                }
                else
                    Console.WriteLine("Error: unsupported operand type for %=");
            }
        }

        public static void XOREquals(string variable, string value, double num, List<(string, string)> strings, List<(string, double)> numbers, (string, int) type)
        {
            if (double.IsNaN(num) == true)
            {
                Console.WriteLine("Error: unsuported operand type for ^=");
                return;
            }
            else
            {
                if (type.Item1 == "number" && num % 1 == 0 && numbers[type.Item2].Item2 % 1 == 0)
                {
                    num = (int) numbers[type.Item2].Item2 ^ (int) num;
                    Equals(variable, value, num, strings, numbers, type);
                }
                else
                    Console.WriteLine("Error: unsupported operand type for ^=");
            }
        }

        public static void DivideEquals(string variable, string value, double num, List<(string, string)> strings, List<(string, double)> numbers, (string, int) type)
        {
            if (double.IsNaN(num) == true)
            {
                Console.WriteLine("Error: unsuported operand type for /=");
                return;
            }
            else
            {
                if (type.Item1 == "number")
                {
                    num = numbers[type.Item2].Item2 / num;
                    Equals(variable, value, num, strings, numbers, type);
                }
                else
                    Console.WriteLine("Error: unsupported operand type for /=");
            }
        }

        public static void MultiplyEquals(string variable, string value, double num, List<(string, string)> strings, List<(string, double)> numbers, (string, int) type)
        {
            if (double.IsNaN(num) == true)
            {
                Console.WriteLine("Error: unsuported operand type for *=");
                return;
            }
            else
            {
                if (type.Item1 == "number")
                {
                    num = numbers[type.Item2].Item2 * num;
                    Equals(variable, value, num, strings, numbers, type);
                }
                else
                    Console.WriteLine("Error: unsupported operand type for *=");
            }
        }

        public static void MinusEquals(string variable, string value, double num, List<(string, string)> strings, List<(string, double)> numbers, (string, int) type)
        {
            if (double.IsNaN(num) == true)
            {
                Console.WriteLine("Error: unsuported operand type for -=");
                return;
            }
            else
            {
                if (type.Item1 == "number")
                {
                    num = numbers[type.Item2].Item2 - num;
                    Equals(variable, value, num, strings, numbers, type);
                }
                else
                    Console.WriteLine("Error: unsupported operand type for -=");
            }
        }

        //handles '+=' for strings and numbers
        public static void PlusEquals(string variable, string value, double num, List<(string, string)> strings, List<(string, double)> numbers, (string, int) type)
        {
            if (double.IsNaN(num) == true)
            {
                if (type.Item1 == "string")
                {
                    value = strings[type.Item2].Item2 + value;
                    Equals(variable, value, num, strings, numbers, type);
                }
                else
                    Console.WriteLine("Error: variable not found");
            }
            else
            {
                if (type.Item1 == "number")
                {
                    num += numbers[type.Item2].Item2;
                    Equals(variable, value, num, strings, numbers, type);
                }
                else
                    Console.WriteLine("Error: variable not found");
                
            }
        }

        //handles '=' operator
        public static void Equals(string variable, string value, double num, List<(string, string)> strings, List<(string, double)> numbers, (string, int) type)
        {
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
                    //differing data type need to change typd of variable
                    if (type.Item1 == "number")
                    {
                        numbers.Remove(numbers[type.Item2]);
                        strings.Add((variable, value));
                        return;
                    }
                }

                strings.Add((variable, value));
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
                    //matching type need to update old value
                    if (type.Item1 == "number")
                    {
                        numbers[type.Item2] = (variable, num);
                        return;
                    }
                    //differing type need to change data type of variable
                    if (type.Item1 == "string")
                    {
                        strings.Remove(strings[type.Item2]);
                        numbers.Add((variable, num));
                        return;
                    }
                }
                return;
            }
        }
    }
}
