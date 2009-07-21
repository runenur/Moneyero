using System.Collections.Generic;
using System.IO;
using Moneyero.Models;

namespace Moneyero.Import
{
    /// <summary>
    /// Defines a method to import transactions.
    /// </summary>
    public interface ITransactionImporter
    {
        /// <summary>
        /// Imports a collection of transactions using the specified <see cref="TextReader"/>.
        /// </summary>
        /// <param name="reader">a <see cref="TextReader"/> that represents the transactions source.</param>
        /// <returns>A collection of transactions.</returns>
        ICollection<Transaction> Import(TextReader reader);
    }
}