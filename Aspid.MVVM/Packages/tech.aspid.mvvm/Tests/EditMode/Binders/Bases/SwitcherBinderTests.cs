using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="SwitcherBinder{T}"/> and <see cref="SwitcherBinder{TTarget,T}"/>.
    /// </summary>
    [TestFixture]
    public sealed class SwitcherBinderTests
    {
        [Test]
        public void Ctor_TwoWayMode_Throws() =>
            Assert.Throws<InvalidOperationException>(() => new ProbeSwitcherBinder(1, 0, mode: BindMode.TwoWay));

        [Test]
        public void SetValue_True_AppliesTheTrueValue()
        {
            var binder = new ProbeSwitcherBinder(1, 0);

            ((IBinder<bool>)binder).SetValue(true);

            Assert.AreEqual(1, binder.Applied);
        }

        [Test]
        public void SetValue_False_AppliesTheFalseValue()
        {
            var binder = new ProbeSwitcherBinder(1, 0);

            ((IBinder<bool>)binder).SetValue(false);

            Assert.AreEqual(0, binder.Applied);
        }

        [Test]
        public void SetValue_WithAConverter_AppliesTheConvertedValue()
        {
            var binder = new ProbeSwitcherBinder(1, 0, new DoublingConverter());

            ((IBinder<bool>)binder).SetValue(true);

            Assert.AreEqual(2, binder.Applied);
        }

        [Test]
        public void TargetCtor_NullTarget_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new ProbeTargetSwitcherBinder(null, 1, 0));

        [Test]
        public void TargetSetValue_True_AppliesTheTrueValue()
        {
            var binder = new ProbeTargetSwitcherBinder(new object(), 1, 0);

            ((IBinder<bool>)binder).SetValue(true);

            Assert.AreEqual(1, binder.Applied);
        }

        [Test]
        public void TargetSetValue_False_AppliesTheFalseValue()
        {
            var binder = new ProbeTargetSwitcherBinder(new object(), 1, 0);

            ((IBinder<bool>)binder).SetValue(false);

            Assert.AreEqual(0, binder.Applied);
        }

        private sealed class ProbeSwitcherBinder : SwitcherBinder<int>
        {
            public int Applied { get; private set; }

            public ProbeSwitcherBinder(int trueValue, int falseValue, IConverter<int, int> converter = null, BindMode mode = BindMode.OneWay)
                : base(trueValue, falseValue, converter, mode) { }

            protected override void SetValue(int value) => Applied = value;
        }

        private sealed class ProbeTargetSwitcherBinder : SwitcherBinder<object, int>
        {
            public int Applied { get; private set; }

            public ProbeTargetSwitcherBinder(object target, int trueValue, int falseValue)
                : base(target, trueValue, falseValue) { }

            protected override void SetValue(int value) => Applied = value;
        }

        private sealed class DoublingConverter : IConverter<int, int>
        {
            public int Convert(int value) => value * 2;
        }
    }
}
