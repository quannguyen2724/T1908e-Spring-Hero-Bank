using System;
using System.Text;
using T1908e_Spring_Hero_Bank.Controller;
using T1908e_Spring_Hero_Bank.View;

namespace T1908e_Spring_Hero_Bank
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var generateMenu = new GenerateMenu();
            generateMenu.GetMenu();
        }
    }
}