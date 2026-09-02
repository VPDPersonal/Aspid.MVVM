using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests that a destroyed <see cref="MonoBinder"/> stops being a subscriber of its ViewModel.
    /// </summary>
    /// <remarks>
    /// These live in the PlayMode assembly because Unity does not run <c>Awake</c> / <c>OnDestroy</c> for a plain
    /// <see cref="MonoBehaviour"/> outside Play Mode.
    /// </remarks>
    [TestFixture]
    public sealed class MonoBinderLifetimeTests : SceneFixture
    {
        /// <summary>
        /// The target component outlives the binder here, so nothing throws — the destroyed binder simply keeps
        /// writing to the scene. That silent mutation is the observable symptom.
        /// </summary>
        [Test]
        public void DestroyedBinder_StopsWritingToItsTarget()
        {
            var member = new OneWayBindableMember<string>(null);
            var (_, binder) = CreateTextBinder();
            var text = binder.GetComponent<TMP_Text>();

            binder.Bind(member);
            member.Value = "before";
            Assert.AreEqual("before", text.text, "The binder did not receive the value before being destroyed");

            Destroy(binder);
            member.Value = "after";

            Assert.AreEqual("before", text.text, "A destroyed binder kept writing to its target");
        }

        /// <summary>
        /// Guards the fix rather than the defect: it passes with or without <c>OnDestroy</c>, because writing to a
        /// destroyed <c>TMP_Text</c> happens to not throw. Kept because it pins the property that matters — one
        /// binder's death must not stop the member from reaching the rest.
        /// </summary>
        [Test]
        public void DestroyedBinder_DoesNotBreakDeliveryToTheOthers()
        {
            var member = new OneWayBindableMember<string>(null);

            var (doomedObject, doomed) = CreateTextBinder();
            var (_, survivor) = CreateTextBinder();
            var survivorText = survivor.GetComponent<TMP_Text>();

            doomed.Bind(member);
            survivor.Bind(member);

            Destroy(doomedObject);

            Assert.DoesNotThrow(() => member.Value = "after", "A dead subscriber threw during delivery");
            Assert.AreEqual("after", survivorText.text, "A destroyed binder broke delivery to the others");
        }

        /// <summary>
        /// Guards the fix rather than the defect: unbinding from <c>OnDestroy</c> runs while sibling components are
        /// being torn down, and command binders remove UnityEvent listeners from their target in <c>OnUnbound</c>.
        /// </summary>
        [Test]
        public void DestroyingTheWholeGameObject_WithABoundCommandBinder_DoesNotThrow()
        {
            var member = new OneWayBindableMember<IRelayCommand>(null);
            var gameObject = Spawn("BinderLifetime");

            gameObject.AddComponent<Button>();
            var binder = SetMode(gameObject.AddComponent<ButtonCommandMonoBinder>(), BindMode.OneWay);

            binder.Bind(member);
            member.Value = new RelayCommand(() => { });

            Assert.DoesNotThrow(() => Destroy(gameObject));
        }

        private (GameObject gameObject, TextMonoBinder binder) CreateTextBinder()
        {
            var gameObject = Spawn("BinderLifetime");
            gameObject.AddComponent<TextMeshProUGUI>();

            return (gameObject, SetMode(gameObject.AddComponent<TextMonoBinder>(), BindMode.OneWay));
        }

        /// <summary>
        /// Sets the mode explicitly so these tests stay about lifetime rather than the default mode. The PlayMode
        /// assembly cannot use <c>SerializedObject</c>, so the private field is written through reflection.
        /// </summary>
        private static TBinder SetMode<TBinder>(TBinder binder, BindMode mode)
            where TBinder : MonoBinder
        {
            typeof(MonoBinder)
                .GetField("_mode", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                .SetValue(binder, mode);

            Assert.AreEqual(mode, binder.Mode, "Failed to set the binder's mode");
            return binder;
        }
    }
}
