﻿using System;
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
                if (amount <= 0)
                {
                    throw new Exception("Invalid Amount!");
                }
                //kiểm tra tài khoản
                var strGetAccount =
                    $"select balance from SHBAccount where accountNumber = {accountNumber} and status = {(int) AccountStatus.Active}";
                var cmdGetAccount = new MySqlCommand(strGetAccount, cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    throw new Exception("Account is not found or has been deleted!");
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
                Console.WriteLine("Deposit success!");
                return true;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction.Rollback();
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
                    throw new Exception("Invalid amount!");
                }
                
                //kiểm tra tài khoản
                var strGetAccount = $"select balance from shbaccount where accountNumber = '{accountNumber}' and status = {(int) AccountStatus.Active}";
                var cmdGetAccount = new MySqlCommand(strGetAccount, cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    throw new Exception("Account is not found or has been deleted!");
                }
                // lấy ra số dư hiện tại
                var currentBalance = accountReader.GetDouble("balance");
                accountReader.Close();
                
                //update số dư tài khoản sau khi rút.
                currentBalance -= amount;
                if (currentBalance <= 0)
                {
                    throw new Exception("Invalid amount");
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
                Console.WriteLine("Withdraw success!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction.Rollback();
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
                if (amount <= 0)
                {
                    throw new Exception("Invalid amount!");
                }
                
                //Kiểm tra tài khoản chuyển tiền.
                var strGetSenderAccount = $"select balance from shbaccount where accountNumber = '{senderAccountNumber}' and status = {(int) AccountStatus.Active}";
                var cmdGetSenderAccount = new MySqlCommand(strGetSenderAccount, cnn);
                var senderAccountReader = cmdGetSenderAccount.ExecuteReader();
                if (!senderAccountReader.Read())
                {
                    throw new Exception("Account is not found or has been deleted!");
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
                  throw  new Exception("Account is not found or has been deleted!");
                }
                // lấy ra số dư hiện tại của tài khoản nhận tiền.
                var currentReceiverBalance = receiverAccountReader.GetDouble("Balance");
                receiverAccountReader.Close();
                
                //Update số dư tài khoản chuyển và tài khoản nhận sau khi chuyển tiền.
                currentSenderBalance -= amount;
                if ( currentSenderBalance <= 0)
                {
                    throw new Exception("Invalid amount");
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
                    Message = "Transfer " + amount + " from " + senderAccountNumber + " to " + receiverAccountNumber,
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
                Console.WriteLine("Transfer success!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction.Rollback();
            }
            return false;
        }
    }
}