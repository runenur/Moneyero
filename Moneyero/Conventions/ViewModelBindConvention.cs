using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ninject;

namespace Moneyero.Conventions
{
    /// <summary>
    /// Represents a convention that all view models shall be bound in a transient scope.
    /// </summary>
    public class ViewModelBindConvention : IBindConvention
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Creates a new <see cref="ViewModelBindConvention"/> instance.
        /// </summary>
        /// 
        /// <param name="kernel">The Ninject kernel.</param>
        public ViewModelBindConvention(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Executes the bind convention.
        /// </summary>
        public void Execute()
        {
            IEnumerable<Type> viewModelTypes = GetViewModelTypes();
            foreach (Type viewModelType in viewModelTypes)
            {
                BindViewModel(viewModelType);
            }
        }

        /// <summary>
        /// Binds the specified view model in a transient scope.
        /// </summary>
        /// 
        /// <param name="viewModelType">The view model type.</param>
        private void BindViewModel(Type viewModelType)
        {
            _kernel.Bind(viewModelType).ToSelf().InTransientScope();
        }

        /// <summary>
        /// Returns a sequence of all the view model types.
        /// </summary>
        /// 
        /// <returns>A sequence of all the view model types.</returns>
        private static IEnumerable<Type> GetViewModelTypes()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.Name.EndsWith("ViewModel", StringComparison.Ordinal));
        }
    }
}