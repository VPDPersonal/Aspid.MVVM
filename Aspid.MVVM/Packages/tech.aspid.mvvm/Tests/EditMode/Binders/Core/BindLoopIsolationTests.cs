using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests failure propagation and cleanup in null-tolerant binder operations.
    /// </summary>
    [TestFixture]
    public sealed class BindLoopIsolationTests : SceneFixture
    {
        [Test]
        public void ScalarSafelyDelegatesContextValidationToTheBinder()
        {
            var binder = new SpyBinder();
            binder.BindSafely(default(BindContext));
            Assert.That(binder.IsBound, Is.True);

            binder.UnbindSafely(default(UnbindContext));
            Assert.That(binder.IsBound, Is.False);
            LogAssert.NoUnexpectedReceived();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void UnbindSafely_RejectsAnUninitializedContextBeforeCallingACustomBinder(int shape)
        {
            var binder = new SpyBinder { IsBound = true };
            Assert.Throws<ArgumentException>(() => UnbindShape(new[] { binder }, default, shape));

            Assert.That(binder.IsBound, Is.True);
            LogAssert.NoUnexpectedReceived();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void UnbindSafely_PreservesTheContextOwner(int shape)
        {
            var owner = new object();
            var member = new OneWayStructBindableMember<int>(1);
            var binders = new[] { new ContextProbeBinder(), new ContextProbeBinder() };
            foreach (var binder in binders)
                binder.Bind(new BindContext(member, owner));

            UnbindShape(binders, new UnbindContext(this), shape);
            foreach (var binder in binders)
            {
                Assert.That(binder.IsBound, Is.True);
                Assert.That(binder.CurrentOwner, Is.SameAs(owner));
            }

            UnbindShape(binders, new UnbindContext(owner), shape);
            member.Value = 2;
            foreach (var binder in binders)
            {
                Assert.That(binder.IsBound, Is.False);
                Assert.That(binder.CurrentOwner, Is.Null);
                Assert.That(binder.Value, Is.EqualTo(1));
            }
            LogAssert.NoUnexpectedReceived();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void BindSafely_PreservesTheContextMemberAndOwner(int shape)
        {
            var owner = new object();
            var member = new OneWayStructBindableMember<int>(1);
            var context = new BindContext(member, owner);
            var binders = new[] { new ContextProbeBinder(), new ContextProbeBinder() };

            switch (shape)
            {
                case 0:
                    foreach (var binder in binders)
                        binder.BindSafely(in context);
                    break;
                case 1:
                    binders.BindSafely(in context);
                    break;
                case 2:
                    new List<ContextProbeBinder>(binders).BindSafely(in context);
                    break;
                default:
                    IEnumerable<ContextProbeBinder> sequence = binders;
                    sequence.BindSafely(in context);
                    break;
            }

            member.Value = 2;
            foreach (var binder in binders)
            {
                Assert.That(binder.CurrentOwner, Is.SameAs(owner));
                Assert.That(binder.Value, Is.EqualTo(2));
                binder.Unbind(new UnbindContext(this));
                Assert.That(binder.IsBound, Is.True);
                binder.Unbind(new UnbindContext(owner));
                Assert.That(binder.IsBound, Is.False);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void ScalarSafelyPropagatesTheBinderFailure(bool unbind)
        {
            var binder = new SpyBinder { ThrowOnBind = true, ThrowOnUnbind = true };
            var member = new OneWayBindableMember<string>(null);
            TestDelegate operation = unbind
                ? () => binder.UnbindSafely(new UnbindContext(this))
                : () => binder.BindSafely(new BindContext(member, this));

            var failure = Assert.Throws<InvalidOperationException>(operation);

            Assert.That(failure.StackTrace, Does.Contain(unbind ? "SpyBinder.Unbind" : "SpyBinder.Bind"));
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void BindSafelySkipsNullAndStopsOnFailure([Values(0, 1, 2, 3, 4)] int shape)
        {
            var first = new SpyBinder();
            var later = new SpyBinder();
            var broken = new SpyBinder { ThrowOnBind = true };
            var binders = new IBinder[] { null, first, broken, later };
            var context = new BindContext(new OneWayBindableMember<string>(null), this);

            Assert.Throws<InvalidOperationException>(() =>
            {
                switch (shape)
                {
                    case 0: binders.BindSafely(in context); break;
                    case 1: new List<IBinder>(binders).BindSafely(in context); break;
                    case 2: new FailingBinderEnumerable(binders).BindSafely(in context); break;
                    case 3: binders.AsSpan().BindSafely(in context); break;
                    case 4: new ReadOnlySpan<IBinder>(binders).BindSafely(in context); break;
                }
            });

            Assert.That(first.IsBound, Is.True);
            Assert.That(later.IsBound, Is.False);
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void UnbindSafelySkipsNullAndRethrowsAfterCleaningUp([Values(0, 1, 2, 3, 4)] int shape)
        {
            var survivor = new SpyBinder { IsBound = true };
            var broken = new SpyBinder { ThrowOnUnbind = true };
            var binders = new IBinder[] { null, broken, survivor };
            var context = new UnbindContext(this);

            var failure = Assert.Throws<InvalidOperationException>(() =>
            {
                switch (shape)
                {
                    case 0: binders.UnbindSafely(in context); break;
                    case 1: new List<IBinder>(binders).UnbindSafely(in context); break;
                    case 2: new FailingBinderEnumerable(binders).UnbindSafely(in context); break;
                    case 3: binders.AsSpan().UnbindSafely(in context); break;
                    case 4: new ReadOnlySpan<IBinder>(binders).UnbindSafely(in context); break;
                }
            });

            Assert.That(failure.StackTrace, Does.Contain("SpyBinder.Unbind"));
            Assert.That(survivor.IsBound, Is.False);
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void NullInputsProduceNoLogs()
        {
            IBinder missing = null;
            IBinder[] missingArray = null;
            var member = new OneWayBindableMember<string>(null);
            missing.BindSafely(new BindContext(member, this));
            missing.UnbindSafely(new UnbindContext(this));
            missingArray.BindSafely(new BindContext(member, this));
            missingArray.UnbindSafely(new UnbindContext(this));

            LogAssert.NoUnexpectedReceived();
        }

        [TestCase(false)]
        [TestCase(true)]
        public void EnumerationFailuresPropagateAfterProcessingEarlierEntries(bool unbind)
        {
            var binder = new SpyBinder { IsBound = unbind };
            var failure = new InvalidOperationException("enumeration");

            Assert.That(Assert.Throws<InvalidOperationException>(() => RunSafely(Enumerate(), unbind)), Is.SameAs(failure));

            Assert.That(binder.IsBound, Is.EqualTo(!unbind));
            LogAssert.NoUnexpectedReceived();

            IEnumerable<IBinder> Enumerate()
            {
                yield return binder;
                throw failure;
            }
        }

        [Test]
        public void Safely_HandlesSimultaneousEnumerationAndDisposalFailures(
            [Values] bool unbind,
            [Values] bool failReadingCurrent,
            [Values(0, 1, 2)] int shape)
        {
            var first = new SpyBinder { IsBound = unbind };
            var second = new SpyBinder { IsBound = unbind };
            var enumeration = new Exception("enumeration");
            var disposal = new Exception("disposal");
            var source = CreateFailingSource(shape, first, second);
            source.MoveNextFailure = failReadingCurrent ? null : enumeration;
            source.CurrentFailure = failReadingCurrent ? enumeration : null;
            source.CurrentFailureIndex = 1;
            source.DisposalFailure = disposal;

            if (unbind)
            {
                var aggregate = Assert.Throws<AggregateException>(() => RunSafely(source, unbind, shape));
                Assert.That(aggregate.InnerExceptions, Is.EqualTo(new[] { enumeration, disposal }));
            }
            else
            {
                Assert.That(Assert.Throws<Exception>(() => RunSafely(source, unbind, shape)), Is.SameAs(disposal));
            }

            Assert.That(first.IsBound, Is.EqualTo(!unbind));
            Assert.That(second.IsBound, Is.EqualTo(failReadingCurrent ? unbind : !unbind));
            Assert.That(source.DisposeCalls, Is.EqualTo(1));
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void Safely_RethrowsTheOriginalEnumeratorFailure(
            [Values] bool unbind,
            [Values(0, 1, 2, 3)] int stage,
            [Values(0, 1, 2)] int shape)
        {
            var failure = new Exception("single");
            var binder = new SpyBinder { IsBound = unbind };
            var source = CreateFailingSource(shape, binder);
            source.AcquisitionFailure = stage is 0 ? failure : null;
            source.MoveNextFailure = stage is 1 ? failure : null;
            source.CurrentFailure = stage is 2 ? failure : null;
            source.DisposalFailure = stage is 3 ? failure : null;
            var index = stage is 1 or 3 ? 1 : 0;
            Assert.That(Assert.Throws<Exception>(() => RunSafely(source, unbind, shape)), Is.SameAs(failure));

            Assert.That(failure.StackTrace, Does.Contain(nameof(ThrowEnumerationFailure)));
            Assert.That(binder.IsBound, Is.EqualTo(index == 1 ? !unbind : unbind));
            Assert.That(source.GetEnumeratorCalls, Is.EqualTo(1));
            Assert.That(source.DisposeCalls, Is.EqualTo(stage is 0 ? 0 : 1));
            LogAssert.NoUnexpectedReceived();
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Safely_HandlesSimultaneousBinderAndDisposalFailures(bool unbind)
        {
            var survivor = new SpyBinder { IsBound = unbind };
            var first = new SpyBinder { ThrowOnBind = true, ThrowOnUnbind = true };
            var second = new SpyBinder { ThrowOnUnbind = true };
            var source = new FailingBinderEnumerable(null, first, second, survivor)
            {
                MoveNextFailure = new Exception("enumeration"),
                DisposalFailure = new Exception("disposal")
            };

            if (unbind)
            {
                var aggregate = Assert.Throws<AggregateException>(() => RunSafely(source, unbind));
                Assert.That(aggregate.InnerExceptions, Has.Count.EqualTo(4));
                Assert.That(aggregate.InnerExceptions[0], Is.TypeOf<InvalidOperationException>());
                Assert.That(aggregate.InnerExceptions[1], Is.TypeOf<InvalidOperationException>());
                Assert.That(aggregate.InnerExceptions[2], Is.SameAs(source.MoveNextFailure));
                Assert.That(aggregate.InnerExceptions[3], Is.SameAs(source.DisposalFailure));
            }
            else
            {
                Assert.That(Assert.Throws<Exception>(() => RunSafely(source, unbind)), Is.SameAs(source.DisposalFailure));
            }

            Assert.That(survivor.IsBound, Is.False);
            Assert.That(source.DisposeCalls, Is.EqualTo(1));
            LogAssert.NoUnexpectedReceived();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InvalidCollectionContextThrowsBeforeEnumeration(bool unbind)
        {
            var source = new FailingBinderEnumerable();

            if (unbind)
                Assert.Throws<ArgumentException>(() => source.UnbindSafely(default(UnbindContext)));
            else
                Assert.Throws<ArgumentException>(() => source.BindSafely(default(BindContext)));

            Assert.That(source.GetEnumeratorCalls, Is.Zero);
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void GeneratedViewSkipsUnresolvedBinders()
        {
            var binder = new SpyBinder { ThrowOnBind = true };
            var view = new FixedArrayBindingView { Value = new IBinder[] { binder }, Nested = binder };

            view.Initialize(new StubViewModel());

            Assert.That(view.ViewModel, Is.Not.Null);
            Assert.That(binder.IsBound, Is.False);
            LogAssert.NoUnexpectedReceived();
            view.Deinitialize();
        }

        private void RunSafely(IEnumerable<IBinder> source, bool unbind, int shape = 0)
        {
            if (unbind)
            {
                var context = new UnbindContext(this);
                if (shape is 1)
                    ((IReadOnlyCollection<IBinder>)source).UnbindSafely(in context);
                else
                    source.UnbindSafely(in context);

                return;
            }

            var bindContext = new BindContext(new OneTimeStructBindableMember<int>(1), this);
            if (shape is 1)
                ((IReadOnlyCollection<IBinder>)source).BindSafely(in bindContext);
            else
                source.BindSafely(in bindContext);
        }

        private static FailingBinderEnumerable CreateFailingSource(int shape, params IBinder[] binders) =>
            shape is 0 ? new FailingBinderEnumerable(binders) : new FailingBinderCollection(binders);

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private static void ThrowEnumerationFailure(Exception failure) =>
            throw failure;

        private static void UnbindShape<T>(T[] binders, in UnbindContext context, int shape)
            where T : IBinder
        {
            switch (shape)
            {
                case 0:
                    foreach (var binder in binders)
                        binder.UnbindSafely(in context);
                    break;
                case 1:
                    binders.UnbindSafely(in context);
                    break;
                case 2:
                    new List<T>(binders).UnbindSafely(in context);
                    break;
                default:
                    IEnumerable<T> sequence = binders;
                    sequence.UnbindSafely(in context);
                    break;
            }
        }

        private class FailingBinderEnumerable : IEnumerable<IBinder>, IEnumerator<IBinder>
        {
            private readonly IBinder[] _binders;
            private int _index = -1;

            public Exception AcquisitionFailure;
            public Exception MoveNextFailure;
            public Exception CurrentFailure;
            public Exception DisposalFailure;
            public int CurrentFailureIndex;
            public int GetEnumeratorCalls;
            public int DisposeCalls;

            public FailingBinderEnumerable(params IBinder[] binders)
            {
                _binders = binders;
            }

            public IBinder Current
            {
                get
                {
                    if (_index == CurrentFailureIndex && CurrentFailure is not null)
                        ThrowEnumerationFailure(CurrentFailure);
                    return _binders[_index];
                }
            }

            object System.Collections.IEnumerator.Current =>
                Current;

            public IEnumerator<IBinder> GetEnumerator()
            {
                GetEnumeratorCalls++;
                if (AcquisitionFailure is not null)
                    ThrowEnumerationFailure(AcquisitionFailure);
                return this;
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() =>
                GetEnumerator();

            public bool MoveNext()
            {
                _index++;
                if (_index < _binders.Length) return true;
                if (MoveNextFailure is not null)
                    ThrowEnumerationFailure(MoveNextFailure);
                return false;
            }

            public void Reset() =>
                throw new NotSupportedException();

            public void Dispose()
            {
                DisposeCalls++;
                if (DisposalFailure is not null)
                    ThrowEnumerationFailure(DisposalFailure);
            }
        }

        private sealed class FailingBinderCollection : FailingBinderEnumerable, IReadOnlyCollection<IBinder>
        {
            public int Count { get; }

            public FailingBinderCollection(IBinder[] binders) : base(binders)
            {
                Count = binders.Length;
            }
        }

        private sealed class ContextProbeBinder : Binder, IBinder<int>
        {
            public int Value;

            public object CurrentOwner =>
                Owner;

            public void SetValue(int value) =>
                Value = value;
        }
    }
}
