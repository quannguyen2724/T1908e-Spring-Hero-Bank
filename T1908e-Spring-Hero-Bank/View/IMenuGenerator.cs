using System;
using T1908e_Spring_Hero_Bank.Controller;
using T1908e_Spring_Hero_Bank.Entity;
using T1908e_Spring_Hero_Bank.Helper;

namespace T1908e_Spring_Hero_Bank.View
{
    public abstract class IMenuGenerator
    {
        protected AccountController _accountController = new AccountController();
        protected TransactionController _transactionController = new TransactionController();
        protected InputHelper _inputHelper = new InputHelper();
        public abstract void GenerateMenu(Account? account);
    }
}