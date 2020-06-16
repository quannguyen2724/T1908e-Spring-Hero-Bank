using System;
using System.Threading;
using MySql.Data.MySqlClient;
using T1908e_Spring_Hero_Bank.Entity;
using T1908e_Spring_Hero_Bank.Helper;
using T1908e_Spring_Hero_Bank.Model;

namespace T1908e_Spring_Hero_Bank.Controller
{
    public class AccountController
    {
        private InputHelper _inputHelper = new InputHelper();
        private PasswordHelper _passwordHelper = new PasswordHelper();
        private AccountModel _accountModel = new AccountModel();
        private DateTime _time = DateTime.Now;
        public void ĐăngKý(Account acc)
        {
            if (!(acc.AccountNumber is null))
            {
                Console.WriteLine("Tài khoản đã tồn tại!!!");
            }
            else
            {
                var salt = _passwordHelper.GenerateSalt();
                Account account = new Account()
                {
                    Username = acc.Username,
                    PasswordHash = _passwordHelper.MD5Hash(_inputHelper.ValidateString("Enter PassWord: ")+salt),
                    Salt = salt,
                    Fullname = _inputHelper.ValidateString("Enter FullName: "),
                    Phone = _inputHelper.ValidateString("Enter Phone: "),
                    Email = _inputHelper.ValidateString("Enter Email: "),
                    Role = (Role) 0,
                    Status = (AccountStatus) 0,
                    CreateDate = _time,
                };
                if (_accountModel.CreateAccount(account))
                {
                    Console.WriteLine("Tạo tài khoản thành công!");
                }
                else
                {
                    Console.WriteLine("Tạo tài khoản thất bại");
                }
            }
        }

        public Account KiểmTraTàiKhoản()
        {
            var str = _inputHelper.ValidateString("Enter UserName: ");
            if (_accountModel.GetListAccount("UserName", str) is null)
            {
                Account account = new Account()
                {
                    Username = str
                };
                return account;
            }
            else
            {
                return _accountModel.GetListAccount("UserName", str)[0];
            }
        }

        public Account? ĐăngNhập(Account acc)
        {
            if ((acc.AccountNumber is null)||!acc.PasswordHash.Equals(_passwordHelper.MD5Hash(_inputHelper.ValidateString("Enter PassWord: ") + acc.Salt)))
            {
                Console.WriteLine("Thông tin không hợp lệ!!!");
                return null;
            }
            else
            {
                if ((int) acc.Status == 1)
                {
                    return acc;
                }

                {
                    Console.WriteLine("Tài khoản chưa được kích hoạt!!!");
                    return null;
                }
            }
        }

        public void DanhSáchNgườiDùng()
        {
            if (_accountModel.GetListAccount(null, null) is null)
            {
                Console.WriteLine("Chưa có tài khoản đăng ký!!");
            }
            else
            {
                Console.WriteLine("AccountNumber | Balance | Username | Fullname | Email | Phone | Role | Status");
                foreach (var acc in _accountModel.GetListAccount(null, null))
                {
                    Console.WriteLine(acc.ToString());
                }
            }
            
        }

        public void TìmKiếmNgườiDùng(string key)
        {
            throw new NotImplementedException();
        }

        public void ThayĐổiThôngTinCáNhân(Account acc)
        {
            throw new NotImplementedException();
        }

        public void ThayĐổiThôngTinMậtKhẩu(Account acc)
        {
            throw new NotImplementedException();
        }

        public void KíchHoạtTàiKhoản(Account acc)
        {
            throw new NotImplementedException();
        }
    }
}