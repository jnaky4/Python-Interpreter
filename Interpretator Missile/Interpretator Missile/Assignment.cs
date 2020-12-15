using System;
using System.Collections.Generic;

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
            if (ourString.Contains("=") && !ourString.Contains("=="))
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
                    Console.WriteLine("found /=");
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
                    Console.WriteLine("found = ");
                    Console.WriteLine("Variable: " + variable + value);
                    equals(variable, value, strings, numbers);
                    break;
            }
        }

        //gets the name of the variable
        public static string getName(string[] input)
        {
            return input[0].TrimEnd();
        }

        //gets the type of a variable entered into the command line
        public static string getType(string assignment)
        {
            //string assignment = input[1].Trim();

            string type;
            if (assignment.Contains("\""))
                type = "String";
            else
                type = "Number";

            return type;
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

        public static void plusEquals(string[] input, List<(string, string)> strings, List<(string, double)> numbers)
        {
            string name = getName(input);

            int index;

            index = isString(strings, name);
        }

        //handles '=' operator
        public static void equals(string variable, string value, List<(string, string)> strings, List<(string, double)> numbers)
        {
            if (getType(value) == "String")
            {
                strings.Add((variable, value));
                Console.WriteLine("Number " + variable + " = " + value);
                return;
            }
            else
            {
                double num = Convert.ToDouble(value);

                numbers.Add((variable, num));

                Console.WriteLine("Number " + variable + " = " + value);

                return;
            }
        }
    }

    //handles '=' operator
    /*static void equals1(string[] input, List<(string, string)> strings, List<(string, double)> numbers)
    {
        string name = getName(input);

        string value = input[1].Trim();

        int index;

        if (getType(input) == "String")
        {
            value = value.Trim('\"');

            index = isString(strings, name);

            //the variable can be found in the list, update it
            if (index != -1)
            {
                Console.WriteLine("String already in list");
                Console.WriteLine("Old String " + name + " " + strings[index].Item2);
                Console.WriteLine("New String " + name + " " + value);
                strings[index] = (name, value);
                return;
            }

            Console.WriteLine("String " + name + " " + value);
            strings.Add((name, value)); //need to create a new variable
            return;
        }
        else
        {
            double num = Convert.ToDouble(value);

            index = isDouble(numbers, name);

            //the variable can be found in the list, update it
            if (index != -1)
            {
                Console.WriteLine("Double already in list");
                Console.WriteLine("Old Double " + name + " " + numbers[index].Item2);
                Console.WriteLine("New Double " + name + " " + num);
                numbers[index] = (name, num);
                return;
            }

            Console.WriteLine("Double " + name + " " + num);
            numbers.Add((name, num));   //need to create a new variable
            return;
        }
    }*/
}
