using Microsoft.Practices.Composite.Events;
using Moneyero.Conventions;
using Moneyero.KernelModules;
using Ninject;

namespace Moneyero
{
    public class KernelConfiguration
    {
        public KernelConfiguration()
        {
            Kernel = CreateKernel();
            ConfigureKernel();
        }

        public IKernel Kernel
        {
            get;
            private set;
        }

        private void ConfigureKernel()
        {
            RegisterBindConventions();
            RegisterServices();
            LoadKernelModules();
        }

        private static StandardKernel CreateKernel()
        {
            return new StandardKernel();
        }

        private void LoadKernelModules()
        {
            Kernel.LoadModule(new BindConventionsExecutionModule());
        }

        /// <summary>
        /// Registers the <see cref="IBindConvention"/> types in the Ninject kernel.
        /// </summary>
        private void RegisterBindConventions()
        {
            Kernel.Bind<IBindConvention>().To<ServiceMockBindConvention>();
            Kernel.Bind<IBindConvention>().To<ViewModelBindConvention>();
        }

        private void RegisterServices()
        {
            Kernel.Bind<IEventAggregator>().To<EventAggregator>();
        }
    }
}