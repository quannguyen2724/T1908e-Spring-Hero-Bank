using System;

namespace T1908e_Spring_Hero_Bank.Entity
{
    public class Transaction
    {
        public string TransactionCode { get; set; }
        public string SenderAccountNumber { get; set; }

        public string ReceiverAccountNumber
        {
            get
            {
                return _receiverAccountNumber;
            }
            set
            {
                for (int i = 0; i < value.Length - 1; i++)
                {
                    if ((value[i] != '0' && value[i] != '1' && value[i] != '2' && value[i] != '3' && value[i] != '4' &&
                         value[i] != '5' && value[i] != '6' && value[i] != '7' && value[i] != '8' && value[i] != '9'))
                    {
                        throw new Exception("Bạn phải nhập vào 1 dãy số có ít nhất 8 kí tự!");
                    }
                }

                if (value.Length < 8)
                {
                    throw new Exception("Bạn phải nhập vào 1 dãy số có ít nhất 8 kí tự!");
                }

                _receiverAccountNumber = value;
            }
        }

        private string _receiverAccountNumber;
        public string Message { get; set; }

        public double Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Số tiền phải lớn hơn 0!!");
                }

                _amount = value;
            }
        }

        private double _amount;
        public double Fee { get; set; }
        public TransactionType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TransactionStatus Status { get; set; }

        public override string ToString()
        {
            return $"{TransactionCode} |      {SenderAccountNumber}     |       {_receiverAccountNumber}      | {Message} | {_amount}  |  {Fee}  | {Type} | {CreatedAt} | {UpdatedAt} | {Status}";
        }
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