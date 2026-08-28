using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Enum used to drive the <c>*EnumMonoBinder</c> variants from a serialized lookup table.
    /// </summary>
    internal enum TestBinderEnum
    {
        Ignored,
        Applied,
    }

    /// <summary>
    /// Regression tests asserting that the Transform binders write to the component assigned in the inspector
    /// rather than to the binder's own <see cref="Component.transform"/>.
    /// </summary>
    /// <remarks>
    /// Every binder here derives from <see cref="ComponentMonoBinder{TComponent}"/>, which resolves the target
    /// through <c>CachedComponent</c> — the serialized field first, <c>GetComponent</c> only as a fallback.
    /// A copy-paste divergence made ten of them dereference the inherited <see cref="Component.transform"/>
    /// instead, so a binder pointed at a child silently moved its own object. Each test therefore puts the binder
    /// and its target on two different GameObjects and asserts both sides: the target changed, the binder's own
    /// transform did not.
    /// <para/>
    /// The <c>*EnumMonoBinder</c> variants resolve their value through a serialized
    /// <c>EnumValues&lt;Vector3&gt;</c> table, populated here the same way Aspid.FastTools tests it: the
    /// assembly-qualified enum type into <c>_enumType</c> and one <c>_key</c> / <c>_value</c> entry into
    /// <c>_values</c>.
    /// </remarks>
    [TestFixture]
    public sealed class TransformBinderTargetTests
    {
        private static readonly Vector3 Applied = new(2f, 3f, 4f);
        private static readonly Vector3 Rotated = new(0f, 90f, 0f);

        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
        }

        [Test]
        public void ScaleMonoBinder_SetValue_WritesToAssignedTransform()
        {
            var (binder, target, own) = Create<TransformScaleMonoBinder>();

            // Через IBinder<Vector3>, как это делает биндинг.
            ((IBinder<Vector3>)binder).SetValue(Applied);

            Assert.AreEqual(Applied, target.localScale, "Значение не доехало до назначенного Transform");
            Assert.AreEqual(Vector3.one, own.localScale, "Биндер изменил собственный Transform");
        }

        [Test]
        public void ScaleMonoBinder_OneWayToSource_ReadsFromAssignedTransform()
        {
            var (binder, target, own) = Create<TransformScaleMonoBinder>();
            target.localScale = Applied;
            own.localScale = -Applied;

            SetMode(binder, BindMode.OneWayToSource);

            Assert.AreEqual(Applied, BindAndCapture<Vector3>(binder));
        }

        [Test]
        public void RotationMonoBinder_SetValue_WritesToAssignedTransform()
        {
            var (binder, target, own) = Create<TransformRotationMonoBinder>();
            var rotation = Quaternion.Euler(0f, 90f, 0f);

            binder.SetValue(rotation);

            Assert.Less(Quaternion.Angle(rotation, target.rotation), 0.01f, "Поворот не доехал до назначенного Transform");
            Assert.Less(Quaternion.Angle(Quaternion.identity, own.rotation), 0.01f, "Биндер повернул собственный Transform");
        }

        [Test]
        public void RotationMonoBinder_OneWayToSource_ReadsFromAssignedTransform()
        {
            var (binder, target, own) = Create<TransformRotationMonoBinder>();
            var rotation = Quaternion.Euler(0f, 90f, 0f);
            target.rotation = rotation;
            own.rotation = Quaternion.Euler(0f, -45f, 0f);

            SetMode(binder, BindMode.OneWayToSource);

            Assert.Less(Quaternion.Angle(rotation, BindAndCapture<Quaternion>(binder)), 0.01f);
        }

        [Test]
        public void ScaleSwitcherMonoBinder_SetValue_WritesToAssignedTransform()
        {
            var (binder, target, own) = CreateSwitcher<TransformScaleSwitcherMonoBinder>(Applied);

            binder.SetValue(true);

            Assert.AreEqual(Applied, target.localScale);
            Assert.AreEqual(Vector3.one, own.localScale, "Биндер изменил собственный Transform");
        }

        [Test]
        public void PositionSwitcherMonoBinder_SetValue_WritesToAssignedTransform()
        {
            var (binder, target, own) = CreateSwitcher<TransformPositionSwitcherMonoBinder>(Applied);

            binder.SetValue(true);

            Assert.AreEqual(Applied, target.position);
            Assert.AreEqual(Vector3.zero, own.position, "Биндер сдвинул собственный Transform");
        }

        [Test]
        public void EulerAnglesSwitcherMonoBinder_SetValue_WritesToAssignedTransform()
        {
            var (binder, target, own) = CreateSwitcher<TransformEulerAnglesSwitcherMonoBinder>(new Vector3(0f, 90f, 0f));

            binder.SetValue(true);

            Assert.Less(Quaternion.Angle(Quaternion.Euler(0f, 90f, 0f), target.rotation), 0.01f);
            Assert.Less(Quaternion.Angle(Quaternion.identity, own.rotation), 0.01f, "Биндер повернул собственный Transform");
        }

        [Test]
        public void RotationSwitcherMonoBinder_SetValue_WritesToAssignedTransform()
        {
            var (binder, target, own) = CreateSwitcher<TransformRotationSwitcherMonoBinder>(new Vector3(0f, 90f, 0f));

            binder.SetValue(true);

            Assert.Less(Quaternion.Angle(Quaternion.Euler(0f, 90f, 0f), target.rotation), 0.01f);
            Assert.Less(Quaternion.Angle(Quaternion.identity, own.rotation), 0.01f, "Биндер повернул собственный Transform");
        }

        [Test]
        public void ScaleEnumMonoBinder_SetValue_WritesToAssignedTransform()
        {
            var (binder, target, own) = CreateEnum<TransformScaleEnumMonoBinder>(Applied);

            binder.SetValue(TestBinderEnum.Applied);

            Assert.AreEqual(Applied, target.localScale);
            Assert.AreEqual(Vector3.one, own.localScale, "Биндер изменил собственный Transform");
        }

        [Test]
        public void PositionEnumMonoBinder_SetValue_WritesToAssignedTransform()
        {
            var (binder, target, own) = CreateEnum<TransformPositionEnumMonoBinder>(Applied);

            binder.SetValue(TestBinderEnum.Applied);

            Assert.AreEqual(Applied, target.position);
            Assert.AreEqual(Vector3.zero, own.position, "Биндер сдвинул собственный Transform");
        }

        [Test]
        public void EulerAnglesEnumMonoBinder_SetValue_WritesToAssignedTransform()
        {
            var (binder, target, own) = CreateEnum<TransformEulerAnglesEnumMonoBinder>(Rotated);

            binder.SetValue(TestBinderEnum.Applied);

            Assert.Less(Quaternion.Angle(Quaternion.Euler(Rotated), target.rotation), 0.01f);
            Assert.Less(Quaternion.Angle(Quaternion.identity, own.rotation), 0.01f, "Биндер повернул собственный Transform");
        }

        [Test]
        public void RotationEnumMonoBinder_SetValue_WritesToAssignedTransform()
        {
            var (binder, target, own) = CreateEnum<TransformRotationEnumMonoBinder>(Rotated);

            binder.SetValue(TestBinderEnum.Applied);

            Assert.Less(Quaternion.Angle(Quaternion.Euler(Rotated), target.rotation), 0.01f);
            Assert.Less(Quaternion.Angle(Quaternion.identity, own.rotation), 0.01f, "Биндер повернул собственный Transform");
        }

        /// <summary>
        /// Puts the binder and its target on two different GameObjects and assigns the target through the
        /// serialized <c>_component</c> field, exactly as the inspector does.
        /// </summary>
        private (TBinder binder, Transform target, Transform own) Create<TBinder>()
            where TBinder : MonoBinder
        {
            var ownerObject = NewGameObject("Binder");
            var targetObject = NewGameObject("Target");

            var binder = ownerObject.AddComponent<TBinder>();
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_component").objectReferenceValue = targetObject.transform;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return (binder, targetObject.transform, ownerObject.transform);
        }

        private (TBinder binder, Transform target, Transform own) CreateSwitcher<TBinder>(Vector3 trueValue)
            where TBinder : MonoBinder
        {
            var created = Create<TBinder>();
            var serializedObject = new SerializedObject(created.binder);

            serializedObject.FindProperty("_trueValue").vector3Value = trueValue;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return created;
        }

        /// <summary>
        /// Populates the binder's serialized <c>EnumValues&lt;Vector3&gt;</c> with a single
        /// <see cref="TestBinderEnum.Applied"/> entry, mirroring how Aspid.FastTools tests the same type.
        /// </summary>
        private (TBinder binder, Transform target, Transform own) CreateEnum<TBinder>(Vector3 appliedValue)
            where TBinder : MonoBinder
        {
            var created = Create<TBinder>();
            var serializedObject = new SerializedObject(created.binder);

            serializedObject.FindProperty("_enumValues._enumType").stringValue =
                typeof(TestBinderEnum).AssemblyQualifiedName;

            var values = serializedObject.FindProperty("_enumValues._values");
            values.arraySize = 1;

            var entry = values.GetArrayElementAtIndex(0);
            entry.FindPropertyRelative("_key").stringValue = nameof(TestBinderEnum.Applied);
            entry.FindPropertyRelative("_value").vector3Value = appliedValue;

            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return created;
        }

        private GameObject NewGameObject(string name)
        {
            var gameObject = new GameObject(name);
            _spawned.Add(gameObject);

            return gameObject;
        }

        private static void SetMode(MonoBinder binder, BindMode mode)
        {
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)mode;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            Assert.AreEqual(mode, binder.Mode, "Не удалось выставить режим биндера через SerializedObject");
        }

        private static T BindAndCapture<T>(IBinder binder)
            where T : struct
        {
            var received = default(T);
            var member = new OneWayToSourceStructBindableMember<T>(value => received = value);

            binder.Bind(member);
            return received;
        }
    }
}
