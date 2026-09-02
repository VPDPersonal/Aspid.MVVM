using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests that one failing binder does not take the rest of the collection with it.
    /// </summary>
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

            Assert.IsTrue(((SpyBinder)binders[0]).IsBound, "The first binder did not bind");
            Assert.IsTrue(((SpyBinder)binders[2]).IsBound, "The binder after the failing one did not bind");
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

            Assert.IsFalse(((SpyBinder)binders[0]).IsBound, "The first binder did not unbind");
            Assert.IsFalse(((SpyBinder)binders[2]).IsBound, "The binder after the failing one stayed subscribed");
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
}
