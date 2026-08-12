using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for the renderer colour binders touching <see cref="Renderer.materials"/> per value.
    /// </summary>
    /// <remarks>
    /// Reading <see cref="Renderer.material"/> or <see cref="Renderer.materials"/> instantiates: Unity replaces the
    /// shared asset with a private copy and hands back a freshly allocated array each time. Bound to a colour that
    /// changes per frame, that is an array per frame; and the getters instantiated a material merely to read a
    /// colour off it, which the shared asset could have answered.
    /// <para/>
    /// The write path still uses <see cref="Renderer.materials"/> on purpose — writing to the shared asset would
    /// recolour every object using it — but fetches the array once, which is what the switcher variant of this same
    /// family already did.
    /// </remarks>
    [TestFixture]
    public sealed class RendererMaterialTests
    {
        private const string ColorProperty = "_Color";

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
        /// Pins the premise: the read the getters used to perform replaces the asset with a copy.
        /// </summary>
        [Test]
        public void UnityRendererMaterial_ReadReplacesTheSharedAssetWithACopy()
        {
            var renderer = NewRenderer(out var asset);

            // Unity сама называет это утечкой — ровно та причина, по которой геттеры переведены на sharedMaterial.
            LogAssert.Expect(LogType.Error, new Regex("Instantiating material due to calling renderer.material"));
            var read = renderer.material;

            Assert.AreNotSame(asset, read, "Unity перестала клонировать материал при чтении");
            Assert.AreNotSame(asset, renderer.sharedMaterial, "Unity перестала подменять sharedMaterial копией");
        }

        [Test]
        public void ColorBinder_Reading_DoesNotInstantiateTheMaterial()
        {
            var renderer = NewRenderer(out var asset);
            var binder = NewBinder(renderer);

            ReadProperty(binder);

            Assert.AreSame(asset, renderer.sharedMaterial, "Чтение цвета подменило материал копией");
        }

        [Test]
        public void ColorBinder_Writing_StillReachesTheMaterial()
        {
            var renderer = NewRenderer(out _);
            var binder = NewBinder(renderer);

            // Запись обязана инстансить: правка общего ассета перекрасила бы все объекты, которые его используют.
            LogAssert.Expect(LogType.Error, new Regex("Instantiating material due to calling renderer.material"));
            ((IBinder<Color>)binder).SetValue(Color.red);

            Assert.AreEqual(Color.red, renderer.sharedMaterial.GetColor(ColorProperty), "Цвет не доехал до материала");
        }

        /// <summary>
        /// The binder defaults to <c>_BaseColor</c>, which the built-in sprite shader does not have; the property
        /// name is retargeted so the test measures the binder rather than a missing shader property.
        /// </summary>
        private static RendererMaterialsColorMonoBinder NewBinder(Renderer renderer)
        {
            var binder = renderer.gameObject.AddComponent<RendererMaterialsColorMonoBinder>();

            var serializedObject = new SerializedObject(binder);
            serializedObject.FindProperty("_colorPropertyName").stringValue = ColorProperty;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return binder;
        }

        private static void ReadProperty(RendererMaterialsColorMonoBinder binder)
        {
            var property = typeof(RendererMaterialsColorMonoBinder)
                .GetProperty("Property", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            Assert.IsNotNull(property, "Свойство Property переименовано — тест больше ничего не проверяет");
            property.GetValue(binder);
        }

        private Renderer NewRenderer(out Material asset)
        {
            var gameObject = new GameObject("Renderer");
            _spawned.Add(gameObject);

            asset = new Material(Shader.Find("Sprites/Default")) { name = "Asset" };
            _spawned.Add(asset);

            var renderer = gameObject.AddComponent<MeshRenderer>();
            renderer.sharedMaterial = asset;

            return renderer;
        }
    }
}
