using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="ValueOneWayToSourceBinder{T}"/>.
    /// </summary>
    [TestFixture]
    public sealed class ValueOneWayToSourceBinderTests
    {
        [Test]
        public void DefaultCtor_FixesTheModeToOneWayToSource()
        {
            var binder = new ValueOneWayToSourceBinder<int>();

            Assert.AreEqual(BindMode.OneWayToSource, binder.Mode);
            Assert.AreEqual(0, binder.Value);
        }

        [Test]
        public void ValueCtor_StoresTheGivenValue()
        {
            var binder = new ValueOneWayToSourceBinder<int>(7);

            Assert.AreEqual(7, binder.Value);
            Assert.AreEqual(BindMode.OneWayToSource, binder.Mode);
        }

        [Test]
        public void Bind_PushesTheCurrentValue()
        {
            var binder = new ValueOneWayToSourceBinder<int>(7);
            var member = new OneWayToSourceBindableMember<int>(_ => { });

            binder.Bind(member);

            Assert.AreEqual(7, member.Value);
        }

        [Test]
        public void Bind_WithATwoWayConverter_PushesTheValueConvertedBack()
        {
            var binder = new ValueOneWayToSourceBinder<int>(6, new DoublingConverter());
            var member = new OneWayToSourceBindableMember<int>(_ => { });

            binder.Bind(member);

            Assert.AreEqual(3, member.Value, "The initial push must go through ConvertBack, like every later one.");
        }

        private sealed class DoublingConverter : ITwoWayConverter<int, int>
        {
            public int Convert(int value) => value * 2;

            public int ConvertBack(int value) => value / 2;
        }
    }
}
