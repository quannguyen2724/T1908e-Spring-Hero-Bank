using System;
using T1908e_Spring_Hero_Bank.Controller;

namespace T1908e_Spring_Hero_Bank.View
{
    public class GuessView
    {
        public static void GenerateGuessMenu()
        {
            var generateMenu = new GenerateMenu();
            var accountController = new AccountController();
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                    Console.WriteLine("1. Đăng ký tài khoản.");
                    Console.WriteLine("2. Đăng nhập hệ thống.");
                    Console.WriteLine("3. Thoát.");
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Nhập lựa chọn của bạn (1, 2, 3): ");
                    var choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            accountController.Register();
                            break;
                        case 2:
                            accountController.Login();
                            break;
                        case 3:
                            Console.WriteLine("Tạm biệt và hẹn gặp lại!!!");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Chọn 1, 2 hoặc 3");
                            break;
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
                Console.ReadLine();
            }
        }
    }
}