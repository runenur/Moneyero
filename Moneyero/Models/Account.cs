using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Moneyero.Models
{
    /// <summary>
    /// Represents an account.
    /// </summary>
    [DebuggerDisplay("Account (Name={Name}, Transactions={Transactions.Count})")]
    public class Account
    {
        private readonly List<Transaction> _transactions;

        /// <summary>
        /// Creates a new <see cref="Account"/> instance.
        /// </summary>
        public Account()
        {
            _transactions = new List<Transaction>();
        }

        /// <summary>
        /// Gets or sets the type of the account.
        /// </summary>
        public AccountType AccountType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the bank that contains the the account.
        /// </summary>
        public string BankName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the account.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the list of transactions associated with the account.
        /// </summary>
        public ReadOnlyCollection<Transaction> Transactions
        {
            get { return _transactions.AsReadOnly(); }
        }

        /// <summary>
        /// Adds a transaction.
        /// TODO: Detect if the same transaction is added more than once.
        /// </summary>
        /// <param name="transaction">The transaction to be added.</param>
        public void AddTransaction(Transaction transaction)
        {
            if (transaction == null)
            {
                return;
            }
            if (_transactions.Contains(transaction))
            {
                return;
            }

            _transactions.Add(transaction);
        }

        /// <summary>
        /// Adds a range of transactions.
        /// </summary>
        /// 
        /// <param name="transactions">The transactions to be added.</param>
        public void AddTransactionRange(IEnumerable<Transaction> transactions)
        {
            if (transactions == null)
            {
                return;
            }

            foreach (Transaction transaction in transactions)
            {
                AddTransaction(transaction);
            }
        }
    }
}