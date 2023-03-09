using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace PROG201_Crafting_Project
{
    internal static class Utility
    {
        public static Random Rand = new Random();

        public static void CloseApp() => Environment.Exit(0);

        public static string ConvertToLower(string str) { return str.ToLower(); }

        public static void Print(string str) => WriteLine(str);


        public static string InputStr()
        {
            string input = ReadLine();
            char[] char_array = input.ToArray();

            bool digit = false;

            foreach (char c in char_array)
            {
                if (Char.IsDigit(c))
                {
                    digit = true;
                }
            }

            if (digit)
            {
                Print("-ERROR- non string -- re enter");
                return InputStr();
            }
            else
            {
                return input;
            }
        }

        public static int InputInt()
        {

            string input = ReadLine();
            int int_input;
            char[] char_array = input.ToArray();

            bool digit = true;

            foreach (char c in char_array)
            {
                if (!Char.IsDigit(c))
                {
                    digit = false;
                }
            }

            if (digit)
            {
                int_input = Convert.ToInt32(input);
                return int_input;
            }
            else
            {
                Print("-ERROR- non int -- re enter");
                return InputInt();
            }
        }



    }
}
