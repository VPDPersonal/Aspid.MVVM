using System;
using System.Linq;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class BindingFailureTests
    {
        [Test]
        public void RepeatedBindThrowsWithoutDisturbingTheOriginalBinding()
        {
            var original = new TwoWayStructBindableMember<int>(1, _ => { });
            var other = new TwoWayStructBindableMember<int>(2, _ => { });
            var binder = new FailureBinder<int>();
            binder.Bind(new BindContext(original, binder));

            Assert.Throws<InvalidOperationException>(() => binder.Bind(new BindContext(other, binder)));
            Assert.That(binder.IsBound, Is.True);
            Assert.That(binder.Resource, Is.True);
            Assert.That(binder.Listeners, Is.EqualTo(1));
            var calls = binder.Calls;
            other.Value = 3;
            Assert.That(binder.Calls, Is.EqualTo(calls));
            original.Value = 4;
            Assert.That(binder.Calls, Is.EqualTo(calls + 1));
            binder.Unbind(new UnbindContext(binder));
            Assert.That(binder.Listeners, Is.Zero);
        }

        [TestCase("binding")]
        [TestCase("value")]
        [TestCase("attach")]
        public void ReentrantBindRollsBackAndCanRetry(string stage)
        {
            var original = new TwoWayStructBindableMember<int>(1, _ => { });
            var other = new TwoWayStructBindableMember<int>(2, _ => { });
            var binder = new FailureBinder<int>();
            binder.OnStage = current =>
            {
                if (current == stage)
                    binder.Bind(new BindContext(other, binder));
            };

            Assert.Throws<InvalidOperationException>(() => binder.Bind(new BindContext(original, binder)));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.Resource, Is.False);
            Assert.That(binder.Listeners, Is.Zero);
            var calls = binder.Calls;
            original.Value = other.Value = 3;
            Assert.That(binder.Calls, Is.EqualTo(calls));
            binder.OnStage = null;
            binder.Bind(new BindContext(original, binder));
            Assert.That(binder.IsBound, Is.True);
            binder.Unbind(new UnbindContext(binder));
        }

        [TestCase(BindMode.None)]
        [TestCase((BindMode)99)]
        [TestCase((BindMode)(-1))]
        public void TwoWayMembersRejectUnsupportedRemovalModes(BindMode mode)
        {
            var reference = new TwoWayBindableMember<string>("initial", _ => { });
            var number = new TwoWayStructBindableMember<int>(1, _ => { });
            var enumeration = new TwoWayEnumBindableMember<DayOfWeek>(DayOfWeek.Monday, _ => { });

            Assert.Throws<InvalidOperationException>(() => ((IBinderRemover)reference).Remove(WithSerializedMode(new FailureBinder<string>(), mode)));
            Assert.Throws<InvalidOperationException>(() => ((IBinderRemover)number).Remove(WithSerializedMode(new FailureBinder<int>(), mode)));
            Assert.Throws<InvalidOperationException>(() => ((IBinderRemover)enumeration).Remove(WithSerializedMode(new FailureBinder<Enum>(), mode)));
        }

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
        [TestCase("attach")]
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
            Assert.Throws<InvalidOperationException>(() => binder.Bind(new BindContext(member, binder)));
            binder.Unbind(new UnbindContext(binder));
            binder.Stage = null;
            binder.Bind(new BindContext(member, binder));
            binder.Unbind(new UnbindContext(binder));
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
            Assert.Throws<InvalidOperationException>(() => boxed.Bind(new BindContext(member, boxed)));
            boxed.Unbind(new UnbindContext(boxed));
            var calls = boxed.Calls;
            member.Value = DayOfWeek.Friday;
            Assert.That(boxed.Calls, Is.EqualTo(calls));
            Assert.That(boxed.Listeners, Is.Zero);

            var any = new AnyFailureBinder();
            Assert.Throws<InvalidOperationException>(() => any.Bind(new BindContext(member, any)));
            any.Unbind(new UnbindContext(any));
            calls = any.Calls;
            member.Value = DayOfWeek.Saturday;
            Assert.That(any.Calls, Is.EqualTo(calls));
            Assert.That(any.Listeners, Is.Zero);
        }

        [TestCase(false, BindMode.TwoWay)]
        [TestCase(true, BindMode.TwoWay)]
        [TestCase(false, BindMode.OneWayToSource)]
        [TestCase(true, BindMode.OneWayToSource)]
        public void ReverseSubscriptionAndRollbackFailuresAreReportedInOrder(bool useStructMember, BindMode mode)
        {
            var member = CreateTwoWayMember(useStructMember);
            var subscription = new InvalidOperationException("subscribe");
            var rollback = new InvalidOperationException("unsubscribe");
            var binder = new FailureBinder<int>(mode);
            binder.OnStage = stage =>
            {
                if (stage == "event")
                    throw subscription;

                if (stage == "removeEvent")
                    throw rollback;
            };

            var failure = Assert.Throws<AggregateException>(() => binder.Bind(new BindContext(member, binder)));
            Assert.That(failure.InnerExceptions, Is.EqualTo(new[] { subscription, rollback }));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.Listeners, Is.Zero);
            var calls = binder.Calls;
            member.Value = 2;
            Assert.That(binder.Calls, Is.EqualTo(calls));

            binder.OnStage = null;
            binder.Bind(new BindContext(member, binder));
            Assert.That(binder.Listeners, Is.EqualTo(1));
            binder.Unbind(new UnbindContext(binder));
            Assert.That(binder.Listeners, Is.Zero);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void IncompatibleSourceOnlyBinderReportsOneTypeError(bool useStructMember)
        {
            var member = CreateTwoWayMember(useStructMember);
            var binder = WithSerializedMode(new ForwardBinder<int>(), BindMode.OneWayToSource);

            Assert.Throws<ReverseBinderInvalidCastException<int>>(() => binder.Bind(new BindContext(member, binder)));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.Calls, Is.Zero);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void FailedInitialPushKeepsAnExistingListener(bool useStructMember)
        {
            var member = CreateTwoWayMember(useStructMember);
            var binder = new FailureBinder<int> { Stage = "value" };
            member.Changed += binder.SetValue;

            Assert.Throws<InvalidOperationException>(() => binder.Bind(new BindContext(member, binder)));
            binder.Stage = null;
            var calls = binder.Calls;
            member.Value = 2;
            Assert.That(binder.Calls, Is.EqualTo(calls + 1));
            Assert.That(binder.Listeners, Is.Zero);
        }

        [Test]
        public void SourceOnlyMembersUndoThrowingEventAccessors()
        {
            var classBinder = new FailureBinder<string>(BindMode.OneWayToSource) { Stage = "event" };
            var structBinder = new FailureBinder<ValueType>(BindMode.OneWayToSource) { Stage = "event" };
            var enumBinder = new FailureBinder<Enum>(BindMode.OneWayToSource) { Stage = "event" };
            Assert.Throws<InvalidOperationException>(() => classBinder.Bind(new BindContext(new OneWayToSourceBindableMember<string>(_ => { }), classBinder)));
            Assert.Throws<InvalidOperationException>(() => structBinder.Bind(new BindContext(new OneWayToSourceStructBindableMember<int>(_ => { }), structBinder)));
            Assert.Throws<InvalidOperationException>(() => enumBinder.Bind(new BindContext(new OneWayToSourceEnumBindableMember<DayOfWeek>(_ => { }), enumBinder)));
            Assert.That(classBinder.Listeners + structBinder.Listeners + enumBinder.Listeners, Is.Zero);
        }

        [TestCase("unbinding")]
        [TestCase("unbound")]
        public void FailedUnbindStillDetachesAndResetsState(string stage)
        {
            var member = new TwoWayStructBindableMember<int>(1, _ => { });
            var binder = new FailureBinder<int>();
            binder.Bind(new BindContext(member, binder));
            binder.Stage = stage;
            Assert.That(Assert.Throws<InvalidOperationException>(() => binder.Unbind(new UnbindContext(binder))).Message, Is.EqualTo(stage));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.Listeners, Is.Zero);
            binder.Unbind(new UnbindContext(binder));
            var calls = binder.Calls;
            member.Value = 2;
            Assert.That(binder.Calls, Is.EqualTo(calls));
            binder.Stage = null;
            binder.Bind(new BindContext(member, binder));
            calls = binder.Calls;
            member.Value = 3;
            Assert.That(binder.Calls, Is.EqualTo(calls + 1));
            binder.Unbind(new UnbindContext(binder));
        }

        [Test]
        public void FailedBindingReportsTheOriginalFailureAndBothRollbackFailures()
        {
            var binder = new FailureBinder<int> { Stage = "attach", FailCleanup = true };
            var context = new BindContext(new TwoWayStructBindableMember<int>(1, _ => { }), binder);
            var failure = Assert.Throws<AggregateException>(() => binder.Bind(in context));
            var errors = failure.Flatten().InnerExceptions;
            Assert.That(errors, Has.All.TypeOf<InvalidOperationException>());
            Assert.That(errors.Select(error => error.Message), Is.EqualTo(new[] { "attach", "unbinding", "unbound" }));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.Resource, Is.False);
            Assert.That(binder.Listeners, Is.Zero);
        }

        private static IBindableMember<int> CreateTwoWayMember(bool useStructMember) => useStructMember
            ? new TwoWayStructBindableMember<int>(1, _ => { })
            : new TwoWayBindableMember<int>(1, _ => { });

        private static T WithSerializedMode<T>(T binder, BindMode mode)
            where T : Binder
        {
            typeof(Binder).GetField("_mode", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(binder, mode);
            return binder;
        }

        private static void AssertIncompatible<TValue, TBinding>(
            IBindableMember<TValue> member,
            ForwardBinder<TBinding> binder,
            TValue next)
        {
            Assert.That(Assert.Catch(() => binder.Bind(new BindContext(member, binder))).GetType().Name, Does.StartWith("ReverseBinderInvalidCastException"));
            binder.Unbind(new UnbindContext(binder));
            member.Value = next;
            Assert.That(binder.Calls, Is.Zero);
        }

        private static void AssertFailure<T>(IBindableMember<T> member, T next, string stage)
        {
            var binder = new FailureBinder<T> { Stage = stage };
            Assert.Throws<InvalidOperationException>(() => binder.Bind(new BindContext(member, binder)));
            binder.Unbind(new UnbindContext(binder));
            var calls = binder.Calls;
            member.Value = next;
            Assert.That(binder.Calls, Is.EqualTo(calls));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.Resource, Is.False);
            Assert.That(binder.Listeners, Is.Zero);
            binder.Stage = null;
            binder.Bind(new BindContext(member, binder));
            calls = binder.Calls;
            member.Value = next;
            Assert.That(binder.Calls, Is.EqualTo(calls + 1));
            Assert.That(binder.Listeners, Is.EqualTo(1));
            binder.Unbind(new UnbindContext(binder));
            binder.Unbind(new UnbindContext(binder));
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
            public Action<string> OnStage;

            public FailureBinder(BindMode mode = BindMode.TwoWay) : base(mode) { }

            public event Action<T> ValueChanged
            {
                add
                {
                    _changed += value;
                    Fail("event");
                }
                remove
                {
                    _changed -= value;
                    Fail("removeEvent");
                }
            }

            public int Listeners =>
                _changed?.GetInvocationList().Length ?? 0;

            public void SetValue(T value)
            {
                Calls++;
                Fail("value");
            }

            protected override void OnBinding()
            {
                Resource = true;
                Fail("binding");
            }

            protected override void OnAttach() =>
                Fail("attach");

            protected override void OnUnbinding() =>
                Fail("unbinding");

            protected override void OnDetach()
            {
                Resource = false;
                Fail("unbound");
            }

            private void Fail(string stage)
            {
                OnStage?.Invoke(stage);
                if (Stage == stage || FailCleanup && stage is "unbinding" or "unbound")
                    throw new InvalidOperationException(stage);
            }
        }

        private sealed class AnyFailureBinder : Binder, IAnyBinder, IAnyReverseBinder
        {
            private Action<object> _changed;
            public int Calls;

            public AnyFailureBinder() : base(BindMode.TwoWay) { }

            public event Action<object> ValueChanged
            {
                add
                {
                    _changed += value;
                    throw new InvalidOperationException("event");
                }
                remove
                {
                    _changed -= value;
                }
            }

            public int Listeners =>
                _changed?.GetInvocationList().Length ?? 0;

            public void SetValue<T>(T value) =>
                Calls++;
        }
    }
}
