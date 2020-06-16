using System;
using Org.BouncyCastle.Asn1.X509;

namespace T1908e_Spring_Hero_Bank.Entity
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public Role Role { get; set; }
        public AccountStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public override string ToString()
        {
            return $"{AccountNumber} | {Balance} | {Username} | {Fullname} | {Email} | {Phone} | {Role} | {Status}";
            // "--------------------" + "\n" + 
            //    "Account Number: " + AccountNumber + "\n" + 
            //    "Balance: " + Balance + "\n" + 
            //    "Username: " + Username + "\n" + 
            //    "PasswordHash: " + PasswordHash + "\n" + 
            //    "Salt: " + Salt + "\n" + 
            //    "Email: " + Email + "\n" + 
            //    "Phone: " + Phone + "\n" + 
            //    "Full Name: " + Fullname + "\n" + 
            //    "Role: " + Role + "\n" + 
            //    "Status: " + Status + "\n" + 
            //    "--------------------";
        }
    }

    public enum Role
    {
        Admin=1,
        User=0
    }

    public enum AccountStatus
    {
        Active = 1,
        Deactive = -1,
        Lock = 0
    }
}