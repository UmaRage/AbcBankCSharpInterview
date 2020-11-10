using abc_bank.Account;
using abc_bank.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace abc_bank
{
    public class Customer
    {
        private string name;
        private List<IAccount> accounts;

        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<IAccount>();
        }

        public String GetName()
        {
            return name;
        }

        public Customer OpenAccount(AccountType accountType)
        {
            accounts.Add(AccountFactory.CreateAccountObject(accountType));
            return this;
        }

        public void Deposit(AccountType accountType, double amt)
        {
            GetAccount(accountType).Deposit(amt);
        }

        public void Withdraw(AccountType accountType, double amt)
        {
            GetAccount(accountType).Withdraw(amt);
        }

        private IAccount GetAccount(AccountType type)
        {
            IAccount account = null;
            switch (type)
            {
                case AccountType.Savings:
                    account = this.accounts.OfType<SavingsAccount>().FirstOrDefault();
                    break;
                case AccountType.Checking:
                    account = this.accounts.OfType<CheckingAccount>().FirstOrDefault();
                    break;
                case AccountType.MaxiSavings:
                    account = this.accounts.OfType<MaxiSavingsAccount>().FirstOrDefault();
                    break;
            }
            if (account == null)
                throw new AccountException("Account not found!");
            return account;
        }


        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }

        public double TotalInterestEarned() 
        {
            double total = 0;
            foreach (IAccount a in accounts)
                total += a.InterestEarned();
            return total;
        }

        public string GetStatement() 
        {
            //String statement = null;
            StringBuilder statement = new StringBuilder();
            statement.AppendFormat("Statement for {0}\n", name);
            double total = 0.0;
            foreach (IAccount a in accounts) 
            {
                statement.AppendFormat("\n{0}\n",  a.GetAccountStatement());
                total -= a.sumTransactions();
            }
            statement.AppendFormat("\nTotal In All Accounts {0}", total.ToDollars());
            return statement.ToString();
        }


        public void TransferBetweenAccounts(AccountType fromAccType, AccountType toAccountType, double amount)
        {
            var fromAccount = GetAccount(fromAccType);
            if (fromAccount.AvailableFunds < amount)
                throw new TransactionException("You account does not have funds to complete the transaction!");
            fromAccount.Withdraw(amount);
            GetAccount(toAccountType).Deposit(amount);
        }
    }
}
