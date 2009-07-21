using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Moneyero.Extensions
{
    /// <summary>
    /// Provides a set of static extension methods that makes it easier to work with
    /// objects that implement <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public static class PropertyChangedExtensions
    {
        /// <summary>
        /// Raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event for the
        /// specified property.
        /// </summary>
        /// 
        /// <param name="handler">The event handler.</param>
        /// <param name="propertySelector">A delegate that returns the property.</param>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public static void Raise(
            this PropertyChangedEventHandler handler,
            Expression<Func<object>> propertySelector)
        {
            if (handler == null) return;
            if (propertySelector == null) return;

            MemberExpression memberExpression = GetMemberExpression(propertySelector);
            if (memberExpression == null) return;

            object sender = ((ConstantExpression) memberExpression.Expression).Value;
            handler(sender, new PropertyChangedEventArgs(memberExpression.Member.Name));
        }

        private static MemberExpression GetMemberExpression(Expression<Func<object>> propertySelector)
        {
            return propertySelector.Body is UnaryExpression
                       ? ((UnaryExpression) propertySelector.Body).Operand as MemberExpression
                       : propertySelector.Body as MemberExpression;
        }
    }
}