using System;
using T1908e_Spring_Hero_Bank.Entity;

namespace T1908e_Spring_Hero_Bank.View
{
    public class GuestView : IMenuGenerator
    {
        public override void GenerateMenu(Account? account)
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
                var choice =  _inputHelper.ValidateInt(1,3);
                switch (choice)
                {
                    case 1:
                        _accountController.Register();
                        break;
                    case 2:
                        IMenuGenerator iMenuGenerator;
                        var acc = _accountController.Login();
                        if (!(acc is null))
                        {
                            if ((int) acc.Role==1)
                            {
                                iMenuGenerator = new AdminView();
                            }
                            else
                            {
                                iMenuGenerator = new UserView();
                            }
                            iMenuGenerator.GenerateMenu(acc);
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

        public bool Account { get; set; }
    }
}