using System;
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

        public List<Account> GetListAccount(string? str, string? str1)
        {
            List<Account> list = new List<Account>();
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            MySqlCommand cmd;
            switch (str)
            {
                case null:
                    cmd = new MySqlCommand($"select * from Account Where Role !=1", cnn);
                    break;
                case "UserName":
                    cmd = new MySqlCommand($"select * from Account where {str} = '{str1}'", cnn);
                    break;
                default:
                    cmd= new MySqlCommand($"select * from Account where {str} LIKE '%{str1}%'", cnn);
                    break;
            }

            var reader = cmd.ExecuteReader();
            while (reader.Read())
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
                    $"INSERT INTO Account( Username, PasswordHash, Salt, FullName, Phone, Email, Role, Status, CreateDate, Balance) VALUES ('{account.Username}','{account.PasswordHash}','{account.Salt}','{account.Fullname}','{account.Phone}','{account.Email}','{(int) account.Role}','{(int) account.Status}','{account.CreateDate.ToString("yyyy-MM-dd HH:mm:ss")}','{account.Balance}')",
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

        public void UpdateAccount(String c,Account account)
        {
            DateTime dateTime = DateTime.Now;
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var strUsername = c.Equals("Username") ? $" WHERE Username = '{account.Username}'" : (!(account.Username is null) ? $" Username = '{account.Username}', " : "");
            var strAccountNumber = c.Equals("AccountNumber")?$" WHERE AccountNumber = '{account.AccountNumber}'":(!(account.AccountNumber is null) ? $" AccountNumber = '{account.AccountNumber}', " : "");
            var strPasswordHash = !(account.PasswordHash is null) ? $" PasswordHash = '{account.PasswordHash}', " : "";
            var strSalt = !(account.Salt is null) ? $" Salt = '{account.Salt}', " : "";
            var strFullname = !(account.Fullname is null) ? $" Fullname = '{account.Fullname}', " : "";
            var strPhone = !(account.Phone is null) ? $" Phone = '{account.Phone}', " : "";
            var strEmail = !(account.Email is null) ? $" Email = '{account.Email}', " : "";
            var strBalance = !(account.Balance.GetHashCode()<1) ? $" Balance = '{account.Balance}', " : "";
            var strStatus = !(account.Status.GetHashCode()<1) ? $" Status = '{(int)account.Status}', " : "";
            var strUpdateDate =  $" UpdateDate = '{dateTime.ToString("yyyy-MM-dd HH:mm:ss")}'";
            var str = strAccountNumber+strPasswordHash+strSalt+strFullname+strPhone+strEmail+strBalance+strStatus+strUpdateDate+strUsername;
            
            var cmd = new MySqlCommand($"select * from shbaccount where accountNumber = '{dateTime}'", cnn);
            
            var reader = cmd.ExecuteReader();
            cnn.Close();
        }
    }
}