using System;
using MySql.Data.MySqlClient;
using T1908e_Spring_Hero_Bank.Entity;
using T1908e_Spring_Hero_Bank.Helper;

namespace T1908e_Spring_Hero_Bank.Model
{
    public class TransactionModel
    {
        public void UpdateTransaction(Account? acc1, Account? acc2, Transaction transaction)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var tran = cnn.BeginTransaction();
            try
            {
                var cmd1 = new MySqlCommand($"UPDATE Account  SET Balance = '{acc1.Balance}', UpdateDate = '{acc1.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE AccountNumber = '{acc1.AccountNumber}'", cnn);
                var cmd2 = new MySqlCommand($"INSERT INTO Transaction (TransactionCode, SenderAccountNumber, Message, Amount, Fee, Type, Status, CreatedAt) VALUES ('{transaction.TransactionCode}', '{transaction.SenderAccountNumber}', '{transaction.Message}', '{transaction.Amount}', '{transaction.Fee}', '{(int)transaction.Type}', '{(int)transaction.Status}', '{transaction.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")}')",cnn);
                MySqlDataReader mySqlDataReader1 = cmd1.ExecuteReader();
                mySqlDataReader1.Close();
                MySqlDataReader mySqlDataReader2 = cmd2.ExecuteReader();
                mySqlDataReader2.Close();
                Console.WriteLine("Uppdate success!!!");
                tran.Commit();
            }
            catch (Exception e)
            {
                tran.Rollback();
                Console.WriteLine(e);
                Console.WriteLine("Update Fail!!!");
            }
            cnn.Close();
        }
    }
}