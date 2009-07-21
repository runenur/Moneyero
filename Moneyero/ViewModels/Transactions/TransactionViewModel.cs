using Moneyero.Models;

namespace Moneyero.ViewModels.Transactions
{
    /// <summary>
    /// Represents a transaction.
    /// </summary>
    public class TransactionViewModel
    {        
        /// <summary>
        /// Gets or sets the transaction's amount.
        /// </summary>
        public string Amount
        {
            get
            {
                return (Model != null)
                           ? Model.Amount + " kr"
                           : "N/A";
            }
        }

        /// <summary>
        /// Gets or sets the transaction's category.
        /// </summary>
        public string Category
        {
            get { return Model.Category.Name; }
        }

        /// <summary>
        /// Gets or sets the transaction's date.
        /// </summary>
        public string Date
        {
            get { return Model.Date.ToLongTimeString(); }
        }

        /// <summary>
        /// Gets or sets the transaction's description.
        /// </summary>
        public string Description
        {
            get { return Model.Description; }
        }

        /// <summary>
        /// Sets the associated <see cref="Transaction"/> model.
        /// </summary>
        public Transaction Model
        {
            get;
            set;
        }
    }
}