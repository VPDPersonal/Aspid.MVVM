using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="RectTransform"/> anchor, pivot and offset binders.
    /// </summary>
    [TestFixture]
    public sealed class RectTransformAnchorBinderTests : SceneFixture
    {
        [Test]
        public void TheAnchorsAndPivot_ReachTheRect()
        {
            var rect = Spawn<RectTransform>("Rect");

            ((IBinder<Vector2>)rect.gameObject.AddComponent<RectTransformAnchorMinMonoBinder>()).SetValue(new Vector2(0.25f, 0.25f));
            ((IBinder<Vector2>)rect.gameObject.AddComponent<RectTransformAnchorMaxMonoBinder>()).SetValue(new Vector2(0.75f, 0.75f));
            ((IBinder<Vector2>)rect.gameObject.AddComponent<RectTransformPivotMonoBinder>()).SetValue(new Vector2(1f, 0f));

            Assert.AreEqual(new Vector2(0.25f, 0.25f), rect.anchorMin, "anchorMin did not reach the rect");
            Assert.AreEqual(new Vector2(0.75f, 0.75f), rect.anchorMax, "anchorMax did not reach the rect");
            Assert.AreEqual(new Vector2(1f, 0f), rect.pivot, "pivot did not reach the rect");
        }

        /// <summary>
        /// Anchors outside 0..1 are how an element is stretched past its parent, so they are kept; a non-finite value
        /// would take the element off the screen and is refused.
        /// </summary>
        [Test]
        public void TheAnchors_KeepValuesOutsideTheUnitRange_AndRefuseNonFinite()
        {
            var rect = Spawn<RectTransform>("Rect");
            var binder = rect.gameObject.AddComponent<RectTransformAnchorMinMonoBinder>();

            ((IBinder<Vector2>)binder).SetValue(new Vector2(-0.5f, 1.5f));
            Assert.AreEqual(new Vector2(-0.5f, 1.5f), rect.anchorMin, "Value outside 0..1 was not saved");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Vector2>)binder).SetValue(new Vector2(float.NaN, 0f));
            Assert.AreEqual(new Vector2(-0.5f, 1.5f), rect.anchorMin, "A non-finite value reached the rect");
        }

        [Test]
        public void TheOffsets_KeepNegativeValues()
        {
            var rect = Spawn<RectTransform>("Rect");

            ((IBinder<Vector2>)rect.gameObject.AddComponent<RectTransformOffsetMinMonoBinder>()).SetValue(new Vector2(-8f, -8f));
            ((IBinder<Vector2>)rect.gameObject.AddComponent<RectTransformOffsetMaxMonoBinder>()).SetValue(new Vector2(8f, 8f));

            Assert.AreEqual(new Vector2(-8f, -8f), rect.offsetMin, "offsetMin did not reach the rect");
            Assert.AreEqual(new Vector2(8f, 8f), rect.offsetMax, "offsetMax did not reach the rect");
        }

        /// <summary>
        /// The reason these are on the Vector2 base: a Vector3 one would report a third component the rect has not got.
        /// </summary>
        [Test]
        public void TheRectBinders_ReportAVector2Back()
        {
            var rect = Spawn<RectTransform>("Rect");
            rect.pivot = new Vector2(0.3f, 0.7f);

            var binder = new RectTransformPivotBinder(rect, mode: BindMode.OneWayToSource);
            var received = default(Vector2);

            binder.Bind(new OneWayToSourceStructBindableMember<Vector2>(value => received = value));

            Assert.AreEqual(new Vector2(0.3f, 0.7f), received, "The ViewModel received the wrong pivot");
        }
    }
}
