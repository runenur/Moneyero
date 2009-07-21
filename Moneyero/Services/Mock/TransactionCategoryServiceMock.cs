using System.Collections.Generic;
using Moneyero.Models;

namespace Moneyero.Services.Mock
{
    /// <summary>
    /// Provides a mock implementation of the <see cref="TransactionCategoryServiceMock"/> interface.
    /// </summary>
    public class TransactionCategoryServiceMock : ITransactionCategoryService
    {
        private readonly IList<TransactionCategoryGroup> _transactionCategoryGroups;

        /// <summary>
        /// Creates a new <see cref="TransactionCategoryServiceMock"/> instance.
        /// </summary>
        public TransactionCategoryServiceMock()
        {
            _transactionCategoryGroups = CreateTransactionCategoryGroups();
        }

        /// <summary>
        /// Retrieves a list of all the available transaction category groups.
        /// </summary>
        /// 
        /// <returns>A list of all the available transaction category groups.</returns>
        public IList<TransactionCategoryGroup> RetrieveTransactionCategoryGroups()
        {
            return _transactionCategoryGroups;
        }

        /// <summary>
        /// Creates a list of mock transaction category groups.
        /// </summary>
        /// 
        /// <returns>A list of mock transaction category groups.</returns>
        private static IList<TransactionCategoryGroup> CreateTransactionCategoryGroups()
        {
            return new List<TransactionCategoryGroup>
            {
                new TransactionCategoryGroup
                {
                    Name = "Food & Dining",
                    ChildCategories =
                        {
                            new TransactionCategory { Name = "Groceries" },
                            new TransactionCategory { Name = "Restaurants" }
                        }
                },
                new TransactionCategoryGroup
                {
                    Name = "Income",
                    ChildCategories =
                        {
                            new TransactionCategory { Name = "Interest Income" },
                            new TransactionCategory { Name = "Paycheck" }
                        }
                }
            };
        }
    }
}