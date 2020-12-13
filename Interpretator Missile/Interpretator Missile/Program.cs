﻿using org.mariuszgromada.math.mxparser;
using System.Collections.Generic;
using System;

namespace Interpretator_Missile
{
    public static class Program
    {
        //variable containers (variable name, variable value)
        static List<(string, double)> numbers = new List<(string, double)>();
        static List<(string, string)> strings = new List<(string, string)>();

        static void Main(string[] args)
        {
            //Strip commenting
            //SWITCH: if, while, else, elif, for  ----> all have terminator ':'
            //and or statements to split on
            //
            /*
                Steps for Breaking down a line of code:
                1) strip commenting
                2) strip on key words
                3) 
             
             */


            string[] key_words = {"while", "if", "elif", "else", "for"};

            Console.WriteLine("Hi! Please use this interpeter to write python code. Use the command \"exit\" to quit the program.");
            int indentation = 0;

            while (true)
            {



                //Write To Console****************
                Console.Write(">>>");
                for (int i = 0; i < indentation; i++)
                {
                    Console.Write(".");
                }
                for (int i = 0; i < indentation; i++)
                {
                    Console.Write("\t");
                }
                Console.Write(" ");
                //formats the command line correctly for us
                //**********************************'


                //START READING*********************
                string ourString = Console.ReadLine();


                //COMMENTING***********************
                //gives us our string that the person wrote in
                if (ourString.Contains("#"))
                {
                    ourString = ourString.Substring(0, ourString.IndexOf("#"));
                }
                //Comment functinallity
                //**********************************




                //TODO NEEDS TO COUNT TABS
                //CONDITIONAL STATEMENT*************
                //while, for, if, elif, else
                foreach (var s in key_words)
                {
                    if (ourString.Contains(s))
                    {
                        if (!(ourString[ourString.Length-1] == ':'))
                        {
                            //ERROR
                            Console.WriteLine("Missing :");
                        }
                        else
                        {
                            //Grab the keyword
                            string key_word = s;
                            int length = ourString.IndexOf(s) + s.Length;
                            //Split between keyword and : 
                            string comparison = ourString.Substring(length);
                            comparison = comparison.Substring(0, comparison.Length - 1);
                            Logical_Statment(comparison, s, 0);
                            
                        }

                    }
                }

                //EXIT*****************************
                if (ourString == "exit") break;
                //Ability to exit from the program
                //**********************************



                //TAB COUNTING**********************
                foreach(var s in ourString)
                {
                    if (s.CompareTo('\t') == 0) indentation++;
                }
                if (ourString.Length == 0) indentation = 0;
                //Gives us correct indentation
                //**********************************


                //PRINTING*****************************
                if (ourString.Contains("print(\"") && ourString.Contains("\")"))
                {
                    ourString = ourString.Substring(ourString.IndexOf("print(\""), ourString.IndexOf("\")"));
                    ourString = ourString.Substring(ourString.IndexOf("\"") + 1);
                    Console.WriteLine(ourString);
                }
                //Print function
                //*************************************


                //MATH*********************************
                Expression e = new Expression(ourString);
                double x = e.calculate();
                
                
                if (double.IsNaN(x) == false)
                {
                    if (ourString.Contains("<") || ourString.Contains("<=") || ourString.Contains(">") || ourString.Contains(">=") || ourString.Contains("==") || ourString.Contains("!="))
                    {
                        if (x == 0) Console.WriteLine("False");
                        else if (x == 1) Console.WriteLine("True");
                        else Console.WriteLine(x.ToString());
                    }
                    else Console.WriteLine(x.ToString());
                }
                else if (ourString.Contains("%"))
                {
                    ourString = ourString.Replace('%', '#');
                    Expression f = new Expression(ourString);
                    double y = f.calculate();
                    if (double.IsNaN(y) == false)
                    {
                        Console.WriteLine(y.ToString());
                    }
                }
                //Most of arithmatic is working
                //******************************************


            }


        }

        //Assignment operators (=, +=, -=, *=, /=, ^=, %=) 
        static void Assignment_Operator(string ourString, string key_word, int tabs)
        {
            string[] assignment_split;
            switch (ourString)
            {

                case string a when a.Contains("=="):
                    Console.WriteLine("found ==");
                    assignment_split = ourString.Split("==");

                    break;
                case string b when b.Contains("+="):
                    Console.WriteLine("found +=");
                    assignment_split = ourString.Split("+=");
                    //TODO CALL ARITHMATIC
                    break;
                case string c when c.Contains("-="):
                    Console.WriteLine("found -=");
                    assignment_split = ourString.Split("-=");
                    //TODO CALL ARITHMATIC
                    break;
                case string d when d.Contains("*="):
                    Console.WriteLine("found *=");
                    assignment_split = ourString.Split("*=");
                    //TODO CALL ARITHMATIC
                    break;
                case string f when f.Contains("/="):
                    Console.WriteLine("found /=");
                    assignment_split = ourString.Split("/=");
                    //TODO CALL ARITHMATIC
                    break;
                case string g when g.Contains("^="):
                    assignment_split = ourString.Split("^=");
                    //TODO CALL ARITHMATIC
                    Console.WriteLine("found ^=");
                    break;
                case string h when h.Contains("%="):
                    assignment_split = ourString.Split("%=");
                    //TODO CALL ARITHMATIC
                    Console.WriteLine("found %=");
                    break;
                case string i when i.Contains("="):
                    assignment_split = ourString.Split("=");
                    //TODO CALL ASSIGNMENT
                    Console.WriteLine("found =");
                    equals(assignment_split, strings, numbers);
                    break;
                default: { } break;
            }
        return;
        }

        //Conditional statements (<, <=, >, >=, ==, !=) 
        //split_at_condition is an array[2], split at condition
        static bool Conditional_Statement(string[] split_at_condition, string condition)
        {
            split_at_condition[0] = split_at_condition[0].Trim();
            split_at_condition[1] = split_at_condition[1].Trim();

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
                    return split_at_condition[0] == split_at_condition[1] ? true: false;
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
        
        //Logical check (if, elif, else, while, for)
        static bool Logical_Statment(string ourString, string key_word, int tabs)
        {
            ourString = ourString.Trim();
            string[] conditional_statements = {"and", "or", "<", "<=", ">", ">=", "==", "!=" };
            string[] split_at_condition;

            foreach(var condition in conditional_statements)
            {
                
                if(ourString.Contains(condition)){
                    //Console.WriteLine("Logic Worked");
                    //split the string at the condition
                    split_at_condition = ourString.Split(condition);
                    if(condition == "and")
                    {
                        //split ourString at condition and send both through this function again, if BOTH return true then return true
                        return (Logical_Statment(split_at_condition[0], key_word, tabs) && Logical_Statment(split_at_condition[1], key_word, tabs)) ? true : false;
                    }
                    if(condition == "or")
                    {
                        //split ourString at condition and send both through this function again, if EITHER return true then return true
                        return (Logical_Statment(split_at_condition[0], key_word, tabs) || Logical_Statment(split_at_condition[1], key_word, tabs)) ? true : false;

                    }
                    //all other statements get passed to conditional statement function, returns boolean from conditional statement
                    return Conditional_Statement(split_at_condition, condition) ? true : false;

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

        //while and for Loops 
        //TODO
        static bool LOOP(string ourString, string key_word, int tabs)
        {
            return true;
        }

        //gets the type of a variable entered into the command line
        static string getType(string[] input)
        {
            string assignment = input[1].TrimStart(' ').TrimEnd(' ');

            string type;
            if (assignment.Contains("\""))
                type = "String";
            else
                type = "Number";

            return type;
        }

        //return -1 if the index isn't found, checks to see if variable is saved as a string
        static int isString(List<(string, string)> strings, string name)
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
        static int isDouble(List<(string, double)> numbers, string name)
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
        static void equals(string[] input, List<(string, string)> strings, List<(string, double)> numbers)
        {
            string name;
            name = input[0].TrimEnd(' ');

            string value = input[1].TrimStart(' ');
            value = value.TrimEnd(' ');

            int index;

            if (getType(input) == "String")
            {
                value = value.TrimStart('\"').TrimEnd('\"');

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
        }
    }
}
