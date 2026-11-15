using System;
using NUnit.Framework;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class BinderOwnershipTests : SceneFixture
    {
        [TestCase(false)]
        [TestCase(true)]
        public void CleanupReportsHooksAndRemovalFailuresWithoutKeepingTheSubscription(bool mono)
        {
            var binder = Create(mono);
            var before = new Exception("before");
            var removal = new Exception("remove");
            var detached = new Exception("detach");
            var notification = new Exception("notification");
            var member = new ThrowingRemovalMember(removal);
            binder.Bind(new BindContext(member, this));
            binder.State.Cleaning = () => throw before;
            binder.State.Cleaned = () => throw detached;
            binder.State.Unbound = () => throw notification;

            var aggregate = Assert.Throws<AggregateException>(() => binder.Unbind(new UnbindContext(this)));

            Assert.That(aggregate.InnerExceptions, Is.EqualTo(new[] { before, removal, detached, notification }));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.CurrentOwner, Is.Null);
            Assert.That(binder.State.Cleanups, Is.EqualTo(1));
            member.Member.Value = 2;
            Assert.That(binder.State.Value, Is.EqualTo(1));
            Assert.DoesNotThrow(() => binder.Unbind(new UnbindContext(this)));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void SingleCleanupFailureRetainsItsInstanceAndThrowSite(bool mono)
        {
            var binder = Create(mono);
            var failure = new Exception("single");
            binder.Bind(new BindContext(new OneTimeStructBindableMember<int>(1), this));
            binder.State.Cleaned = () => ThrowFromCleanup(failure);

            Assert.That(Assert.Catch(() => binder.Unbind(new UnbindContext(this))), Is.SameAs(failure));
            Assert.That(failure.StackTrace, Does.Contain(nameof(ThrowFromCleanup)));
            Assert.That(binder.CurrentOwner, Is.Null);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void InstallationFailureSurvivesMultipleRollbackFailures(bool mono)
        {
            var binder = Create(mono);
            var installation = new Exception("installation");
            binder.State.Attaching = () => throw installation;
            binder.State.Cleaning = () => throw new Exception("before cleanup");
            binder.State.Cleaned = () => throw new Exception("detach");
            binder.State.Unbound = () => throw new Exception("notification");

            Assert.That(Assert.Catch(() => binder.Bind(
                new BindContext(new OneWayStructBindableMember<int>(1), this))), Is.SameAs(installation));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.CurrentOwner, Is.Null);
            Assert.That(binder.State.Cleanups, Is.EqualTo(1));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void MultipleCleanupFailuresPreserveABindingCreatedByTheUnboundNotification(bool mono)
        {
            var binder = Create(mono);
            var next = new OneWayStructBindableMember<int>(2);
            binder.Bind(new BindContext(new OneWayStructBindableMember<int>(1), this));
            var cleanup = new Exception("cleanup");
            var notification = new Exception("notification");
            binder.State.Cleaning = () => throw cleanup;
            binder.State.Unbound = () =>
            {
                binder.State.Cleaning = null;
                binder.State.Unbound = null;
                binder.Bind(new BindContext(next, binder));
                throw notification;
            };

            var aggregate = Assert.Throws<AggregateException>(() => binder.Unbind(new UnbindContext(this)));

            Assert.That(aggregate.InnerExceptions, Is.EqualTo(new[] { cleanup, notification }));
            Assert.That(binder.CurrentOwner, Is.SameAs(binder));
            Assert.That(binder.IsBound, Is.True);
            next.Value = 3;
            Assert.That(binder.State.Value, Is.EqualTo(3));
            binder.Unbind(new UnbindContext(binder));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void IsBoundReflectsAttachmentDuringEveryCallback(bool mono)
        {
            var binder = Create(mono);
            binder.State.Check = () => Assert.That(binder.IsBound, Is.False);
            binder.State.Binding = () => Assert.That(binder.IsBound, Is.False);
            binder.State.Receiving = () => Assert.That(binder.IsBound, Is.False);
            binder.State.Bound = () => Assert.That(binder.IsBound, Is.True);
            binder.State.Attaching = () => Assert.That(binder.IsBound, Is.False);
            binder.State.Cleaning = () => Assert.That(binder.IsBound, Is.False);
            binder.State.Cleaned = () => Assert.That(binder.IsBound, Is.False);
            binder.State.Unbound = () => Assert.That(binder.CurrentOwner, Is.Null);
            binder.Bind(new BindContext(new OneWayStructBindableMember<int>(1), this));
            binder.Unbind(new UnbindContext(this));
            Assert.That(binder.IsBound, Is.False);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void SelfReleaseAllowsRebindingBeforeTheOuterBindReturns(bool mono)
        {
            var binder = Create(mono);
            var original = new OneWayStructBindableMember<int>(1);
            var next = new OneWayStructBindableMember<int>(2);
            binder.State.Bound = () =>
            {
                binder.State.Bound = null;
                binder.Unbind(new UnbindContext(this));
                binder.Bind(new BindContext(next, binder));
            };
            binder.Bind(new BindContext(original, this));
            original.Value = 3;
            Assert.That(binder.State.Value, Is.EqualTo(2));
            next.Value = 4;
            Assert.That(binder.IsBound, Is.True);
            Assert.That(binder.State.Value, Is.EqualTo(4));
            Assert.That(binder.CurrentOwner, Is.SameAs(binder));
            binder.Unbind(new UnbindContext(binder));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void CompletionFailurePreservesTheCompletedBinding(bool mono)
        {
            var binder = Create(mono);
            var member = new OneWayStructBindableMember<int>(1);
            var failure = new InvalidOperationException("notification");
            binder.State.Bound = () => throw failure;
            Assert.That(Assert.Catch(() => binder.Bind(new BindContext(member, this))), Is.SameAs(failure));
            member.Value = 2;
            Assert.That(binder.IsBound, Is.True);
            Assert.That(binder.CurrentOwner, Is.SameAs(this));
            Assert.That(binder.State.Value, Is.EqualTo(2));
            Assert.That(binder.State.Cleanups, Is.Zero);
            binder.Unbind(new UnbindContext(this));
        }

        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void CompletionFailureDoesNotRollBackAReplacementBinding(bool mono, bool sameOwner)
        {
            var binder = Create(mono);
            var next = new OneWayStructBindableMember<int>(2);
            var owner = sameOwner ? this : (object)binder;
            var failure = new InvalidOperationException("notification");
            binder.State.Bound = () =>
            {
                binder.State.Bound = null;
                binder.Unbind(new UnbindContext(this));
                binder.Bind(new BindContext(next, owner));
                throw failure;
            };
            Assert.That(Assert.Catch(() => binder.Bind(
                new BindContext(new OneWayStructBindableMember<int>(1), this))), Is.SameAs(failure));
            next.Value = 3;
            Assert.That(binder.State.Value, Is.EqualTo(3));
            Assert.That(binder.CurrentOwner, Is.SameAs(owner));
            Assert.That(binder.IsBound, Is.True);
            binder.Unbind(new UnbindContext(owner));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void AttachFailureReleasesResourcesBeforeNotifying(bool mono)
        {
            var binder = Create(mono);
            var member = new OneWayStructBindableMember<int>(1);
            var failure = new InvalidOperationException("attach");
            binder.State.Attaching = () =>
            {
                binder.Unbind(new UnbindContext(binder));
                Assert.Throws<InvalidOperationException>(() => binder.Unbind(new UnbindContext(this)));
                throw failure;
            };
            binder.State.Bound = () => Assert.Fail("An incomplete binding was reported as ready.");
            binder.State.Unbound = () =>
            {
                Assert.That(binder.CurrentOwner, Is.Null);
                Assert.That(binder.State.Cleanups, Is.EqualTo(1));
            };
            Assert.That(Assert.Catch(() => binder.Bind(new BindContext(member, this))), Is.SameAs(failure));
            member.Value = 2;
            Assert.That(binder.State.Value, Is.EqualTo(1));
            Assert.That(binder.IsBound, Is.False);
        }

        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void UnboundNotificationCanRebindEvenAfterCleanupFails(bool mono, bool failCleanup)
        {
            var binder = Create(mono);
            var next = new OneWayStructBindableMember<int>(2);
            var failure = new InvalidOperationException("cleanup");
            binder.Bind(new BindContext(new OneWayStructBindableMember<int>(1), this));
            binder.State.Cleaned = () => { if (failCleanup) throw failure; };
            binder.State.Unbound = () =>
            {
                Assert.That(binder.CurrentOwner, Is.Null);
                binder.State.Unbound = null;
                binder.State.Cleaned = null;
                binder.Bind(new BindContext(next, binder));
            };
            if (failCleanup)
                Assert.That(Assert.Catch(() => binder.Unbind(new UnbindContext(this))), Is.SameAs(failure));
            else
                binder.Unbind(new UnbindContext(this));
            next.Value = 3;
            Assert.That(binder.IsBound, Is.True);
            Assert.That(binder.State.Value, Is.EqualTo(3));
            Assert.That(binder.CurrentOwner, Is.SameAs(binder));
            binder.Unbind(new UnbindContext(binder));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void RollbackDoesNotClearABindingCreatedByItsCompletionNotification(bool mono)
        {
            var binder = Create(mono);
            var failure = new InvalidOperationException("attach");
            var next = new OneWayStructBindableMember<int>(2);
            binder.State.Attaching = () => throw failure;
            binder.State.Unbound = () =>
            {
                binder.State.Attaching = null;
                binder.State.Unbound = null;
                binder.Bind(new BindContext(next, binder));
            };
            Assert.That(Assert.Catch(() => binder.Bind(
                new BindContext(new OneWayStructBindableMember<int>(1), this))), Is.SameAs(failure));
            next.Value = 3;
            Assert.That(binder.IsBound, Is.True);
            Assert.That(binder.State.Value, Is.EqualTo(3));
            Assert.That(binder.CurrentOwner, Is.SameAs(binder));
            binder.Unbind(new UnbindContext(binder));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void OnlyTheSameOwnerInstanceCanReleaseTheBinding(bool mono)
        {
            var binder = Create(mono);
            var owner = new EqualOwner();
            var other = new EqualOwner();
            var member = new OneWayStructBindableMember<int>(1);
            binder.Bind(new BindContext(member, owner));
            binder.Unbind(new UnbindContext(other));
            member.Value = 2;
            Assert.That(binder.IsBound, Is.True);
            Assert.That(binder.State.Value, Is.EqualTo(2));
            Assert.That(binder.State.Cleanups, Is.Zero);

            binder.Unbind(new UnbindContext(owner));
            member.Value = 3;
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.State.Value, Is.EqualTo(2));
            Assert.That(binder.State.Cleanups, Is.EqualTo(1));
            Assert.That(binder.CurrentOwner, Is.Null);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void RepeatedBindPreservesTheCurrentOwnerAndMember(bool mono)
        {
            var binder = Create(mono);
            var owner = new object();
            var member = new OneWayStructBindableMember<int>(1);
            var other = new OneWayStructBindableMember<int>(9);
            binder.Bind(new BindContext(member, owner));
            Assert.Throws<InvalidOperationException>(() => binder.Bind(new BindContext(other, owner)));
            Assert.Throws<InvalidOperationException>(() => binder.Bind(new BindContext(other, this)));
            member.Value = 2;
            Assert.That(binder.State.Value, Is.EqualTo(2));
            Assert.That(binder.CurrentOwner, Is.SameAs(owner));
            binder.Unbind(new UnbindContext(owner));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void CanBindReservesTheOwnerAndRejectsReentrantBinding(bool mono)
        {
            var binder = Create(mono);
            var member = new OneTimeStructBindableMember<int>(1);
            binder.State.Check = () => {
                Assert.That(binder.CurrentOwner, Is.SameAs(this));
                Assert.Throws<InvalidOperationException>(() => binder.Bind(new BindContext(member, binder)));
            };
            binder.State.Allow = false;
            binder.Bind(new BindContext(member, this));
            Assert.That(binder.CurrentOwner, Is.Null);
            Assert.That(binder.State.Cleanups, Is.Zero);
            binder.State.Check = null;
            binder.State.Allow = true;
            binder.Bind(new BindContext(member, binder));
            binder.Unbind(new UnbindContext(binder));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void InitialValueCannotReleaseAnIncompleteAttachment(bool mono)
        {
            var binder = Create(mono);
            binder.State.Receiving = () => binder.Unbind(new UnbindContext(this));
            Assert.Throws<InvalidOperationException>(() => binder.Bind(new BindContext(new OneWayStructBindableMember<int>(1), this)));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.CurrentOwner, Is.Null);
            Assert.That(binder.State.Cleanups, Is.EqualTo(1));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void CleanupKeepsTheOwnerAndRejectsRebindingUntilAllHooksFinish(bool mono)
        {
            var binder = Create(mono);
            var member = new OneWayStructBindableMember<int>(1);
            binder.State.Cleaning = binder.State.Cleaned = () => {
                Assert.That(binder.CurrentOwner, Is.SameAs(this));
                binder.Unbind(new UnbindContext(this));
                Assert.Throws<InvalidOperationException>(() => binder.Bind(new BindContext(member, binder)));
            };
            binder.Bind(new BindContext(member, this));
            binder.Unbind(new UnbindContext(this));
            Assert.That(binder.State.Cleanups, Is.EqualTo(1));
            Assert.That(binder.CurrentOwner, Is.Null);
            binder.State.Cleaning = binder.State.Cleaned = null;
            binder.Bind(new BindContext(member, binder));
            binder.Unbind(new UnbindContext(binder));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void SelfReleaseFollowedByBindingFailureDoesNotCleanUpTwice(bool mono)
        {
            var binder = Create(mono);
            var failure = new InvalidOperationException("bound");
            binder.State.Bound = () => {
                binder.Unbind(new UnbindContext(this));
                throw failure;
            };
            Assert.That(Assert.Catch(() => binder.Bind(
                new BindContext(new OneWayStructBindableMember<int>(1), this))), Is.SameAs(failure));
            Assert.That(binder.State.Cleanups, Is.EqualTo(1));
            Assert.That(binder.CurrentOwner, Is.Null);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void CleanupFailureClearsOwnershipAndAllowsAnotherOwner(bool mono)
        {
            var binder = Create(mono);
            var member = new OneWayStructBindableMember<int>(1);
            var failure = new InvalidOperationException("cleanup");
            binder.State.Cleaning = () => throw failure;
            binder.Bind(new BindContext(member, this));
            Assert.That(Assert.Catch(() => binder.Unbind(new UnbindContext(this))), Is.SameAs(failure));
            Assert.That(binder.CurrentOwner, Is.Null);
            binder.State.Cleaning = null;
            binder.Bind(new BindContext(member, binder));
            binder.Unbind(new UnbindContext(binder));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void UninitializedContextsAreRejected(bool mono)
        {
            var binder = Create(mono);
            Assert.Throws<ArgumentException>(() => binder.Bind(default));
            Assert.Throws<ArgumentException>(() => binder.Unbind(default));
            Assert.That(binder.CurrentOwner, Is.Null);
        }

        [Test]
        public void ContextConstructorsRejectMissingMembersAndOwners()
        {
            var member = new OneTimeStructBindableMember<int>(1);
            Assert.Throws<ArgumentNullException>(() => new BindContext(null, this));
            Assert.Throws<ArgumentNullException>(() => new BindContext(member, null));
            Assert.Throws<ArgumentNullException>(() => new UnbindContext(null));
        }

#if (UNITY_EDITOR || DEBUG) && !ASPID_MVVM_EDITOR_DISABLED
        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void DebugRebindRestoresTheRequestedModeAfterCleanupFailure(bool mono, bool rebindInCallback)
        {
            var binder = Create(mono);
            var member = new OneWayStructBindableMember<int>(1);
            binder.Bind(new BindContext(member, this));
            var modeField = (mono ? typeof(MonoBinder) : typeof(Binder)).GetField(
                "_mode", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            modeField.SetValue(binder, BindMode.OneTime);
            var failure = new InvalidOperationException("cleanup");
            binder.State.Cleaned = () => throw failure;
            if (rebindInCallback)
            {
                binder.State.Unbound = () =>
                {
                    binder.State.Unbound = null;
                    binder.State.Cleaned = null;
                    binder.Bind(new BindContext(member, binder));
                };
            }

            var rebindable = binder.GetType().GetInterface("Aspid.MVVM.IRebindableBinder");
            var exception = Assert.Throws<System.Reflection.TargetInvocationException>(
                () => rebindable.GetMethod("Rebind").Invoke(binder, null));

            Assert.That(exception.InnerException, Is.SameAs(failure));
            Assert.That(binder.IsBound, Is.EqualTo(rebindInCallback));
            Assert.That(binder.Mode, Is.EqualTo(rebindInCallback ? BindMode.OneWay : BindMode.OneTime));
            binder.State.Cleaned = null;
            if (rebindInCallback)
                binder.Unbind(new UnbindContext(binder));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void DebugRebindPreservesTheOriginalOwner(bool mono)
        {
            var binder = Create(mono);
            binder.Bind(new BindContext(new OneWayStructBindableMember<int>(1), this));
            var rebindable = binder.GetType().GetInterface("Aspid.MVVM.IRebindableBinder");
            Assert.That(rebindable, Is.Not.Null);
            rebindable.GetMethod("Rebind").Invoke(binder, null);
            Assert.That(binder.CurrentOwner, Is.SameAs(this));
            binder.Unbind(new UnbindContext(binder));
            Assert.That(binder.IsBound, Is.True);
            binder.Unbind(new UnbindContext(this));
        }
#endif

        [TestCase(false)]
        [TestCase(true)]
        public void NotificationFailureDoesNotClearABindingCreatedDuringUnbind(bool mono)
        {
            var binder = Create(mono);
            var next = new OneWayStructBindableMember<int>(2);
            var failure = new InvalidOperationException("notification");
            binder.Bind(new BindContext(new OneWayStructBindableMember<int>(1), this));
            binder.State.Unbound = () =>
            {
                binder.State.Unbound = null;
                binder.Bind(new BindContext(next, binder));
                throw failure;
            };
            Assert.That(Assert.Catch(() => binder.Unbind(new UnbindContext(this))), Is.SameAs(failure));
            next.Value = 3;
            Assert.That(binder.IsBound, Is.True);
            Assert.That(binder.State.Value, Is.EqualTo(3));
            Assert.That(binder.CurrentOwner, Is.SameAs(binder));
            binder.Unbind(new UnbindContext(binder));
        }

#if (UNITY_EDITOR || DEBUG) && !ASPID_MVVM_EDITOR_DISABLED
        [TestCase(false)]
        [TestCase(true)]
        public void DebugRebindPreservesABindingCreatedByTheUnboundNotification(bool mono)
        {
            var binder = Create(mono);
            var next = new OneWayStructBindableMember<int>(2);
            binder.Bind(new BindContext(new OneWayStructBindableMember<int>(1), this));
            binder.State.Unbound = () =>
            {
                binder.State.Unbound = null;
                binder.Bind(new BindContext(next, binder));
            };
            var rebindable = binder.GetType().GetInterface("Aspid.MVVM.IRebindableBinder");
            Assert.DoesNotThrow(() => rebindable.GetMethod("Rebind").Invoke(binder, null));
            next.Value = 3;
            Assert.That(binder.CurrentOwner, Is.SameAs(binder));
            Assert.That(binder.State.Value, Is.EqualTo(3));
            binder.Unbind(new UnbindContext(binder));
        }
#endif

        [Test]
        public void ModeChangesAreAllowedOnlyAfterCleanupCompletes()
        {
            var binder = Spawn<OwnedMonoBinder>();
            binder.State.Attaching = () => Assert.Throws<InvalidOperationException>(() => binder.Mode = BindMode.OneTime);
            binder.State.Cleaning = () => Assert.Throws<InvalidOperationException>(() => binder.Mode = BindMode.OneTime);
            binder.State.Cleaned = () => Assert.Throws<InvalidOperationException>(() => binder.Mode = BindMode.OneTime);
            binder.State.Unbound = () => binder.Mode = BindMode.OneTime;
            binder.Bind(new BindContext(new OneWayStructBindableMember<int>(1), this));
            binder.Unbind(new UnbindContext(this));
            Assert.That(binder.Mode, Is.EqualTo(BindMode.OneTime));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void DestroyedUnityOwnerStillMatchesTheOriginalReference(bool mono)
        {
            var binder = Create(mono);
            var owner = Spawn("Owner");
            binder.Bind(new BindContext(new OneWayStructBindableMember<int>(1), owner));
            Destroy(owner);
            binder.Unbind(new UnbindContext(owner));
            Assert.That(binder.IsBound, Is.False);
        }

        [Test]
        public void DirectCleanupAlsoReleasesADestroyedComponentReference()
        {
            var binder = Spawn<OwnedMonoBinder>();
            var member = new OneWayStructBindableMember<int>(1);
            binder.Bind(new BindContext(member, this));
            var state = binder.State;
            Destroy(binder);
            BinderExtensions.Unbind(new IBinder[] { binder }, new UnbindContext(this));
            member.Value = 2;
            Assert.That(state.Value, Is.EqualTo(1));
            Assert.That(binder.IsBound, Is.False);
        }

        [Test]
        public void CompositeBinderOwnsItsChildIndependentlyOfTheView()
        {
            var child = new OwnedBinder();
            var parent = new CompositeBinder(child);
            parent.Bind(new BindContext(new OneTimeStructBindableMember<int>(1), this));
            child.Unbind(new UnbindContext(this));
            Assert.That(child.IsBound, Is.True);
            Assert.That(child.CurrentOwner, Is.SameAs(parent));
            parent.Unbind(new UnbindContext(this));
            Assert.That(child.IsBound, Is.False);
        }

        [TestCase("binding")]
        [TestCase("value")]
        [TestCase("attach")]
        public void DestructionDuringAttachmentReleasesAnyAcquiredSubscription(string stage)
        {
            var binder = Spawn<OwnedMonoBinder>();
            var state = binder.State;
            var member = new OneWayStructBindableMember<int>(1);
            if (stage == "attach")
                state.Attaching = () => UnityEngine.Object.DestroyImmediate(binder);
            else if (stage == "value")
                state.Receiving = () => UnityEngine.Object.DestroyImmediate(binder);
            else
                state.Binding = () => UnityEngine.Object.DestroyImmediate(binder);
            binder.Bind(new BindContext(member, this));
            var value = state.Value;
            member.Value = 2;
            Assert.That(state.Value, Is.EqualTo(value));
            Assert.That(state.Cleanups, Is.EqualTo(1));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.CurrentOwner, Is.Null);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private static void ThrowFromCleanup(Exception failure) =>
            throw failure;

        private IProbe Create(bool mono) => mono
            ? Spawn<OwnedMonoBinder>()
            : new OwnedBinder();

        private interface IProbe : IBinder
        {
            public ProbeState State { get; }
            public object CurrentOwner { get; }
        }

        private sealed class ProbeState
        {
            public bool Allow = true;
            public int Value;
            public int Cleanups;
            public Action Check;
            public Action Binding;
            public Action Receiving;
            public Action Attaching;
            public Action Bound;
            public Action Unbound;
            public Action Cleaning;
            public Action Cleaned;

            public bool CanBind()
            {
                Check?.Invoke();
                return Allow;
            }

            public void Receive(int value)
            {
                Value = value;
                Receiving?.Invoke();
            }

            public void Cleanup()
            {
                Cleanups++;
                Cleaned?.Invoke();
            }
        }

        private sealed class OwnedBinder : Binder, IBinder<int>, IProbe
        {
            public ProbeState State { get; } = new();
            public object CurrentOwner =>
                Owner;
            public override bool CanBind =>
                State.CanBind();
            public void SetValue(int value) =>
                State.Receive(value);
            protected override void OnBinding() =>
                State.Binding?.Invoke();
            protected override void OnAttach() =>
                State.Attaching?.Invoke();
            protected override void OnBound() =>
                State.Bound?.Invoke();
            protected override void OnUnbinding() =>
                State.Cleaning?.Invoke();
            protected override void OnDetach() =>
                State.Cleanup();
            protected override void OnUnbound() =>
                State.Unbound?.Invoke();
        }

        private sealed class OwnedMonoBinder : MonoBinder, IBinder<int>, IProbe
        {
            public ProbeState State { get; } = new();
            public object CurrentOwner =>
                Owner;
            public override bool CanBind =>
                State.CanBind();
            public void SetValue(int value) =>
                State.Receive(value);
            protected override void OnBinding() =>
                State.Binding?.Invoke();
            protected override void OnAttach() =>
                State.Attaching?.Invoke();
            protected override void OnBound() =>
                State.Bound?.Invoke();
            protected override void OnUnbinding() =>
                State.Cleaning?.Invoke();
            protected override void OnDetach() =>
                State.Cleanup();
            protected override void OnUnbound() =>
                State.Unbound?.Invoke();
        }

        private sealed class CompositeBinder : Binder, IBinder<int>
        {
            private readonly OwnedBinder _child;

            public CompositeBinder(OwnedBinder child)
            {
                _child = child;
            }

            public void SetValue(int value) { }
            protected override void OnAttach() =>
                _child.Bind(new BindContext(new OneTimeStructBindableMember<int>(2), this));
            protected override void OnDetach() =>
                _child.Unbind(new UnbindContext(this));
        }

        private sealed class ThrowingRemovalMember : IBinderAdder, IBinderRemover
        {
            private readonly Exception _failure;
            private IBinderRemover _remover;

            public readonly OneWayStructBindableMember<int> Member = new(1);

            public ThrowingRemovalMember(Exception failure)
            {
                _failure = failure;
            }

            public BindMode Mode =>
                BindMode.OneWay;

            public IBinderRemover Add(IBinder binder)
            {
                _remover = ((IBinderAdder)Member).Add(binder);
                return this;
            }

            public void Remove(IBinder binder)
            {
                _remover?.Remove(binder);
                throw _failure;
            }
        }

        private sealed class EqualOwner
        {
            public override bool Equals(object obj) =>
                obj is EqualOwner;

            public override int GetHashCode() => 1;
        }
    }
}
