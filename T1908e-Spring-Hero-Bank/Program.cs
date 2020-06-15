using System;
using T1908e_Spring_Hero_Bank.Controller;

namespace T1908e_Spring_Hero_Bank
{
    internal class Program
    {
        public static void Main(string[] args)
        {
           AccountController accountController = new AccountController();
           accountController.CheckAccountByAccountnumber();
           Console.WriteLine("text 1");
        }
    }
}