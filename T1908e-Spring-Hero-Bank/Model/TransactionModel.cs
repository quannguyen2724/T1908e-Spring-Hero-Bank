using System;
using System.Collections.Generic;
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
                MySqlDataReader mySqlDataReader1 = cmd1.ExecuteReader();
                mySqlDataReader1.Close();
                if (acc2 is null)
                {
                    var cmd2 = new MySqlCommand($"INSERT INTO Transaction (TransactionCode, SenderAccountNumber, Message, Amount, Fee, Type, Status, CreatedAt) VALUES ('{transaction.TransactionCode}', '{transaction.SenderAccountNumber}', '{transaction.Message}', '{transaction.Amount}', '{transaction.Fee}', '{(int)transaction.Type}', '{(int)transaction.Status}', '{transaction.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")}')",cnn);
                    MySqlDataReader mySqlDataReader2 = cmd2.ExecuteReader();
                    mySqlDataReader2.Close();
                }
                else
                {
                    var cmd2 = new MySqlCommand($"INSERT INTO Transaction (TransactionCode, SenderAccountNumber, ReceiverAccountNumber, Message, Amount, Fee, Type, Status, CreatedAt) VALUES ('{transaction.TransactionCode}', '{transaction.SenderAccountNumber}', '{transaction.ReceiverAccountNumber}', '{transaction.Message}', '{transaction.Amount}', '{transaction.Fee}', '{(int)transaction.Type}', '{(int)transaction.Status}', '{transaction.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")}')",cnn);
                    MySqlDataReader mySqlDataReader2 = cmd2.ExecuteReader();
                    mySqlDataReader2.Close();
                    var cmd3 = new MySqlCommand($"UPDATE Account  SET Balance = '{acc2.Balance}', UpdateDate = '{acc2.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE AccountNumber = '{acc2.AccountNumber}'", cnn);
                    MySqlDataReader mySqlDataReader3 = cmd3.ExecuteReader();
                    mySqlDataReader3.Close();
                }
                
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

        public List<Transaction> GetListTransaction(string? str)
        {
            List<Transaction> list = new List<Transaction>();
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            MySqlCommand cmd;
            if (str is null)
            {
                cmd = new MySqlCommand($"select * from Transaction", cnn);
            }
            else
            {
                cmd= new MySqlCommand($"select * from Transaction where SenderAccountNumber = '{str}' OR ReceiverAccountNumber = '{str}'", cnn);
            }

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Transaction()
                {
                    TransactionCode = reader.GetString("TransactionCode"),
                    SenderAccountNumber = reader.GetString("SenderAccountNumber"),
                    ReceiverAccountNumber = reader.GetString("ReceiverAccountNumber"),
                    Message = reader.GetString("Message"),
                    Amount = reader.GetInt32("Amount"),
                    Fee = reader.GetInt32("Email"),
                    Type = (TransactionType)reader.GetInt32("Type"),
                    Status = (TransactionStatus)reader.GetInt32("Status"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                });
            }
            reader.Close();
            cnn.Close();
            return list;
        }
    }
}