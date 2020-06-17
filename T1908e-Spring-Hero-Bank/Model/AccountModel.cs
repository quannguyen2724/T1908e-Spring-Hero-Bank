using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using T1908e_Spring_Hero_Bank.Controller;
using T1908e_Spring_Hero_Bank.Entity;
using T1908e_Spring_Hero_Bank.Helper;

namespace T1908e_Spring_Hero_Bank.Model
{
    public class AccountModel
    {
        private Account _account;

        public void Save(Account _account)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand(
                $"insert into accounts (accountNumber,balance,username,passwordHash, salt,role,fullname,phone,email,status) values ('{_account.AccountNumber}'," +
                $"'{_account.Balance}','{_account.Username}','{_account.PasswordHash}','{_account.Salt}','{_account.Role}','{_account.Fullname}','{_account.Phone}'," +
                $"{_account.Email},'{_account.Status}')", cnn);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Success!");
        }
        public Account GetAccountByUsername(string username)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand($"select * from shbaccount where username = '{username}'", cnn);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                _account = new Account()
                {
                    AccountNumber = reader.GetString("accountNumber"),
                    Balance = reader.GetDouble("balance"),
                    Username = reader.GetString("username"),
                    PasswordHash = reader.GetString("passwordHash"),
                    Salt = reader.GetString("salt"),
                    Email = reader.GetString("email"),
                    Phone = reader.GetString("phone"),
                    Fullname = reader.GetString("fullname"),
                    Status = (AccountStatus) reader.GetInt32("status")
                };
            }
            cnn.Close();
            return _account;
        }


        public Account GetAccountByPhone(string phone)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand($"select * from shbaccount where phone = '{phone}'", cnn);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                _account = new Account()
                {
                    AccountNumber = reader.GetString("accountNumber"),
                    Balance = reader.GetDouble("balance"),
                    Username = reader.GetString("username"),
                    PasswordHash = reader.GetString("passwordHash"),
                    Salt = reader.GetString("salt"),
                    Email = reader.GetString("email"),
                    Phone = reader.GetString("phone"),
                    Fullname = reader.GetString("fullname"),
                    Status = (AccountStatus) reader.GetInt32("status")
                };
            }
            cnn.Close();
            return _account;
        }

        public Account GetAccountByAccountnumber(string accountNumber)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand($"select * from shbaccount where accountNumber = '{accountNumber}'", cnn);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                _account = new Account()
                {
                    AccountNumber = reader.GetString("accountNumber"),
                    Balance = reader.GetDouble("balance"),
                    Username = reader.GetString("username"),
                    PasswordHash = reader.GetString("passwordHash"),
                    Salt = reader.GetString("salt"),
                    Email = reader.GetString("email"),
                    Phone = reader.GetString("phone"),
                    Fullname = reader.GetString("fullname"),
                    Status = (AccountStatus) reader.GetInt32("status")
                };
            }
            cnn.Close();
            return _account;
        }

        public List<Account> GetList()
        {
            List<Account> list = new List<Account>();
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand("select * from accounts",cnn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Account()
                {
                    AccountNumber = reader.GetString("accountNumber"),
                    Balance = reader.GetInt32("balance"),
                    Username = reader.GetString("username"),
                    PasswordHash = reader.GetString("passwordHash"),
                    Email = reader.GetString("email"),
                    Role = reader.GetInt32("role"),
                    Fullname = reader.GetString("fullname"),
                    Phone = reader.GetString("phone")


                });
            }
            cnn.Close();
            return list;
        }
    }
}