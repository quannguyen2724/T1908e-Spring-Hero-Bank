using System;
using T1908e_Spring_Hero_Bank.Helper;
using T1908e_Spring_Hero_Bank.Model;

namespace T1908e_Spring_Hero_Bank.Controller
{
    public class TransactionController
    {
        private InputHelper _inputHelper;

        public void Deposit()
        {
            var transactionModel = new TransactionModel();
            Console.WriteLine("Enter the amount: ");
            var amount = double.Parse(Console.ReadLine());
            transactionModel.Deposit(AccountController.currentAccount.AccountNumber, amount);
        }

        public void Withdraw()
        {
            var transactionModel = new TransactionModel();
            Console.WriteLine("Enter the amount: ");
            var amount = double.Parse(Console.ReadLine());
            transactionModel.Withdraw(AccountController.currentAccount.AccountNumber, amount);
        }

        public void Transfer()
        {
            var transactionModel = new TransactionModel();
            Console.WriteLine("Enter receiver account number: ");
            var receiverAccount = Console.ReadLine();
            Console.WriteLine("Enter the amount: ");
            var amount = double.Parse(Console.ReadLine());
            transactionModel.Transfer(AccountController.currentAccount.AccountNumber, receiverAccount, amount);
        }
    }
}