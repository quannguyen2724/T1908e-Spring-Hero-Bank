using System;
using MySql.Data.MySqlClient;
using T1908e_Spring_Hero_Bank.Entity;
using T1908e_Spring_Hero_Bank.Helper;
using T1908e_Spring_Hero_Bank.Model;

namespace T1908e_Spring_Hero_Bank.Controller
{
    public class AccountController
    {
        private AccountModel _accountModel = new AccountModel();

        public void CheckAccountByUsername()
        {
            Console.WriteLine("Enter username: ");
            var username = Console.ReadLine();
            var account = _accountModel.GetAccountByUsername(username);
            if (account != null)
            {
                Console.WriteLine(account.ToString());
                return;
            }

            Console.WriteLine("Account not found");
        }

        public void CheckAccountByPhonenumber()
        {
            Console.WriteLine("Enter phone number: ");
            var phone = Console.ReadLine();
            var account = _accountModel.GetAccountByPhone(phone);
            if (account != null)
            {
                Console.WriteLine(account.ToString());
                return;
            }

            Console.WriteLine("Account not found");
        }

        public void CheckAccountByAccountnumber()
        {
            Console.WriteLine("Enter account number: ");
            var accountNumber = Console.ReadLine();
            var account = _accountModel.GetAccountByAccountnumber(accountNumber);
            if (account != null)
            {
                Console.WriteLine(account.ToString());
                return;
            }

            Console.WriteLine("Account not found");
        }

        public void Register()
        {
            throw new NotImplementedException();
        }

        public Account Login()
        {
            throw new NotImplementedException();
        }
    }
}