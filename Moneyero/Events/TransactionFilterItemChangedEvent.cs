using Microsoft.Practices.Composite.Presentation.Events;
using Moneyero.ViewModels.Transactions;

namespace Moneyero.Events
{
    public class TransactionFilterItemChangedEvent
        : CompositePresentationEvent<TransactionFilterItemViewModel>
    {
    }
}