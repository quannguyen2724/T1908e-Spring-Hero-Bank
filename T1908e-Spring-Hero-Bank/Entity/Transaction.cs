using System;

namespace T1908e_Spring_Hero_Bank.Entity
{
    public class Transaction
    {
        public string TransactionCode { get; set; }
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public string Message { get; set; }
        public double Amount { get; set; }
        public double Fee { get; set; }
        public TransactionType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TransactionStatus Status { get; set; }
    }
    public enum TransactionType
    {
        Withdraw = 1, Deposit = 2, Tranfer = 3 
    }
    public enum TransactionStatus
    {
        Pending = 0, Done = 1, Fails = 0
    }
}