using System;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class BindingFailureTests
    {
        [Test]
        public void ReverseCompatibilityIsCheckedBeforeForwardEmission()
        {
            AssertIncompatible(new TwoWayBindableMember<string>("initial", _ => { }), new ForwardBinder<string>(), "next");
            AssertIncompatible(new TwoWayStructBindableMember<int>(1, _ => { }), new ForwardBinder<int>(), 2);
            AssertIncompatible(new TwoWayStructBindableMember<int>(1, _ => { }), new ForwardBinder<ValueType>(), 2);
            AssertIncompatible(new TwoWayEnumBindableMember<DayOfWeek>(DayOfWeek.Monday, _ => { }), new ForwardBinder<DayOfWeek>(), DayOfWeek.Tuesday);
            AssertIncompatible(new TwoWayEnumBindableMember<DayOfWeek>(DayOfWeek.Monday, _ => { }), new ForwardBinder<Enum>(), DayOfWeek.Tuesday);
        }

        [TestCase("binding")]
        [TestCase("value")]
        [TestCase("bound")]
        [TestCase("event")]
        public void FailedBindReleasesResourcesAndCanRetry(string stage)
        {
            AssertFailure(new TwoWayBindableMember<string>("first", _ => { }), "next", stage);
            AssertFailure(new TwoWayStructBindableMember<int>(1, _ => { }), 2, stage);
            AssertFailure(new TwoWayEnumBindableMember<DayOfWeek>(DayOfWeek.Monday, _ => { }), DayOfWeek.Tuesday, stage);
        }

        [TestCase(BindMode.OneWay)]
        [TestCase(BindMode.OneTime)]
        [TestCase(BindMode.OneWayToSource)]
        public void OtherDirectionsCanRetryAfterCallbackFailure(BindMode mode)
        {
            var member = new TwoWayStructBindableMember<int>(1, _ => { });
            var binder = new FailureBinder<int>(mode) { Stage = mode is BindMode.OneWayToSource ? "event" : "value" };
            Assert.Throws<InvalidOperationException>(() => binder.Bind(member));
            binder.Unbind();
            binder.Stage = null;
            binder.Bind(member);
            binder.Unbind();
            var calls = binder.Calls;
            member.Value = 5;
            Assert.That(binder.Calls, Is.EqualTo(calls));
            Assert.That(binder.Listeners, Is.Zero);
        }

        [Test]
        public void BoxedAndAnyReverseEventFailuresRollBackBothDirections()
        {
            var member = new TwoWayEnumBindableMember<DayOfWeek>(DayOfWeek.Monday, _ => { });
            var boxed = new FailureBinder<Enum> { Stage = "event" };
            Assert.Throws<InvalidOperationException>(() => boxed.Bind(member));
            boxed.Unbind();
            var calls = boxed.Calls;
            member.Value = DayOfWeek.Friday;
            Assert.That(boxed.Calls, Is.EqualTo(calls));
            Assert.That(boxed.Listeners, Is.Zero);

            var any = new AnyFailureBinder();
            Assert.Throws<InvalidOperationException>(() => any.Bind(member));
            any.Unbind();
            calls = any.Calls;
            member.Value = DayOfWeek.Saturday;
            Assert.That(any.Calls, Is.EqualTo(calls));
            Assert.That(any.Listeners, Is.Zero);
        }

        [Test]
        public void SourceOnlyMembersUndoThrowingEventAccessors()
        {
            var classBinder = new FailureBinder<string>(BindMode.OneWayToSource) { Stage = "event" };
            var structBinder = new FailureBinder<ValueType>(BindMode.OneWayToSource) { Stage = "event" };
            var enumBinder = new FailureBinder<Enum>(BindMode.OneWayToSource) { Stage = "event" };
            Assert.Throws<InvalidOperationException>(() => classBinder.Bind(new OneWayToSourceBindableMember<string>(_ => { })));
            Assert.Throws<InvalidOperationException>(() => structBinder.Bind(new OneWayToSourceStructBindableMember<int>(_ => { })));
            Assert.Throws<InvalidOperationException>(() => enumBinder.Bind(new OneWayToSourceEnumBindableMember<DayOfWeek>(_ => { })));
            Assert.That(classBinder.Listeners + structBinder.Listeners + enumBinder.Listeners, Is.Zero);
        }

        [TestCase("unbinding")]
        [TestCase("unbound")]
        public void FailedUnbindStillDetachesAndResetsState(string stage)
        {
            var member = new TwoWayStructBindableMember<int>(1, _ => { });
            var binder = new FailureBinder<int>();
            binder.Bind(member);
            binder.Stage = stage;
            Assert.That(Assert.Throws<InvalidOperationException>(() => binder.Unbind()).Message, Is.EqualTo(stage));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.Listeners, Is.Zero);
            binder.Unbind();
            var calls = binder.Calls;
            member.Value = 2;
            Assert.That(binder.Calls, Is.EqualTo(calls));
            binder.Stage = null;
            binder.Bind(member);
            calls = binder.Calls;
            member.Value = 3;
            Assert.That(binder.Calls, Is.EqualTo(calls + 1));
            binder.Unbind();
        }

        [Test]
        public void CleanupFailuresDoNotReplaceTheOriginalBindingFailure()
        {
            var binder = new FailureBinder<int> { Stage = "bound", FailCleanup = true };
            Assert.That(Assert.Throws<InvalidOperationException>(() => binder.Bind(new TwoWayStructBindableMember<int>(1, _ => { }))).Message,
                Is.EqualTo("bound"));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.Resource, Is.False);
            Assert.That(binder.Listeners, Is.Zero);
        }

        private static void AssertIncompatible<TValue, TBinding>(
            IBindableMember<TValue> member,
            ForwardBinder<TBinding> binder,
            TValue next)
        {
            Assert.That(Assert.Catch(() => binder.Bind(member)).GetType().Name, Does.StartWith("ReverseBinderInvalidCastException"));
            binder.Unbind();
            member.Value = next;
            Assert.That(binder.Calls, Is.Zero);
        }

        private static void AssertFailure<T>(IBindableMember<T> member, T next, string stage)
        {
            var binder = new FailureBinder<T> { Stage = stage };
            Assert.Throws<InvalidOperationException>(() => binder.Bind(member));
            binder.Unbind();
            var calls = binder.Calls;
            member.Value = next;
            Assert.That(binder.Calls, Is.EqualTo(calls));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.Resource, Is.False);
            Assert.That(binder.Listeners, Is.Zero);
            binder.Stage = null;
            binder.Bind(member);
            calls = binder.Calls;
            member.Value = next;
            Assert.That(binder.Calls, Is.EqualTo(calls + 1));
            Assert.That(binder.Listeners, Is.EqualTo(1));
            binder.Unbind();
            binder.Unbind();
        }

        private class ForwardBinder<T> : Binder, IBinder<T>
        {
            public int Calls;
            public ForwardBinder() : base(BindMode.TwoWay) { }
            public void SetValue(T value) =>
                Calls++;
        }

        private class FailureBinder<T> : Binder, IBinder<T>, IReverseBinder<T>
        {
            private Action<T> _changed;
            public string Stage;
            public bool FailCleanup;
            public bool Resource;
            public int Calls;
            public int Listeners => _changed?.GetInvocationList().Length ?? 0;
            public FailureBinder(BindMode mode = BindMode.TwoWay) : base(mode) { }
            public event Action<T> ValueChanged
            {
                add { _changed += value; Fail("event"); }
                remove { _changed -= value; }
            }
            public void SetValue(T value) { Calls++; Fail("value"); }
            protected override void OnBinding() { Resource = true; Fail("binding"); }
            protected override void OnBound() =>
                Fail("bound");
            protected override void OnUnbinding() =>
                Fail("unbinding");
            protected override void OnUnbound() { Resource = false; Fail("unbound"); }
            private void Fail(string stage)
            {
                if (Stage == stage || FailCleanup && stage is "unbinding" or "unbound")
                    throw new InvalidOperationException(stage);
            }
        }

        private sealed class AnyFailureBinder : Binder, IAnyBinder, IAnyReverseBinder
        {
            private Action<object> _changed;
            public int Calls;
            public int Listeners => _changed?.GetInvocationList().Length ?? 0;
            public AnyFailureBinder() : base(BindMode.TwoWay) { }
            public event Action<object> ValueChanged
            {
                add { _changed += value; throw new InvalidOperationException("event"); }
                remove { _changed -= value; }
            }
            public void SetValue<T>(T value) =>
                Calls++;
        }
    }
}
