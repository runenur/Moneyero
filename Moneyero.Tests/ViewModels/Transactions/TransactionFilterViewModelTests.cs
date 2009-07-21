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
    public class TransactionFilterViewModelTests
    {
        [Test]
        public void Constructor_WithOneAccount_AddsOneGroupWithOneItem()
        {
            var accounts = new List<Account>
            {
                new Account { AccountType = AccountType.CheckingAccount }
            };
            TransactionFilterViewModel viewModel = CreateTransactionFilterViewModel(accounts);
            Assert.AreEqual(
                accounts.Single(),
                viewModel
                    .FilterGroups.Single()
                    .Items.Single()
                    .Account);
        }

        [Test]
        public void Constructor_WithOneCheckingAccount_SetsTitleCorrectly()
        {
            AssertThatAccountsResultsInOneFilterGroup(
                "Brukskontoer",
                new Account { AccountType = AccountType.CheckingAccount });
        }

        [Test]
        public void Constructor_WithOneCreditCardAccount_SetsTitleCorrectly()
        {
            AssertThatAccountsResultsInOneFilterGroup(
                "Kredittkort",
                new Account { AccountType = AccountType.CreditCardAccount });
        }

        [Test]
        public void Constructor_WithOneInvestmentAccount_SetsTitleCorrectly()
        {
            AssertThatAccountsResultsInOneFilterGroup(
                "Investeringer",
                new Account { AccountType = AccountType.InvestmentAccount });
        }

        [Test]
        public void Constructor_WithOneLoadAccount_SetsTitleCorrectly()
        {
            AssertThatAccountsResultsInOneFilterGroup(
                "Lån",
                new Account { AccountType = AccountType.LoanAccount });
        }

        [Test]
        public void Constructor_WithOneSavingsAccount_SetsTitleCorrectly()
        {
            AssertThatAccountsResultsInOneFilterGroup(
                "Sparekontoer",
                new Account { AccountType = AccountType.SavingsAccount });
        }

        [Test]
        public void Constructor_WithTwoAccountsOfDifferentTypes_AddsTwoGroupsWithOneItemInEach()
        {
            var accounts = new List<Account>
            {
                new Account { AccountType = AccountType.CheckingAccount },
                new Account { AccountType = AccountType.SavingsAccount }
            };
            TransactionFilterViewModel viewModel = CreateTransactionFilterViewModel(accounts);

            Assert.AreEqual(2, viewModel.FilterGroups.Count);
            Assert.AreEqual(
                accounts.ElementAt(0),
                viewModel
                    .FilterGroups.ElementAt(0)
                    .Items.Single()
                    .Account);
            Assert.AreEqual(
                accounts.ElementAt(1),
                viewModel
                    .FilterGroups.ElementAt(1)
                    .Items.Single()
                    .Account);
        }

        [Test]
        public void Constructor_WithTwoAccountsOfSameType_AddsOneGroupsWithBothItems()
        {
            var accounts = new List<Account>
            {
                new Account { AccountType = AccountType.CheckingAccount },
                new Account { AccountType = AccountType.CheckingAccount }
            };
            TransactionFilterViewModel viewModel = CreateTransactionFilterViewModel(accounts);

            Assert.AreEqual(2, viewModel.FilterGroups.Single().Items.Count);
            Assert.AreEqual(
                accounts.ElementAt(0),
                viewModel
                    .FilterGroups.Single()
                    .Items.ElementAt(0)
                    .Account);
            Assert.AreEqual(
                accounts.ElementAt(1),
                viewModel
                    .FilterGroups.Single()
                    .Items.ElementAt(1)
                    .Account);
        }

        private static void AssertThatAccountsResultsInOneFilterGroup(
            string filterGroupTitle, params Account[] accounts)
        {
            TransactionFilterViewModel viewModel = CreateTransactionFilterViewModel(accounts);

            Assert.AreEqual(
                filterGroupTitle,
                viewModel.FilterGroups.Single().Title);
            Assert.AreEqual(
                filterGroupTitle,
                viewModel.FilterGroups.ElementAt(0).Title);
        }

        private static TransactionFilterViewModel CreateTransactionFilterViewModel(
            IList<Account> accounts)
        {
            var accountService = MockRepository.GenerateStub<IAccountService>();
            accountService
                .Stub(s => s.RetrieveAccounts())
                .Return(accounts);

            var serviceLocator = MockRepository.GenerateStub<IKernel>();
            serviceLocator.StubGet(() => new TransactionFilterGroupViewModel());
            serviceLocator.StubGet(() => new TransactionFilterItemViewModel());

            var eventAggregator = MockRepository.GenerateStub<IEventAggregator>();
            eventAggregator
                .Stub(ea => ea.GetEvent<TransactionFilterItemChangedEvent>())
                .Return(new TransactionFilterItemChangedEvent());

            return new TransactionFilterViewModel(
                accountService,
                eventAggregator,
                serviceLocator);
        }
    }

    // ReSharper restore InconsistentNaming
}