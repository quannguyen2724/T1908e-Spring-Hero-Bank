using System;
using System.Collections.Generic;
using T1908e_Spring_Hero_Bank.Entity;
using T1908e_Spring_Hero_Bank.Helper;
using T1908e_Spring_Hero_Bank.Model;

namespace T1908e_Spring_Hero_Bank.Controller
{
    public class TransactionController
    {
        private AccountController _accountController = new AccountController();
        private InputHelper _inputHelper = new InputHelper();
        private TransactionModel _transactionModel = new TransactionModel();
        private AccountModel _accountModel = new AccountModel();
        private DateTime _time = DateTime.Now;

        public void TruyVấnLịchSửGiaoDịch(string? accountNumber)
        {
            List<Transaction>? list = _transactionModel.GetListTransaction(accountNumber);
            if (list.Count < 1)
            {
                Console.WriteLine("Tài khoản chưa có Lịch sử giao dịch");
            }
            else
            {
                Console.WriteLine("Lịch sử giao dịch: ");
                Console.WriteLine("TransactionCode | SenderAccountNumber | ReceiverAccountNumber | Message | Amount | Fee | Type | Status | UpdatedAt");
                foreach (var t in list)
                {
                    Console.WriteLine(t.ToString());
                }
            }
            
        }

        public void GửiTiền(Account? account)
        {
            List<Account> list = new List<Account>();
            list.Add(account);
            Console.WriteLine("Thông tin người dùng: ");
            _accountController.DanhSáchNgườiDùng(list);
            Console.WriteLine("Nhập số tiền cần gửi vào tài khoản: ");
            var amount = _inputHelper.ValidateDouble(0, null, "Số tiền không được <0, yêu cầu nhập lại:");
            Transaction transaction = new Transaction()
            {
                TransactionCode = Guid.NewGuid().ToString(),
                SenderAccountNumber = account.AccountNumber,
                Message = _inputHelper.ValidateString("Nhập lời nhắn: "),
                Amount = amount,
                Fee = 0,
                Type = (TransactionType) 2,
                Status = (TransactionStatus) 1,
                CreatedAt = _time
            };
            account.Balance = account.Balance + amount;
            account.UpdateDate = _time;
            _transactionModel.UpdateTransaction(account, null, transaction);
        }

        public void RútTiền(Account? account)
        {
            List<Account> list = new List<Account>();
            list.Add(account);
            Console.WriteLine("Thông tin người dùng: ");
            _accountController.DanhSáchNgườiDùng(list);
            Console.WriteLine("Nhập số tiền cần rút từ tài khoản: ");
            var amount = _inputHelper.ValidateDouble(0, account.Balance,
                $"Số tiền phải > 0 và không vượt quá số dư của bạn ({account.Balance} đ)");
            Transaction transaction = new Transaction()
            {
                TransactionCode = Guid.NewGuid().ToString(),
                SenderAccountNumber = account.AccountNumber,
                Message = _inputHelper.ValidateString("Nhập lời nhắn: "),
                Amount = amount,
                Fee = 0,
                Type = (TransactionType) 1,
                Status = (TransactionStatus) 1,
                CreatedAt = _time
            };
            account.Balance = account.Balance - amount;
            account.UpdateDate = _time;
            _transactionModel.UpdateTransaction(account, null, transaction);
        }

        public void ChuyểnKhoản(Account? account)
        {
            Account acc2 = new Account();
            List<Account> list = new List<Account>();
            Console.WriteLine("Bạn muốn chuyển tiền theo: ");
            Console.WriteLine("1. Số tài khoản ");
            Console.WriteLine("2. Tài khoản ");
            Console.WriteLine("Chọn: ");
            var choice = _inputHelper.ValidateInt(1, 2);
            while (true)
            {
                list = _accountModel.GetListAccount(choice == 1 ? "AccountNumber" : "Username",
                    _inputHelper.ValidateString(choice == 1 ? "Enter AccountNumber" : "Enter Username"));
                if (list.Count > 0)
                {
                    acc2 = list[0];
                    break;
                }
                else
                {
                    list = null;
                    Console.WriteLine("Số tài khoản không đúng, Yêu cầu nhập lại!");
                }

            }

            Console.WriteLine("Thông tin tài khoản nhận tiền: ");
            Console.WriteLine("AccountNumber | Fullname");
            Console.WriteLine($"{acc2.AccountNumber} | {acc2.Fullname}");
            Console.WriteLine("Nhập số tiền cần chuyển khoản từ tài khoản: ");
            var amount = _inputHelper.ValidateDouble(0, account.Balance,
                $"Số tiền phải > 0 và không vượt quá số dư của bạn ({account.Balance} đ)");
            account.Balance = account.Balance - amount;
            account.UpdateDate = _time;
            acc2.Balance = acc2.Balance + amount;
            acc2.UpdateDate = _time;
            Transaction transaction = new Transaction()
            {
                TransactionCode = Guid.NewGuid().ToString(),
                SenderAccountNumber = account.AccountNumber,
                ReceiverAccountNumber = acc2.AccountNumber,
                Message = _inputHelper.ValidateString("Nhập lời nhắn: "),
                Amount = amount,
                Fee = 0,
                Type = (TransactionType) 3,
                Status = (TransactionStatus) 1,
                CreatedAt = _time
            };
            _transactionModel.UpdateTransaction(account, acc2, transaction);
        }
        public void TruyVấnSốDư(Account? account)
        {
            Console.WriteLine($"Số dư tài khoản của bạn là: {account.Balance} nghìn đồng!");
        }
    }
}