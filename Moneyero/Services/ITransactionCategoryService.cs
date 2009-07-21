using System.Collections.Generic;
using Moneyero.Models;

namespace Moneyero.Services
{
    /// <summary>
    /// Provides various methods that can be used to manage transaction categories.
    /// </summary>
    public interface ITransactionCategoryService
    {
        /// <summary>
        /// Retrieves a list of all the available transaction category groups.
        /// </summary>
        /// 
        /// <returns>A list of all the available transaction category groups.</returns>
        IList<TransactionCategoryGroup> RetrieveTransactionCategoryGroups();
    }
}