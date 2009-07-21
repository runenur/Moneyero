using System;
using System.Diagnostics;

namespace Moneyero.Models
{
    /// <summary>
    /// Represents a transaction.
    /// </summary>
    [DebuggerDisplay("Transaction (Amount={Amount}, Date={Date}, Description={Description})")]
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the account that the transaction is associated with.
        /// </summary>
        public Account Account
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the transaction amount.
        /// </summary>
        /// 
        /// <remarks>
        /// A positive value represents an income, while a negative value represents an expense.
        /// </remarks>
        public double Amount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the transaction category.
        /// </summary>
        public TransactionCategory Category
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the date when the transaction occurred.
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the description of the transaction.
        /// </summary>
        public string Description
        {
            get;
            set;
        }
    }
}