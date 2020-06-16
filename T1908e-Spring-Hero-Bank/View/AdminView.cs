using System;
using T1908e_Spring_Hero_Bank.Entity;

namespace T1908e_Spring_Hero_Bank.View
{
    public class AdminView: IMenuGenerator
    {
        public override void GenerateMenu(Account? account)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                Console.WriteLine($"Chào mừng Admin {account.Fullname} quay trở lại. Vui lòng chọn thao tác:");
                Console.WriteLine("1. Danh sách người dùng.");
                Console.WriteLine("2. Danh sách lịch sử giao dịch.");
                Console.WriteLine("3. Tìm kiếm người dùng theo tên.");
                Console.WriteLine("4. Tìm kiếm người dùng theo số tài khoản.");
                Console.WriteLine("5. Tìm kiếm người dùng theo số điện thoại.");
                Console.WriteLine("6. Thêm người dùng mới.");
                Console.WriteLine("7. Khoá và mở tài khoản người dùng.");
                Console.WriteLine("8. Tìm kiếm lịch sử giao dịch theo số tài khoản.");
                Console.WriteLine("9. Thay đổi thông tin tài khoản.");
                Console.WriteLine("10. Thay đổi thông tin mật khẩu.");
                Console.WriteLine("11. Đăng xuất.");
                Console.WriteLine("12. Thoát.");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Nhập lựa chọn của bạn (1-12): ");
                var choice =  _inputHelper.ValidateInt(1,12);
                switch (choice)
                {
                    case 1:
                        _accountController.DanhSáchNgườiDùng(null);
                        break;
                    case 2:
                        _transactionController.TruyVấnLịchSửGiaoDịch(null);
                        break;
                    case 3:
                        _accountController.DanhSáchNgườiDùng(_accountController.TìmKiếmNgườiDùng("FullName",_inputHelper.ValidateString("Enter FullName")));
                        break;
                    case 4:
                        _accountController.DanhSáchNgườiDùng(_accountController.TìmKiếmNgườiDùng("AccountNumber",_inputHelper.ValidateString("Enter AccountNumber")));
                        break;
                    case 5:
                        _accountController.DanhSáchNgườiDùng(_accountController.TìmKiếmNgườiDùng("Phone",_inputHelper.ValidateString("Enter Phone")));
                        break;
                    case 6:
                        _accountController.ĐăngKý(_accountController.KiểmTraTàiKhoản());
                        break;
                    case 7:
                        _accountController.ThayĐổiThôngTinAccount("KíchHoạtTàiKhoản","UserName",_accountController.KiểmTraTàiKhoản());
                        break;
                    case 8:
                        _transactionController.TruyVấnLịchSửGiaoDịch(_accountController.KiểmTraTàiKhoản().AccountNumber);
                        break;
                    case 9:
                        _accountController.ThayĐổiThôngTinAccount("ThôngTinNgườiDùng","UserName",_accountController.KiểmTraTàiKhoản());
                        break;
                    case 10:
                        _accountController.ThayĐổiThôngTinAccount("MậtKhẩu","UserName",_accountController.KiểmTraTàiKhoản());
                        break;
                    case 11:
                        break;
                    case 12:
                        Console.WriteLine("Googbye");
                        return;
                }
                Console.ReadLine();
                if (choice == 11)
                {
                    break;
                }
            }
        }
    }
}