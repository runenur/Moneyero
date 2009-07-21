using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Activation;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Rhino.Mocks;
using System.Linq;

namespace Moneyero.TestUtils
{
    public static class KernelExtensions
    {
        public static void StubGet<T>(this IKernel self, Func<T> getCallback)
        {
            var request = MockRepository.GenerateStub<IRequest>();
            self
                .Stub(kernel => kernel.CreateRequest(
                    Arg<Type>.Is.Equal(typeof(T)),
                    Arg<Func<IBindingMetadata, bool>>.Is.Anything,
                    Arg<IEnumerable<IParameter>>.Is.Anything,
                    Arg<bool>.Is.Anything))
                .Return(request)
                .Repeat.Any();

            var hook = new Hook(() => getCallback());
            self
                .Stub(kernel => kernel.Resolve(request))
                .Do(new Func<IRequest, IEnumerable<Hook>>(delegate
                {
                    return new List<Hook> { hook };
                }))
                .Repeat.Any();
        }

        public static void StubGetAll<T>(this IKernel self, params Func<T>[] getCallbacks)
        {
            var request = MockRepository.GenerateStub<IRequest>();
            self
                .Stub(kernel => kernel.CreateRequest(
                    Arg<Type>.Is.Equal(typeof(T)),
                    Arg<Func<IBindingMetadata, bool>>.Is.Anything,
                    Arg<IEnumerable<IParameter>>.Is.Anything,
                    Arg<bool>.Is.Anything))
                .Return(request)
                .Repeat.Any();

            self
                .Stub(kernel => kernel.Resolve(request))
                .Do(new Func<IRequest, IEnumerable<Hook>>(delegate
                {
                    return getCallbacks
                        .Select(getCallback => new Hook(() => getCallback()))
                        .ToList();
                }))
                .Repeat.Any();
        }
    }
}