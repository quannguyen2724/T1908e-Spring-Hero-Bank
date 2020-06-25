﻿using System;
using T1908e_Spring_Hero_Bank.Model;

namespace T1908e_Spring_Hero_Bank.Controller
{
    public class TransactionController
    {
        private TransactionModel _transactionModel = new TransactionModel();

        public void Deposit()
        {
            Console.WriteLine("Enter the amount: ");
            var amount = double.Parse(Console.ReadLine());
            _transactionModel.Deposit(AccountController.currentAccount.AccountNumber, amount);
        }

        public void Withdraw()
        {
            Console.WriteLine("Enter the amount: ");
            var amount = double.Parse(Console.ReadLine());
            _transactionModel.Withdraw(AccountController.currentAccount.AccountNumber, amount);
        }

        public void Transfer()
        {
            Console.WriteLine("Enter receiver account number: ");
            var receiverAccount = Console.ReadLine();
            Console.WriteLine("Enter the amount: ");
            var amount = double.Parse(Console.ReadLine());
            _transactionModel.Transfer(AccountController.currentAccount.AccountNumber, receiverAccount, amount);
        }
    }
}