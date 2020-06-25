using System;
using T1908e_Spring_Hero_Bank.Controller;
using T1908e_Spring_Hero_Bank.Entity;

namespace T1908e_Spring_Hero_Bank.View
{
    public class GenerateMenu
    {
        public void GetMenu(Account account)
        {
            if (account == null)
            {
                GuessView.GenerateGuessMenu();
            }
            else
            {
                if (account.Role ==  AccountRole.User)
                {
                    UserView.GenerateUserMenu();
                }
                else if (account.Role == AccountRole.Admin)
                {
                    AdminView.GenerateAdminMenu();
                }
            }
        }
    }
}