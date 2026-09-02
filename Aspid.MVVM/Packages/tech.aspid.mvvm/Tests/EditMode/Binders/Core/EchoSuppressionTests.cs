using System;
using UnityEditor;
using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests that the View → ViewModel channel survives an exception thrown while the binder is writing a value.
    /// </summary>
    /// <remarks>
    /// Toggle and Slider binders suppress their own echo by clearing a flag around the assignment: the component
    /// raises <c>onValueChanged</c> synchronously, and the binder must not mistake its own write for user input.
    /// An exception from another listener on that event must not leave the flag cleared for good.
    /// </remarks>
    [TestFixture]
    public sealed class EchoSuppressionTests : SceneFixture
    {
        [Test]
        public void ToggleBinder_AfterAListenerThrows_StillReportsUserInput()
        {
            var gameObject = Spawn("EchoSuppression");
            var toggle = gameObject.AddComponent<Toggle>();
            var binder = SetMode(gameObject.AddComponent<ToggleIsOnMonoBinder>(), BindMode.TwoWay);

            var received = new List<bool>();
            ((IReverseBinder<bool>)binder).ValueChanged += value => received.Add(value);
            binder.Bind(new TwoWayStructBindableMember<bool>(false, _ => { }));

            // Removed by reference: RemoveAllListeners would also drop the binder's own listener.
            UnityEngine.Events.UnityAction<bool> thrower = _ => throw new InvalidOperationException("unrelated listener");
            toggle.onValueChanged.AddListener(thrower);

            Assert.Throws<InvalidOperationException>(() => binder.SetValue(true));

            received.Clear();
            toggle.onValueChanged.RemoveListener(thrower);
            toggle.isOn = false;

            Assert.IsNotEmpty(received, "The View to ViewModel channel stayed dead after the exception");
        }

        [Test]
        public void SliderBinder_AfterAListenerThrows_StillReportsUserInput()
        {
            var gameObject = Spawn();
            var slider = gameObject.AddComponent<Slider>();
            slider.minValue = 0f;
            slider.maxValue = 10f;

            var binder = SetMode(gameObject.AddComponent<SliderValueMonoBinder>(), BindMode.TwoWay);

            var received = new List<float>();
            ((IReverseBinder<float>)binder).ValueChanged += value => received.Add(value);
            binder.Bind(new TwoWayStructBindableMember<float>(0f, _ => { }));

            // Removed by reference: RemoveAllListeners would also drop the binder's own listener.
            UnityEngine.Events.UnityAction<float> thrower = _ => throw new InvalidOperationException("unrelated listener");
            slider.onValueChanged.AddListener(thrower);

            Assert.Throws<InvalidOperationException>(() => binder.SetValue(5f));

            received.Clear();
            slider.onValueChanged.RemoveListener(thrower);
            slider.value = 7f;

            Assert.IsNotEmpty(received, "The View to ViewModel channel stayed dead after the exception");
        }

        private static TBinder SetMode<TBinder>(TBinder binder, BindMode mode)
            where TBinder : MonoBinder
        {
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)mode;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return binder;
        }
    }
}
