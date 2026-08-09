using TMPro;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests that a destroyed <see cref="MonoBinder"/> stops being a subscriber of its ViewModel.
    /// </summary>
    /// <remarks>
    /// A binder is a component in its own right: pooling, or a <c>Destroy</c> on a child while the View lives on,
    /// removes it while the bindable member still holds its <c>SetValue</c> delegate. The member then keeps a managed
    /// reference to a dead <see cref="MonoBehaviour"/> and keeps calling it — which both leaks and, once the binder's
    /// target component is gone too, throws <c>MissingReferenceException</c> and stops delivery to every binder
    /// subscribed after it.
    /// <para/>
    /// These live in the PlayMode assembly because Unity does not run <c>Awake</c> / <c>OnDestroy</c> for a plain
    /// <see cref="MonoBehaviour"/> outside Play Mode — an EditMode version of these tests passes whether the fix is
    /// present or not, which makes it worse than no test at all.
    /// </remarks>
    [TestFixture]
    public sealed class MonoBinderLifetimeTests
    {
        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.Destroy(gameObject);
            }

            _spawned.Clear();
        }

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
            Assert.AreEqual("before", text.text, "Биндер не получил значение до уничтожения");

            Object.DestroyImmediate(binder);
            member.Value = "after";

            Assert.AreEqual("before", text.text, "Уничтоженный биндер продолжает писать в цель");
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

            Object.DestroyImmediate(doomedObject);

            Assert.DoesNotThrow(() => member.Value = "after", "Мёртвый подписчик бросает исключение при рассылке");
            Assert.AreEqual("after", survivorText.text, "Уничтоженный биндер оборвал доставку остальным");
        }

        /// <summary>
        /// Guards the fix rather than the defect: unbinding from <c>OnDestroy</c> runs while sibling components are
        /// being torn down, and command binders remove UnityEvent listeners from their target in <c>OnUnbound</c>.
        /// </summary>
        [Test]
        public void DestroyingTheWholeGameObject_WithABoundCommandBinder_DoesNotThrow()
        {
            var member = new OneWayBindableMember<IRelayCommand>(null);
            var gameObject = NewGameObject();

            gameObject.AddComponent<Button>();
            var binder = SetMode(gameObject.AddComponent<ButtonCommandMonoBinder>(), BindMode.OneWay);

            binder.Bind(member);
            member.Value = new RelayCommand(() => { });

            Assert.DoesNotThrow(() => Object.DestroyImmediate(gameObject));
        }

        private (GameObject gameObject, TextMonoBinder binder) CreateTextBinder()
        {
            var gameObject = NewGameObject();
            gameObject.AddComponent<TextMeshProUGUI>();

            return (gameObject, SetMode(gameObject.AddComponent<TextMonoBinder>(), BindMode.OneWay));
        }

        /// <summary>
        /// A binder added through <c>AddComponent</c> starts in <see cref="BindMode.TwoWay"/> — the serialized
        /// default contradicts the field's own <c>[BindMode(OneWay, OneTime)]</c>, so a one-way member rejects it.
        /// That is a separate defect; these tests set the mode explicitly to stay about lifetime. The PlayMode
        /// assembly cannot use <c>SerializedObject</c>, so the private field is written through reflection.
        /// </summary>
        private static TBinder SetMode<TBinder>(TBinder binder, BindMode mode)
            where TBinder : MonoBinder
        {
            typeof(MonoBinder)
                .GetField("_mode", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                .SetValue(binder, mode);

            Assert.AreEqual(mode, binder.Mode, "Не удалось выставить режим биндера");
            return binder;
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("BinderLifetime");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
