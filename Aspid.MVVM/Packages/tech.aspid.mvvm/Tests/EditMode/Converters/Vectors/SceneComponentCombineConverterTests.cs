using System;
using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage confirming each thin <see cref="Vector3CombineConverter"/> and
    /// <see cref="Vector2CombineConverter"/> subclass reads the scene property it is named for,
    /// rather than exercising the shared axis-selection algorithm again — that is already pinned in
    /// <see cref="Vector3CombineConverterTests"/> against a stub.
    /// </summary>
    /// <remarks>
    /// Every subclass is sealed with only a default constructor, so the target component and the
    /// inherited <c>_mode</c> field are written through reflection. <c>Mode.X</c> is used throughout:
    /// it takes X from the bound vector and everything else from the reference one, which is the only
    /// way to see the reference vector in the result at all.
    /// </remarks>
    [TestFixture]
    public sealed class SceneComponentCombineConverterTests : SceneFixture
    {
        [Test]
        public void TransformPositionCombine_ReadsTheTransformsPosition()
        {
            var transform = NewTransform();
            transform.position = new Vector3(1f, 2f, 3f);

            var converter = new TransformPositionCombineConverter();
            SetField(converter, "_transform", transform);
            SetMode(converter, 0);

            Assert.AreEqual(new Vector3(0f, 2f, 3f), converter.Convert(Vector3.zero));
        }

        [Test]
        public void TransformPosition2DCombine_ReadsTheTransformsPosition()
        {
            var transform = NewTransform();
            transform.position = new Vector3(1f, 2f, 3f);

            var converter = new TransformPosition2DCombineConverter();
            SetField(converter, "_transform", transform);
            SetMode(converter, 0);

            Assert.AreEqual(new Vector2(0f, 2f), converter.Convert(Vector2.zero));
        }

        [Test]
        public void TransformEulerAnglesCombine_ReadsTheTransformsEulerAngles()
        {
            var transform = NewTransform();
            transform.eulerAngles = new Vector3(10f, 20f, 30f);

            var converter = new TransformEulerAnglesCombineConverter();
            SetField(converter, "_transform", transform);
            SetMode(converter, 0);

            var result = converter.Convert(Vector3.zero);

            Assert.AreEqual(20f, result.y, 1e-3f);
            Assert.AreEqual(30f, result.z, 1e-3f);
        }

        [Test]
        public void TransformScaleCombine_ReadsTheTransformsLocalScale()
        {
            var transform = NewTransform();
            transform.localScale = new Vector3(2f, 3f, 4f);

            var converter = new TransformScaleCombineConverter();
            SetField(converter, "_transform", transform);
            SetMode(converter, 0);

            Assert.AreEqual(new Vector3(0f, 3f, 4f), converter.Convert(Vector3.zero));
        }

        [Test]
        public void BoxColliderCenterCombine_ReadsTheColliderCenter()
        {
            var collider = NewComponent<BoxCollider>();
            collider.center = new Vector3(1f, 2f, 3f);

            var converter = new BoxColliderCenterCombineConverter();
            SetField(converter, "_collider", collider);
            SetMode(converter, 0);

            Assert.AreEqual(new Vector3(0f, 2f, 3f), converter.Convert(Vector3.zero));
        }

        [Test]
        public void BoxColliderSizeCombine_ReadsTheColliderSize()
        {
            var collider = NewComponent<BoxCollider>();
            collider.size = new Vector3(2f, 3f, 4f);

            var converter = new BoxColliderSizeCombineConverter();
            SetField(converter, "_collider", collider);
            SetMode(converter, 0);

            Assert.AreEqual(new Vector3(0f, 3f, 4f), converter.Convert(Vector3.zero));
        }

        [Test]
        public void SphereColliderCenterCombine_ReadsTheColliderCenter()
        {
            var collider = NewComponent<SphereCollider>();
            collider.center = new Vector3(1f, 2f, 3f);

            var converter = new SphereColliderCenterCombineConverter();
            SetField(converter, "_collider", collider);
            SetMode(converter, 0);

            Assert.AreEqual(new Vector3(0f, 2f, 3f), converter.Convert(Vector3.zero));
        }

        [Test]
        public void CapsuleColliderCenterCombine_ReadsTheColliderCenter()
        {
            var collider = NewComponent<CapsuleCollider>();
            collider.center = new Vector3(1f, 2f, 3f);

            var converter = new CapsuleColliderCenterCombineConverter();
            SetField(converter, "_collider", collider);
            SetMode(converter, 0);

            Assert.AreEqual(new Vector3(0f, 2f, 3f), converter.Convert(Vector3.zero));
        }

        [Test]
        public void BoxCollider2DOffsetCombine_ReadsTheColliderOffset()
        {
            var collider = NewComponent<BoxCollider2D>();
            collider.offset = new Vector2(1f, 2f);

            var converter = new BoxCollider2DOffsetCombineConverter();
            SetField(converter, "_collider", collider);
            SetMode(converter, 0);

            Assert.AreEqual(new Vector2(0f, 2f), converter.Convert(Vector2.zero));
        }

        [Test]
        public void BoxCollider2DSizeCombine_ReadsTheColliderSize()
        {
            var collider = NewComponent<BoxCollider2D>();
            collider.size = new Vector2(3f, 4f);

            var converter = new BoxCollider2DSizeCombineConverter();
            SetField(converter, "_collider", collider);
            SetMode(converter, 0);

            Assert.AreEqual(new Vector2(0f, 4f), converter.Convert(Vector2.zero));
        }

        [Test]
        public void RectTransformAnchoredPositionCombine_ReadsTheAnchoredPosition()
        {
            var transform = NewComponent<RectTransform>();
            transform.anchoredPosition3D = new Vector3(1f, 2f, 3f);

            var converter = new RectTransformAnchoredPositionCombineConverter();
            SetField(converter, "_transform", transform);
            SetMode(converter, 0);

            var result = converter.Convert(Vector3.zero);

            Assert.AreEqual(2f, result.y, 1e-4f);
            Assert.AreEqual(3f, result.z, 1e-4f);
        }

        [Test]
        public void RectTransformAnchoredPosition2DCombine_ReadsTheAnchoredPosition()
        {
            var transform = NewComponent<RectTransform>();
            transform.anchoredPosition = new Vector2(1f, 2f);

            var converter = new RectTransformAnchoredPosition2DCombineConverter();
            SetField(converter, "_transform", transform);
            SetMode(converter, 0);

            Assert.AreEqual(new Vector2(0f, 2f), converter.Convert(Vector2.zero));
        }

        [Test]
        public void RectTransformSizeDeltaCombine_ReadsTheSizeDelta()
        {
            var transform = NewComponent<RectTransform>();
            transform.sizeDelta = new Vector2(3f, 4f);

            var converter = new RectTransformSizeDeltaCombineConverter();
            SetField(converter, "_transform", transform);
            SetMode(converter, 0);

            Assert.AreEqual(new Vector2(0f, 4f), converter.Convert(Vector2.zero));
        }

        // Every subclass shares the base's missing-target degrade, so one representative pins that
        // the wiring did not accidentally bypass it.
        [Test]
        public void MissingTarget_LogsAndReturnsTheInputUnchanged()
        {
            LogAssert.Expect(LogType.Error, new Regex("no target assigned"));

            Assert.AreEqual(Vector3.one, new TransformPositionCombineConverter().Convert(Vector3.one));
        }

        private Transform NewTransform() => NewComponent<Transform>();

        private TComponent NewComponent<TComponent>() where TComponent : Component
        {
            var gameObject = Spawn(nameof(SceneComponentCombineConverterTests));

            // TryGetComponent rather than GetComponent(...) ?? AddComponent(...): a missing component
            // comes back as Unity's fake null, which is not a null reference, so ?? never fires and
            // the component is never added — the caller then gets a MissingComponentException on the
            // first property it touches.
            return gameObject.TryGetComponent<TComponent>(out var existing)
                ? existing
                : gameObject.AddComponent<TComponent>();
        }

        // The concrete subclass declares its own target field; the mode lives on the abstract base.
        // Both are private, so the search walks up the hierarchy rather than assuming one level.
        private static void SetField(object target, string name, object value)
        {
            for (var type = target.GetType(); type is not null; type = type.BaseType)
            {
                var field = type.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
                if (field is null) continue;

                field.SetValue(target, value);
                return;
            }

            Assert.Fail($"{target.GetType().Name} has no field {name} in its hierarchy.");
        }

        // Mode is declared on each abstract base as its own enum type, and FieldInfo.SetValue rejects
        // a plain int for an enum field, so the int is boxed as that field's own enum type first.
        private static void SetMode(object converter, int mode)
        {
            for (var type = converter.GetType(); type is not null; type = type.BaseType)
            {
                var field = type.GetField("_mode", BindingFlags.Instance | BindingFlags.NonPublic);
                if (field is null) continue;

                field.SetValue(converter, Enum.ToObject(field.FieldType, mode));
                return;
            }

            Assert.Fail($"{converter.GetType().Name} has no field _mode in its hierarchy.");
        }
    }
}
