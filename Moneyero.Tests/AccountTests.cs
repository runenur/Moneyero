using System.Collections.Generic;
using Moneyero.Models;
using NUnit.Framework;

namespace Moneyero.Tests
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    public class AccountTests
    {
        [Test]
        public void AddTransaction_CalledTwiceWithSameTransaction_AddsTransactionOnce()
        {
            var account = new Account();
            var transactions = new List<Transaction>
            {
                new Transaction()
            };

            account.AddTransaction(transactions[0]);
            account.AddTransaction(transactions[0]);

            CollectionAssert.AreEqual(transactions, account.Transactions);
        }

        [Test]
        public void AddTransaction_CalledWithNull_NothingAdded()
        {
            var account = new Account();
            account.AddTransaction(null);
            CollectionAssert.IsEmpty(account.Transactions);
        }

        [Test]
        public void AddTransaction_ValidTransaction_AddsTransaction()
        {
            var account = new Account();
            var transactions = new List<Transaction>
            {
                new Transaction()
            };

            account.AddTransaction(transactions[0]);

            CollectionAssert.AreEqual(transactions, account.Transactions);
        }

        [Test]
        public void AddTransactionRange_CalledWithNull_NothingAdded()
        {
            var account = new Account();
            account.AddTransactionRange(null);
            CollectionAssert.IsEmpty(account.Transactions);
        }

        [Test]
        public void AddTransactionRange_NoTransactions_NothingAdded()
        {
            var account = new Account();
            var transactions = new List<Transaction>();

            account.AddTransactionRange(transactions);

            CollectionAssert.IsEmpty(account.Transactions);
        }

        [Test]
        public void AddTransactionRange_TransactionAlreadyAdded_NotAdded()
        {
            var account = new Account();
            var transactions = new List<Transaction>
            {
                new Transaction()
            };
            account.AddTransaction(transactions[0]);

            account.AddTransactionRange(transactions);

            CollectionAssert.AreEquivalent(transactions, account.Transactions);
        }

        [Test]
        public void AddTransactionRange_TwoValidTransactions_AddsTwoTransactions()
        {
            var account = new Account();
            var transactions = new List<Transaction>
            {
                new Transaction(),
                new Transaction()
            };

            account.AddTransactionRange(transactions);

            CollectionAssert.AreEquivalent(transactions, account.Transactions);
        }
    }

    // ReSharper restore InconsistentNaming
}