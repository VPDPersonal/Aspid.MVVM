using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the new <see cref="LayoutElement"/> and <see cref="Canvas"/> binders.
    /// </summary>
    /// <remarks>
    /// A <see cref="LayoutElement"/> is how one child overrides what its layout group would otherwise decide, and
    /// none of its numbers could be bound — so a ViewModel could not widen a panel or take an element out of the
    /// flow. <see cref="Canvas.sortingOrder"/> had no binder either, which is what brings a panel to the front.
    /// </remarks>
    [TestFixture]
    public sealed class LayoutBinderTests
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
        public void PreferredSizeBinders_ReachTheLayoutElement()
        {
            var gameObject = NewGameObject();
            var element = gameObject.AddComponent<LayoutElement>();

            var width = gameObject.AddComponent<LayoutElementPreferredWidthMonoBinder>();
            var height = gameObject.AddComponent<LayoutElementPreferredHeightMonoBinder>();

            ((IBinder<float>)width).SetValue(120f);
            ((IBinder<float>)height).SetValue(48f);

            Assert.AreEqual(120f, element.preferredWidth, 0.001f);
            Assert.AreEqual(48f, element.preferredHeight, 0.001f);
        }

        /// <summary>
        /// A negative preferred size means "no preference" to Unity, so it is passed through rather than clamped.
        /// </summary>
        [Test]
        public void ANegativePreferredSize_MeansNoPreferenceAndIsPassedThrough()
        {
            var gameObject = NewGameObject();
            var element = gameObject.AddComponent<LayoutElement>();
            var binder = gameObject.AddComponent<LayoutElementPreferredWidthMonoBinder>();

            ((IBinder<float>)binder).SetValue(-1f);

            Assert.AreEqual(-1f, element.preferredWidth, 0.001f, "Отрицательное значение было обрезано");
        }

        [Test]
        public void IgnoreLayoutBinder_TakesTheElementOutOfTheFlow()
        {
            var gameObject = NewGameObject();
            var element = gameObject.AddComponent<LayoutElement>();
            var binder = gameObject.AddComponent<LayoutElementIgnoreLayoutMonoBinder>();

            ((IBinder<bool>)binder).SetValue(true);

            Assert.IsTrue(element.ignoreLayout, "Элемент не исключён из раскладки");
        }

        /// <summary>
        /// The canvas is nested on purpose: Unity ignores <see cref="Canvas.overrideSorting"/> on a root canvas,
        /// which already sorts on its own — the property only means something for a child canvas.
        /// </summary>
        [Test]
        public void CanvasBinders_BringThePanelForward()
        {
            var root = NewGameObject().AddComponent<Canvas>();

            var gameObject = NewGameObject();
            gameObject.transform.SetParent(root.transform, worldPositionStays: false);

            var canvas = gameObject.AddComponent<Canvas>();

            var order = gameObject.AddComponent<CanvasSortingOrderMonoBinder>();
            var over = gameObject.AddComponent<CanvasOverrideSortingMonoBinder>();

            ((IBinder<bool>)over).SetValue(true);
            ((IBinder<int>)order).SetValue(10);

            Assert.IsTrue(canvas.overrideSorting, "Независимая сортировка не включена");
            Assert.AreEqual(10, canvas.sortingOrder, "Порядок сортировки не доехал");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var gameObject = NewGameObject();
            var element = gameObject.AddComponent<LayoutElement>();
            var canvas = NewGameObject().AddComponent<Canvas>();

            Assert.IsTrue(new LayoutElementPreferredWidthBinder(element).IsBind);
            Assert.IsTrue(new LayoutElementFlexibleHeightBinder(element).IsBind);
            Assert.IsTrue(new LayoutElementIgnoreLayoutBinder(element).IsBind);
            Assert.IsTrue(new CanvasSortingOrderBinder(canvas).IsBind);
            Assert.IsTrue(new CanvasOverrideSortingBinder(canvas).IsBind);
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("Layout");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
