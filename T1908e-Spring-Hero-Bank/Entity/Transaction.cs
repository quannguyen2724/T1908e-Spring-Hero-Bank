using System;

namespace T1908e_Spring_Hero_Bank.Entity
{
    public class Transaction
    {
        public string TransactionCode { get; set; } //Mã giao dịch
        public string SenderAccountNumber { get; set; } //Tài khoản gửi
        public string ReceiverAccountNumber { get; set; }//Tài Khoản nhận
        public string Message { get; set; } //Lời nhắn
        public double Amount { get; set; } //Tiền giao dịch
        public double Fee { get; set; } //Phí giao dịch
        public TransactionType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TransactionStatus Status { get; set; }
        public override string ToString()
        {
            return $"{TransactionCode} | {SenderAccountNumber} | {ReceiverAccountNumber} | {Message} | {Amount} | {Fee} | {Status} | {Type} | {CreatedAt}";
        }
    }
    public enum TransactionType //Kiểu giao dịch
    {
        Withdraw = 1, Deposit = 2, Tranfer = 3 // Withdraw: Rút lại, Deposit: Gửi tiền, Tranfer: truyển khoản
    }
    public enum TransactionStatus //Trạng thái giao dịch
    {
        Pending = 0, Done = 1, Fails = 0 //pending : Việc chưa xong
    }
    
}