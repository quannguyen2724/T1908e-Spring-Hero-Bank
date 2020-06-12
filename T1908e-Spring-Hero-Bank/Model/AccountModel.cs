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
    }
}