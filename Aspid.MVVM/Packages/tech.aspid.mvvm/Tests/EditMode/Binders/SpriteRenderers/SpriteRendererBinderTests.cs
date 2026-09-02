using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="SpriteRenderer"/> domain: sprite, tint, facing, draw order and sliced size.
    /// </summary>
    [TestFixture]
    public sealed class SpriteRendererBinderTests : SceneFixture
    {
        [Test]
        public void SpriteBinder_ReachesTheRenderer()
        {
            var renderer = NewRenderer();
            var binder = renderer.gameObject.AddComponent<SpriteRendererSpriteMonoBinder>();
            var sprite = NewSprite();

            ((IBinder<Sprite>)binder).SetValue(sprite);

            Assert.AreSame(sprite, renderer.sprite, "The sprite did not reach the renderer");
        }

        [Test]
        public void ColorBinder_TintsTheRendererDirectly()
        {
            var renderer = NewRenderer();
            var binder = renderer.gameObject.AddComponent<SpriteRendererColorMonoBinder>();

            // The default SpriteRenderer material is Sprite-Unlit-Default, not none, so the check is that it stays
            // the same instance rather than that it is absent.
            var material = renderer.sharedMaterial;

            ((IBinder<Color>)binder).SetValue(Color.green);

            Assert.AreEqual(Color.green, renderer.color, "The colour did not reach the renderer");
            Assert.AreSame(material, renderer.sharedMaterial, "Tinting replaced the material with an instance");
        }

        [Test]
        public void FlipBinders_MirrorTheSprite()
        {
            var renderer = NewRenderer();
            var flipX = renderer.gameObject.AddComponent<SpriteRendererFlipXMonoBinder>();
            var flipY = renderer.gameObject.AddComponent<SpriteRendererFlipYMonoBinder>();

            ((IBinder<bool>)flipX).SetValue(true);
            ((IBinder<bool>)flipY).SetValue(true);

            Assert.IsTrue(renderer.flipX, "flipX was not set");
            Assert.IsTrue(renderer.flipY, "flipY was not set");
        }

        [Test]
        public void SortingOrderBinder_ReachesTheRenderer()
        {
            var renderer = NewRenderer();
            var binder = renderer.gameObject.AddComponent<SpriteRendererSortingOrderMonoBinder>();

            ((IBinder<int>)binder).SetValue(7);

            Assert.AreEqual(7, renderer.sortingOrder, "The sorting order did not reach the renderer");
        }

        [Test]
        public void SizeBinder_ReachesTheRenderer()
        {
            var renderer = NewSlicedRenderer();
            var binder = renderer.gameObject.AddComponent<SpriteRendererSizeMonoBinder>();

            ((IBinder<Vector2>)binder).SetValue(new Vector2(3f, 4f));

            Assert.AreEqual(new Vector2(3f, 4f), renderer.size, "The size did not reach the renderer");
        }

        [Test]
        public void SizeBinder_NegativeAndNonFinite_AreClampedToZero()
        {
            var renderer = NewSlicedRenderer();
            var binder = renderer.gameObject.AddComponent<SpriteRendererSizeMonoBinder>();

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Vector2>)binder).SetValue(new Vector2(-2f, float.NaN));

            Assert.AreEqual(Vector2.zero, renderer.size, "A negative or non-finite size was not clamped");
        }

        /// <summary>
        /// The serializable twins are only reachable through their constructors, which the Mono binders never run.
        /// </summary>
        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var renderer = NewRenderer();

            Assert.IsTrue(new SpriteRendererSpriteBinder(renderer).CanBind);
            Assert.IsTrue(new SpriteRendererColorBinder(renderer).CanBind);
            Assert.IsTrue(new SpriteRendererFlipXBinder(renderer).CanBind);
            Assert.IsTrue(new SpriteRendererFlipYBinder(renderer).CanBind);
            Assert.IsTrue(new SpriteRendererSortingOrderBinder(renderer).CanBind);
            Assert.IsTrue(new SpriteRendererSizeBinder(renderer).CanBind);
        }

        private Sprite NewSprite()
        {
            var texture = Track(new Texture2D(4, 4));
            return Track(Sprite.Create(texture, new Rect(0f, 0f, 4f, 4f), Vector2.zero));
        }

        private SpriteRenderer NewRenderer() =>
            Spawn<SpriteRenderer>("SpriteRenderer");

        private SpriteRenderer NewSlicedRenderer()
        {
            var renderer = NewRenderer();
            renderer.drawMode = SpriteDrawMode.Sliced;

            return renderer;
        }
    }
}
