﻿using org.mariuszgromada.math.mxparser;
using System;

namespace Interpretator_Missile
{
    public static class Program
    {
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



                //NEEDS TO COUNT TABS
                //CONDITIONAL STATEMENT*************
                //while, for, if, elif, else
                foreach(var s in key_words)
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
                //***********************************


                    /*                switch (ourString)
                                    {
                                        case string a when a.Contains("while"):
                                            while_string = ourString.Substring(ourString.IndexOf("while"), );
                                            break;
                                        case string b when b.Contains("for"):
                                            break;
                                        case string c when c.Contains("if"):
                                            break;
                                        case string d when d.Contains("elif"):
                                            break;
                                        case string f when f.Contains("else"):
                                            break;
                                        case string g when g.Contains("print"):
                                            break;
                                    }*/









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

/*                if (ourString.Contains("=") || ourString.Contains("==") )
                {
                    split by spaces into array
                    string[] var_split = ourString.Split('=');
                    foreach(var s in var_split)
                    {

                       } 
                }*/

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
                    break;
                case string a when a.Contains("-="):
                    Console.WriteLine("found -=");
                    assignment_split = ourString.Split("-=");
                    break;
                case string a when a.Contains("*="):
                    Console.WriteLine("found *=");
                    assignment_split = ourString.Split("+=");
                    break;
                case string a when a.Contains("/="):
                    Console.WriteLine("found /=");
                    assignment_split = ourString.Split("+=");
                    break;
                case string a when a.Contains("^="):
                    assignment_split = ourString.Split("+=");
                    Console.WriteLine("found ^=");
                    break;
                case string a when a.Contains("%="):
                    assignment_split = ourString.Split("%=");
                    Console.WriteLine("found %=");
                    break;
                case string a when a.Contains("="):
                    assignment_split = ourString.Split("=");
                    Console.WriteLine("found =");
                    break;
                default: { } break;
            }
        return;
        }

        //Conditional statements (<, <=, >, >=, ==, !=) 
        static bool Conditional_Statement(string[] split_at_condition, string condition)
        {
            Console.WriteLine(condition);
            Console.WriteLine(split_at_condition[0]);
            Console.WriteLine(split_at_condition[1]);
            return true;
        }
        
        //Logical check (if, elif, else, while, for)
        static bool Logical_Statment(string ourString, string key_word, int tabs)
        {
            string[] conditional_statements = { "<", "<=", ">", ">=", "==", "!=" };
            string[] split_at_condition;
            foreach(var condition in conditional_statements)
            {
                if(ourString.Contains(condition)){
                    Console.WriteLine("Logic Worked");
                    split_at_condition = ourString.Split(condition);
                    if (Conditional_Statement(split_at_condition, condition))
                    {
                        //CALL BLOCK STATEMENT METHOD
                        //Console.WriteLine("Logic Worked");
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            //DEBUG PRINTING
            //Console.WriteLine(key_word);
            //Console.WriteLine(ourString);
            //Console.WriteLine(tabs);
            //DEBUG CONDITION
            return true;
        }

        //while and for Loops 
        static bool LOOP(string ourString, string key_word, int tabs)
        {
            return true;
        }
    }
}
