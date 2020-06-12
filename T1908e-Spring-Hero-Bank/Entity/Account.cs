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
               public AccountStatus Status { get; set; }
    }
    public enum AccountStatus
    {
        Active = 1, Deactive = -1, Lock = 0
    }
}