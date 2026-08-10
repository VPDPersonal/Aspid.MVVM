using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the new <see cref="SpriteRenderer"/> domain.
    /// </summary>
    /// <remarks>
    /// The package covered uGUI thoroughly and 2D not at all: a <see cref="SpriteRenderer"/> had no binder for its
    /// sprite, its tint, its facing, or its draw order. Flipping a character used to mean binding a negative scale,
    /// and tinting one meant going through the shared material.
    /// </remarks>
    [TestFixture]
    public sealed class SpriteRendererBinderTests
    {
        private readonly List<Object> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var spawned in _spawned)
            {
                if (spawned) Object.DestroyImmediate(spawned);
            }

            _spawned.Clear();
        }

        [Test]
        public void SpriteBinder_ReachesTheRenderer()
        {
            var renderer = NewRenderer();
            var binder = renderer.gameObject.AddComponent<SpriteRendererSpriteMonoBinder>();
            var sprite = NewSprite();

            ((IBinder<Sprite>)binder).SetValue(sprite);

            Assert.AreSame(sprite, renderer.sprite, "Спрайт не доехал до рендерера");
        }

        [Test]
        public void ColorBinder_TintsTheRendererDirectly()
        {
            var renderer = NewRenderer();
            var binder = renderer.gameObject.AddComponent<SpriteRendererColorMonoBinder>();

            // Материал у SpriteRenderer по умолчанию не пуст — это Sprite-Unlit-Default,
            // поэтому проверяется не его отсутствие, а то, что он остался тем же самым.
            var material = renderer.sharedMaterial;

            ((IBinder<Color>)binder).SetValue(Color.green);

            Assert.AreEqual(Color.green, renderer.color, "Цвет не доехал до рендерера");
            Assert.AreSame(material, renderer.sharedMaterial, "Тонирование подменило материал экземпляром");
        }

        [Test]
        public void FlipBinders_MirrorTheSprite()
        {
            var renderer = NewRenderer();
            var flipX = renderer.gameObject.AddComponent<SpriteRendererFlipXMonoBinder>();
            var flipY = renderer.gameObject.AddComponent<SpriteRendererFlipYMonoBinder>();

            ((IBinder<bool>)flipX).SetValue(true);
            ((IBinder<bool>)flipY).SetValue(true);

            Assert.IsTrue(renderer.flipX, "flipX не выставлен");
            Assert.IsTrue(renderer.flipY, "flipY не выставлен");
        }

        [Test]
        public void SortingOrderBinder_ReachesTheRenderer()
        {
            var renderer = NewRenderer();
            var binder = renderer.gameObject.AddComponent<SpriteRendererSortingOrderMonoBinder>();

            ((IBinder<int>)binder).SetValue(7);

            Assert.AreEqual(7, renderer.sortingOrder, "Порядок отрисовки не доехал до рендерера");
        }

        /// <summary>
        /// The serializable twins are only reachable through their constructors, which the Mono binders never run.
        /// </summary>
        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var renderer = NewRenderer();

            Assert.IsTrue(new SpriteRendererSpriteBinder(renderer).IsBind);
            Assert.IsTrue(new SpriteRendererColorBinder(renderer).IsBind);
            Assert.IsTrue(new SpriteRendererFlipXBinder(renderer).IsBind);
            Assert.IsTrue(new SpriteRendererFlipYBinder(renderer).IsBind);
            Assert.IsTrue(new SpriteRendererSortingOrderBinder(renderer).IsBind);
        }

        private Sprite NewSprite()
        {
            var texture = new Texture2D(4, 4);
            _spawned.Add(texture);

            var sprite = Sprite.Create(texture, new Rect(0f, 0f, 4f, 4f), Vector2.zero);
            _spawned.Add(sprite);

            return sprite;
        }

        private SpriteRenderer NewRenderer()
        {
            var gameObject = new GameObject("SpriteRenderer");
            _spawned.Add(gameObject);

            return gameObject.AddComponent<SpriteRenderer>();
        }
    }
}
