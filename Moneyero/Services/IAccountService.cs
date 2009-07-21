using System.Collections.Generic;
using Moneyero.Models;

namespace Moneyero.Services
{
    /// <summary>
    /// Provides various methods that can be used to manage accounts.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Retrieves a list of all the available accounts.
        /// </summary>
        /// 
        /// <returns>A list of all the available accounts.</returns>
        IList<Account> RetrieveAccounts();
    }
}