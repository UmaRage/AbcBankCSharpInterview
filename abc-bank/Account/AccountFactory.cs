using System;

namespace abc_bank.Account
{
    public static class AccountFactory
    {
        public static IAccount CreateAccountObject(AccountType type)
        {
            IAccount objIAccount = null;

            switch (type)
            {
                case AccountType.Savings:
                    objIAccount = new SavingsAccount();
                    break;

                case AccountType.Checking:
                    objIAccount = new CheckingAccount();
                    break;
                case AccountType.MaxiSavings:
                    objIAccount = new MaxiSavingsAccount();
                    break;
                default:
                    break;
            }

            return objIAccount;
        }


        public static string ToDollars(this double d)
        {
            return string.Format("{0:C}", Math.Abs(d));
        }
    }
}

