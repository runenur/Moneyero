using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Practices.Composite.Events;
using Moneyero.Events;
using Moneyero.Models;
using Moneyero.Services;
using Ninject;

namespace Moneyero.ViewModels.Transactions
{
    /// <summary>
    /// Represents a transaction filter. The filter contains a list of transaction 
    /// filter groups, all of which contains a list of transaction filter items.
    /// </summary>
    public class TransactionFilterViewModel
    {
        private readonly IAccountService _accountService;
        private readonly IKernel _kernel;
        private readonly TransactionFilterItemChangedEvent _filterItemChangedEvent;
        private TransactionFilterItemViewModel _selectedFilterItem;

        /// <summary>
        /// Creates a new <see cref="TransactionFilterViewModel"/> instance.
        /// </summary>
        /// 
        /// <param name="accountService">The account service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="kernel">The Ninject kernel.</param>
        public TransactionFilterViewModel(
            IAccountService accountService,
            IEventAggregator eventAggregator,
            IKernel kernel)
        {
            _accountService = accountService;
            _filterItemChangedEvent = eventAggregator.GetEvent<TransactionFilterItemChangedEvent>();
            _kernel = kernel;

            FilterGroups = new ObservableCollection<TransactionFilterGroupViewModel>();
            FilterGroups.CollectionChanged += OnFilterGroupsCollectionChanged;

            AddFilterItems();
        }

        /// <summary>
        /// Gets the transaction filter item that includes all transactions.
        /// </summary>
        public TransactionFilterItemViewModel AllTransactionsFilterItem
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the transaction filter groups.
        /// </summary>
        public ObservableCollection<TransactionFilterGroupViewModel> FilterGroups
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the selected transaction filter item.
        /// </summary>
        public void SetSelectedFilterItem(TransactionFilterItemViewModel filterItem)
        {
            _selectedFilterItem = filterItem;
            _filterItemChangedEvent.Publish(_selectedFilterItem);
        }

        private void AddFilterItems()
        {
            AllTransactionsFilterItem = _kernel.Get<TransactionFilterItemViewModel>();
            AllTransactionsFilterItem.Title = "Alle transaksjoner";
            AllTransactionsFilterItem.ParentFilter = this;
            AllTransactionsFilterItem.IsSelected = true;

            ILookup<AccountType, Account> accountsOfTypes = _accountService
                .RetrieveAccounts()
                .ToLookup(account => account.AccountType, account => account);

            foreach (IGrouping<AccountType, Account> accountsOfType in accountsOfTypes)
            {
                var accountTypeViewModel = _kernel.Get<TransactionFilterGroupViewModel>();
                switch (accountsOfType.Key)
                {
                    case AccountType.CheckingAccount:
                        accountTypeViewModel.Title = "Brukskontoer";
                        break;
                    case AccountType.CreditCardAccount:
                        accountTypeViewModel.Title = "Kredittkort";
                        break;
                    case AccountType.InvestmentAccount:
                        accountTypeViewModel.Title = "Investeringer";
                        break;
                    case AccountType.LoanAccount:
                        accountTypeViewModel.Title = "Lån";
                        break;
                    case AccountType.SavingsAccount:
                        accountTypeViewModel.Title = "Sparekontoer";
                        break;
                }
                foreach (Account account in accountsOfType)
                {
                    var accountViewModel = _kernel.Get<TransactionFilterItemViewModel>();
                    accountViewModel.Description = account.Name;
                    accountViewModel.Title = account.BankName;
                    accountViewModel.Account = account;
                    accountTypeViewModel.Items.Add(accountViewModel);
                }
                FilterGroups.Add(accountTypeViewModel);
            }
        }

        /// <summary>
        /// Called when the <see cref="FilterGroups"/> collection changes.
        /// </summary>
        /// 
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The event data.</param>
        private void OnFilterGroupsCollectionChanged(
            object sender, NotifyCollectionChangedEventArgs args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    SetParentFilterToFilterGroups(
                        args.NewItems.Cast<TransactionFilterGroupViewModel>());
                    break;
            }
        }

        /// <summary>
        /// Sets the <see cref="TransactionFilterItemViewModel.ParentFilter"/> property
        /// to this <see cref="TransactionFilterViewModel"/> instance for all the items in
        /// the specified filter group.
        /// </summary>
        /// 
        /// <param name="filterGroup">The filter group that contains the filter items.</param>
        private void SetParentFilterToFilterGroup(TransactionFilterGroupViewModel filterGroup)
        {
            filterGroup.ParentFilter = this;
            foreach (TransactionFilterItemViewModel filterItem in filterGroup.Items)
            {
                filterItem.ParentFilter = this;
            }
        }

        /// <summary>
        /// Sets the <see cref="TransactionFilterItemViewModel.ParentFilter"/> property
        /// to this <see cref="TransactionFilterViewModel"/> instance for all the items in
        /// all the specified filter groups.
        /// </summary>
        /// 
        /// <param name="filterGroups">The filter groups that contains the filter items.</param>
        private void SetParentFilterToFilterGroups(
            IEnumerable<TransactionFilterGroupViewModel> filterGroups)
        {
            foreach (TransactionFilterGroupViewModel filterGroup in filterGroups)
            {
                SetParentFilterToFilterGroup(filterGroup);
            }
        }
    }
}