using System.Collections.Generic;

namespace Moneyero.Models
{
    /// <summary>
    /// Represents a transaction category group.
    /// </summary>
    public class TransactionCategoryGroup : TransactionCategory
    {
        /// <summary>
        /// Creates a new <see cref="TransactionCategoryGroup"/> instance.
        /// </summary>
        public TransactionCategoryGroup()
        {
            ChildCategories = new List<TransactionCategory>();
        }

        /// <summary>
        /// Gets or sets the list of this transaction category's child categories.
        /// </summary>
        public IList<TransactionCategory> ChildCategories
        {
            get;
            private set;
        }
    }
}