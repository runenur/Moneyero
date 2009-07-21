using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Composite.Events;
using Moneyero.Events;
using Moneyero.Models;
using Moneyero.Services;
using Moneyero.TestUtils;
using Moneyero.ViewModels.Transactions;
using Ninject;
using NUnit.Framework;
using Rhino.Mocks;

namespace Moneyero.Tests.ViewModels.Transactions
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    public class TransactionListViewModelTests
    {
        [Test]
        public void TransactionListViewModel_WhenCreated_AddsAllTransactions()
        {
            var transactions = new List<Transaction>
            {
                new Transaction()
            };

            TransactionListViewModel viewModel = CreateTransactionListViewModel(transactions);

            Assert.AreEqual(transactions.Single(), viewModel.Transactions.Single().Model);
        }

        [Test]
        public void CurrentFilterItem_WhenSetToNull_AddsAllTransactions()
        {
            var transactions = new List<Transaction>
            {
                new Transaction()
            };

            TransactionListViewModel viewModel = CreateTransactionListViewModel(transactions);
            viewModel.CurrentFilterItem = null;

            Assert.AreEqual(transactions.Single(), viewModel.Transactions.Single().Model);
        }

        [Test]
        public void CurrentFilterItem_WhenSet_AddFilteredTransactions()
        {
            var account1 = new Account();
            var account2 = new Account();
            var transactions = new List<Transaction>
            {
                new Transaction { Account = account1 },
                new Transaction { Account = account2 }
            };

            TransactionListViewModel viewModel = CreateTransactionListViewModel(transactions);
            viewModel.CurrentFilterItem = new TransactionFilterItemViewModel
            {
                Account = account1
            };

            Assert.AreEqual(transactions.First(), viewModel.Transactions.Single().Model);
        }

        [Test]
        public void TransactionFilterItemChangedEvent_WhenPublished_CurrentFilterItemIsSet()
        {
            var filterItemChangedEvent = new TransactionFilterItemChangedEvent();
            var filterItem = new TransactionFilterItemViewModel();

            TransactionListViewModel viewModel;
            {
                var eventAggregator = MockRepository.GenerateStub<IEventAggregator>();
                eventAggregator
                    .Stub(ea => ea.GetEvent<TransactionFilterItemChangedEvent>())
                    .Return(filterItemChangedEvent);

                viewModel = CreateTransactionListViewModel(eventAggregator);
            }

            filterItemChangedEvent.Publish(filterItem);

            Assert.AreEqual(filterItem, viewModel.CurrentFilterItem);
        }

        private static TransactionListViewModel CreateTransactionListViewModel(
            IList<Transaction> transactions)
        {
            var transactionService = MockRepository.GenerateStub<ITransactionService>();
            transactionService
                .Stub(s => s.RetrieveTransactions())
                .Return(transactions);

            var kernel = MockRepository.GenerateStub<IKernel>();
            kernel.StubGet(() => new TransactionViewModel());

            return new TransactionListViewModel(
                MockRepository.GenerateStub<IEventAggregator>(),
                kernel,
                transactionService);
        }

        private static TransactionListViewModel CreateTransactionListViewModel(
            IEventAggregator eventAggregator)
        {
            var transactionService = MockRepository.GenerateStub<ITransactionService>();
            transactionService
                .Stub(s => s.RetrieveTransactions())
                .Return(new List<Transaction>());

            var kernel = MockRepository.GenerateStub<IKernel>();
            kernel.StubGet(() => new TransactionViewModel());

            return new TransactionListViewModel(
                eventAggregator,
                kernel,
                transactionService);
        }
    }

    // ReSharper restore InconsistentNaming
}