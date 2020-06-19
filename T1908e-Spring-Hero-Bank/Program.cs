using System;
using System.Threading;
using T1908e_Spring_Hero_Bank.Controller;
using T1908e_Spring_Hero_Bank.Entity;
using T1908e_Spring_Hero_Bank.Helper;
using T1908e_Spring_Hero_Bank.Model;
using T1908e_Spring_Hero_Bank.View;

namespace T1908e_Spring_Hero_Bank
{
    internal class Program
    {
        private static IMenuGenerator _iMenuGenerator = new GuestView();
        private static AccountModel _accountModel = new AccountModel();
        private static PasswordHelper _passwordHelper = new PasswordHelper();
        private static AccountController _accountController = new AccountController();
        private static TransactionModel _transactionModel = new TransactionModel();
        private static TransactionController _transactionController = new TransactionController();
        public static void Main(string[] args)
        {
            _iMenuGenerator.GenerateMenu(null);
        }
    }
}