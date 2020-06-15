using System;

namespace T1908e_Spring_Hero_Bank.Helper
{
    public class InputHelper
    {
        public string ValidateString(string note)
        {
            string str;
            Console.WriteLine(note);
            while (true)
            {
                str = Console.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    Console.WriteLine("Can't be empty! Input once more!");
                }
                else
                {
                    break;
                }
            }
            return str;
        }
    }
}