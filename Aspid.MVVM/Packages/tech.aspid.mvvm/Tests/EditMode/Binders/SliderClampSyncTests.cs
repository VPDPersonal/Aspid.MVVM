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
    /// Regression tests for a slider quietly refusing a value the ViewModel believes it accepted.
    /// </summary>
    /// <remarks>
    /// A value outside <c>[minValue, maxValue]</c> is clamped by Unity inside <c>Slider.Set</c>. The clamp does
    /// raise <c>onValueChanged</c> — the value really did change — but the binder suppresses that event with the
    /// echo guard around its own assignment, so the corrected value never reached the ViewModel. In
    /// <see cref="BindMode.TwoWay"/> and <see cref="BindMode.OneWayToSource"/> the two then stayed apart until the
    /// ViewModel next changed the property on its own.
    /// </remarks>
    [TestFixture]
    public sealed class SliderClampSyncTests
    {
        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
        }

        [Test]
        public void AValueAboveTheRange_IsReportedBackAsClamped()
        {
            var (binder, slider) = Create();

            var received = new List<float>();
            binder.FloatValueChanged += value => received.Add(value);

            ((IBinder<float>)binder).SetValue(5f);

            Assert.AreEqual(1f, slider.value, "Ползунок принял значение вне диапазона");
            Assert.AreEqual(new[] { 1f }, received, "ViewModel не узнала, что значение обрезано");
        }

        [Test]
        public void AValueBelowTheRange_IsReportedBackAsClamped()
        {
            var (binder, slider) = Create();

            var received = new List<float>();
            binder.FloatValueChanged += value => received.Add(value);

            ((IBinder<float>)binder).SetValue(-5f);

            Assert.AreEqual(0f, slider.value);
            Assert.AreEqual(new[] { 0f }, received, "ViewModel не узнала, что значение обрезано");
        }

        /// <summary>
        /// A value the slider can hold must stay silent — otherwise every ViewModel update would bounce back.
        /// </summary>
        [Test]
        public void AValueInsideTheRange_IsNotReportedBack()
        {
            var (binder, slider) = Create();

            var received = new List<float>();
            binder.FloatValueChanged += value => received.Add(value);

            ((IBinder<float>)binder).SetValue(0.5f);

            Assert.AreEqual(0.5f, slider.value, 0.001f);
            Assert.IsEmpty(received, $"Обычное значение вернулось обратно: [{string.Join(", ", received)}]");
        }

        [Test]
        public void ANonFiniteValue_DoesNotReachTheSlider()
        {
            var (binder, slider) = Create();
            slider.value = 0.5f;

            ((IBinder<float>)binder).SetValue(float.NaN);

            Assert.IsFalse(float.IsNaN(slider.value), "NaN дошёл до ползунка");
        }

        private (SliderValueMonoBinder binder, Slider slider) Create()
        {
            var gameObject = new GameObject("Slider");
            _spawned.Add(gameObject);

            var slider = gameObject.AddComponent<Slider>();
            slider.minValue = 0f;
            slider.maxValue = 1f;

            var binder = gameObject.AddComponent<SliderValueMonoBinder>();

            var serializedObject = new SerializedObject(binder);
            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.TwoWay;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return (binder, slider);
        }
    }
}
