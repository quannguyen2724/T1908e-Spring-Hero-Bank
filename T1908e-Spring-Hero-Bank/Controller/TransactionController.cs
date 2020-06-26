using System;
using System.Collections.Generic;
using T1908e_Spring_Hero_Bank.Entity;
using T1908e_Spring_Hero_Bank.Model;

namespace T1908e_Spring_Hero_Bank.Controller
{
    public class TransactionController
    {
        private TransactionModel _transactionModel = new TransactionModel();

        public void Deposit()
        {
            double amount;
            while (true)
            {
                try
                {
                    Console.WriteLine("Nhập số tiền: ");
                    amount = double.Parse(Console.ReadLine());
                    if (amount <= 0)
                    {
                        throw new Exception("Số tiền gửi phải lớn hơn 0");
                    }

                    break;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Bạn phải nhập vào 1 số!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            _transactionModel.Deposit(AccountController.currentAccount.AccountNumber, amount);
        }

        public void Withdraw()
        {
            double amount;
            while (true)
            {
                try
                {
                    Console.WriteLine("Nhập số tiền: ");
                    amount = double.Parse(Console.ReadLine());
                    if (amount <= 0)
                    {
                        throw new Exception("Số tiền gửi phải lớn hơn 0");
                    }

                    break;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Bạn phải nhập vào 1 số!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            _transactionModel.Withdraw(AccountController.currentAccount.AccountNumber, amount);
        }

        public void Transfer()
        {
            string receiverAccount;
            var receiveraccount = new Account();
            while (true)
            {
                try
                {
                    Console.WriteLine("Nhập số tài khoản hưởng thụ: ");
                    receiveraccount.AccountNumber = Console.ReadLine();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            receiverAccount = receiveraccount.AccountNumber;
            double amount;
            while (true)
            {
                try
                {
                    Console.WriteLine("Nhập số tiền: ");
                    amount = double.Parse(Console.ReadLine());
                    if (amount <= 0)
                    {
                        throw new Exception("Số tiền gửi phải lớn hơn 0");
                    }
                    break;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Bạn phải nhập vào 1 số!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            _transactionModel.Transfer(AccountController.currentAccount.AccountNumber, receiverAccount, amount);
        }

        public void PrintListTransaction()
        {
            string accountNumber;
            if (AccountController.currentAccount.Role == AccountRole.Admin)
            {
                Account account = new Account();
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Vui lòng nhập số tài khoản cần kiểm tra giao dịch:");
                        account.AccountNumber = Console.ReadLine();
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                accountNumber = account.AccountNumber;
            }
            else
            {
                accountNumber = AccountController.currentAccount.AccountNumber;
            }

            var listTransaction = _transactionModel.GetTransactionHistory(accountNumber);
            Console.WriteLine(
                "------------------------------------------------------------------------------------------------");
            if (listTransaction.Count > 0)
            {
                _transactionModel.TransactionPage(listTransaction);
            }
            else
            {
                Console.WriteLine("Không có bản ghi nào hoặc tài khoản không tồn tại");
            }
            Console.WriteLine(
                "-------------------------------------------------------------------------------------------------");
         
        }

        public void PrintlistTransactionHistory()
        {
            var listTransaction = _transactionModel.GetAllTransactionHistory();
            if (listTransaction.Count > 0)
            {
                foreach (var transaction in listTransaction)
                {
                    Console.WriteLine(
                        $"{transaction.TransactionCode} | {transaction.SenderAccountNumber} | {transaction.ReceiverAccountNumber} | {transaction.Type} | {transaction.Amount} | {transaction.Fee} | {transaction.Message} | {transaction.CreatedAt} | {transaction.UpdatedAt} | {transaction.Status}");
                }
            }
            else
            {
                Console.WriteLine("Không có bản ghi nào");
            }
            _transactionModel.TransactionPage(listTransaction);
        }
    }
}