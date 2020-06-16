using System;
using T1908e_Spring_Hero_Bank.Controller;
using T1908e_Spring_Hero_Bank.View;

namespace T1908e_Spring_Hero_Bank
{
    internal class Program
    {           
        private static IMenuGenerator _iMenuGenerator = new GuestView();

        public static void Main(string[] args)
        {
            _iMenuGenerator.GenerateMenu(null);
        }
    }
}