using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ninject;

namespace Moneyero.Conventions
{
    /// <summary>
    /// Represents a convention that all service interfaces shall be bound to a mock class,
    /// if found.
    /// </summary>
    /// 
    /// <remarks>
    /// The name of the mock class has to be the same as the name of the interface,
    /// but without the "I" prefix, and with a "Mock" postfix. For example, an interface named
    /// <c>IFooService</c> would be bound to a class named <c>FooServiceMock</c> if one could
    /// be found.
    /// </remarks>
    public class ServiceMockBindConvention : IBindConvention
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Creates a new <see cref="ServiceMockBindConvention"/> instance.
        /// </summary>
        /// 
        /// <param name="kernel">The Ninject kernel.</param>
        public ServiceMockBindConvention(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Executes the bind convention.
        /// </summary>
        public void Execute()
        {
            IEnumerable<Type> serviceTypes = GetServiceTypes();
            foreach (Type serviceType in serviceTypes)
            {
                FindAndBindMockService(serviceType);
            }
        }

        /// <summary>
        /// Finds the related mock class and binds it, if found, to the specified service interface.
        /// </summary>
        /// 
        /// <param name="interfaceType">The service interface type.</param>
        private void FindAndBindMockService(Type interfaceType)
        {
            Type implementationType = GetMockClassType(interfaceType);
            if (implementationType != null)
            {
                _kernel.Bind(interfaceType).To(implementationType);
            }
        }

        /// <summary>
        /// Returns the mock class related to the specified service interface.
        /// </summary>
        /// 
        /// <param name="interfaceType">The service interface type.</param>
        /// 
        /// <returns>The mock class, if found; null otherwise.</returns>
        private static Type GetMockClassType(Type interfaceType)
        {
            string mockClassTypeName = interfaceType.Name.Substring(1) + "Mock";
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .SingleOrDefault(type => type.Name == mockClassTypeName);
        }

        /// <summary>
        /// Returns a sequence of all the service interface types.
        /// </summary>
        /// 
        /// <returns>A sequence of all the service interface types.</returns>
        private static IEnumerable<Type> GetServiceTypes()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.IsInterface &&
                               type.Name.EndsWith("Service", StringComparison.Ordinal));
        }
    }
}