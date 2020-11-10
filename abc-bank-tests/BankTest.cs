using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Account;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void CustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(AccountType.Checking);
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
             
            Customer bill = new Customer("Bill").OpenAccount(AccountType.Checking);
            bank.AddCustomer(bill);

            bill.Deposit(AccountType.Checking, 100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            
            bank.AddCustomer(new Customer("Bill").OpenAccount(AccountType.Savings));

            bank.FindCustomer("Bill").Deposit(AccountType.Savings, 1500.0);
            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
             
            bank.AddCustomer(new Customer("Bill").OpenAccount(AccountType.MaxiSavings));

            bank.FindCustomer("Bill").Deposit(AccountType.MaxiSavings, 3000.0);

            Assert.AreEqual(3, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account_transactionLessThanTenDaysInterestTest()
        {
            Bank bank = new Bank();
 
            bank.AddCustomer(new Customer("Bill").OpenAccount(AccountType.MaxiSavings));

            bank.FindCustomer("Bill").Deposit(AccountType.MaxiSavings, 3000.0);

            Assert.AreEqual(3, bank.totalInterestPaid(), DOUBLE_DELTA);
        }




        [TestMethod]
        [Ignore]
        public void Maxi_savings_account_transactionMoreThanTenDaysInterestTest()
        {

            //TODO: We can use autofixure to generate the transactions list with past dates 
            //and test to calculate the intereset rates with 5%.

            Bank bank = new Bank();

            bank.AddCustomer(new Customer("Bill").OpenAccount(AccountType.MaxiSavings));

            bank.FindCustomer("Bill").Deposit(AccountType.MaxiSavings, 3000.0);

            Assert.AreEqual(3, bank.totalInterestPaid(), DOUBLE_DELTA);
        }
    }
}
