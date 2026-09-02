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
    /// Tests for the <see cref="ScrollRect.normalizedPosition"/> binder.
    /// </summary>
    [TestFixture]
    public sealed class ScrollRectNormalizedPositionBinderTests : SceneFixture
    {
        [Test]
        public void NormalizedPosition_MovesBothAxesAtOnce()
        {
            var scrollRect = NewScrollRect();
            var binder = scrollRect.gameObject.AddComponent<ScrollRectNormalizedPositionMonoBinder>();

            ((IBinder<Vector2>)binder).SetValue(new Vector2(1f, 1f));

            Assert.AreEqual(1f, scrollRect.horizontalNormalizedPosition, 0.001f, "The horizontal position did not reach the ScrollRect");
            Assert.AreEqual(1f, scrollRect.verticalNormalizedPosition, 0.001f, "The vertical position did not reach the ScrollRect");
        }

        [Test]
        public void NormalizedPosition_ClampsEachAxisSeparately()
        {
            var scrollRect = NewScrollRect();
            var binder = scrollRect.gameObject.AddComponent<ScrollRectNormalizedPositionMonoBinder>();

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Vector2>)binder).SetValue(new Vector2(5f, float.NaN));

            Assert.AreEqual(1f, scrollRect.horizontalNormalizedPosition, 0.001f, "A position outside 0..1 was not clamped");
            Assert.IsFalse(float.IsNaN(scrollRect.verticalNormalizedPosition), "NaN reached the ScrollRect");
        }

        /// <summary>
        /// The reason this binder is on the Vector2 base: a Vector3 base would report a third component the
        /// property has not got.
        /// </summary>
        [Test]
        public void NormalizedPosition_ReportsAVector2Back()
        {
            var scrollRect = NewScrollRect();
            var binder = new ScrollRectNormalizedPositionBinder(scrollRect, mode: BindMode.OneWayToSource);

            var received = default(Vector2);
            binder.Bind(new OneWayToSourceStructBindableMember<Vector2>(value => received = value));

            Assert.AreEqual(scrollRect.normalizedPosition, received, "The ViewModel received the wrong position");
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
            content.sizeDelta = new Vector2(500f, 500f);

            scrollRect.viewport = viewport;
            scrollRect.content = content;

            return scrollRect;
        }
    }
}
