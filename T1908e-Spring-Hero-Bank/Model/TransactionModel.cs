using System.Collections.Generic;
using MySql.Data.MySqlClient;
using T1908e_Spring_Hero_Bank.Entity;
using T1908e_Spring_Hero_Bank.Helper;

namespace T1908e_Spring_Hero_Bank.Model
{
    public class TransactionModel
    {
        public List<Transaction> GetTransactionHistory(string accountNumber)
        {
            var listTransaction = new List<Transaction>();
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var strGetListTransaction = $"select * from shbtransaction where senderAccountNumber = '{accountNumber}' or receiverAccountNumber = '{accountNumber}'";
            var cmdGetListTransaction = new MySqlCommand(strGetListTransaction, cnn);
            var reader = cmdGetListTransaction.ExecuteReader();
            while (reader.Read())
            {
                listTransaction.Add(new Transaction()
                {
                    TransactionCode = reader.GetString("transactionCode"),
                    SenderAccountNumber = reader.GetString("senderAccountNumber"),
                    ReceiverAccountNumber = reader.GetString("receiverAccountNumber"),
                    Type = (TransactionType) reader.GetInt32("type"),
                    Amount = reader.GetDouble("amount"),
                    Fee = reader.GetDouble("fee"),
                    Message = reader.GetString("message"),
                    CreatedAt = reader.GetDateTime("createdAt"),
                    UpdatedAt = reader.GetDateTime("updateAt"),
                    Status = (TransactionStatus) reader.GetInt32("status")
                });
            }
            return listTransaction;
            
        }
    }
}