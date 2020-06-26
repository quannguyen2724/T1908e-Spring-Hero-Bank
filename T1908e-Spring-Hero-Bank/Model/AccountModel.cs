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

        public bool InsertAccount(Account account)
        {
            var cnn = ConnectionHelper.GetConnection();
            try
            {
                cnn.Open();
                MySqlCommand cmd =
                    new MySqlCommand
                    ("insert into shbaccount (accountNumber, balance, username, passwordHAsh, salt, role, email , fullname, phone, status) "
                     + $"values( '{account.AccountNumber}',{account.Balance},'{account.Username}', '{account.PasswordHash}', '{account.Salt}', {(int) account.Role},'{account.Email}', '{account.Fullname}', '{account.Phone}', {(int) account.Status})",
                        cnn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                cnn.Close();
            }
        }

        public Account GetAccountByUsername(string username)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            try
            {
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
                        Role = (AccountRole) reader.GetInt32("role"),
                        Status = (AccountStatus) reader.GetInt32("status")
                    };
                }

                return _account;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                cnn.Close();
            }
        }

        public List<Account> GetList(string key, string str)
        {
            List<Account> list = new List<Account>();
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            try
            {
                MySqlCommand cmd;
                switch (key)
                {
                    case null:
                        cmd = new MySqlCommand($"select * from shbaccount Where Role = 0", cnn);
                        break;
                    // case "username":
                    //     cmd = new MySqlCommand($"select * from shbaccount where {key} = '{str}'", cnn);
                    //     break;
                    // case "AccountNumber":
                    //     cmd = new MySqlCommand($"select * from shbaccount where {key} = '{str}'", cnn);
                    //     break;
                    default:
                        cmd = new MySqlCommand($"select * from shbaccount where {key} = '{str}'", cnn);
                        break;
                }

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(_account = new Account()
                    {
                        AccountNumber = reader.GetString("accountNumber"),
                        Balance = reader.GetDouble("balance"),
                        Username = reader.GetString("username"),
                        PasswordHash = reader.GetString("passwordHash"),
                        Salt = reader.GetString("salt"),
                        Email = reader.GetString("email"),
                        Phone = reader.GetString("phone"),
                        Fullname = reader.GetString("fullname"),
                        Role = (AccountRole) reader.GetInt32("role"),
                        Status = (AccountStatus) reader.GetInt32("status")
                    });
                }

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            finally
            {
                cnn.Close();
            }
        }

        public Boolean CheckExistAccount(String accountNumber)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            try
            {
                var cmd = new MySqlCommand($"select * from shbaccount where accountNumber = '{accountNumber}'", cnn);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var account = new Account()
                    {
                        AccountNumber = reader.GetString("accountNumber"),
                        Balance = reader.GetDouble("balance"),
                        Username = reader.GetString("username"),
                        PasswordHash = reader.GetString("passwordHash"),
                        Salt = reader.GetString("salt"),
                        Email = reader.GetString("email"),
                        Phone = reader.GetString("phone"),
                        Fullname = reader.GetString("fullname"),
                        Role = (AccountRole) reader.GetInt32("role"),
                        Status = (AccountStatus) reader.GetInt32("status")
                    };
                    Console.WriteLine("Thông tin tài khoản: ");
                    Console.WriteLine(account.ToString());
                }
                else
                {
                    Console.WriteLine("Không tìm thấy dữ liệu cần tìm");
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Có lỗi xảy ra!!");
                return false;
            }
            finally
            {
                cnn.Close();
            }
        }

        public void UpdatetAccount(string accountNumber, string str)
        {
            var cnn = ConnectionHelper.GetConnection();

            if (CheckExistAccount(accountNumber))
            {
                cnn.Open();
                Account account;
                switch (str)
                {
                    case "updateInfor":
                        account = new Account();
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Nhập tên đầy đủ: ");
                                account.Fullname = Console.ReadLine();
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }

                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Nhập Email: ");
                                account.Email = Console.ReadLine();
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }

                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Nhập số điện thoại: ");
                                account.Phone = Console.ReadLine();
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }

                        try
                        {
                            var cmdInfor =
                                new MySqlCommand
                                ($"update shbaccount set fullName = '{account.Fullname}', email = '{account.Email}', phone = '{account.Phone}' where accountNumber = '{accountNumber}'",
                                    cnn);
                            cmdInfor.ExecuteNonQuery();
                            Console.WriteLine("Cập nhật thành công");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Cập nhật thất bại, email hoặc số điện thoại đã tồn tại!!");
                        }
                        finally
                        {
                            cnn.Close();
                        }

                        break;
                    case "activeAccount":
                        int status;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Nhập trạng thái (-1: Deactivate; 0: Lock; 1: Activate):");
                                status = int.Parse(Console.ReadLine());
                                if (status != -1 && status != 0 && status != 1)
                                {
                                    throw new Exception("Bạn phải nhập -1, 1 hoặc 1");
                                }
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Bạn phải nhập vào 1 số!!");
                            }
                        }

                        try
                        {
                            var cmdActive =
                                new MySqlCommand
                                ($"update shbaccount set status = '{status}' where accountNumber = '{accountNumber}'",
                                    cnn);
                            cmdActive.ExecuteNonQuery();
                            AccountController.currentAccount.Status =(AccountStatus) status;
                            Console.WriteLine("Cập nhật thành công");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Có lỗi xảy ra");
                        }
                        finally
                        {
                            cnn.Close();
                        }

                        break;
                    case "updatePassword":
                        string password;
                        while (true)
                        {
                            Console.WriteLine("Nhập Mật Khẩu: ");
                            password = Console.ReadLine();
                            if (password.Length > 0)
                            {
                                break;
                            }

                            Console.WriteLine("Password không đưuọc để trống!!");
                        }

                        account = new Account();
                        account.Salt = PasswordHelper.GenerateSalt();
                        account.PasswordHash = PasswordHelper.MD5Hash(password + account.Salt);
                        try
                        {
                            var cmdPassword =
                                new MySqlCommand
                                ($"update shbaccount set passwordHash = '{account.PasswordHash}', salt = '{account.Salt}' where accountNumber = '{accountNumber}'",
                                    cnn);
                            cmdPassword.ExecuteNonQuery();
                            Console.WriteLine("Cập nhật thành công!");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Có lỗi xảy ra!!");
                        }
                        finally
                        {
                            cnn.Close();
                        }

                        break;
                }
            }
            else
            {
                Console.WriteLine("Cập nhật không thành công!");
            }
        }

        public void AccountPage(List<Account> listUser)
        {
            var i = 0;
            while (true)
            {
                Console.Clear();
                var j = i + 1;
                int sum = listUser.Count % 5 > 0 ? ((listUser.Count / 5) + 1) : listUser.Count;
                var s = "";
                foreach (var acc in listUser.GetRange(i * 5, (j == sum) ? (listUser.Count % 5) : 5))
                {
                    Console.WriteLine(acc.ToString());
                }

                if (sum == 1)
                {
                    break;
                }

                Console.WriteLine($"Trang {j}/{sum}");
                Console.WriteLine("Nhập '< >' để chuyển trang, 'Backspace' Để quay lại!!!");
                string key = Console.ReadKey().Key.ToString();
                switch (key)
                {
                    case "LeftArrow":
                        if (i == 0)
                        {
                            i = sum - 1;
                        }
                        else
                        {
                            i--;
                        }

                        break;
                    case "RightArrow":
                        if (i == sum - 1)
                        {
                            i = 0;
                        }
                        else
                        {
                            i++;
                        }

                        break;
                    case "Backspace":
                        break;
                }

                if (key.Equals("Backspace"))
                {
                    Console.WriteLine("Enter để xác nhận!!!");
                    break;
                }
            }
        }
    }
}