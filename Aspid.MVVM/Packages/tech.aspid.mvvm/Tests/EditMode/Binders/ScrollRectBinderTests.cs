using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the new <see cref="ScrollRect"/> binders.
    /// </summary>
    /// <remarks>
    /// The ScrollRect domain had Command and OneWayToSource binders only, so scrolling a list to the top from the
    /// ViewModel — the usual reason to reach for a ScrollRect at all — had no binder. Locking an axis had none
    /// either.
    /// </remarks>
    [TestFixture]
    public sealed class ScrollRectBinderTests
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
        public void VerticalScrollBinder_MovesTheContent()
        {
            var scrollRect = NewScrollRect();
            var binder = scrollRect.gameObject.AddComponent<ScrollRectVerticalNormalizedPositionMonoBinder>();

            ((IBinder<float>)binder).SetValue(1f);

            Assert.AreEqual(1f, scrollRect.verticalNormalizedPosition, 0.001f, "Позиция не доехала до ScrollRect");
        }

        [Test]
        public void AScrollPositionOutsideTheRange_IsClamped()
        {
            var scrollRect = NewScrollRect();
            var binder = scrollRect.gameObject.AddComponent<ScrollRectVerticalNormalizedPositionMonoBinder>();

            ((IBinder<float>)binder).SetValue(5f);

            Assert.AreEqual(1f, scrollRect.verticalNormalizedPosition, 0.001f, "Позиция вне 0..1 не обрезана");
        }

        [Test]
        public void ANonFiniteScrollPosition_DoesNotReachTheContent()
        {
            var scrollRect = NewScrollRect();
            var binder = scrollRect.gameObject.AddComponent<ScrollRectVerticalNormalizedPositionMonoBinder>();

            ((IBinder<float>)binder).SetValue(float.NaN);

            Assert.IsFalse(float.IsNaN(scrollRect.verticalNormalizedPosition), "NaN дошёл до ScrollRect");
        }

        [Test]
        public void AxisBinders_LockAndUnlockScrolling()
        {
            var scrollRect = NewScrollRect();
            var horizontal = scrollRect.gameObject.AddComponent<ScrollRectHorizontalMonoBinder>();
            var vertical = scrollRect.gameObject.AddComponent<ScrollRectVerticalMonoBinder>();

            ((IBinder<bool>)horizontal).SetValue(false);
            ((IBinder<bool>)vertical).SetValue(false);

            Assert.IsFalse(scrollRect.horizontal, "Горизонтальная прокрутка не заблокирована");
            Assert.IsFalse(scrollRect.vertical, "Вертикальная прокрутка не заблокирована");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var scrollRect = NewScrollRect();

            Assert.IsTrue(new ScrollRectVerticalNormalizedPositionBinder(scrollRect).IsBind);
            Assert.IsTrue(new ScrollRectHorizontalNormalizedPositionBinder(scrollRect).IsBind);
            Assert.IsTrue(new ScrollRectHorizontalBinder(scrollRect).IsBind);
            Assert.IsTrue(new ScrollRectVerticalBinder(scrollRect).IsBind);
        }

        /// <summary>
        /// A ScrollRect reports a position only once it has content and a viewport to measure against.
        /// </summary>
        private ScrollRect NewScrollRect()
        {
            var gameObject = NewGameObject("ScrollRect");
            var scrollRect = gameObject.AddComponent<ScrollRect>();

            var viewport = NewGameObject("Viewport").AddComponent<RectTransform>();
            viewport.SetParent(gameObject.transform, worldPositionStays: false);
            viewport.sizeDelta = new Vector2(100f, 100f);

            var content = NewGameObject("Content").AddComponent<RectTransform>();
            content.SetParent(viewport, worldPositionStays: false);
            content.sizeDelta = new Vector2(100f, 500f);

            scrollRect.viewport = viewport;
            scrollRect.content = content;

            return scrollRect;
        }

        private GameObject NewGameObject(string name)
        {
            var gameObject = new GameObject(name);
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
