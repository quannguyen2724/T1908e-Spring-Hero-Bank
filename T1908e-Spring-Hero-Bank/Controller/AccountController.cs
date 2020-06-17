using System;
using System.Security.Principal;
using MySql.Data.MySqlClient;
using T1908e_Spring_Hero_Bank.Entity;
using T1908e_Spring_Hero_Bank.Helper;
using T1908e_Spring_Hero_Bank.Model;

namespace T1908e_Spring_Hero_Bank.Controller
{
    public class AccountController
    {
        public static Account currentAccount;
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
// Create Registry Method
        public void Registry()
        {
            try
            {
                var account = new Account();
                Console.WriteLine("Enter your username: ");
                account.Username = Console.ReadLine();
                Console.WriteLine("Enter your password: ");
                account.PasswordHash = Console.ReadLine();
                _accountModel.Save(account);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong, please do it again!");
                throw;
            }
        }
// Create Login Method
        public Account Login()
        {
            try 
            {
                Console.WriteLine("Login...");
                Console.WriteLine("Please Enter Your Username: ");
                var username = Console.ReadLine();
                Console.WriteLine("Please Enter Your Password: ");
                var password = Console.ReadLine();
                var account = _accountModel.GetAccountByUsername(username);
                if (account !=null
                    && PasswordHelper.ComparePassword(password, account.Salt, account.PasswordHash))
                {
                    return account;
                }

                return;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
                throw;
            }
        }

        public void List()
        {
            foreach (var account in _accountModel.GetList())
            {
                Console.WriteLine(account.ToString());
            }
        }
    }
}