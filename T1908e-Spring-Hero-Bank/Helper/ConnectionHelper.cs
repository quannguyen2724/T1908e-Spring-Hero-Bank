using System;
using MySql.Data.MySqlClient;

namespace T1908e_Spring_Hero_Bank.Helper
{
    public class ConnectionHelper
    {
        private const string DatabaseServer = "127.0.0.1";
        private const string DatabaseName = "test bank";
        private const string DatabaseUid = "root";
        private const string DatabasePassword = "";
        private static MySqlConnection _connection;

        public static MySqlConnection GetConnection()
        {
            if (_connection == null)
            {
                Console.WriteLine("Create new connection...");
                _connection =
                    new MySqlConnection(
                        $"SERVER={DatabaseServer};DATABASE={DatabaseName};UID={DatabaseUid};PASSWORD={DatabasePassword}");
                Console.WriteLine("...success!");
            }
            return _connection;
        }
    }
}