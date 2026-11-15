#nullable enable
using System;
using System.Collections.Generic;
using Aspid.MVVM.StarterKit;
using NUnit.Framework;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class BindingModeCompatibilityTests : SceneFixture
    {
        [TestCase(BindMode.OneWay)]
        [TestCase(BindMode.TwoWay)]
        [TestCase(BindMode.OneTime)]
        public void OneWayMembersUseOnlyTheRequestedForwardDirection(BindMode mode)
        {
            AssertForward(new OneWayBindableMember<string>("first"), new TypedProbe<string>(mode), "next", "input");
            AssertForward(new OneWayBindableMember<string>("first"), new AnyProbe(mode), "next", "input");
            AssertForward(new OneWayStructBindableMember<int>(1), new TypedProbe<int>(mode), 2, 3);
            AssertForward(new OneWayStructBindableMember<int>(1), new TypedProbe<ValueType>(mode), 2, 3);
            AssertForward(new OneWayStructBindableMember<int>(1), new AnyProbe(mode), 2, 3);
            AssertForward(new OneWayEnumBindableMember<DayOfWeek>(DayOfWeek.Monday), new TypedProbe<DayOfWeek>(mode), DayOfWeek.Tuesday, DayOfWeek.Wednesday);
            AssertForward(new OneWayEnumBindableMember<DayOfWeek>(DayOfWeek.Monday), new TypedProbe<Enum>(mode), DayOfWeek.Tuesday, DayOfWeek.Wednesday);
            AssertForward(new OneWayEnumBindableMember<DayOfWeek>(DayOfWeek.Monday), new AnyProbe(mode), DayOfWeek.Tuesday, DayOfWeek.Wednesday);
        }

        [TestCase(BindMode.OneWay)]
        [TestCase(BindMode.TwoWay)]
        [TestCase(BindMode.OneTime)]
        public void OneTimeMembersNeverSubscribeInEitherDirection(BindMode mode)
        {
            var reference = new OneTimeBindableMember<string>("first");
            var number = new OneTimeStructBindableMember<int>(1);
            var enumeration = new OneTimeEnumBindableMember<DayOfWeek>(DayOfWeek.Monday);

            AssertOneTime(reference, new TypedProbe<string>(mode), () => reference.Value = "next", "input");
            AssertOneTime(reference, new AnyProbe(mode), () => reference.Value = "last", "input");
            AssertOneTime(number, new TypedProbe<int>(mode), () => number.Value = 2, 3);
            AssertOneTime(number, new TypedProbe<ValueType>(mode), () => number.Value = 4, 5);
            AssertOneTime(number, new AnyProbe(mode), () => number.Value = 6, 7);
            AssertOneTime(enumeration, new TypedProbe<DayOfWeek>(mode), () => enumeration.Value = DayOfWeek.Tuesday, DayOfWeek.Wednesday);
            AssertOneTime(enumeration, new TypedProbe<Enum>(mode), () => enumeration.Value = DayOfWeek.Thursday, DayOfWeek.Friday);
            AssertOneTime(enumeration, new AnyProbe(mode), () => enumeration.Value = DayOfWeek.Saturday, DayOfWeek.Sunday);
        }

        [TestCase(BindMode.OneWay)]
        [TestCase(BindMode.TwoWay)]
        [TestCase(BindMode.OneTime)]
        [TestCase(BindMode.OneWayToSource)]
        public void TwoWayMembersRespectRequestedDirections(BindMode mode)
        {
            AssertTwoWay<string>(setter => new TwoWayBindableMember<string>("first", setter), new TypedProbe<string>(mode), "next", "input");
            AssertTwoWay<string>(setter => new TwoWayBindableMember<string>("first", setter), new AnyProbe(mode), "next", "input");
            AssertTwoWay<int>(setter => new TwoWayStructBindableMember<int>(1, setter), new TypedProbe<int>(mode), 2, 3);
            AssertTwoWay<int>(setter => new TwoWayStructBindableMember<int>(1, setter), new TypedProbe<ValueType>(mode), 2, 3);
            AssertTwoWay<int>(setter => new TwoWayStructBindableMember<int>(1, setter), new AnyProbe(mode), 2, 3);
            AssertTwoWay<DayOfWeek>(setter => new TwoWayEnumBindableMember<DayOfWeek>(DayOfWeek.Monday, setter), new TypedProbe<DayOfWeek>(mode), DayOfWeek.Tuesday, DayOfWeek.Wednesday);
            AssertTwoWay<DayOfWeek>(setter => new TwoWayEnumBindableMember<DayOfWeek>(DayOfWeek.Monday, setter), new TypedProbe<Enum>(mode), DayOfWeek.Tuesday, DayOfWeek.Wednesday);
            AssertTwoWay<DayOfWeek>(setter => new TwoWayEnumBindableMember<DayOfWeek>(DayOfWeek.Monday, setter), new AnyProbe(mode), DayOfWeek.Tuesday, DayOfWeek.Wednesday);
        }

        [TestCase(BindMode.None)]
        [TestCase(BindMode.OneWayToSource)]
        [TestCase((BindMode)99)]
        public void MembersRejectModesWithoutAForwardDirectionBeforeEmitting(BindMode mode)
        {
            IBinderAdder[] members =
            {
                new OneWayBindableMember<string>("first"),
                new OneTimeBindableMember<string>("first"),
                new OneWayStructBindableMember<int>(1),
                new OneTimeStructBindableMember<int>(1),
                new OneWayEnumBindableMember<DayOfWeek>(DayOfWeek.Monday),
                new OneTimeEnumBindableMember<DayOfWeek>(DayOfWeek.Monday)
            };

            foreach (var member in members)
            {
                var binder = new AnyProbe(BindMode.OneWay);
                typeof(Binder).GetField("_mode", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                    .SetValue(binder, mode);
                Assert.Throws<InvalidOperationException>(() => binder.Bind(new BindContext(member, binder)));
                Assert.That(binder.IsBound, Is.False);
                Assert.That(binder.Values, Is.Empty);
                Assert.That(binder.ReverseSubscriptions, Is.Zero);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TwoWaySliderCanRebindBetweenOneWayAndTwoWayMembers(bool mono)
        {
            var slider = Spawn<Slider>("Slider");
            slider.maxValue = 10f;

            IBinder binder = mono
                ? slider.gameObject.AddComponent<SliderValueMonoBinder>()
                : new SliderValueBinder(slider);

            if (binder is MonoBinder monoBinder)
                monoBinder.Mode = BindMode.TwoWay;

            Assert.That(binder.Mode, Is.EqualTo(BindMode.TwoWay));

            var oneWay = new OneWayStructBindableMember<float>(2f);
            var writes = new List<float>();
            var twoWay = new TwoWayStructBindableMember<float>(3f, writes.Add);

            binder.Bind(new BindContext(oneWay, binder));
            Assert.That(slider.value, Is.EqualTo(2f));
            oneWay.Value = 4f;
            Assert.That(slider.value, Is.EqualTo(4f));
            slider.value = 5f;
            Assert.That(oneWay.Value, Is.EqualTo(4f));
            binder.Unbind(new UnbindContext(binder));

            binder.Bind(new BindContext(twoWay, binder));
            Assert.That(slider.value, Is.EqualTo(3f));
            oneWay.Value = 6f;
            Assert.That(slider.value, Is.EqualTo(3f));
            slider.value = 7f;
            Assert.That(writes, Is.EqualTo(new[] { 7f }));
            twoWay.Value = 8f;
            Assert.That(slider.value, Is.EqualTo(8f));
            binder.Unbind(new UnbindContext(binder));

            binder.Bind(new BindContext(oneWay, binder));
            Assert.That(slider.value, Is.EqualTo(6f));
            slider.value = 9f;
            Assert.That(writes, Is.EqualTo(new[] { 7f }));
            Assert.That(oneWay.Value, Is.EqualTo(6f));
            Assert.That(binder.Mode, Is.EqualTo(BindMode.TwoWay));
            binder.Unbind(new UnbindContext(binder));
        }

        private static void AssertTwoWay<T>(
            Func<Action<T>, IBindableMember<T>> createMember,
            ProbeBinder binder,
            T next,
            T input)
        {
            var writes = new List<T>();
            var member = createMember(writes.Add);
            var initial = ((IReadOnlyValueBindableMember<T>)member).Value;
            var bindToView = binder.Mode is not BindMode.OneWayToSource;
            var bindToSource = binder.Mode is BindMode.TwoWay or BindMode.OneWayToSource;

            binder.Bind(new BindContext(member, binder));
            var expected = new List<object?>();
            if (bindToView)
                expected.Add(initial);

            Assert.That(binder.Values, Is.EqualTo(expected));
            Assert.That(binder.ReverseSubscriptions, Is.EqualTo(bindToSource ? 1 : 0));
            Assert.That(writes, Is.Empty);

            member.Value = next;
            if (binder.Mode is BindMode.OneWay or BindMode.TwoWay)
                expected.Add(next);

            Assert.That(binder.Values, Is.EqualTo(expected));
            binder.Report(input);
            Assert.That(writes, Is.EqualTo(bindToSource ? new[] { input } : Array.Empty<T>()));

            binder.Unbind(new UnbindContext(binder));
            Assert.That(binder.ReverseSubscriptions, Is.Zero);
            member.Value = input;
            binder.Report(next);
            Assert.That(binder.Values, Is.EqualTo(expected));
            Assert.That(writes, Is.EqualTo(bindToSource ? new[] { input } : Array.Empty<T>()));
        }

        private static void AssertForward<T>(IBindableMember<T> member, ProbeBinder binder, T next, T input)
        {
            var initial = ((IReadOnlyValueBindableMember<T>)member).Value;
            binder.Bind(new BindContext(member, binder));
            Assert.That(binder.Values, Is.EqualTo(new object?[] { initial }));
            Assert.That(binder.ReverseSubscriptions, Is.Zero);

            binder.Report(input);
            Assert.That(((IReadOnlyValueBindableMember<T>)member).Value, Is.EqualTo(initial));

            member.Value = next;
            var expected = binder.Mode is BindMode.OneTime
                ? new object?[] { initial }
                : new object?[] { initial, next };
            Assert.That(binder.Values, Is.EqualTo(expected));

            binder.Unbind(new UnbindContext(binder));
            member.Value = input;
            Assert.That(binder.Values, Is.EqualTo(expected));
            Assert.That(binder.ReverseSubscriptions, Is.Zero);
        }

        private static void AssertOneTime<T>(
            IReadOnlyValueBindableMember<T> member,
            ProbeBinder binder,
            Action changeValue,
            T input)
        {
            var initial = member.Value;
            binder.Bind(new BindContext(member, binder));
            Assert.That(binder.Values, Is.EqualTo(new object?[] { initial }));
            Assert.That(binder.ReverseSubscriptions, Is.Zero);

            binder.Report(input);
            Assert.That(member.Value, Is.EqualTo(initial));
            changeValue();
            binder.Unbind(new UnbindContext(binder));
            Assert.That(binder.Values, Is.EqualTo(new object?[] { initial }));
        }

        private abstract class ProbeBinder : Binder
        {
            public readonly List<object?> Values = new();
            public int ReverseSubscriptions;

            protected ProbeBinder(BindMode mode) : base(mode) { }

            public abstract void Report(object? value);
        }

        private class TypedProbe<T> : ProbeBinder, IBinder<T>, IReverseBinder<T>
        {
            private Action<T?>? _valueChanged;

            public TypedProbe(BindMode mode) : base(mode) { }

            public event Action<T?>? ValueChanged
            {
                add { _valueChanged += value; ReverseSubscriptions++; }
                remove { _valueChanged -= value; ReverseSubscriptions--; }
            }

            public void SetValue(T? value) =>
                Values.Add(value);

            public override void Report(object? value) =>
                _valueChanged?.Invoke((T?)value);
        }

        private sealed class AnyProbe : ProbeBinder, IAnyBinder, IAnyReverseBinder
        {
            private Action<object>? _valueChanged;

            public AnyProbe(BindMode mode) : base(mode) { }

            public event Action<object>? ValueChanged
            {
                add { _valueChanged += value; ReverseSubscriptions++; }
                remove { _valueChanged -= value; ReverseSubscriptions--; }
            }

            public void SetValue<T>(T value) =>
                Values.Add(value);

            public override void Report(object? value) =>
                _valueChanged?.Invoke(value!);
        }
    }
}
