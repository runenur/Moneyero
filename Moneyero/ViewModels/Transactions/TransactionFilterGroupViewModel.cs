using System.Collections.ObjectModel;
using System.ComponentModel;
using Moneyero.Extensions;

namespace Moneyero.ViewModels.Transactions
{
    /// <summary>
    /// Represents a group of transaction filter items.
    /// </summary>
    public class TransactionFilterGroupViewModel : INotifyPropertyChanged
    {
        private TransactionFilterViewModel _parentFilter;
        private string _title;

        /// <summary>
        /// Creates a new <see cref="TransactionFilterGroupViewModel"/> instance.
        /// </summary>
        public TransactionFilterGroupViewModel()
        {
            Items = new ObservableCollection<TransactionFilterItemViewModel>();
        }

        /// <summary>
        /// Gets the transaction filter items.
        /// </summary>
        public ObservableCollection<TransactionFilterItemViewModel> Items
        {
            get;
            private set;
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
        /// Gets or sets the title of the transaction filter group.
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