using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for the places where one member of a binder family behaved unlike the rest.
    /// </summary>
    /// <remarks>
    /// The <c>SwitcherFloatBinder</c> stub removed alongside these has no test of its own on purpose: it was a
    /// <c>NotImplementedException</c> override that suppressed the compiler's demand for a real implementation, and
    /// deleting it means the compiler enforces that demand again. That the package still builds is the check.
    /// </remarks>
    [TestFixture]
    public sealed class BinderFamilyDivergenceTests
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

        /// <summary>
        /// <c>enabled = !_disabledWhenNull || value</c> is always <see langword="true"/> once the option is off, so
        /// the binder force-enabled the component on every value instead of leaving it alone.
        /// </summary>
        [Test]
        public void SpriteBinder_WithTheOptionOff_LeavesEnabledAlone()
        {
            var (binder, image) = NewSpriteBinder(disabledWhenNull: false);
            image.enabled = false;

            ((IBinder<Sprite>)binder).SetValue(null);

            Assert.IsFalse(image.enabled, "Биндер включил компонент, хотя опция выключена");
        }

        [Test]
        public void SpriteBinder_WithTheOptionOn_StillDisablesOnNull()
        {
            var (binder, image) = NewSpriteBinder(disabledWhenNull: true);
            image.enabled = true;

            ((IBinder<Sprite>)binder).SetValue(null);
            Assert.IsFalse(image.enabled, "Опция включена, но компонент не отключён на null");

            ((IBinder<Sprite>)binder).SetValue(NewSprite());
            Assert.IsTrue(image.enabled, "Компонент не включён обратно при непустом значении");
        }

        /// <summary>
        /// The binder is declared for <see cref="Graphic"/>, but its only constructor took a
        /// <see cref="RawImage"/> — so it could not be built for an <see cref="Image"/> at all.
        /// This test is really a compile-time assertion; it fails to build on the unfixed tree.
        /// </summary>
        [Test]
        public void GraphicMaterialBinder_AcceptsAnyGraphic()
        {
            var image = NewGameObject().AddComponent<Image>();
            var binder = new GraphicMaterialBinder(image);

            Assert.IsTrue(binder.IsBind, "Биндер не принял Image как Graphic");
        }

        private (ImageSpriteBinder binder, Image image) NewSpriteBinder(bool disabledWhenNull)
        {
            var image = NewGameObject().AddComponent<Image>();
            return (new ImageSpriteBinder(image, disabledWhenNull), image);
        }

        private Sprite NewSprite()
        {
            var texture = new Texture2D(4, 4);
            _spawned.Add(texture);

            var sprite = Sprite.Create(texture, new Rect(0f, 0f, 4f, 4f), Vector2.zero);
            _spawned.Add(sprite);

            return sprite;
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("Divergence");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
