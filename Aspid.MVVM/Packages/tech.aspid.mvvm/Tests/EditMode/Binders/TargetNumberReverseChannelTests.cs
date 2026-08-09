using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for the <see cref="BindMode.OneWayToSource"/> channel of the serializable numeric binders.
    /// </summary>
    /// <remarks>
    /// <see cref="TargetFloatBinder{TTarget}"/> and <see cref="TargetIntBinder{TTarget}"/> implement
    /// <see cref="INumberReverseBinder"/>, whose default interface methods bridge each
    /// <see cref="IReverseBinder{T}"/> instantiation to a concrete numeric event. For the type the base class is
    /// already closed over — <see langword="float"/> and <see langword="int"/> respectively — that bridge does not
    /// apply: a class member always wins over a default interface implementation, so the inherited
    /// <c>ValueChanged</c> is what <see cref="IBinderAdder"/> subscribes to. These tests pin every combination of
    /// binder and ViewModel field type so the native channel cannot go silent again.
    /// </remarks>
    [TestFixture]
    public sealed class TargetNumberReverseChannelTests
    {
        private const float FloatProperty = 12.5f;
        private const int IntProperty = 42;

        [Test]
        public void FloatBinder_OneWayToSource_DeliversToFloatMember() =>
            Assert.AreEqual(FloatProperty, BindFloatBinder<float>());

        [Test]
        public void FloatBinder_OneWayToSource_DeliversToIntMember() =>
            Assert.AreEqual((int)FloatProperty, BindFloatBinder<int>());

        [Test]
        public void FloatBinder_OneWayToSource_DeliversToLongMember() =>
            Assert.AreEqual((long)FloatProperty, BindFloatBinder<long>());

        [Test]
        public void FloatBinder_OneWayToSource_DeliversToDoubleMember() =>
            Assert.AreEqual((double)FloatProperty, BindFloatBinder<double>());

        [Test]
        public void IntBinder_OneWayToSource_DeliversToIntMember() =>
            Assert.AreEqual(IntProperty, BindIntBinder<int>());

        [Test]
        public void IntBinder_OneWayToSource_DeliversToLongMember() =>
            Assert.AreEqual((long)IntProperty, BindIntBinder<long>());

        [Test]
        public void IntBinder_OneWayToSource_DeliversToFloatMember() =>
            Assert.AreEqual((float)IntProperty, BindIntBinder<float>());

        [Test]
        public void IntBinder_OneWayToSource_DeliversToDoubleMember() =>
            Assert.AreEqual((double)IntProperty, BindIntBinder<double>());

        [Test]
        public void FloatBinder_OneWay_IsRejectedByReverseMember()
        {
            var binder = new TestTargetFloatBinder(new FloatHolder { Value = FloatProperty }, converter: null, BindMode.OneWay);
            var member = new OneWayToSourceStructBindableMember<float>(_ => { });

            Assert.Throws<InvalidOperationException>(() => binder.Bind(member));
        }

        [Test]
        public void FloatBinder_OneWayToSource_AppliesConverterBeforeDelivering()
        {
            var binder = new TestTargetFloatBinder(
                new FloatHolder { Value = FloatProperty },
                new DoublingFloatConverter(),
                BindMode.OneWayToSource);

            Assert.AreEqual(FloatProperty * 2f, Bind<float>(binder));
        }

        private static T BindFloatBinder<T>()
            where T : struct =>
            Bind<T>(new TestTargetFloatBinder(
                new FloatHolder { Value = FloatProperty }, converter: null, BindMode.OneWayToSource));

        private static T BindIntBinder<T>()
            where T : struct =>
            Bind<T>(new TestTargetIntBinder(
                new IntHolder { Value = IntProperty }, converter: null, BindMode.OneWayToSource));

        /// <summary>
        /// Binds <paramref name="binder"/> through the same <see cref="IBinderAdder"/> the generated ViewModel uses
        /// and returns whatever reached the ViewModel-side setter.
        /// </summary>
        private static T Bind<T>(IBinder binder)
            where T : struct
        {
            var received = default(T);
            var member = new OneWayToSourceStructBindableMember<T>(value => received = value);

            binder.Bind(member);
            return received;
        }
    }

    internal sealed class FloatHolder
    {
        public float Value;
    }

    internal sealed class IntHolder
    {
        public int Value;
    }

    internal sealed class TestTargetFloatBinder : TargetFloatBinder<FloatHolder>
    {
        public TestTargetFloatBinder(FloatHolder target, IConverter<float, float> converter, BindMode mode)
            : base(target, converter, mode) { }

        protected override float Property
        {
            get => Target.Value;
            set => Target.Value = value;
        }
    }

    internal sealed class TestTargetIntBinder : TargetIntBinder<IntHolder>
    {
        public TestTargetIntBinder(IntHolder target, IConverter<int, int> converter, BindMode mode)
            : base(target, converter, mode) { }

        protected override int Property
        {
            get => Target.Value;
            set => Target.Value = value;
        }
    }

    [Serializable]
    internal sealed class DoublingFloatConverter : IConverter<float, float>
    {
        public float Convert(float value) => value * 2f;
    }
}
