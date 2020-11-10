using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Account;
using abc_bank.Framework;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            Customer henry = new Customer("Henry").OpenAccount(AccountType.Checking).OpenAccount(AccountType.Savings);

            henry.Deposit(AccountType.Checking,100.0);
            henry.Deposit(AccountType.Savings,4000.0);
            henry.Withdraw(AccountType.Savings, 200.0);

            var expectedResult = "Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00";


            Assert.AreEqual(expectedResult, henry.GetStatement());
        }

        [TestMethod]
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(AccountType.Savings);
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(AccountType.Savings);
            oscar.OpenAccount(AccountType.Checking);
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(AccountType.Savings);
            oscar.OpenAccount(AccountType.Checking).OpenAccount(AccountType.MaxiSavings);
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTranferBetweenAccounts()
        {
            var henry = new Customer("Henry").OpenAccount(AccountType.Checking).OpenAccount(AccountType.Savings);

            henry.Deposit(AccountType.Checking, 100.0);
            henry.Deposit(AccountType.Savings, 4000.0);
            henry.Withdraw(AccountType.Savings, 200.0);

            henry.TransferBetweenAccounts(AccountType.Savings, AccountType.Checking, 1000);

            var expectedResult = "Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "  deposit $1,000.00\n" +
                    "Total $1,100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "  withdrawal $1,000.00\n" +
                    "Total $2,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00";


            Assert.AreEqual(expectedResult, henry.GetStatement());
        }

        [TestMethod]
        [ExpectedException(typeof(TransactionException))]
        public void TestTranferBetweenAccounts_UnsufficientFunds()
        {
            var henry = new Customer("Henry").OpenAccount(AccountType.Checking).OpenAccount(AccountType.Savings);

            henry.Deposit(AccountType.Checking, 100.0);
            henry.Deposit(AccountType.Savings, 4000.0);
            henry.Withdraw(AccountType.Savings, 200.0);

            henry.TransferBetweenAccounts(AccountType.Savings, AccountType.Checking, 5000);
        }


        [TestMethod]
        [ExpectedException(typeof(AccountException))]
        public void TestTranferBetweenAccounts_AccountNotFoundException()
        {
            var henry = new Customer("Henry").OpenAccount(AccountType.Checking).OpenAccount(AccountType.Savings);

            henry.Deposit(AccountType.Checking, 100.0);
            henry.Deposit(AccountType.Savings, 4000.0);
            henry.Withdraw(AccountType.Savings, 200.0);

            henry.TransferBetweenAccounts(AccountType.Savings, AccountType.MaxiSavings, 1000);
        }
    }
}
