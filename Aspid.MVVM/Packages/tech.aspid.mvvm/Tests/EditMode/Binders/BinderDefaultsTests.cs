using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for binders that failed with their out-of-the-box inspector defaults.
    /// </summary>
    [TestFixture]
    public sealed class BinderDefaultsTests
    {
        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) UnityEngine.Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
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

        [Test]
        public void GetColor_WithStartAndEndMode_ReturnsStartColorInsteadOfThrowing()
        {
            var lineRenderer = NewLineRenderer();
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.green;

            Assert.AreEqual(Color.red, lineRenderer.GetColor(LineRendererColorMode.StartAndEnd));
        }

        /// <summary>
        /// <c>StartAndEnd</c> is the MonoBinder's default color mode, so in
        /// <see cref="BindMode.OneWayToSource"/> the very first <c>Bind</c> reads the property back and used to throw
        /// before the ViewModel ever saw a value.
        /// </summary>
        [Test]
        public void LineRendererColorMonoBinder_OneWayToSource_BindsWithDefaultColorMode()
        {
            var gameObject = NewGameObject();
            var lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.startColor = Color.red;

            var binder = gameObject.AddComponent<LineRendererColorMonoBinder>();
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.OneWayToSource;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            var received = default(Color);
            var member = new OneWayToSourceStructBindableMember<Color>(value => received = value);

            Assert.DoesNotThrow(() => binder.Bind(member));
            Assert.AreEqual(Color.red, received);
        }

        private Renderer NewRenderer() =>
            NewGameObject().AddComponent<MeshRenderer>();

        private LineRenderer NewLineRenderer() =>
            NewGameObject().AddComponent<LineRenderer>();

        private Material NewMaterial() =>
            new(Shader.Find("Unlit/Color"));

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("BinderDefaults");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }

    /// <summary>
    /// Guard test: binders sharing a base class must agree on <see cref="SerializableAttribute"/>.
    /// </summary>
    /// <remarks>
    /// Unity only serializes a plain class field when the concrete type itself is marked <c>[Serializable]</c> —
    /// the flag is not inherited. A binder that misses it never appears in the inspector, and the field it should
    /// have filled stays <see langword="null"/> at <c>Bind</c>, so the binding silently does nothing instead of
    /// failing loudly. <c>TextFontSwitcherBinder</c> and <c>DropdownOptionsSwitcherBinder</c> shipped that way while
    /// their 53 siblings were marked.
    /// <para/>
    /// The assertion is deliberately narrow: it flags a type only when the majority of its siblings under the same
    /// base class are marked. A whole family may legitimately be code-only — the casters and the view binders are,
    /// and none of them is reported. It is the odd one out in an otherwise serializable family that indicates a lost
    /// attribute, and that is all this test claims.
    /// </remarks>
    [TestFixture]
    public sealed class BinderSerializableCoverageTests
    {
        [Test]
        public void ABinderInASerializableFamily_IsAlsoSerializable()
        {
            var inconsistent = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.GetName().Name.StartsWith("Aspid.MVVM", StringComparison.Ordinal))
                .Where(assembly => !assembly.GetName().Name.Contains("Tests", StringComparison.Ordinal))
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: false })
                .Where(type => typeof(IBinder).IsAssignableFrom(type))
                .Where(type => !typeof(UnityEngine.Object).IsAssignableFrom(type))
                .Where(type => type.BaseType is not null)
                // По определению генерика, а не по сконструированному типу: SwitcherBinder<TMP_Text, TMP_FontAsset>
                // и SwitcherBinder<Image, Sprite> — одно семейство, иначе у каждого биндера семья из себя одного.
                .GroupBy(type => type.BaseType!.IsGenericType
                    ? type.BaseType.GetGenericTypeDefinition()
                    : type.BaseType)
                .Where(family => family.Count(type => type.IsSerializable) > family.Count() / 2)
                .SelectMany(family => family.Where(type => !type.IsSerializable)
                    .Select(type => $"{type.FullName} — базовый {Name(family.Key)}, "
                        + $"помечено {family.Count(sibling => sibling.IsSerializable)} из {family.Count()}"))
                .OrderBy(entry => entry)
                .ToList();

            Assert.IsEmpty(
                inconsistent,
                "Биндеры без [Serializable] там, где соседи по базовому классу его имеют — "
                + "Unity не покажет их в инспекторе, поле останется null:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, inconsistent));
        }

        private static string Name(Type type)
        {
            if (!type.IsGenericType) return type.Name;

            var name = type.Name[..type.Name.IndexOf('`')];
            return $"{name}<{string.Join(", ", type.GetGenericArguments().Select(Name))}>";
        }
    }
}
