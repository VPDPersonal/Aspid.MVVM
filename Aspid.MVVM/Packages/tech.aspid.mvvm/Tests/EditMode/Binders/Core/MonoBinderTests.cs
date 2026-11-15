using UnityEditor;
using UnityEngine;
using NUnit.Framework;

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

            binder.Bind(new BindContext(new OneWayBindableMember<bool>(false), binder));

            Assert.IsTrue(binder.IsBound, "Bind did not mark the binder as bound");
        }

        [Test]
        public void Bind_WhenAlreadyBound_ThrowsAndPreservesTheOriginalBinding()
        {
            var binder = NewBinder();
            var original = new OneWayBindableMember<bool>(false);
            binder.Bind(new BindContext(original, binder));
            var callsAfterFirstBind = binder.Calls.Count;

            Assert.Throws<System.InvalidOperationException>(() => binder.Bind(new BindContext(new OneWayBindableMember<bool>(true), binder)));

            Assert.AreEqual(callsAfterFirstBind, binder.Calls.Count, "A second bind ran the binding hooks again");
            Assert.That(binder.IsBound, Is.True);
        }

        [Test]
        public void Bind_WhenCanBindIsFalse_DoesNothing()
        {
            var binder = NewBinder();
            binder.ForcedCanBind = false;

            binder.Bind(new BindContext(new OneWayBindableMember<bool>(false), binder));

            Assert.IsFalse(binder.IsBound, "A binder that refused to bind still marked itself as bound");
            Assert.IsEmpty(binder.Calls, "A binder that refused to bind still ran its hooks");
        }

        [Test]
        public void Bind_RunsOnBindingThenOnBound()
        {
            var binder = NewBinder();

            binder.Bind(new BindContext(new OneWayBindableMember<bool>(false), binder));

            CollectionAssert.AreEqual(
                new[] { "OnBinding", "OnAttach", "OnBound" }, binder.Calls, "Hooks did not run in the OnBinding -> OnBound order");
        }

        [Test]
        public void Unbind_RunsOnUnbindingThenOnUnbound()
        {
            var binder = NewBinder();
            binder.Bind(new BindContext(new OneWayBindableMember<bool>(false), binder));
            binder.Calls.Clear();

            binder.Unbind(new UnbindContext(binder));

            CollectionAssert.AreEqual(
                new[] { "OnUnbinding", "OnDetach", "OnUnbound" }, binder.Calls, "Hooks did not run in the OnUnbinding -> OnUnbound order");
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
