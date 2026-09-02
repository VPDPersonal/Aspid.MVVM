using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="Graphic.raycastTarget"/> and <see cref="MaskableGraphic.maskable"/> binders.
    /// </summary>
    [TestFixture]
    public sealed class GraphicTests : SceneFixture
    {
        [Test]
        public void RaycastTargetBinder_DrivesTheFlag()
        {
            var image = Spawn<Image>("Flags");
            var binder = image.gameObject.AddComponent<GraphicRaycastTargetMonoBinder>();

            ((IBinder<bool>)binder).SetValue(false);
            Assert.IsFalse(image.raycastTarget, "The flag did not turn off");

            ((IBinder<bool>)binder).SetValue(true);
            Assert.IsTrue(image.raycastTarget, "The flag did not turn back on");
        }

        [Test]
        public void MaskableBinder_DrivesTheFlag()
        {
            var image = Spawn<Image>("Flags");
            var binder = image.gameObject.AddComponent<GraphicMaskableMonoBinder>();

            ((IBinder<bool>)binder).SetValue(false);

            Assert.IsFalse(image.maskable, "The flag did not turn off");
        }

        /// <summary>
        /// The serializable twins take a target in the constructor, which is the path the Mono binders do not
        /// exercise — a wrong target type would have gone unnoticed until someone wrote code against it.
        /// </summary>
        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var image = Spawn<Image>("Flags");

            Assert.IsTrue(new GraphicRaycastTargetBinder(image).CanBind);
            Assert.IsTrue(new GraphicMaskableBinder(image).CanBind);
        }

        [Test]
        public void GraphicMaterialBinder_AcceptsAnyGraphic()
        {
            var image = Spawn<Image>("Material");
            var binder = new GraphicMaterialBinder(image);

            Assert.IsTrue(binder.CanBind, "The binder did not accept an Image as a Graphic");
        }
    }
}
