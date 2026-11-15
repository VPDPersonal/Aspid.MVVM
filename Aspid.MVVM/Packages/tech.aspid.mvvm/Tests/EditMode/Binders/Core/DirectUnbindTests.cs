using System;
using NUnit.Framework;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class DirectUnbindTests
    {
        [Test]
        public void CleanupEnumerationAndEnumeratorDisposalFailuresAreAllRetained()
        {
            var cleanup = new Exception("cleanup");
            var enumeration = new Exception("enumeration");
            var disposal = new Exception("dispose enumerator");
            var binder = new CallbackTestBinder { Unbound = () => throw cleanup };
            binder.Bind(new BindContext(new OneTimeStructBindableMember<int>(1), this));
            var source = new FaultingEnumerable(binder, enumeration, disposal);

            var aggregate = Assert.Throws<AggregateException>(() => source.Unbind(new UnbindContext(this)));

            Assert.That(aggregate.InnerExceptions, Is.EqualTo(new[] { cleanup, enumeration, disposal }));
            Assert.That(binder.IsBound, Is.False);
        }

        [Test]
        public void NestedBinderAndCollectionFailuresCanBeFlattenedWithoutLosingErrors()
        {
            var before = new Exception("before");
            var detached = new Exception("detach");
            var other = new Exception("other binder");
            var nested = new DoubleFailureBinder(before, detached);
            var second = new CallbackTestBinder { Unbound = () => throw other };
            var member = new OneTimeStructBindableMember<int>(1);
            nested.Bind(new BindContext(member, this));
            second.Bind(new BindContext(member, this));

            var aggregate = Assert.Throws<AggregateException>(() =>
                new IBinder[] { nested, second }.Unbind(new UnbindContext(this)));

            Assert.That(aggregate.Flatten().InnerExceptions, Is.EquivalentTo(new[] { before, detached, other }));
            Assert.That(nested.IsBound || second.IsBound, Is.False);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void CollectionsFinishCleanupAndReportEveryFailure(int route)
        {
            var firstFailure = new InvalidOperationException("first");
            var first = new CallbackTestBinder { Unbound = () => throw firstFailure };
            var secondFailure = new Exception("second");
            var second = new CallbackTestBinder { Unbound = () => throw secondFailure };
            var healthy = new CallbackTestBinder();
            var foreign = new CallbackTestBinder();
            var member = new OneTimeStructBindableMember<int>(1);
            var context = new UnbindContext(this);
            foreach (var binder in new[] { first, second, healthy })
                binder.Bind(new BindContext(member, this));
            foreign.Bind(new BindContext(member, foreign));
            IBinder[] binders = { first, null, second, healthy, foreign };

            var failure = Assert.Catch(() =>
            {
                switch (route)
                {
                    case 0:
                        BinderExtensions.Unbind(binders, in context);
                        break;
                    case 1:
                        BinderExtensions.Unbind(new List<IBinder>(binders), in context);
                        break;
                    default:
                        BinderExtensions.Unbind(Enumerate(), in context);
                        break;
                }
            });

            Assert.That(failure, Is.TypeOf<AggregateException>());
            Assert.That(((AggregateException)failure).InnerExceptions, Is.EqualTo(new[] { firstFailure, secondFailure }));
            Assert.That(first.IsBound || second.IsBound || healthy.IsBound, Is.False);
            Assert.That(foreign.IsBound, Is.True);
            foreign.Unbind(new UnbindContext(foreign));

            IEnumerable<IBinder> Enumerate()
            {
                foreach (var binder in binders)
                    yield return binder;
            }
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void OneCollectionFailureRetainsTheOriginalException(int route)
        {
            var failure = new InvalidOperationException("single");
            var binder = new CallbackTestBinder { Unbound = () => throw failure };
            binder.Bind(new BindContext(new OneTimeStructBindableMember<int>(1), this));
            IBinder[] binders = { null, binder };
            var context = new UnbindContext(this);
            var thrown = Assert.Catch(() =>
            {
                switch (route)
                {
                    case 0: binders.Unbind(in context); break;
                    case 1: new List<IBinder>(binders).Unbind(in context); break;
                    default: ((IEnumerable<IBinder>)binders).Unbind(in context); break;
                }
            });
            Assert.That(thrown, Is.SameAs(failure));
            Assert.That(binder.IsBound, Is.False);
        }

        [Test]
        public void EnumerationAndCleanupFailuresAreBothReported()
        {
            var failure = new InvalidOperationException("cleanup");
            var binder = new CallbackTestBinder { Unbound = () => throw failure };
            binder.Bind(new BindContext(new OneTimeStructBindableMember<int>(1), this));
            var enumerationFailure = new Exception("enumeration");
            var aggregate = Assert.Throws<AggregateException>(() => BinderExtensions.Unbind(
                Enumerate(), new UnbindContext(this)));
            Assert.That(aggregate.InnerExceptions, Is.EqualTo(new[] { failure, enumerationFailure }));
            Assert.That(binder.IsBound, Is.False);

            IEnumerable<IBinder> Enumerate()
            {
                yield return binder;
                throw enumerationFailure;
            }
        }
        private sealed class FaultingEnumerable : IEnumerable<IBinder>, IEnumerator<IBinder>
        {
            private readonly IBinder _binder;
            private readonly Exception _moveNextFailure;
            private readonly Exception _disposeFailure;
            private bool _yielded;

            public FaultingEnumerable(
                IBinder binder,
                Exception moveNextFailure,
                Exception disposeFailure)
            {
                _binder = binder;
                _disposeFailure = disposeFailure;
                _moveNextFailure = moveNextFailure;
            }

            public IBinder Current =>
                _binder;

            object System.Collections.IEnumerator.Current =>
                Current;

            public IEnumerator<IBinder> GetEnumerator() =>
                this;

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() =>
                GetEnumerator();

            public bool MoveNext()
            {
                if (_yielded)
                    throw _moveNextFailure;
                _yielded = true;
                return true;
            }

            public void Reset() =>
                throw new NotSupportedException();

            public void Dispose() =>
                throw _disposeFailure;
        }

        private sealed class DoubleFailureBinder : Binder, IBinder<int>
        {
            private readonly Exception _first;
            private readonly Exception _second;

            public DoubleFailureBinder(
                Exception first,
                Exception second)
            {
                _first = first;
                _second = second;
            }

            public void SetValue(int value) { }

            protected override void OnUnbinding() =>
                throw _first;

            protected override void OnDetach() =>
                throw _second;
        }
    }
}
