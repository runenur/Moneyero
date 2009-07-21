using System.Collections.Generic;
using Moneyero.Models;

namespace Moneyero.Services
{
    /// <summary>
    /// Provides various methods that can be used to manage transactions.
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Retrieves a list of all the available transactions.
        /// </summary>
        /// 
        /// <returns>A list of all the available transactions.</returns>
        IList<Transaction> RetrieveTransactions();
    }
}