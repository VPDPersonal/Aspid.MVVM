using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the new binders over the boolean flags that decide whether a component participates.
    /// </summary>
    /// <remarks>
    /// <c>Graphic.raycastTarget</c> and <c>MaskableGraphic.maskable</c> had no binder at all, and
    /// <c>Renderer.enabled</c> could not be bound either: a <see cref="Renderer"/> is a <see cref="Component"/>
    /// rather than a <see cref="Behaviour"/>, so the behaviour binders do not accept one.
    /// </remarks>
    [TestFixture]
    public sealed class ComponentFlagBinderTests
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
        public void RaycastTargetBinder_DrivesTheFlag()
        {
            var gameObject = NewGameObject();
            var image = gameObject.AddComponent<Image>();
            var binder = gameObject.AddComponent<GraphicRaycastTargetMonoBinder>();

            ((IBinder<bool>)binder).SetValue(false);
            Assert.IsFalse(image.raycastTarget, "Флаг не выключился");

            ((IBinder<bool>)binder).SetValue(true);
            Assert.IsTrue(image.raycastTarget, "Флаг не включился обратно");
        }

        [Test]
        public void MaskableBinder_DrivesTheFlag()
        {
            var gameObject = NewGameObject();
            var image = gameObject.AddComponent<Image>();
            var binder = gameObject.AddComponent<GraphicMaskableMonoBinder>();

            ((IBinder<bool>)binder).SetValue(false);

            Assert.IsFalse(image.maskable, "Флаг не выключился");
        }

        [Test]
        public void RendererEnabledBinder_DrivesTheFlag()
        {
            var gameObject = NewGameObject();
            var renderer = gameObject.AddComponent<MeshRenderer>();
            var binder = gameObject.AddComponent<RendererEnabledMonoBinder>();

            ((IBinder<bool>)binder).SetValue(false);
            Assert.IsFalse(renderer.enabled, "Рендерер не выключился");

            ((IBinder<bool>)binder).SetValue(true);
            Assert.IsTrue(renderer.enabled, "Рендерер не включился обратно");
        }

        /// <summary>
        /// The serializable twins take a target in the constructor, which is the path the Mono binders do not
        /// exercise — a wrong target type would have gone unnoticed until someone wrote code against it.
        /// </summary>
        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var gameObject = NewGameObject();
            var image = gameObject.AddComponent<Image>();
            var renderer = NewGameObject().AddComponent<MeshRenderer>();

            Assert.IsTrue(new GraphicRaycastTargetBinder(image).IsBind);
            Assert.IsTrue(new GraphicMaskableBinder(image).IsBind);
            Assert.IsTrue(new RendererEnabledBinder(renderer).IsBind);
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("Flags");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
