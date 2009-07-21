using System.ComponentModel;
using Moneyero.Extensions;
using NUnit.Framework;

namespace Moneyero.Tests.Extensions
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    public class PropertyChangedExtensionsTests
    {
        [Test]
        public void Raise_WhenHandlerIsNull_DoesNothing()
        {
            const PropertyChangedEventHandler handler = null;
            handler.Raise(null);
        }

        [Test]
        public void Raise_WhenPropertySelectorIsNull_DoesNothing()
        {
            var handler = new PropertyChangedEventHandler(delegate { });
            handler.Raise(null);
        }

        [Test]
        public void Raise_WhenPropertySelectorReturnsNull_DoesNothing()
        {
            var handler = new PropertyChangedEventHandler(delegate { });
            handler.Raise(() => null);
        }

        [Test]
        public void Raise_WhenPropertySelectorReturnsInt32Property_RaisesEvent()
        {
            var propertyClass = new PropertyChangedEnabledClass();

            bool propertyChangedWasCalled = false;
            propertyClass.PropertyChanged +=
                delegate(object sender, PropertyChangedEventArgs e)
                {
                    propertyChangedWasCalled = e.PropertyName == "Int32Property";
                };

            propertyClass.Int32Property = 1;

            Assert.IsTrue(propertyChangedWasCalled);
        }

        [Test]
        public void Raise_WhenPropertySelectorReturnsStringProperty_RaisesEvent()
        {
            var propertyClass = new PropertyChangedEnabledClass();

            bool propertyChangedWasCalled = false;
            propertyClass.PropertyChanged +=
                delegate(object sender, PropertyChangedEventArgs e)
                {
                    propertyChangedWasCalled = e.PropertyName == "StringProperty";
                };

            propertyClass.StringProperty = "A";

            Assert.IsTrue(propertyChangedWasCalled);
        }

        private class PropertyChangedEnabledClass : INotifyPropertyChanged
        {
            private int _int32Property;
            private string _stringProperty;

            public int Int32Property
            {
                private get { return _int32Property; }
                set
                {
                    _int32Property = value;
                    PropertyChanged.Raise(() => Int32Property);
                }
            }

            public string StringProperty
            {
                private get { return _stringProperty; }
                set
                {
                    _stringProperty = value;
                    PropertyChanged.Raise(() => StringProperty);
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }
    }

    // ReSharper restore InconsistentNaming
}