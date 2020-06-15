using System;
using T1908e_Spring_Hero_Bank.Controller;
using T1908e_Spring_Hero_Bank.Helper;

namespace T1908e_Spring_Hero_Bank.View
{
    public class MainView
    {
        private static  AccountController _accountController = new AccountController();
        private static  InputHelper _inputHelper = new InputHelper();
        public static void GenerateMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                Console.WriteLine("1. Đăng ký tài khoản.");
                Console.WriteLine("2. Đăng nhập hệ thống.");
                Console.WriteLine("3. Thoát.");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Nhập lựa chọn của bạn (1, 2, 3): ");
                var choice =  int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        _accountController.Register();
                        break;
                    case 2:
                        if ((int) _accountController.Login().Role==1)
                        {
                            AdminMenu();
                        }
                        else
                        {
                            UserMenu();
                        }
                        break;
                    case 3:
                        Console.WriteLine("Good bye!!!");
                        break;
                }
                Console.ReadLine();
                if (choice == 3)
                {
                    break;
                }
            }
        }

        private static void AdminMenu()
        {
            throw new NotImplementedException();
        }

        private static void UserMenu()
        {
            throw new NotImplementedException();
        }
    }
}