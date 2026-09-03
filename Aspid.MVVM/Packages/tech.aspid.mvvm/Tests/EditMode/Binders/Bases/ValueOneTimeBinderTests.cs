using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="ValueOneTimeBinder{T}"/>.
    /// </summary>
    [TestFixture]
    public sealed class ValueOneTimeBinderTests
    {
        [Test]
        public void DefaultCtor_FixesTheModeToOneTime()
        {
            var binder = new ValueOneTimeBinder<int>();

            Assert.AreEqual(BindMode.OneTime, binder.Mode);
            Assert.AreEqual(0, binder.Value);
        }

        [Test]
        public void ValueCtor_StoresTheGivenValue()
        {
            var binder = new ValueOneTimeBinder<int>(7);

            Assert.AreEqual(7, binder.Value);
            Assert.AreEqual(BindMode.OneTime, binder.Mode);
        }

        [Test]
        public void ConverterCtor_StoresTheGivenValueAndConverter()
        {
            var binder = new ValueOneTimeBinder<int>(3, new DoublingConverter());

            Assert.AreEqual(3, binder.Value);
            Assert.AreEqual(BindMode.OneTime, binder.Mode);
        }

        [Test]
        public void BoundThroughAMember_TheSecondPush_DoesNotReachTheBinder()
        {
            var binder = new ValueOneTimeBinder<int>(0);
            var member = new OneWayBindableMember<int>(1);

            binder.Bind(member);
            member.Value = 2;

            Assert.AreEqual(1, binder.Value, "A second push must not reach a OneTime binder.");
        }

        private sealed class DoublingConverter : IConverter<int, int>
        {
            public int Convert(int value) => value * 2;
        }
    }
}
