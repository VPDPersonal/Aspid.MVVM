using System;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests that the View → ViewModel channel survives an exception thrown while the binder is writing a value.
    /// </summary>
    /// <remarks>
    /// Toggle, Slider and InputField binders suppress their own echo by clearing a flag around the assignment: the
    /// component raises <c>onValueChanged</c> synchronously, and the binder must not mistake its own write for user
    /// input. The flag was cleared and restored without <c>try</c>/<c>finally</c>, so an exception from any other
    /// listener on that event — user code wired in the inspector, for instance — left it cleared for good and the
    /// reverse channel went permanently silent, with nothing in the log to say why.
    /// </remarks>
    [TestFixture]
    public sealed class EchoSuppressionTests
    {
        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) UnityEngine.Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
        }

        [Test]
        public void ToggleBinder_AfterAListenerThrows_StillReportsUserInput()
        {
            var gameObject = NewGameObject();
            var toggle = gameObject.AddComponent<Toggle>();
            var binder = SetMode(gameObject.AddComponent<ToggleIsOnMonoBinder>(), BindMode.TwoWay);

            var received = new List<bool>();
            ((IReverseBinder<bool>)binder).ValueChanged += value => received.Add(value);
            binder.Bind(new TwoWayStructBindableMember<bool>(false, _ => { }));

            // Снимается адресно: RemoveAllListeners убрал бы и слушателя самого биндера.
            UnityEngine.Events.UnityAction<bool> thrower = _ => throw new InvalidOperationException("чужой слушатель");
            toggle.onValueChanged.AddListener(thrower);

            Assert.Throws<InvalidOperationException>(() => binder.SetValue(true));

            received.Clear();
            toggle.onValueChanged.RemoveListener(thrower);
            toggle.isOn = false;

            Assert.IsNotEmpty(received, "После исключения канал View → ViewModel остался обесточен");
        }

        [Test]
        public void SliderBinder_AfterAListenerThrows_StillReportsUserInput()
        {
            var gameObject = NewGameObject();
            var slider = gameObject.AddComponent<Slider>();
            slider.minValue = 0f;
            slider.maxValue = 10f;

            var binder = SetMode(gameObject.AddComponent<SliderValueMonoBinder>(), BindMode.TwoWay);

            var received = new List<float>();
            ((IReverseBinder<float>)binder).ValueChanged += value => received.Add(value);
            binder.Bind(new TwoWayStructBindableMember<float>(0f, _ => { }));

            // Снимается адресно: RemoveAllListeners убрал бы и слушателя самого биндера.
            UnityEngine.Events.UnityAction<float> thrower = _ => throw new InvalidOperationException("чужой слушатель");
            slider.onValueChanged.AddListener(thrower);

            Assert.Throws<InvalidOperationException>(() => binder.SetValue(5f));

            received.Clear();
            slider.onValueChanged.RemoveListener(thrower);
            slider.value = 7f;

            Assert.IsNotEmpty(received, "После исключения канал View → ViewModel остался обесточен");
        }

        private static TBinder SetMode<TBinder>(TBinder binder, BindMode mode)
            where TBinder : MonoBinder
        {
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)mode;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return binder;
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("EchoSuppression");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
