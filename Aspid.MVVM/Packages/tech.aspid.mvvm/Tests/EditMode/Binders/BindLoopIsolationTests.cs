using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests that one failing binder does not take the rest of the collection with it.
    /// </summary>
    /// <remarks>
    /// <c>BindSafely</c> and <c>UnbindSafely</c> were "safe" only with respect to <see langword="null"/> elements:
    /// the loop body called into the binder unguarded, so the first exception abandoned every binder after it. On
    /// bind that leaves the View half-initialised; on unbind — which <c>MonoView.OnDestroy</c> drives — it leaves the
    /// remaining binders subscribed to a ViewModel that is going away, which is a leak rather than a glitch.
    /// </remarks>
    [TestFixture]
    public sealed class BindLoopIsolationTests
    {
        [Test]
        public void BindSafely_WhenOneBinderThrows_StillBindsTheRest()
        {
            var binders = new IBinder[]
            {
                new SpyBinder(),
                new SpyBinder { ThrowOnBind = true },
                new SpyBinder(),
            };

            ExpectFailureLog("BindSafely");

            binders.BindSafely(new OneWayBindableMember<string>(null), owner: null, memberName: "_binders");

            Assert.IsTrue(((SpyBinder)binders[0]).IsBound, "Первый биндер не привязался");
            Assert.IsTrue(((SpyBinder)binders[2]).IsBound, "Биндер после упавшего не привязался");
        }

        [Test]
        public void UnbindSafely_WhenOneBinderThrows_StillUnbindsTheRest()
        {
            var binders = new IBinder[]
            {
                new SpyBinder { IsBound = true },
                new SpyBinder { IsBound = true, ThrowOnUnbind = true },
                new SpyBinder { IsBound = true },
            };

            ExpectFailureLog("UnbindSafely");

            binders.UnbindSafely(owner: null, memberName: "_binders");

            Assert.IsFalse(((SpyBinder)binders[0]).IsBound, "Первый биндер не отвязался");
            Assert.IsFalse(((SpyBinder)binders[2]).IsBound, "Биндер после упавшего остался подписан");
        }

        [Test]
        public void BindSafely_WhenABinderThrows_ReportsTheIndexAndMember()
        {
            var binders = new IBinder[] { new SpyBinder(), new SpyBinder { ThrowOnBind = true } };

            LogAssert.Expect(LogType.Error, new Regex(@"index 1.*_binders.*InvalidOperationException"));
            LogAssert.Expect(LogType.Exception, new Regex("InvalidOperationException"));

            binders.BindSafely(new OneWayBindableMember<string>(null), owner: null, memberName: "_binders");
        }

        private static void ExpectFailureLog(string operation)
        {
            LogAssert.Expect(LogType.Error, new Regex(operation));
            LogAssert.Expect(LogType.Exception, new Regex("InvalidOperationException"));
        }
    }

    /// <summary>
    /// A binder that records whether it was bound and can be told to throw from either half of the lifecycle.
    /// </summary>
    internal sealed class SpyBinder : IBinder
    {
        public bool IsBound { get; set; }

        public bool ThrowOnBind { get; set; }

        public bool ThrowOnUnbind { get; set; }

        public BindMode Mode => BindMode.OneWay;

        public void Bind(IBinderAdder binderAdder)
        {
            if (ThrowOnBind) throw new InvalidOperationException("сломанный биндер");
            IsBound = true;
        }

        public void Unbind()
        {
            if (ThrowOnUnbind) throw new InvalidOperationException("сломанный биндер");
            IsBound = false;
        }
    }
}
