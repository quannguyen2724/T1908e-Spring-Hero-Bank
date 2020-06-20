using System;
using T1908e_Spring_Hero_Bank.Entity;

namespace T1908e_Spring_Hero_Bank.View
{
    public class UserView : IMenuGenerator
    {
        public override void GenerateMenu(Account? account)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                Console.WriteLine($"Chào mừng {account.Fullname} quay trở lại. Vui lòng chọn thao tác:");
                Console.WriteLine("1. Gửi tiền.");
                Console.WriteLine("2. Rút tiền.");
                Console.WriteLine("3. Chuyển khoản.");
                Console.WriteLine("4. Truy vấn số dư.");
                Console.WriteLine("5. Thay đổi thông tin cá nhân.");
                Console.WriteLine("6. Thay đổi thông tin mật khẩu.");
                Console.WriteLine("7. Truy vấn lịch sử giao dịch.");
                Console.WriteLine("8. Đăng xuất");
                Console.WriteLine("9. Thoát.");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Nhập lựa chọn của bạn (1-8): ");
                var choice =  _inputHelper.ValidateInt(1,9);
                switch (choice)
                {
                    case 1:
                        _transactionController.GửiTiền(account);
                        break;
                    case 2:
                        _transactionController.RútTiền(account);
                        break;
                    case 3:
                        _transactionController.ChuyểnKhoản(account);
                        break;
                    case 4:
                        _transactionController.TruyVấnSốDư(account);
                        break;
                    case 5:
                        _accountController.ThayĐổiThôngTinAccount("ThôngTinNgườiDùng","Username",account);
                        break;
                    case 6:
                        _accountController.ThayĐổiThôngTinAccount("MậtKhẩu","Username",account);
                        break;
                    case 7:
                        _transactionController.TruyVấnLịchSửGiaoDịch(account.AccountNumber);
                        break;
                    case 8:
                        Console.WriteLine("Đăng xuất thành công!!");
                        break;
                    case 9:
                        Console.WriteLine("Goodbye!!!");
                        return;
                }
                if (choice == 8)
                {
                    break;
                }
                else
                {
                    Console.ReadLine();
                }
            }
        }
    }
}