using System;
using System.Collections.Generic;
using System.Linq;
using Moneyero.Models;

namespace Moneyero.Services.Mock
{
    /// <summary>
    /// Provides a mock implementation of the <see cref="ITransactionService"/> interface.
    /// </summary>
    public class TransactionServiceMock : ITransactionService
    {
        private readonly IList<Account> _accounts;
        private readonly IAccountService _accountService;
        private readonly Random _random;
        private readonly List<TransactionCategory> _transactionCategories;
        private readonly ITransactionCategoryService _transactionCategoryService;
        private readonly List<Transaction> _transactions;

        /// <summary>
        /// Creates a new <see cref="TransactionServiceMock"/> instance.
        /// </summary>
        /// 
        /// <param name="accountService">The account service.</param>
        /// <param name="transactionCategoryService">The transaction category service.</param>
        public TransactionServiceMock(
            IAccountService accountService,
            ITransactionCategoryService transactionCategoryService)
        {
            _accountService = accountService;
            _transactionCategoryService = transactionCategoryService;

            _random = new Random();
            _accounts = RetrieveAccounts();
            _transactionCategories = RetrieveTransactionCategories();
            _transactions = CreateTransactions();
        }

        /// <summary>
        /// Retrieves a list of all the available transactions.
        /// </summary>
        /// 
        /// <returns>A list of all the available transactions.</returns>
        public IList<Transaction> RetrieveTransactions()
        {
            return _transactions;
        }

        /// <summary>
        /// Creates a random <see cref="Transaction"/> object.
        /// </summary>
        /// 
        /// <returns>A random <see cref="Transaction"/> object.</returns>
        private Transaction CreateRandomTransaction()
        {
            return new Transaction
            {
                Account = GetRandomAccount(),
                Amount = GetRandomAmount(),
                Category = GetRandomTransactionCategory(),
                Date = DateTime.Now,
                Description = "TODO"
            };
        }

        /// <summary>
        /// Creates a list of mock transactions.
        /// </summary>
        /// 
        /// <returns>A list of mock transactions.</returns>
        private List<Transaction> CreateTransactions()
        {
            var transactions = new List<Transaction>();
            for (int i = 0; i < 20; i++)
            {
                transactions.Add(CreateRandomTransaction());
            }
            return transactions;
        }

        /// <summary>
        /// Gets a random <see cref="Account"/> instance.
        /// </summary>
        /// 
        /// <returns>A random <see cref="Account"/> instance.</returns>
        private Account GetRandomAccount()
        {
            return _accounts[_random.Next(_accounts.Count)];
        }

        /// <summary>
        /// Gets a random amount.
        /// </summary>
        /// 
        /// <returns>A random amount.</returns>
        private int GetRandomAmount()
        {
            bool shallBePositive = _random.Next(2) == 0;
            return (shallBePositive ? 1 : -1) * (100 + _random.Next(20) * 100);
        }

        /// <summary>
        /// Gets a random <see cref="TransactionCategory"/> instance.
        /// </summary>
        /// 
        /// <returns>A random <see cref="TransactionCategory"/> instance.</returns>
        private TransactionCategory GetRandomTransactionCategory()
        {
            return _transactionCategories[_random.Next(_transactionCategories.Count)];
        }

        /// <summary>
        /// Gets a list of all the available accounts.
        /// </summary>
        /// 
        /// <returns>A list of all the available accounts.</returns>
        private IList<Account> RetrieveAccounts()
        {
            return _accountService.RetrieveAccounts();
        }

        /// <summary>
        /// Gets a list of all the available transaction categories.
        /// </summary>
        /// 
        /// <returns>A list of all the available transaction categories.</returns>
        private List<TransactionCategory> RetrieveTransactionCategories()
        {
            IList<TransactionCategoryGroup> categoryGroups =
                _transactionCategoryService.RetrieveTransactionCategoryGroups();
            return categoryGroups
                .SelectMany(group => group.ChildCategories)
                .Concat(categoryGroups.Cast<TransactionCategory>())
                .ToList();
        }
    }
}