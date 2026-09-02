using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="Scrollbar.value"/> binders.
    /// </summary>
    [TestFixture]
    public sealed class ScrollbarValueTests : SceneFixture
    {
        [Test]
        public void SetValue_ReachesTheScrollbar()
        {
            var (binder, scrollbar) = Create();

            ((IBinder<float>)binder).SetValue(0.25f);

            Assert.AreEqual(0.25f, scrollbar.value, 0.001f, "The value did not reach the scrollbar");
        }

        [Test]
        public void AUserDrag_ReachesTheViewModel()
        {
            var (binder, scrollbar) = Create();

            var received = new List<float>();
            ((IReverseBinder<float>)binder).ValueChanged += value => received.Add(value);

            scrollbar.value = 0.75f;

            Assert.AreEqual(new[] { 0.75f }, received, "The change from the View did not reach the ViewModel");
        }

        [Test]
        public void AValueOutsideTheRange_IsClampedAndReportedBack()
        {
            var (binder, scrollbar) = Create();

            var received = new List<float>();
            ((IReverseBinder<float>)binder).ValueChanged += value => received.Add(value);

            ((IBinder<float>)binder).SetValue(5f);

            Assert.AreEqual(1f, scrollbar.value, "A value outside 0..1 was not clamped");
            Assert.AreEqual(new[] { 1f }, received, "The ViewModel was not told the value was clamped");
        }

        [Test]
        public void AValueInsideTheRange_IsNotReportedBack()
        {
            var (binder, _) = Create();

            var received = new List<float>();
            ((IReverseBinder<float>)binder).ValueChanged += value => received.Add(value);

            ((IBinder<float>)binder).SetValue(0.5f);

            Assert.IsEmpty(received, $"An ordinary value was reported back: [{string.Join(", ", received)}]");
        }

        [Test]
        public void SwitcherBinder_AppliesTheSelectedValue()
        {
            var gameObject = Spawn("Scrollbar");
            var scrollbar = gameObject.AddComponent<Scrollbar>();
            var binder = gameObject.AddComponent<ScrollbarValueSwitcherMonoBinder>();

            var serializedObject = new SerializedObject(binder);
            serializedObject.FindProperty("_trueValue").floatValue = 0.8f;
            serializedObject.FindProperty("_falseValue").floatValue = 0.2f;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            ((IBinder<bool>)binder).SetValue(true);
            Assert.AreEqual(0.8f, scrollbar.value, 0.001f);

            ((IBinder<bool>)binder).SetValue(false);
            Assert.AreEqual(0.2f, scrollbar.value, 0.001f);
        }

        private (ScrollbarValueMonoBinder binder, Scrollbar scrollbar) Create()
        {
            var gameObject = Spawn("Scrollbar");
            var scrollbar = gameObject.AddComponent<Scrollbar>();
            var binder = gameObject.AddComponent<ScrollbarValueMonoBinder>();

            var serializedObject = new SerializedObject(binder);
            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.TwoWay;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            ((IBinder)binder).Bind(new TwoWayStructBindableMember<float>(0f, _ => { }));

            return (binder, scrollbar);
        }
    }
}
