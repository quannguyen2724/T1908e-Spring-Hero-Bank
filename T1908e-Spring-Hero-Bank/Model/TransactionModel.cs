﻿using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using T1908e_Spring_Hero_Bank.Entity;
using T1908e_Spring_Hero_Bank.Helper;

namespace T1908e_Spring_Hero_Bank.Model
{
    public class TransactionModel
    {
        public bool Deposit(string accountNumber, double amount)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            
            //tạo transaction
            var transaction = cnn.BeginTransaction();
            try
            {
                //kiểm tra tài khoản
                var strGetAccount =
                    $"select balance from shbaccount where accountNumber = {accountNumber} and status = {(int) AccountStatus.Active}";
                var cmdGetAccount = new MySqlCommand(strGetAccount, cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    throw new Exception("Tài khoản không tồn tại hoặc đã bị xóa!");
                }

                // lấy ra số dư hiện tại
                var currentBalance = accountReader.GetDouble("balance");
                accountReader.Close();

                //Update số dư tài khoản
                currentBalance += amount;
                var strUpdateAccount =
                    $"update shbaccount set balance = {currentBalance} where accountNumber = {accountNumber} and status = {(int) AccountStatus.Active}";
                var cmdUpdateAccount = new MySqlCommand(strUpdateAccount, cnn);
                cmdUpdateAccount.ExecuteNonQuery();

                //Lưu transaction history
                var shbTransaction = new Transaction()
                {
                    TransactionCode = Guid.NewGuid().ToString(),
                    SenderAccountNumber = accountNumber,
                    ReceiverAccountNumber = accountNumber,
                    Type = TransactionType.Deposit,
                    Amount = amount,
                    Fee = 0,
                    Message = "Deposit " + amount,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = TransactionStatus.Done
                };
                var strInsertTransaction =
                    $"INSERT INTO `shbtransaction`(`transactionCode`, `senderAccountNumber`, `receiverAccountNumber`, `type`, `amount`, `fee`, `message`, `createdAt`, `updateAt`, `status`) " +
                    $"VALUES ('{shbTransaction.TransactionCode}', '{shbTransaction.SenderAccountNumber}', '{shbTransaction.ReceiverAccountNumber}', {(int) shbTransaction.Type}, {shbTransaction.Amount}, {shbTransaction.Fee}, '{shbTransaction.Message}', '{shbTransaction.CreatedAt:yy-MM-dd hh:mm:ss}', '{shbTransaction.UpdatedAt:yy-MM-dd hh:mm:ss}', {(int) shbTransaction.Status})";
                var cmdInsertTransaction = new MySqlCommand(strInsertTransaction, cnn);
                cmdInsertTransaction.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("Gửi tiền thành công!");
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                transaction.Rollback();
            }
            finally
            {
                cnn.Close();
            }
            return false;
        }
        
        public bool Withdraw(string accountNumber, double amount)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            
            //tạo transaction
            var transaction = cnn.BeginTransaction();
            try
            {
                if (amount <= 0)
                {
                    throw new Exception("Số tiền không hợp lệ!");
                }
                
                //kiểm tra tài khoản
                var strGetAccount = $"select balance from shbaccount where accountNumber = '{accountNumber}' and status = {(int) AccountStatus.Active}";
                var cmdGetAccount = new MySqlCommand(strGetAccount, cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    throw new Exception("Tài khoản không tìm thấy hoặc đã bị xóa!");
                }
                // lấy ra số dư hiện tại
                var currentBalance = accountReader.GetDouble("balance");
                accountReader.Close();
                
                //update số dư tài khoản sau khi rút.
                currentBalance -= amount;
                if (currentBalance <= 0)
                {
                    throw new Exception("Số tiền không hợp lệ");
                }
                var strUpdateAccount =
                    $"update shbaccount set balance = {currentBalance} where accountNumber = {accountNumber} and status = {(int) AccountStatus.Active}";
                var cmdUpdateAccount = new MySqlCommand(strUpdateAccount, cnn);
                cmdUpdateAccount.ExecuteNonQuery();
                
                //lưu transaction history.
                var shbTransaction = new Transaction()
                {
                    TransactionCode = Guid.NewGuid().ToString(),
                    SenderAccountNumber = accountNumber,
                    ReceiverAccountNumber = accountNumber,
                    Type = TransactionType.Withdraw,
                    Amount = amount,
                    Fee = 0,
                    Message = "Withdraw " + amount,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = TransactionStatus.Done
                };
                var strInsertTransaction =
                    $"INSERT INTO `shbtransaction`(`transactionCode`, `senderAccountNumber`, `receiverAccountNumber`, `type`, `amount`, `fee`, `message`, `createdAt`, `updateAt`, `status`) " +
                    $"VALUES ('{shbTransaction.TransactionCode}', '{shbTransaction.SenderAccountNumber}', '{shbTransaction.ReceiverAccountNumber}', {(int) shbTransaction.Type}, {shbTransaction.Amount}, {shbTransaction.Fee}, '{shbTransaction.Message}', '{shbTransaction.CreatedAt:yy-MM-dd hh:mm:ss}', '{shbTransaction.UpdatedAt:yy-MM-dd hh:mm:ss}', {(int) shbTransaction.Status})";
                var cmdInsertTransaction = new MySqlCommand(strInsertTransaction, cnn);
                cmdInsertTransaction.ExecuteNonQuery();
                
                transaction.Commit();
                Console.WriteLine("Rút tiền thành công!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                transaction.Rollback();
            }
            finally
            {
                cnn.Close();
            }
            return false;
        }
        
        public bool Transfer(string senderAccountNumber, string receiverAccountNumber, double amount)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            
            //Tạo transaction.
            var transaction = cnn.BeginTransaction();
            try
            {
                Console.WriteLine("Nhập vào lời nhắn: ");
                var message = Console.ReadLine();
                if (amount <= 0)
                {
                    throw new Exception("Số tiền không hợp lệ!");
                }
                
                //Kiểm tra tài khoản chuyển tiền.
                var strGetSenderAccount = $"select balance from shbaccount where accountNumber = '{senderAccountNumber}' and status = {(int) AccountStatus.Active}";
                var cmdGetSenderAccount = new MySqlCommand(strGetSenderAccount, cnn);
                var senderAccountReader = cmdGetSenderAccount.ExecuteReader();
                if (!senderAccountReader.Read())
                {
                    throw new Exception("Tài khoản không tồn tại hoặc đã bị khóa!");
                }
                // lấy ra số dư hiện tại của tài khoản chuyển tiền.
                var currentSenderBalance = senderAccountReader.GetDouble("balance");
                senderAccountReader.Close();
                
                //Kiểm tra tài khoản nhận tiền.
                var strGetReceiverAccount = $"select balance from shbaccount where accountNumber = '{receiverAccountNumber}' and status = {(int) AccountStatus.Active}";
                var cmdGetReceiverAccount = new MySqlCommand(strGetReceiverAccount, cnn);
                var receiverAccountReader = cmdGetReceiverAccount.ExecuteReader();
                if (!receiverAccountReader.Read())
                { 
                  throw  new Exception("Tài khoản không tìm thấy hoặc đã bị khóa!");
                }
                // lấy ra số dư hiện tại của tài khoản nhận tiền.
                var currentReceiverBalance = receiverAccountReader.GetDouble("Balance");
                receiverAccountReader.Close();
                
                //Update số dư tài khoản chuyển và tài khoản nhận sau khi chuyển tiền.
                currentSenderBalance -= amount;
                if ( currentSenderBalance <= 0)
                {
                    throw new Exception("Số tiền không hợp lệ");
                }
                var strUpdateSenderAccount =
                    $"update shbaccount set balance = {currentSenderBalance} where accountNumber = {senderAccountNumber} and status = {(int) AccountStatus.Active}";
                var cmdUpdateSenderAccount = new MySqlCommand(strUpdateSenderAccount, cnn);
                cmdUpdateSenderAccount.ExecuteNonQuery();

                currentReceiverBalance += amount;
                var strUpdateReceiverAccount = 
                    $"update shbaccount set balance = {currentReceiverBalance} where accountNumber = {receiverAccountNumber} and status = {(int) AccountStatus.Active}";
                var cmdUpdateReceiverAccount = new MySqlCommand(strUpdateReceiverAccount, cnn);
                cmdUpdateReceiverAccount.ExecuteNonQuery();
                
                //Lưu transaction history.
                var shbTransaction = new Transaction()
                {
                    TransactionCode = Guid.NewGuid().ToString(),
                    SenderAccountNumber = senderAccountNumber,
                    ReceiverAccountNumber = receiverAccountNumber,
                    Type = TransactionType.Tranfer,
                    Amount = amount,
                    Fee = 0,
                    Message = message,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = TransactionStatus.Done
                };
                var strInsertTransaction =
                    $"INSERT INTO `shbtransaction`(`transactionCode`, `senderAccountNumber`, `receiverAccountNumber`, `type`, `amount`, `fee`, `message`, `createdAt`, `updateAt`, `status`) " +
                    $"VALUES ('{shbTransaction.TransactionCode}', '{shbTransaction.SenderAccountNumber}', '{shbTransaction.ReceiverAccountNumber}', {(int) shbTransaction.Type}, {shbTransaction.Amount}, {shbTransaction.Fee}, '{shbTransaction.Message}', '{shbTransaction.CreatedAt:yy-MM-dd hh:mm:ss}', '{shbTransaction.UpdatedAt:yy-MM-dd hh:mm:ss}', {(int) shbTransaction.Status})";
                var cmdInsertTransaction = new MySqlCommand(strInsertTransaction, cnn);
                cmdInsertTransaction.ExecuteNonQuery();
                
                transaction.Commit();
                Console.WriteLine("Chuyển tiền thành công!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                transaction.Rollback();
            }
            finally
            {
                cnn.Close();
            }
            return false;
        }

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
            cnn.Close();
            return listTransaction;
        }

        public List<Transaction> GetAllTransactionHistory()
        {
            var listTransaction = new List<Transaction>();
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            try
            {
                var strGetListTransaction = "select * from shbtransaction";
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            finally
            {
                cnn.Close();
            }
        }
        
        public void TransactionPage(List<Transaction> list)
        {
            var i = 0;
            while (true)
            {
                Console.Clear();
                var j = i + 1;
                int sum = list.Count % 5 > 0 ? ((list.Count / 5) + 1) : list.Count/5;
                Console.WriteLine(
                    "           TransactionCode           | SenderAccountNumber | ReceiverAccountNumber |     Message    | Amount | Fee |   Type   |       CreatedAt     |       UpdatedAt     | Status");
                var s = "";
                foreach (var acc in list.GetRange(i * 5, (j<  sum) ?5: (list.Count-i*5)))
                {
                    Console.WriteLine(acc.ToString());
                }

                if (sum == 1)
                {
                    break;
                }

                Console.WriteLine($"Trang {j}/{sum}");
                Console.WriteLine("Nhập '< >' để chuyển trang, 'Backspace' Để quay lại!!!");
                string key = Console.ReadKey().Key.ToString();
                switch (key)
                {
                    case "LeftArrow":
                        if (i == 0)
                        {
                            i = sum - 1;
                        }
                        else
                        {
                            i--;
                        }

                        break;
                    case "RightArrow":
                        if (i == sum - 1)
                        {
                            i = 0;
                        }
                        else
                        {
                            i++;
                        }

                        break;
                    case "Backspace":
                        break;
                }

                if (key.Equals("Backspace"))
                {
                    Console.WriteLine("Enter để xác nhận!!!");
                    break;
                }
            }
        }
    }
}