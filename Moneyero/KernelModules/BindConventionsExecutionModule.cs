using System.Collections.Generic;
using Moneyero.Conventions;
using Ninject;
using Ninject.Modules;

namespace Moneyero.KernelModules
{
    /// <summary>
    /// Represents a <see cref="Module"/> that will execute every <see cref="IBindConvention"/>
    /// that is registered in the IoC container.
    /// </summary>
    public class BindConventionsExecutionModule : Module
    {
        /// <summary>
        /// Called when the module has been loaded.
        /// </summary>
        public override void Load()
        {
            ExecuteBindConventions();
        }

        /// <summary>
        /// Executes every <see cref="IBindConvention"/> that is registered in
        /// the IoC container.
        /// </summary>
        private void ExecuteBindConventions()
        {
            IEnumerable<IBindConvention> bindConventions = GetBindConventions();
            foreach (IBindConvention bindConvention in bindConventions)
            {
                bindConvention.Execute();
            }
        }

        /// <summary>
        /// Gets a sequence of every <see cref="IBindConvention"/> that is registered in
        /// the IoC container.
        /// </summary>
        /// 
        /// <returns>A sequence of <see cref="IBindConvention"/> objects.</returns>
        private IEnumerable<IBindConvention> GetBindConventions()
        {
            return Kernel.GetAll<IBindConvention>();
        }
    }
}