using System;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for the renderer colour binders touching <see cref="Renderer.materials"/> per value.
    /// </summary>
    /// <remarks>
    /// Reading <see cref="Renderer.material"/> or <see cref="Renderer.materials"/> instantiates a private copy and allocates an array,
    /// so the binders read through the shared material and fetch the array once per write.
    /// </remarks>
    [TestFixture]
    public sealed class RendererMaterialTests : SceneFixture
    {
        private const string ColorProperty = "_Color";

        /// <summary>
        /// Pins the premise: the read the getters used to perform replaces the asset with a copy.
        /// </summary>
        [Test]
        public void UnityRendererMaterial_ReadReplacesTheSharedAssetWithACopy()
        {
            var renderer = NewRenderer(out var asset);

            // Unity calls this a leak itself — the exact reason the getters moved to sharedMaterial.
            LogAssert.Expect(LogType.Error, new Regex("Instantiating material due to calling renderer.material"));
            var read = renderer.material;

            Assert.AreNotSame(asset, read, "Unity stopped cloning the material on read");
            Assert.AreNotSame(asset, renderer.sharedMaterial, "Unity stopped replacing sharedMaterial with a copy");
        }

        [Test]
        public void ColorBinder_Reading_DoesNotInstantiateTheMaterial()
        {
            var renderer = NewRenderer(out var asset);
            var binder = NewBinder(renderer);

            ReadProperty(binder);

            Assert.AreSame(asset, renderer.sharedMaterial, "Reading the color replaced the material with a copy");
        }

        [Test]
        public void ColorBinder_Writing_StillReachesTheMaterial()
        {
            var renderer = NewRenderer(out _);
            var binder = NewBinder(renderer);

            // Writing must instantiate: editing the shared asset would recolour every object using it.
            LogAssert.Expect(LogType.Error, new Regex("Instantiating material due to calling renderer.material"));
            ((IBinder<Color>)binder).SetValue(Color.red);

            Assert.AreEqual(Color.red, renderer.sharedMaterial.GetColor(ColorProperty), "The colour did not reach the material");
        }

        [Test]
        public void SetMaterials_WithNullCollection_ClearsInsteadOfThrowing()
        {
            var renderer = NewRenderer();

            Assert.DoesNotThrow(() => renderer.SetMaterials(converter: null, (IReadOnlyCollection<Material>)null));
            Assert.IsEmpty(renderer.sharedMaterials);
        }

        [Test]
        public void SetMaterials_WithEmptyCollection_ClearsInsteadOfThrowing()
        {
            var renderer = NewRenderer();

            Assert.DoesNotThrow(() => renderer.SetMaterials(converter: null, Array.Empty<Material>()));
            Assert.IsEmpty(renderer.sharedMaterials);
        }

        [Test]
        public void SetMaterials_WithNullParamsArray_ClearsInsteadOfThrowing()
        {
            var renderer = NewRenderer();

            Assert.DoesNotThrow(() => renderer.SetMaterials(converter: null, (Material[])null));
            Assert.IsEmpty(renderer.sharedMaterials);
        }

        [Test]
        public void SetMaterials_WithSingleMaterial_AssignsIt()
        {
            var renderer = NewRenderer();
            var material = NewMaterial();

            Assert.DoesNotThrow(() => renderer.SetMaterials(converter: null, new[] { material }));
            Assert.AreEqual(1, renderer.sharedMaterials.Length);
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

            Assert.IsNotNull(property, "Property was renamed — this test no longer checks anything");
            property.GetValue(binder);
        }

        private Renderer NewRenderer(out Material asset)
        {
            asset = Track(new Material(Shader.Find("Sprites/Default")) { name = "Asset" });
            var renderer = Spawn("Renderer").AddComponent<MeshRenderer>();
            renderer.sharedMaterial = asset;

            return renderer;
        }

        private Renderer NewRenderer() =>
            Spawn("Renderer").AddComponent<MeshRenderer>();

        private Material NewMaterial() =>
            Track(new Material(Shader.Find("Unlit/Color")));
    }
}
