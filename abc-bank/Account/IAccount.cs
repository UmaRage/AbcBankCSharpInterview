namespace abc_bank.Account
{
    public interface IAccount
    {
        double AvailableFunds { get; }

        void Deposit(double amount);

        void Withdraw(double amount);

        double sumTransactions();

        double InterestEarned();

        string GetAccountStatement();

    }
}
