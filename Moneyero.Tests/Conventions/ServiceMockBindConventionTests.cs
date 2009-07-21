using Moneyero.Conventions;
using Ninject;
using NUnit.Framework;
using Rhino.Mocks;

namespace Moneyero.Tests.Conventions
{
    [TestFixture]
    public class ServiceMockBindConventionTests
    {
        [Test]
        public void Foo()
        {
            var kernel = MockRepository.GenerateStub<IKernel>();

            var convention = new ServiceMockBindConvention(kernel);
            convention.Execute();

            kernel.AssertWasCalled(k => k.Bind(typeof(object)));
        }
    }
}