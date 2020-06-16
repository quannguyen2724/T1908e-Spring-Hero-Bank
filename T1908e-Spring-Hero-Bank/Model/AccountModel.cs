﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using T1908e_Spring_Hero_Bank.Entity;
using T1908e_Spring_Hero_Bank.Helper;

namespace T1908e_Spring_Hero_Bank.Model
{
    public class AccountModel
    {
        private Account _account;

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

        public List<Account> GetListAccount(string str, string str1)
        {
            List<Account> list = new List<Account>();
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            MySqlCommand cmd = new MySqlCommand("");
            switch (str)
            {
                case null:
                    cmd = new MySqlCommand($"select * from Account Where Role !=1", cnn);
                    break;
                case "UserName":
                    cmd = new MySqlCommand($"select * from Account where {str} = '{str1}'", cnn);
                    break;
            }

            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                list.Add(new Account()
                {
                    AccountNumber = reader.GetString("AccountNumber"),
                    Balance = reader.GetDouble("Balance"),
                    Username = reader.GetString("Username"),
                    PasswordHash = reader.GetString("PasswordHash"),
                    Salt = reader.GetString("Salt"),
                    Email = reader.GetString("Email"),
                    Phone = reader.GetString("Phone"),
                    Fullname = reader.GetString("Fullname"),
                    Role = (Role) reader.GetInt32("Role"),
                    Status = (AccountStatus) reader.GetInt32("Status")
                });
            }

            cnn.Close();
            return list;
        }

        public bool CreateAccount(Account account)
        {
            try
            {
                var cnn = ConnectionHelper.GetConnection();
                cnn.Open();
                // Console.WriteLine($"INSERT INTO Account( Username, PasswordHash, Salt, FullName, Phone, Email, Role, Status, CreateDate) VALUES ('{account.Username}','{account.PasswordHash}', '{account.Salt}','{account.Fullname}','{account.Phone}','{account.Email}',{(int)account.Role},{(int)account.Status},'{account.CreateDate.ToString("yyyy-MM-dd HH:mm:ss")}'");
                MySqlCommand cmd = new MySqlCommand(
                    $"INSERT INTO Account( Username, PasswordHash, Salt, FullName, Phone, Email, Role, Status, CreateDate) VALUES ('{account.Username}','{account.PasswordHash}','{account.Salt}','{account.Fullname}','{account.Phone}','{account.Email}','{(int) account.Role}','{(int) account.Status}','{account.CreateDate.ToString("yyyy-MM-dd HH:mm:ss")}')",
                    cnn);
                cmd.ExecuteReader();
                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}