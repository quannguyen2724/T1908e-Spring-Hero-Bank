using System;
using T1908e_Spring_Hero_Bank.Model;

namespace T1908e_Spring_Hero_Bank.Controller
{
    public class TransactionController
    {
       private TransactionModel _transactionModel = new TransactionModel();
       public void PrintListTransaction()
       {
           string accountNumber;
           if (Program.account == null)
           {
               Console.WriteLine("Vui lòng nhập số tài khoản cần kiểm tra giao dịch:");
                accountNumber = Console.ReadLine();
           }
           else
           {
               accountNumber = Program.account.AccountNumber;
           }
           var listTransaction = _transactionModel.GetTransactionHistory(accountNumber);
           Console.WriteLine("------------------------------------------------------------------------------------------------");
           if (listTransaction.Count > 0)
           {
               foreach (var transaction in listTransaction)
               {
                   Console.WriteLine($"{transaction.TransactionCode} | {transaction.SenderAccountNumber} | {transaction.ReceiverAccountNumber} | {transaction.Type} | {transaction.Amount} | {transaction.Fee} | {transaction.Message} | {transaction.CreatedAt} | {transaction.UpdatedAt} | {transaction.Status}");
               }
           }
           else
           {
               Console.WriteLine("Không có bản ghi nào hoặc tài khoản không tồn tại");
           }
           Console.WriteLine("-------------------------------------------------------------------------------------------------");
       }
    }
}