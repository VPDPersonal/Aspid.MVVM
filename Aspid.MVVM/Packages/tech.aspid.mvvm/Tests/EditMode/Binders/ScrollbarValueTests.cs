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
    /// Tests for the new <see cref="Scrollbar.value"/> binders.
    /// </summary>
    /// <remarks>
    /// The Scrollbar domain shipped only Command and OneWayToSource binders, so the one property a scrollbar is
    /// actually for could not be bound at all — while <see cref="Slider"/> had the full matrix. These mirror the
    /// slider family, with one difference: a scrollbar has no configurable range, its value is always 0..1, so the
    /// clamp is fixed rather than read from the component.
    /// </remarks>
    [TestFixture]
    public sealed class ScrollbarValueTests
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
        public void SetValue_ReachesTheScrollbar()
        {
            var (binder, scrollbar) = Create();

            ((IBinder<float>)binder).SetValue(0.25f);

            Assert.AreEqual(0.25f, scrollbar.value, 0.001f, "Значение не доехало до скроллбара");
        }

        [Test]
        public void AUserDrag_ReachesTheViewModel()
        {
            var (binder, scrollbar) = Create();

            var received = new List<float>();
            binder.FloatValueChanged += value => received.Add(value);

            scrollbar.value = 0.75f;

            Assert.AreEqual(new[] { 0.75f }, received, "Изменение со стороны View не доехало до ViewModel");
        }

        [Test]
        public void AValueOutsideTheRange_IsClampedAndReportedBack()
        {
            var (binder, scrollbar) = Create();

            var received = new List<float>();
            binder.FloatValueChanged += value => received.Add(value);

            ((IBinder<float>)binder).SetValue(5f);

            Assert.AreEqual(1f, scrollbar.value, "Значение вне 0..1 не обрезано");
            Assert.AreEqual(new[] { 1f }, received, "ViewModel не узнала, что значение обрезано");
        }

        [Test]
        public void AValueInsideTheRange_IsNotReportedBack()
        {
            var (binder, _) = Create();

            var received = new List<float>();
            binder.FloatValueChanged += value => received.Add(value);

            ((IBinder<float>)binder).SetValue(0.5f);

            Assert.IsEmpty(received, $"Обычное значение вернулось обратно: [{string.Join(", ", received)}]");
        }

        [Test]
        public void SwitcherBinder_AppliesTheSelectedValue()
        {
            var gameObject = NewGameObject();
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
            var gameObject = NewGameObject();
            var scrollbar = gameObject.AddComponent<Scrollbar>();
            var binder = gameObject.AddComponent<ScrollbarValueMonoBinder>();

            var serializedObject = new SerializedObject(binder);
            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.TwoWay;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            ((IBinder)binder).Bind(new TwoWayStructBindableMember<float>(0f, _ => { }));

            return (binder, scrollbar);
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("Scrollbar");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
