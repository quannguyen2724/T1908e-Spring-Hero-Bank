using System;
using T1908e_Spring_Hero_Bank.Controller;
using T1908e_Spring_Hero_Bank.Entity;

namespace T1908e_Spring_Hero_Bank.View
{
    public class GenerateMenu
    {
        public void GetMenu()
        {
            if (AccountController.currentAccount == null)
            {
                GuessView.GenerateGuessMenu();
            }
            else
            {
                if (AccountController.currentAccount.Role ==  AccountRole.User)
                {
                    UserView.GenerateUserMenu();
                }
                else if(AccountController.currentAccount.Role == AccountRole.Admin)
                {
                    AdminView.GenerateAdminMenu();
                }
            }
        }
    }
}