using System;
using System.Collections.Generic;
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
                    PasswordHash = _passwordHelper.MD5Hash(_inputHelper.ValidateString("Enter PassWord: ") + salt),
                    Salt = salt,
                    Fullname = _inputHelper.ValidateString("Enter FullName: "),
                    Phone = _inputHelper.ValidateString("Enter Phone: "),
                    Email = _inputHelper.ValidateString("Enter Email: "),
                    Role = (Role) 0,
                    Status = (AccountStatus) (-1),
                    CreateDate = _time,
                    Balance = 0
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

        public Account KiểmTraTàiKhoản(string? t)
        {
            string str;
            List<Account> list;
            Account account;
            if (t is null)
            {
                str = _inputHelper.ValidateString("Enter UserName: ");
                list = _accountModel.GetListAccount("UserName", str);
            }
            else
            {
                str = _inputHelper.ValidateString($"Enter {t}: ");
                list = _accountModel.GetListAccount(t, str);
            }

            if (list.Count < 1)
            {
                if (t is null)
                {
                     account = new Account()
                    {
                        Username = str
                    };
                    
                }
                else
                {
                     account = new Account()
                    {
                        AccountNumber = "Không tồn tại số tài khoản này."
                    };
                }
                return account;
            }
            else
            {
                return list[0];
            }
        }

        public Account? ĐăngNhập(Account acc)
        {
            if ((acc.AccountNumber is null) ||
                !acc.PasswordHash.Equals(
                    _passwordHelper.MD5Hash(_inputHelper.ValidateString("Enter PassWord: ") + acc.Salt)))
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

        public void DanhSáchNgườiDùng(List<Account>? l)
        {
            List<Account> list;
            if (l is null)
            {
                list = _accountModel.GetListAccount(null, null);
            }
            else
            {
                list = l;
            }

            if (list.Count < 1)
            {
                Console.WriteLine("Không tồn tại!!");
            }
            else
            {
                var i = 0;
                while (true)
                {
                    Console.Clear();
                    var j = i + 1;
                    int sum = list.Count % 5 > 0 ? ((list.Count / 5) + 1) : list.Count;
                    Console.WriteLine("AccountNumber | Balance | Username | Fullname | Email | Phone | Role | Status");
                    var s = "";
                    foreach (var acc in list.GetRange(i * 5, (j == sum) ? (list.Count % 5) : 5))
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

        public List<Account> TìmKiếmNgườiDùng(string key, string str)
        {
            return _accountModel.GetListAccount(key, str);
        }

        public void ThayĐổiThôngTinAccount(string str, string str1, Account acc)
        {
            if (acc.AccountNumber is null)
            {
                Console.WriteLine("Tài khoản không tồn tại!!!");
            }
            else
            {
                List<Account> list = new List<Account>();
                list.Add(acc);
                Console.WriteLine("Thông tin người dùng: ");
                DanhSáchNgườiDùng(list);
                Console.WriteLine("Nhập thông tin mới");
                Account account = null;
                switch (str)
                {
                    case "ThôngTinNgườiDùng":
                        account = new Account()
                        {
                            Username = acc.Username,
                            Fullname = _inputHelper.ValidateString("Enter new FullName: "),
                            Phone = _inputHelper.ValidateString("Enter new Phone: "),
                            Email = _inputHelper.ValidateString("Enter new Email: "),
                        };
                        break;
                    case "KíchHoạtTàiKhoản":
                        Console.WriteLine("Enter Status (-1, 0, 1):");
                        account = new Account()
                        {
                            Username = acc.Username,
                            Status = (AccountStatus) _inputHelper.ValidateInt(-1, 1),
                        };
                        break;
                    case "MậtKhẩu":
                        var salt = _passwordHelper.GenerateSalt();
                        account = new Account()
                        {
                            Username = acc.Username,
                            PasswordHash =
                                _passwordHelper.MD5Hash(_inputHelper.ValidateString("Enter PassWord: ") + salt),
                            Salt = salt
                        };
                        break;
                }

                _accountModel.UpdateAccount(str1, account);
            }
        }
    }
}