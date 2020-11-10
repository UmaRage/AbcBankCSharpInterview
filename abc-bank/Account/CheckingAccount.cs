using abc_bank.Framework;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;


namespace abc_bank.Account
{
    public class CheckingAccount : IAccount
    {
        public List<Transaction> transactions;

        private double _availableFunds;

        public double AvailableFunds => _availableFunds;

        public CheckingAccount()
        {
            this.transactions = new List<Transaction>();
            this._availableFunds = this.transactions.Sum( x=> x.amount);
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new TransactionException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount));
                _availableFunds += amount;
            }
        } 

        public double sumTransactions()
        {
            return this._availableFunds;
        }

        private double CheckIfTransactionsExist(bool checkAll)
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new TransactionException("amount must be greater than zero");
            }
            else
            {
                if (sumTransactions() < amount)
                    throw new TransactionException("You account does not have funds to complete the transaction!");

                transactions.Add(new Transaction(-amount));
                _availableFunds += -amount;
            }
        }

        public double InterestEarned()
        {
            return _availableFunds * 0.001;
        }

        public string GetAccountStatement()
        {
            StringBuilder s = new StringBuilder();
            s.Append("Checking Account");
            double total = 0.0;
            foreach (Transaction t in this.transactions)
            {
                s.AppendFormat("\n  {0} {1}", (t.amount < 0 ? "withdrawal" : "deposit"), t.amount.ToDollars());
                total += t.amount;
            }
            s.AppendFormat("\nTotal {0}", total.ToDollars());
            return s.ToString();
        }
    }
}
