using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests that safe binding reports failures and isolates each binder.
    /// </summary>
    [TestFixture]
    public sealed class BindLoopIsolationTests : SceneFixture
    {
        [Test]
        public void UninitializedContextIsLoggedBeforeCallingACustomBinder()
        {
            var binder = new SpyBinder();
            LogAssert.Expect(LogType.Exception, new Regex(@"\[BindSafely\].*ArgumentException"));

            Assert.DoesNotThrow(() => binder.BindSafely(default(BindContext), "_single"));

            Assert.That(binder.IsBound, Is.False);
            LogAssert.NoUnexpectedReceived();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void UnbindSafely_LogsAnUninitializedContextBeforeCallingACustomBinder(int shape)
        {
            var binder = new SpyBinder { IsBound = true };
            LogAssert.Expect(LogType.Exception, new Regex(@"\[UnbindSafely\].*ArgumentException"));

            Assert.DoesNotThrow(() => UnbindShape(new[] { binder }, default, shape));

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
        public void BindSafely_ForOneBinder_ReportsTheFailureWithContext(bool lookup)
        {
            var binder = new SpyBinder { ThrowOnBind = true };
            var member = new OneWayBindableMember<string>(null);
            LogAssert.Expect(LogType.Exception,
                new Regex(@"\[Object\] \[BindSafely\] Binder '_single'.*InvalidOperationException"));

            Assert.DoesNotThrow(() =>
            {
                if (lookup)
                    binder.BindSafely(new FindBindableMemberResult(member), new object(), "_single");
                else
                    binder.BindSafely(new BindContext(member, new object()), "_single");
            });
            Assert.That(binder.IsBound, Is.False);
        }

        [Test]
        public void UnbindSafely_ForOneBinder_ReportsTheFailureWithContext()
        {
            var binder = new SpyBinder { IsBound = true, ThrowOnUnbind = true };
            LogAssert.Expect(LogType.Exception,
                new Regex(@"\[Object\] \[UnbindSafely\] Binder '_single'.*InvalidOperationException"));

            Assert.DoesNotThrow(() => binder.UnbindSafely(new UnbindContext(new object()), "_single"));
        }

        [TestCase(0, false)]
        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(1, true)]
        [TestCase(2, false)]
        [TestCase(2, true)]
        public void BindSafely_ReportsMissingAndFailingEntriesAndBindsTheRest(int collection, bool lookup)
        {
            var survivor = new SpyBinder();
            var binders = new IBinder[] { null, new SpyBinder { ThrowOnBind = true }, survivor };
            var member = new OneWayBindableMember<string>(null);
            ExpectMissingLog("BindSafely");
            ExpectFailureLog("BindSafely");

            switch (collection)
            {
                case 0:
                    if (lookup)
                        binders.BindSafely(new FindBindableMemberResult(member), owner: this, memberName: "_binders");
                    else
                        binders.BindSafely(new BindContext(member, this), memberName: "_binders");
                    break;
                case 1:
                    var list = new List<IBinder>(binders);
                    if (lookup)
                        list.BindSafely(new FindBindableMemberResult(member), owner: this, memberName: "_binders");
                    else
                        list.BindSafely(new BindContext(member, this), memberName: "_binders");
                    break;
                default:
                    IEnumerable<IBinder> sequence = binders;
                    if (lookup)
                        sequence.BindSafely(new FindBindableMemberResult(member), owner: this, memberName: "_binders");
                    else
                        sequence.BindSafely(new BindContext(member, this), memberName: "_binders");
                    break;
            }

            Assert.That(survivor.IsBound, Is.True);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void UnbindSafely_ReportsMissingAndFailingEntriesAndUnbindsTheRest(int collection)
        {
            var survivor = new SpyBinder { IsBound = true };
            var binders = new IBinder[] { null, new SpyBinder { ThrowOnUnbind = true }, survivor };
            ExpectMissingLog("UnbindSafely");
            ExpectFailureLog("UnbindSafely");

            switch (collection)
            {
                case 0:
                    binders.UnbindSafely(new UnbindContext(this), memberName: "_binders");
                    break;
                case 1:
                    new List<IBinder>(binders).UnbindSafely(new UnbindContext(this), memberName: "_binders");
                    break;
                default:
                    ((IEnumerable<IBinder>)binders).UnbindSafely(new UnbindContext(this), memberName: "_binders");
                    break;
            }

            Assert.That(survivor.IsBound, Is.False);
        }

        [TestCase(false, 0)]
        [TestCase(true, 0)]
        [TestCase(false, 1)]
        [TestCase(true, 1)]
        [TestCase(false, 2)]
        [TestCase(true, 2)]
        [TestCase(false, 3)]
        [TestCase(true, 3)]
        public void FailureLogPreservesTheOriginalExceptionAndUsesOnlyLiveUnityOwners(bool unbind, int ownerKind)
        {
            object owner = this;
            if (ownerKind is 1 or 2)
                owner = Spawn("DiagnosticOwner");
            else if (ownerKind is 3)
                owner = Track(ScriptableObject.CreateInstance<DiagnosticOwner>());
            if (ownerKind is 2)
                Destroy((Object)owner);

            var originalHandler = Debug.unityLogger.logHandler;
            var handler = new CapturingLogHandler(originalHandler);
            Debug.unityLogger.logHandler = handler;
            RestoreOnTearDown(() => Debug.unityLogger.logHandler = originalHandler);
            var binder = new SpyBinder { ThrowOnBind = true, ThrowOnUnbind = true };
            ExpectFailureLog(unbind ? "UnbindSafely" : "BindSafely");

            if (unbind)
                binder.UnbindSafely(new UnbindContext(owner), "_single");
            else
                binder.BindSafely(new BindContext(new OneWayBindableMember<string>(null), owner), "_single");

            Assert.That(handler.Exception, Is.TypeOf<InvalidOperationException>());
            Assert.That(handler.Exception.StackTrace,
                Does.Contain(unbind ? "SpyBinder.Unbind" : "SpyBinder.Bind"));
            Assert.That(handler.Message, Does.Contain(handler.Exception.ToString()));
            Assert.That(handler.Message, Does.Contain("'_single'"));
            if (ownerKind is 1 or 3)
                Assert.That(handler.Context, Is.SameAs(owner));
            else
                Assert.That(handler.Context, Is.Null);
            if (ownerKind is 2)
                Assert.That(handler.Message, Does.Contain("destroyed Unity object"));
            if (ownerKind is 3)
                Assert.That(handler.Message, Does.Contain("Object '").And.Not.Contain("GameObject '"));
        }

        [Test]
        public void NullInputsAndUnsuccessfulLookupsProduceNoLogs()
        {
            IBinder missing = null;
            IBinder[] missingArray = null;
            var member = new OneWayBindableMember<string>(null);
            missing.BindSafely(new BindContext(member, this));
            missing.UnbindSafely(new UnbindContext(this));
            missingArray.BindSafely(new BindContext(member, this));
            missingArray.UnbindSafely(new UnbindContext(this));

            var binder = new SpyBinder { ThrowOnBind = true };
            var result = new FindBindableMemberResult();
            binder.BindSafely(result, this);
            new[] { binder }.BindSafely(result, this);
            new List<SpyBinder> { binder }.BindSafely(result, this);
            ((IEnumerable<SpyBinder>)new[] { binder }).BindSafely(result, this);
            Assert.That(binder.IsBound, Is.False);
            LogAssert.NoUnexpectedReceived();
        }

        [TestCase(false)]
        [TestCase(true)]
        public void EnumerationFailuresPropagateAfterProcessingEarlierEntries(bool unbind)
        {
            var binder = new SpyBinder { IsBound = unbind };
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                if (unbind)
                    Enumerate().UnbindSafely(new UnbindContext(this));
                else
                    Enumerate().BindSafely(new BindContext(new OneWayBindableMember<string>(null), this));
            });
            Assert.That(exception.Message, Is.EqualTo("enumeration"));
            Assert.That(binder.IsBound, Is.EqualTo(!unbind));
            LogAssert.NoUnexpectedReceived();

            IEnumerable<IBinder> Enumerate()
            {
                yield return binder;
                throw new InvalidOperationException("enumeration");
            }
        }

        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void BindSafely_PreservesEnumerationAndDisposalFailures(bool lookup, bool current)
        {
            var first = new SpyBinder();
            var second = new SpyBinder();
            var enumeration = new Exception("enumeration");
            var disposal = new Exception("disposal");
            var source = new FailingBinderEnumerable(first, second)
            {
                MoveNextFailure = current ? null : enumeration,
                CurrentFailure = current ? enumeration : null,
                CurrentFailureIndex = 1,
                DisposalFailure = disposal
            };

            var aggregate = Assert.Throws<AggregateException>(() => BindSource(source, lookup));

            Assert.That(aggregate.InnerExceptions, Is.EqualTo(new[] { enumeration, disposal }));
            Assert.That(first.IsBound, Is.True);
            Assert.That(second.IsBound, Is.EqualTo(!current));
            Assert.That(source.DisposeCalls, Is.EqualTo(1));
            LogAssert.NoUnexpectedReceived();
        }

        [TestCase(false, 0)]
        [TestCase(false, 1)]
        [TestCase(false, 2)]
        [TestCase(false, 3)]
        [TestCase(true, 0)]
        [TestCase(true, 1)]
        [TestCase(true, 2)]
        [TestCase(true, 3)]
        public void BindSafely_PreservesASingleEnumeratorFailure(bool lookup, int stage)
        {
            var failure = new Exception("single");
            var source = new FailingBinderEnumerable(new SpyBinder())
            {
                AcquisitionFailure = stage is 0 ? failure : null,
                MoveNextFailure = stage is 1 ? failure : null,
                CurrentFailure = stage is 2 ? failure : null,
                DisposalFailure = stage is 3 ? failure : null
            };

            Assert.That(Assert.Catch(() => BindSource(source, lookup)), Is.SameAs(failure));
            Assert.That(failure.StackTrace, Does.Contain(nameof(ThrowEnumerationFailure)));
            Assert.That(source.DisposeCalls, Is.EqualTo(stage is 0 ? 0 : 1));
            LogAssert.NoUnexpectedReceived();
        }

        [TestCase(false)]
        [TestCase(true)]
        public void BindSafely_LogsBinderFailuresSeparatelyFromEnumeratorFailures(bool lookup)
        {
            var survivor = new SpyBinder();
            var enumeration = new Exception("enumeration");
            var disposal = new Exception("disposal");
            var source = new FailingBinderEnumerable(null, new SpyBinder { ThrowOnBind = true }, survivor)
            {
                MoveNextFailure = enumeration,
                DisposalFailure = disposal
            };
            ExpectMissingLog("BindSafely");
            LogAssert.Expect(LogType.Exception,
                new Regex(@"\[BindSafely\] Binder at index 1 '_binders'.*InvalidOperationException"));

            var aggregate = Assert.Throws<AggregateException>(() => BindSource(source, lookup));

            Assert.That(aggregate.InnerExceptions, Is.EqualTo(new[] { enumeration, disposal }));
            Assert.That(survivor.IsBound, Is.True);
            Assert.That(source.DisposeCalls, Is.EqualTo(1));
            LogAssert.NoUnexpectedReceived();
        }

        [TestCase(false)]
        [TestCase(true)]
        public void UnbindSafely_PreservesEnumerationAndDisposalFailures(bool current)
        {
            var first = new SpyBinder { IsBound = true };
            var second = new SpyBinder { IsBound = true };
            var enumeration = new Exception("enumeration");
            var disposal = new Exception("disposal");
            var source = new FailingBinderEnumerable(first, second)
            {
                MoveNextFailure = current ? null : enumeration,
                CurrentFailure = current ? enumeration : null,
                CurrentFailureIndex = 1,
                DisposalFailure = disposal
            };

            var aggregate = Assert.Throws<AggregateException>(() => source.UnbindSafely(new UnbindContext(this)));

            Assert.That(aggregate.InnerExceptions, Is.EqualTo(new[] { enumeration, disposal }));
            Assert.That(first.IsBound, Is.False);
            Assert.That(second.IsBound, Is.EqualTo(current));
            Assert.That(source.DisposeCalls, Is.EqualTo(1));
            LogAssert.NoUnexpectedReceived();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void UnbindSafely_PreservesASingleEnumeratorFailure(int stage)
        {
            var failure = new Exception("single");
            var source = new FailingBinderEnumerable(new SpyBinder { IsBound = true })
            {
                AcquisitionFailure = stage is 0 ? failure : null,
                MoveNextFailure = stage is 1 ? failure : null,
                CurrentFailure = stage is 2 ? failure : null,
                DisposalFailure = stage is 3 ? failure : null
            };

            Assert.That(Assert.Catch(() => source.UnbindSafely(new UnbindContext(this))), Is.SameAs(failure));
            Assert.That(failure.StackTrace, Does.Contain(nameof(ThrowEnumerationFailure)));
            Assert.That(source.DisposeCalls, Is.EqualTo(stage is 0 ? 0 : 1));
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void UnbindSafely_LogsBinderFailuresSeparatelyFromEnumeratorFailures()
        {
            var survivor = new SpyBinder { IsBound = true };
            var enumeration = new Exception("enumeration");
            var disposal = new Exception("disposal");
            var source = new FailingBinderEnumerable(null, new SpyBinder { ThrowOnUnbind = true }, survivor)
            {
                MoveNextFailure = enumeration,
                DisposalFailure = disposal
            };
            ExpectMissingLog("UnbindSafely");
            LogAssert.Expect(LogType.Exception,
                new Regex(@"\[UnbindSafely\] Binder at index 1 '_binders'.*InvalidOperationException"));

            var aggregate = Assert.Throws<AggregateException>(() =>
                source.UnbindSafely(new UnbindContext(this), "_binders"));

            Assert.That(aggregate.InnerExceptions, Is.EqualTo(new[] { enumeration, disposal }));
            Assert.That(survivor.IsBound, Is.False);
            Assert.That(source.DisposeCalls, Is.EqualTo(1));
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void AnUnsuccessfulLookupDoesNotAcquireTheEnumerator()
        {
            var source = new FailingBinderEnumerable(new SpyBinder())
            {
                AcquisitionFailure = new Exception("unexpected enumeration")
            };

            Assert.DoesNotThrow(() => source.BindSafely(default(FindBindableMemberResult), this));

            Assert.That(source.GetEnumeratorCalls, Is.Zero);
            Assert.That(source.DisposeCalls, Is.Zero);
            LogAssert.NoUnexpectedReceived();
        }

        private void BindSource(IEnumerable<IBinder> source, bool lookup)
        {
            var member = new OneTimeStructBindableMember<int>(1);
            if (lookup)
                source.BindSafely(new FindBindableMemberResult(member), this, "_binders");
            else
                source.BindSafely(new BindContext(member, this), "_binders");
        }

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

        private static void ExpectFailureLog(string operation) =>
            LogAssert.Expect(LogType.Exception, new Regex($@"\[{operation}\].*InvalidOperationException"));

        private static void ExpectMissingLog(string operation)
        {
#if DEBUG
            LogAssert.Expect(LogType.Error, new Regex($@"\[{operation}\].*index 0.*_binders.*null or destroyed"));
#endif
        }

        private sealed class FailingBinderEnumerable : IEnumerable<IBinder>, IEnumerator<IBinder>
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

        private sealed class ContextProbeBinder : Binder, IBinder<int>
        {
            public int Value;

            public object CurrentOwner =>
                Owner;

            public void SetValue(int value) =>
                Value = value;
        }

        private sealed class DiagnosticOwner : ScriptableObject { }

        private sealed class CapturingLogHandler : ILogHandler
        {
            private readonly ILogHandler _original;
            public Exception Exception;
            public Object Context;
            public string Message;

            public CapturingLogHandler(ILogHandler original)
            {
                _original = original;
            }

            public void LogException(Exception exception, Object context)
            {
                Exception = exception;
                Context = context;
                _original.LogException(exception, context);
            }

            public void LogFormat(LogType logType, Object context, string format, params object[] args)
            {
                if (logType is LogType.Exception)
                {
                    Exception = (Exception)args[1];
                    Context = context;
                    Message = string.Format(format, args);
                }

                _original.LogFormat(logType, context, format, args);
            }
        }
    }
}
