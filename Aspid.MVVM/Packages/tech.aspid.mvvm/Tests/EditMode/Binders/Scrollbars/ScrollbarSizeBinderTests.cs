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
    /// Tests for the <see cref="Scrollbar.size"/> binder.
    /// </summary>
    [TestFixture]
    public sealed class ScrollbarSizeBinderTests : SceneFixture
    {
        [Test]
        public void ScrollbarSize_ReachesTheScrollbar()
        {
            var scrollbar = Spawn<Scrollbar>("Scrollbar");
            var binder = scrollbar.gameObject.AddComponent<ScrollbarSizeMonoBinder>();

            ((IBinder<float>)binder).SetValue(0.25f);

            Assert.AreEqual(0.25f, scrollbar.size, 0.001f, "The handle size did not reach the scrollbar");
        }

        [Test]
        public void ScrollbarSize_OutsideTheRange_IsClamped()
        {
            var scrollbar = Spawn<Scrollbar>("Scrollbar");
            var binder = scrollbar.gameObject.AddComponent<ScrollbarSizeMonoBinder>();

            ((IBinder<float>)binder).SetValue(5f);

            Assert.AreEqual(1f, scrollbar.size, 0.001f, "A size outside 0..1 was not clamped");
        }

        [Test]
        public void ScrollbarSize_NonFinite_DoesNotReachTheScrollbar()
        {
            var scrollbar = Spawn<Scrollbar>("Scrollbar");
            var binder = scrollbar.gameObject.AddComponent<ScrollbarSizeMonoBinder>();

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);

            Assert.IsFalse(float.IsNaN(scrollbar.size), "NaN reached the scrollbar");
        }

        [Test]
        public void TheSerializableTwin_AcceptsItsTarget()
        {
            var scrollbar = Spawn<Scrollbar>("Scrollbar");

            Assert.IsTrue(new ScrollbarSizeBinder(scrollbar).CanBind);
        }
    }
}
