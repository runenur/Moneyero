using Moneyero.Conventions;
using Moneyero.KernelModules;
using Moneyero.TestUtils;
using Ninject;
using NUnit.Framework;
using Rhino.Mocks;

namespace Moneyero.Tests.KernelModules
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    public class BindConventionsExecutionModuleTests
    {
        [Test]
        public void Load_WhenCalledWithOneBindConvention_CallsExecuteOnBindConvention()
        {
            var module = new BindConventionsExecutionModule
            {
                Kernel = MockRepository.GenerateStub<IKernel>()
            };

            var bindConvention = MockRepository.GenerateMock<IBindConvention>();
            module.Kernel.StubGetAll(() => bindConvention);

            module.Load();

            bindConvention.AssertWasCalled(bc => bc.Execute());
        }

        [Test]
        public void Load_WhenCalledWithTwoBindConvention_CallsExecuteOnBothBindConventions()
        {
            var module = new BindConventionsExecutionModule
            {
                Kernel = MockRepository.GenerateStub<IKernel>()
            };

            var bindConventions = new []
            {
                MockRepository.GenerateMock<IBindConvention>(),
                MockRepository.GenerateMock<IBindConvention>()
            };
            module.Kernel.StubGetAll(
                () => bindConventions[0],
                () => bindConventions[1]
                );

            module.Load();

            foreach (IBindConvention bindConvention in bindConventions)
            {
                bindConvention.AssertWasCalled(bc => bc.Execute());
            }
        }
    }

    // ReSharper restore InconsistentNaming
}