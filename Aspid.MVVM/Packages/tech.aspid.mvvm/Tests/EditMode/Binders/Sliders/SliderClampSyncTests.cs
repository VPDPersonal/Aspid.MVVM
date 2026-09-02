using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests that a slider clamping a value outside its range reports the clamped result back to the ViewModel.
    /// </summary>
    [TestFixture]
    public sealed class SliderClampSyncTests : SceneFixture
    {
        [Test]
        public void AValueAboveTheRange_IsReportedBackAsClamped()
        {
            var (binder, slider) = Create();

            var received = new List<float>();
            ((IReverseBinder<float>)binder).ValueChanged += value => received.Add(value);

            ((IBinder<float>)binder).SetValue(5f);

            Assert.AreEqual(1f, slider.value, "The slider accepted a value outside its range");
            Assert.AreEqual(new[] { 1f }, received, "The ViewModel was not told the value was clamped");
        }

        [Test]
        public void AValueBelowTheRange_IsReportedBackAsClamped()
        {
            var (binder, slider) = Create();

            var received = new List<float>();
            ((IReverseBinder<float>)binder).ValueChanged += value => received.Add(value);

            ((IBinder<float>)binder).SetValue(-5f);

            Assert.AreEqual(0f, slider.value);
            Assert.AreEqual(new[] { 0f }, received, "The ViewModel was not told the value was clamped");
        }

        /// <summary>
        /// A value the slider can hold must stay silent — otherwise every ViewModel update would bounce back.
        /// </summary>
        [Test]
        public void AValueInsideTheRange_IsNotReportedBack()
        {
            var (binder, slider) = Create();

            var received = new List<float>();
            ((IReverseBinder<float>)binder).ValueChanged += value => received.Add(value);

            ((IBinder<float>)binder).SetValue(0.5f);

            Assert.AreEqual(0.5f, slider.value, 0.001f);
            Assert.IsEmpty(received, $"An ordinary value was reported back: [{string.Join(", ", received)}]");
        }

        [Test]
        public void ANonFiniteValue_DoesNotReachTheSlider()
        {
            var (binder, slider) = Create();
            slider.value = 0.5f;

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);

            Assert.IsFalse(float.IsNaN(slider.value), "NaN reached the slider");
        }

        private (SliderValueMonoBinder binder, Slider slider) Create()
        {
            var slider = Spawn<Slider>("Slider");
            slider.minValue = 0f;
            slider.maxValue = 1f;

            var binder = slider.gameObject.AddComponent<SliderValueMonoBinder>();

            var serializedObject = new SerializedObject(binder);
            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.TwoWay;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return (binder, slider);
        }
    }
}
