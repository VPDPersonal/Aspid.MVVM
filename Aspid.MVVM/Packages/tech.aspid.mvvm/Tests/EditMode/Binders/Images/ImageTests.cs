using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="Image"/> fill and type binders.
    /// </summary>
    [TestFixture]
    public sealed class ImageTests : SceneFixture
    {
        [Test]
        public void TheImageOptions_ReachTheImage()
        {
            var image = Spawn<Image>("Image");

            ((IBinder<Image.Type>)image.gameObject.AddComponent<ImageTypeMonoBinder>()).SetValue(Image.Type.Filled);
            ((IBinder<bool>)image.gameObject.AddComponent<ImagePreserveAspectMonoBinder>()).SetValue(true);
            ((IBinder<int>)image.gameObject.AddComponent<ImageFillOriginMonoBinder>()).SetValue(2);
            ((IBinder<bool>)image.gameObject.AddComponent<ImageFillClockwiseMonoBinder>()).SetValue(false);

            Assert.AreEqual(Image.Type.Filled, image.type, "The image type did not reach the image");
            Assert.IsTrue(image.preserveAspect, "preserveAspect did not reach the image");
            Assert.AreEqual(2, image.fillOrigin, "fillOrigin did not reach the image");
            Assert.IsFalse(image.fillClockwise, "fillClockwise did not reach the image");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var image = Spawn<Image>("Image");

            Assert.IsTrue(new ImageTypeBinder(image).CanBind);
            Assert.IsTrue(new ImagePreserveAspectBinder(image).CanBind);
            Assert.IsTrue(new ImageFillOriginBinder(image).CanBind);
            Assert.IsTrue(new ImageFillClockwiseBinder(image).CanBind);
        }

        [Test]
        public void SpriteBinder_WithTheOptionOff_LeavesEnabledAlone()
        {
            var (binder, image) = NewSpriteBinder(disabledWhenNull: false);
            image.enabled = false;

            ((IBinder<Sprite>)binder).SetValue(null);

            Assert.IsFalse(image.enabled, "The binder enabled the component even though the option is off");
        }

        [Test]
        public void SpriteBinder_WithTheOptionOn_StillDisablesOnNull()
        {
            var (binder, image) = NewSpriteBinder(disabledWhenNull: true);
            image.enabled = true;

            ((IBinder<Sprite>)binder).SetValue(null);
            Assert.IsFalse(image.enabled, "The option is on, but the component was not disabled on null");

            ((IBinder<Sprite>)binder).SetValue(NewSprite());
            Assert.IsTrue(image.enabled, "The component was not re-enabled on a non-null value");
        }

        private (ImageSpriteBinder binder, Image image) NewSpriteBinder(bool disabledWhenNull)
        {
            var image = Spawn<Image>("Sprite");
            return (new ImageSpriteBinder(image, disabledWhenNull), image);
        }

        private Sprite NewSprite()
        {
            var texture = Track(new Texture2D(4, 4));
            return Track(Sprite.Create(texture, new Rect(0f, 0f, 4f, 4f), Vector2.zero));
        }
    }
}
