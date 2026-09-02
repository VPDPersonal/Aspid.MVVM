using UnityEditor;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests the bind/unbind contract every <see cref="MonoBinder"/> shares: <see cref="MonoBinder.IsBound"/>,
    /// the double-bind guard, the <see cref="MonoBinder.CanBind"/> gate, hook ordering, and <c>Reset</c>.
    /// </summary>
    [TestFixture]
    public sealed class MonoBinderTests : SceneFixture
    {
        [Test]
        public void Bind_MarksTheBinderAsBound()
        {
            var binder = NewBinder();

            binder.Bind(new OneWayBindableMember<bool>(false));

            Assert.IsTrue(binder.IsBound, "Bind did not mark the binder as bound");
        }

        [Test]
        public void Bind_WhenAlreadyBound_LogsErrorAndSkipsTheSecondBind()
        {
            var binder = NewBinder();
            binder.Bind(new OneWayBindableMember<bool>(false));
            var callsAfterFirstBind = binder.Calls.Count;

            LogAssert.Expect(LogType.Error, new Regex("already bound"));
            binder.Bind(new OneWayBindableMember<bool>(true));

            Assert.AreEqual(callsAfterFirstBind, binder.Calls.Count, "A second bind ran the binding hooks again");
        }

        [Test]
        public void Bind_WhenCanBindIsFalse_DoesNothing()
        {
            var binder = NewBinder();
            binder.ForcedCanBind = false;

            binder.Bind(new OneWayBindableMember<bool>(false));

            Assert.IsFalse(binder.IsBound, "A binder that refused to bind still marked itself as bound");
            Assert.IsEmpty(binder.Calls, "A binder that refused to bind still ran its hooks");
        }

        [Test]
        public void Bind_RunsOnBindingThenOnBound()
        {
            var binder = NewBinder();

            binder.Bind(new OneWayBindableMember<bool>(false));

            CollectionAssert.AreEqual(
                new[] { "OnBinding", "OnBound" }, binder.Calls, "Hooks did not run in the OnBinding -> OnBound order");
        }

        [Test]
        public void Unbind_RunsOnUnbindingThenOnUnbound()
        {
            var binder = NewBinder();
            binder.Bind(new OneWayBindableMember<bool>(false));
            binder.Calls.Clear();

            binder.Unbind();

            CollectionAssert.AreEqual(
                new[] { "OnUnbinding", "OnUnbound" }, binder.Calls, "Hooks did not run in the OnUnbinding -> OnUnbound order");
        }

        [Test]
        public void Reset_AppliesDefaultMode()
        {
            var binder = Spawn<ProbeMonoBinder>();
            binder.ForcedDefaultMode = BindMode.OneTime;

            binder.InvokeReset();

            Assert.AreEqual(BindMode.OneTime, binder.Mode);
        }

        private ProbeMonoBinder NewBinder()
        {
            var binder = Spawn<ProbeMonoBinder>();
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.OneWay;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return binder;
        }
    }
}
