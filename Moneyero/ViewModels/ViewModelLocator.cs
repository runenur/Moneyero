using System.Diagnostics.CodeAnalysis;
using Moneyero.ViewModels.Transactions;
using Ninject;

namespace Moneyero.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            Kernel = new KernelConfiguration().Kernel;
        }

        /// <summary>
        /// Gets a <see cref="TransactionFilterItemViewModel"/> instance.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public TransactionFilterItemViewModel TransactionFilterItemViewModel
        {
            get { return Kernel.Get<TransactionFilterItemViewModel>(); }
        }

        /// <summary>
        /// Gets a <see cref="TransactionFilterViewModel"/> instance.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public TransactionFilterViewModel TransactionFilterViewModel
        {
            get
            {
                return Kernel.Get<TransactionFilterViewModel>();
            }
        }

        /// <summary>
        /// Gets a <see cref="TransactionListViewModel"/> instance.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public TransactionListViewModel TransactionListViewModel
        {
            get { return Kernel.Get<TransactionListViewModel>(); }
        }

        protected static IKernel Kernel
        {
            get;
            private set;
        }
    }
}