using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="ScrollRect"/> binders.
    /// </summary>
    [TestFixture]
    public sealed class ScrollRectBinderTests : SceneFixture
    {
        [Test]
        public void VerticalScrollBinder_MovesTheContent()
        {
            var scrollRect = NewScrollRect();
            var binder = scrollRect.gameObject.AddComponent<ScrollRectVerticalNormalizedPositionMonoBinder>();

            ((IBinder<float>)binder).SetValue(1f);

            Assert.AreEqual(1f, scrollRect.verticalNormalizedPosition, 0.001f, "The position did not reach the ScrollRect");
        }

        [Test]
        public void AScrollPositionOutsideTheRange_IsClamped()
        {
            var scrollRect = NewScrollRect();
            var binder = scrollRect.gameObject.AddComponent<ScrollRectVerticalNormalizedPositionMonoBinder>();

            ((IBinder<float>)binder).SetValue(5f);

            Assert.AreEqual(1f, scrollRect.verticalNormalizedPosition, 0.001f, "A position outside 0..1 was not clamped");
        }

        [Test]
        public void ANonFiniteScrollPosition_DoesNotReachTheContent()
        {
            var scrollRect = NewScrollRect();
            var binder = scrollRect.gameObject.AddComponent<ScrollRectVerticalNormalizedPositionMonoBinder>();

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);

            Assert.IsFalse(float.IsNaN(scrollRect.verticalNormalizedPosition), "NaN reached the ScrollRect");
        }

        [Test]
        public void AxisBinders_LockAndUnlockScrolling()
        {
            var scrollRect = NewScrollRect();
            var horizontal = scrollRect.gameObject.AddComponent<ScrollRectHorizontalMonoBinder>();
            var vertical = scrollRect.gameObject.AddComponent<ScrollRectVerticalMonoBinder>();

            ((IBinder<bool>)horizontal).SetValue(false);
            ((IBinder<bool>)vertical).SetValue(false);

            Assert.IsFalse(scrollRect.horizontal, "Horizontal scrolling was not locked");
            Assert.IsFalse(scrollRect.vertical, "Vertical scrolling was not locked");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var scrollRect = NewScrollRect();

            Assert.IsTrue(new ScrollRectVerticalNormalizedPositionBinder(scrollRect).CanBind);
            Assert.IsTrue(new ScrollRectHorizontalNormalizedPositionBinder(scrollRect).CanBind);
            Assert.IsTrue(new ScrollRectHorizontalBinder(scrollRect).CanBind);
            Assert.IsTrue(new ScrollRectVerticalBinder(scrollRect).CanBind);
        }

        /// <summary>
        /// A ScrollRect reports a position only once it has content and a viewport to measure against.
        /// </summary>
        private ScrollRect NewScrollRect()
        {
            var scrollRect = Spawn<ScrollRect>("ScrollRect");

            var viewport = Spawn<RectTransform>("Viewport");
            viewport.SetParent(scrollRect.transform, worldPositionStays: false);
            viewport.sizeDelta = new Vector2(100f, 100f);

            var content = Spawn<RectTransform>("Content");
            content.SetParent(viewport, worldPositionStays: false);
            content.sizeDelta = new Vector2(100f, 500f);

            scrollRect.viewport = viewport;
            scrollRect.content = content;

            return scrollRect;
        }
    }
}
