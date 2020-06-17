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
            throw new System.NotImplementedException();
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
            _transactionModel.UpdateTransaction(account,null, transaction);
        }

        public void RútTiền(Account? account)
        {
            List<Account> list = new List<Account>();
            list.Add(account);
            Console.WriteLine("Thông tin người dùng: ");
            _accountController.DanhSáchNgườiDùng(list);
            Console.WriteLine("Nhập số tiền cần rút từ tài khoản: ");
            var amount = _inputHelper.ValidateDouble(0, account.Balance,$"Số tiền phải > 0 và không vượt quá số dư của bạn ({account.Balance} đ)");
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
            account.Balance = account.Balance - amount;
            account.UpdateDate = _time;
            _transactionModel.UpdateTransaction(account,null, transaction);
        }

        public void ChuyểnKhoản(Account? account)
        {
            throw new System.NotImplementedException();
        }

        public void TruyVấnSốDư(Account? account)
        {
            throw new System.NotImplementedException();
        }
    }
}