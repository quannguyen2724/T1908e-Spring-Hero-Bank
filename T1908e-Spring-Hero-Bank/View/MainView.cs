using System;
using T1908e_Spring_Hero_Bank.Controller;

namespace T1908e_Spring_Hero_Bank.View
{
    public class MainView
    {
        public void Guest_Menu()
        {
            AccountController accountController = new AccountController();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("------Ngân hàng Spring Hero Bank------");
                Console.WriteLine("1.Đăng ký tài khoản.");
                Console.WriteLine("2.Đăng nhập hệ thống.");
                Console.WriteLine("3. Thoát");
                Console.WriteLine("______________________________________");
                Console.WriteLine("Nhập lựa chọn của bạn(1,2,3):");
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        accountController.Registry();
                        break;
                    case 2:
                        accountController.Login();
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }

                Console.ReadLine();
                if (choice == 3)
                {
                    break;
                }

            }
        }

        public void Admin_Menu()
        {
            AccountController accountController = new AccountController();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("------Ngân hàng Spring Hero Bank------");
                Console.WriteLine("Chào mừng " + AccountController.currentAccount.Fullname + "quay trở lại");
                Console.WriteLine("1.Danh sách người dùng");
                Console.WriteLine("2.Danh sách lịch sử giao dịch");
                Console.WriteLine("3.Tìm kiếm người dùng theo tên");
                Console.WriteLine("4.Tìm kiếm người dùng theo số tài khoản");
                Console.WriteLine("5.Tìm kiếm người dùng theo số điện thoại");
                Console.WriteLine("6.Thêm người dùng mới");
                Console.WriteLine("7.Khóa và mở khóa tài khoản người dùng");
                Console.WriteLine("8.Tìm kiếm lịch sử giao dịch theo số tài khoản");
                Console.WriteLine("9.Thay đổi thông tin tài khoản.");
                Console.WriteLine("10.Thay đổi thông tin mật khẩu.");
                Console.WriteLine("11.Thoát.");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Nhập lựa chọn của bạn: ");
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        accountController.List();
                        break;
                    case 2:
                        TransactionController.List();
                        break;
                    case 3:
                        accountController.CheckAccountByUsername();
                        break;
                    case 4:
                        accountController.CheckAccountByAccountnumber();
                        break;
                    case 5:
                        accountController.CheckAccountByPhonenumber();
                        break;
                    case 6:
                        accountController.Registry();
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:
                        break;
                    case 11:
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }

                Console.ReadLine();
                if (choice == 11)
                {
                    break;
                }
            }
        }

        public void User_Menu()
        {
            AccountController accountController=new AccountController();
            while (true)
            {
                Console.WriteLine("-----Ngân hàng Spring Hero Bank-----");
                Console.WriteLine("Chào mừng " + AccountController.currentAccount.Fullname + "quay trở lại");
                Console.WriteLine("1. Gửi tiền.");
                Console.WriteLine("2. Rút tiền.");
                Console.WriteLine("3.Chuyển khoản.");
                Console.WriteLine("4.Truy vấn số dư.");
                Console.WriteLine("5. Thay đổi thông tin cá nhân");
                Console.WriteLine("6. Thay đổi thông tin mật khẩu.");
                Console.WriteLine("7.Truy vấn lịch sử giao dịch.");
                Console.WriteLine("8.Thoát.");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Nhập lựa chọn của bạn(Từ 1 đến 8):");
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    default:
                        Console.WriteLine("Invalid Number");
                        break;
                
                }

                Console.ReadLine();
                if (choice == 8)
                {
                    break;
                }

            }
            
        }
    }
}