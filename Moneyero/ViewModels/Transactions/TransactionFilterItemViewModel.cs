using System.ComponentModel;
using Moneyero.Extensions;
using Moneyero.Models;

namespace Moneyero.ViewModels.Transactions
{
    /// <summary>
    /// Represents an item that can be used to filter a list of transactions.
    /// </summary>
    public class TransactionFilterItemViewModel : INotifyPropertyChanged
    {
        private Account _account;
        private string _description;
        private bool _isSelected;
        private TransactionFilterViewModel _parentFilter;
        private string _title;

        /// <summary>
        /// Gets or sets the account that shall be used to filter transactions.
        /// </summary>
        //
        // TODO: Replace this with a more generic filter method, i.e. a search string.
        public Account Account
        {
            get { return _account; }
            set
            {
                _account = value;
                PropertyChanged.Raise(() => Account);
            }
        }

        /// <summary>
        /// Gets or sets the description of the transaction filter item.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                PropertyChanged.Raise(() => Description);
            }
        }

        /// <summary>
        /// Gets or sets whether the transaction filter item is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                PropertyChanged.Raise(() => IsSelected);

                ParentFilter.SetSelectedFilterItem(_isSelected ? this : null);
            }
        }

        /// <summary>
        /// Gets or sets the parent transaction filter.
        /// </summary>
        public TransactionFilterViewModel ParentFilter
        {
            get { return _parentFilter; }
            set
            {
                _parentFilter = value;
                PropertyChanged.Raise(() => ParentFilter);
            }
        }

        /// <summary>
        /// Gets or sets the title of the transaction filter item.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                PropertyChanged.Raise(() => Title);
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}