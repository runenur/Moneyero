using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.Practices.Composite.Events;
using Moneyero.Events;
using Moneyero.Extensions;
using Moneyero.Models;
using Moneyero.Services;
using Ninject;

namespace Moneyero.ViewModels.Transactions
{
    /// <summary>
    /// Represents a list of transactions.
    /// </summary>
    public class TransactionListViewModel : INotifyPropertyChanged
    {
        private TransactionFilterItemViewModel _currentFilterItem;
        private readonly IKernel _kernel;
        private readonly ITransactionService _transactionService;

        /// <summary>
        /// Creates a new <see cref="TransactionListViewModel"/> instance.
        /// </summary>
        /// 
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="kernel">The Ninject kernel.</param>
        /// <param name="transactionService">The transaction service.</param>
        public TransactionListViewModel(
            IEventAggregator eventAggregator,
            IKernel kernel,
            ITransactionService transactionService)
        {
            _kernel = kernel;
            _transactionService = transactionService;

            ConfigureEventAggregator(eventAggregator);

            Transactions = new ObservableCollection<TransactionViewModel>();
            AddTransactions();
        }

        /// <summary>
        /// Gets or sets the current transaction filter item.
        /// </summary>
        public TransactionFilterItemViewModel CurrentFilterItem
        {
            get { return _currentFilterItem; }
            set
            {
                _currentFilterItem = value;

                Transactions.Clear();
                AddTransactions();

                PropertyChanged.Raise(() => CurrentFilterItem);
            }
        }

        /// <summary>
        /// Gets the list of transactions.
        /// </summary>
        public ObservableCollection<TransactionViewModel> Transactions
        {
            get;
            private set;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// TODO
        /// </summary>
        private void AddTransactions()
        {
            IEnumerable<Transaction> transactions = _transactionService.RetrieveTransactions();
            if (CurrentFilterItem != null && CurrentFilterItem.Account != null)
            {
                transactions = transactions
                    .Where(transaction => transaction.Account == CurrentFilterItem.Account);
            }

            foreach (Transaction transaction in transactions)
            {
                var transactionViewModel = _kernel.Get<TransactionViewModel>();
                transactionViewModel.Model = transaction;
                Transactions.Add(transactionViewModel);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void ConfigureEventAggregator(IEventAggregator eventAggregator)
        {
            var filterItemChangedEvent = eventAggregator.GetEvent<TransactionFilterItemChangedEvent>();
            if (filterItemChangedEvent != null)
            {
                filterItemChangedEvent.Subscribe(SetCurrentFilterItem);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public void SetCurrentFilterItem(TransactionFilterItemViewModel filterItem)
        {
            CurrentFilterItem = filterItem;
        }
    }
}