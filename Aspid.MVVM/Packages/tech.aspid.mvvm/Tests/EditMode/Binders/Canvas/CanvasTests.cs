using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="Canvas"/> sorting binders.
    /// </summary>
    [TestFixture]
    public sealed class CanvasTests : SceneFixture
    {
        /// <summary>
        /// The child canvas is nested on purpose: Unity ignores <see cref="Canvas.overrideSorting"/> on a root
        /// canvas, which already sorts on its own — the property only means something for a child canvas.
        /// </summary>
        [Test]
        public void CanvasBinders_BringThePanelForward()
        {
            var root = Spawn<Canvas>("Root");

            var canvas = Spawn<Canvas>("Canvas");
            canvas.transform.SetParent(root.transform, worldPositionStays: false);

            var order = canvas.gameObject.AddComponent<CanvasSortingOrderMonoBinder>();
            var over = canvas.gameObject.AddComponent<CanvasOverrideSortingMonoBinder>();

            ((IBinder<bool>)over).SetValue(true);
            ((IBinder<int>)order).SetValue(10);

            Assert.IsTrue(canvas.overrideSorting, "Independent sorting was not enabled");
            Assert.AreEqual(10, canvas.sortingOrder, "The sorting order did not reach the canvas");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var canvas = Spawn<Canvas>("Canvas");

            Assert.IsTrue(new CanvasSortingOrderBinder(canvas).CanBind);
            Assert.IsTrue(new CanvasOverrideSortingBinder(canvas).CanBind);
        }
    }
}
