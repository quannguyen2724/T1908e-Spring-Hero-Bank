using System;

namespace T1908e_Spring_Hero_Bank.View
{
    public class MainView
    {
        public static void GenerateMenu()
        {
            // var controller = new StudentController();
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
                        // controller.CreateStudent();
                        break;
                    case 2:
                        // controller.ShowListStudent("");
                        break;
                    case 3:
                        // controller.EditStudent();
                        break;
                }
                Console.ReadLine();
                if (choice == 3)
                {
                    break;
                }
            }
        }
    }
}