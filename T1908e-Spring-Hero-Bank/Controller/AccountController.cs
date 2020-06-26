using System;
using System.Runtime.Remoting;
using T1908e_Spring_Hero_Bank.Entity;
using T1908e_Spring_Hero_Bank.Helper;
using T1908e_Spring_Hero_Bank.Model;
using T1908e_Spring_Hero_Bank.View;

namespace T1908e_Spring_Hero_Bank.Controller
{
    public class AccountController
    {
        
        private AccountModel _accountModel = new AccountModel();
        private PasswordHelper _passwordHelper = new PasswordHelper();
        private GenerateMenu generateMenu = new GenerateMenu();
        public static Account currentAccount;

        public void Register()
        {
            var account = new Account();
            Console.WriteLine("Tạo tài khoản!");
            while (true)
            {
                try
                {
                    Console.WriteLine("Nhập số tài khoản:");
                    account.AccountNumber = Console.ReadLine();
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Bạn phải điền vào một dãy số có ít nhất 8 chữ số!!");
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
                    Console.WriteLine("Nhập tên người dùng:");
                    account.Username = Console.ReadLine();
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
                    Console.WriteLine("Nhập mật khẩu:");
                    var password = Console.ReadLine();
                    account.Salt = PasswordHelper.GenerateSalt();
                    account.PasswordHash = PasswordHelper.MD5Hash(password + account.Salt);
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
                    Console.WriteLine("Nhập Email:");
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
                    Console.WriteLine("Nhập tên đầy đủ:");
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
                    Console.WriteLine("Nhập số điện thoại:");
                    account.Phone = Console.ReadLine();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            account.Balance = 0;
            account.Role = AccountRole.User;
            account.Status = AccountStatus.Active;
            var result = _accountModel.InsertAccount(account);
            if (result)
            {
                Console.WriteLine("Đăng ký thành công!!");
            }
            else
            {
                Console.WriteLine("Đăng ký thất bại, số tài khoản, số điện thoại hoặc email đã có người sử dụng!!");
            }
        }

        public Account Login()
        {
            
            string username;
            string password;
            while (true)
            {
                Console.WriteLine("Nhập tên người dùng: ");
                 username = Console.ReadLine();
                if (username.Length > 2)
                {
                    break;
                }
                Console.WriteLine("Tên người dùng phải có ít nhất 3 kí tự!!");
            }

            while (true)
            {
                Console.WriteLine("Nhập mật khẩu: ");
                password = Console.ReadLine();
                if (password.Length > 0)
                {
                    break;
                }

                Console.WriteLine("Mật khẩu không được để trống!");
            }
           

            var account = _accountModel.GetAccountByUsername(username);
            if (account != null
                && _passwordHelper.ComparePassword(password, account.Salt, account.PasswordHash))
            {
                if (account.Status == AccountStatus.Active)
                {
                    Console.WriteLine("Đăng nhập thành công");
                    currentAccount = account;
                    generateMenu.GetMenu(currentAccount);
                    return currentAccount;
                }

                Console.WriteLine("Tài khoản của bạn đã bị khóa, vui lòng liên hệ admin để biết thêm thông tin!!");
                return null;
            }

            Console.WriteLine("Sai tên đăng nhập hoặc mật khẩu!!");
            return null;
        }

        public void ListUser()
        {
            var listUser = _accountModel.GetList(null, null);
            if (listUser.Count > 0)
            {
                _accountModel.AccountPage(listUser);
            }
            else
            {
                Console.WriteLine("Không có tài khoản nào");
            }
           
        }

        public void BalanceQty()
        {
            var account = _accountModel.GetAccountByUsername(AccountController.currentAccount.Username);
           currentAccount.Balance = account.Balance;
           Console.WriteLine($"Số dư trong tài khoản của bạn là: {currentAccount.Balance}");
        }

        public void UpdateAccountInfor()
        {
            if (currentAccount.Role == AccountRole.User)
            {
                _accountModel.UpdatetAccount(currentAccount.AccountNumber, "updateInfor");
            }
            else
            {
                var userAccount = new Account();
                
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Nhập số tài khoản muốn thay đổi thông tin: ");
                        userAccount.AccountNumber = Console.ReadLine();
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                _accountModel.UpdatetAccount(userAccount.AccountNumber, "updateInfor");
            }
        }

        public void UpdateAccountPassword()
        {
            if (currentAccount.Role == AccountRole.User)
            {
                _accountModel.UpdatetAccount(currentAccount.AccountNumber, "updatePassword");
            }
            else
            {
                var userAccount = new Account();
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Nhập số tài khoản muốn thay đổi mật khẩu: ");
                        userAccount.AccountNumber = Console.ReadLine();
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                _accountModel.UpdatetAccount(userAccount.AccountNumber, "updatePassword");
            }
        }

        public void UpdateAccountStatus()
        {
            var userAccount = new Account();
            while (true)
            {
                try
                {
                    Console.WriteLine("Nhập số tài khoản muốn thay đổi trạng thái: ");
                    userAccount.AccountNumber = Console.ReadLine();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            _accountModel.UpdatetAccount(userAccount.AccountNumber, "activeAccount");
         generateMenu.GetMenu(currentAccount);   
        }

        public void FindUserByUsername()
        {
            var acc = new Account();
            while (true)
            {
                try
                {
                    Console.WriteLine("Nhập tên người dùng: ");
                    acc.Username = Console.ReadLine();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            _accountModel.GetList("username", acc.Username);
        }

        public void FindUserByAccountNumber()
        {
            var acc = new Account();
            while (true)
            {
                try
                {
                    Console.WriteLine("Nhập số tài khoản: ");
                    acc.AccountNumber = Console.ReadLine();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            _accountModel.GetList("accountNumber", acc.AccountNumber);
        }

        public void FindUserByPhone()
        {
            var acc = new Account();
            
            while (true)
            {
                try
                {
                    Console.WriteLine("Nhập số điện thoại: ");
                    acc.Phone = Console.ReadLine();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            var result = _accountModel.GetList("phone", acc.Phone);
            if (result == null)
            {
                Console.WriteLine("Không tìm thấy người dùng nào!!");
            }
        }
    }
}