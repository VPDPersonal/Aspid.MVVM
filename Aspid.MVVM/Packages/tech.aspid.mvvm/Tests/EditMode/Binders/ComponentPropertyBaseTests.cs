using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the property bases the binder set was missing:
    /// <see cref="ComponentVector2MonoBinder{TComponent}"/>, <see cref="ComponentQuaternionMonoBinder{TComponent}"/>,
    /// <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/> and their <c>Target</c> counterparts.
    /// </summary>
    /// <remarks>
    /// Two-dimensional properties used to be bound through a Vector3 base, which reports <c>Vector3(x, y, 0)</c>
    /// back to the ViewModel — a value the property never held. Object-typed properties had no base at all, so
    /// nothing stopped a destroyed asset from being written or reported as live. These pin both.
    /// </remarks>
    [TestFixture]
    public sealed class ComponentPropertyBaseTests
    {
        private readonly List<GameObject> _spawned = new();
        private readonly List<Object> _assets = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            foreach (var asset in _assets)
            {
                if (asset) Object.DestroyImmediate(asset);
            }

            _spawned.Clear();
            _assets.Clear();
        }

        #region Vector2
        /// <summary>
        /// The reason the base exists: a Vector3 base would have reported a third component the property has not got.
        /// </summary>
        [Test]
        public void Vector2MonoBinder_OneWayToSource_ReportsExactlyTheTwoComponents()
        {
            var (component, binder) = NewVector2Binder(BindMode.OneWayToSource);
            component.Value = new Vector2(3f, 4f);

            var received = default(Vector2);
            binder.Bind(new OneWayToSourceStructBindableMember<Vector2>(value => received = value));

            Assert.AreEqual(new Vector2(3f, 4f), received, "ViewModel получила не то значение, что лежит в свойстве");
        }

        [Test]
        public void Vector2MonoBinder_DropsTheZComponentOfAVector3()
        {
            var (component, binder) = NewVector2Binder(BindMode.OneWay);
            binder.Bind(new OneWayStructBindableMember<Vector2>(Vector2.zero));

            binder.SetValue(new Vector3(1f, 2f, 3f));

            Assert.AreEqual(new Vector2(1f, 2f), component.Value, "Vector3 применился не как (X, Y)");
        }

        [Test]
        public void Vector2MonoBinder_BroadcastsAScalarToBothComponents()
        {
            var (component, binder) = NewVector2Binder(BindMode.OneWay);
            binder.Bind(new OneWayStructBindableMember<Vector2>(Vector2.zero));

            binder.SetValue(5f);

            Assert.AreEqual(new Vector2(5f, 5f), component.Value, "Скаляр не разошёлся по обеим компонентам");
        }

        /// <summary>
        /// Guards the redeclared <c>SetValue(Vector2)</c>: without it the Vector3 overload wins for a Vector2
        /// argument, because Vector2 converts to Vector3 implicitly.
        /// </summary>
        [Test]
        public void Vector2MonoBinder_ADirectVector2Call_AppliesTheVectorItself()
        {
            var (component, binder) = NewVector2Binder(BindMode.OneWay);
            binder.Bind(new OneWayStructBindableMember<Vector2>(Vector2.zero));

            binder.SetValue(new Vector2(7f, 8f));

            Assert.AreEqual(new Vector2(7f, 8f), component.Value, "Прямой вызов SetValue(Vector2) потерял значение");
        }

        [Test]
        public void Vector2TargetBinder_OneWayToSource_ReportsExactlyTheTwoComponents()
        {
            var component = NewGameObject().AddComponent<Vector2Component>();
            component.Value = new Vector2(9f, 10f);

            var binder = new TestTargetVector2Binder(component, mode: BindMode.OneWayToSource);
            var received = default(Vector2);

            binder.Bind(new OneWayToSourceStructBindableMember<Vector2>(value => received = value));

            Assert.AreEqual(new Vector2(9f, 10f), received, "ViewModel получила не то значение, что лежит в свойстве");
        }
        #endregion

        #region Quaternion
        [Test]
        public void QuaternionMonoBinder_ReadsAVector3AsEulerAngles()
        {
            var (component, binder) = NewQuaternionBinder(BindMode.OneWay);
            binder.Bind(new OneWayStructBindableMember<Quaternion>(Quaternion.identity));

            ((IBinder<Vector3>)binder).SetValue(new Vector3(0f, 90f, 0f));

            Assert.AreEqual(Quaternion.Euler(0f, 90f, 0f).eulerAngles, component.Value.eulerAngles,
                "Vector3 не прочитался как углы Эйлера");
        }

        [Test]
        public void QuaternionMonoBinder_AppliesAScalarToAllThreeAxes()
        {
            var (component, binder) = NewQuaternionBinder(BindMode.OneWay);
            binder.Bind(new OneWayStructBindableMember<Quaternion>(Quaternion.identity));

            binder.SetValue(30f);

            Assert.AreEqual(Quaternion.Euler(30f, 30f, 30f).eulerAngles, component.Value.eulerAngles,
                "Скаляр не применился как одинаковый угол по трём осям");
        }

        /// <summary>
        /// A rotation property raises no change event, so TwoWay would be a channel that never delivers.
        /// The base refuses it rather than degrading silently.
        /// </summary>
        [Test]
        public void QuaternionTargetBinder_RefusesTwoWay()
        {
            var component = NewGameObject().AddComponent<QuaternionComponent>();

            Assert.Throws<System.ArgumentException>(
                () => _ = new TestTargetQuaternionBinder(component, mode: BindMode.TwoWay),
                "TwoWay принят режимом, в котором обратный канал невозможен");
        }
        #endregion

        #region Object
        [Test]
        public void ObjectMonoBinder_ADestroyedObjectArrivesAsNull()
        {
            var (component, binder) = NewObjectBinder(BindMode.OneWay);
            var texture = NewTexture();

            binder.Bind(new OneWayBindableMember<Texture2D>(null));
            Object.DestroyImmediate(texture);

            binder.SetValue(texture);

            Assert.IsNull(component.Value, "Уничтоженный объект записался в свойство как живой");
        }

        [Test]
        public void ObjectMonoBinder_OneWayToSource_ReportsADestroyedPropertyAsNull()
        {
            var (component, binder) = NewObjectBinder(BindMode.OneWayToSource);
            var texture = NewTexture();

            component.Value = texture;
            Object.DestroyImmediate(texture);

            var received = new List<Texture2D>();
            binder.Bind(new OneWayToSourceBindableMember<Texture2D>(received.Add));

            Assert.AreEqual(1, received.Count, "ViewModel не получила значение при установке связи");
            Assert.IsNull(received[0], "Уничтоженный объект ушёл во ViewModel как живой");
        }

        [Test]
        public void ObjectMonoBinder_ALiveObjectPassesThrough()
        {
            var (component, binder) = NewObjectBinder(BindMode.OneWay);
            var texture = NewTexture();

            binder.Bind(new OneWayBindableMember<Texture2D>(null));
            binder.SetValue(texture);

            Assert.AreSame(texture, component.Value, "Живой объект не доехал до свойства");
        }

        [Test]
        public void ObjectTargetBinder_ADestroyedObjectArrivesAsNull()
        {
            var component = NewGameObject().AddComponent<ObjectComponent>();
            var texture = NewTexture();
            var binder = new TestTargetObjectBinder(component, BindMode.OneWay);

            binder.Bind(new OneWayBindableMember<Texture2D>(null));
            Object.DestroyImmediate(texture);

            binder.SetValue(texture);

            Assert.IsNull(component.Value, "Уничтоженный объект записался в свойство как живой");
        }
        #endregion

        #region TransformRotation regression
        /// <summary>
        /// <see cref="TransformRotationMonoBinder"/> and <see cref="TransformRotationBinder"/> were moved onto the
        /// new Quaternion bases; these pin that the members they used to declare themselves still behave the same.
        /// </summary>
        [Test]
        public void TransformRotationMonoBinder_StillAcceptsEulerAnglesAndScalars()
        {
            var gameObject = NewGameObject();
            var binder = gameObject.AddComponent<TransformRotationMonoBinder>();

            binder.Bind(new OneWayStructBindableMember<Quaternion>(Quaternion.identity));

            ((IBinder<Vector3>)binder).SetValue(new Vector3(0f, 45f, 0f));
            Assert.AreEqual(Quaternion.Euler(0f, 45f, 0f).eulerAngles, gameObject.transform.rotation.eulerAngles,
                "Углы Эйлера перестали применяться после переноса на базу");

            binder.SetValue(15f);
            Assert.AreEqual(Quaternion.Euler(15f, 15f, 15f).eulerAngles, gameObject.transform.rotation.eulerAngles,
                "Скаляр перестал применяться после переноса на базу");
        }

        [Test]
        public void TransformRotationBinder_StillRefusesTwoWay()
        {
            var gameObject = NewGameObject();

            Assert.Throws<System.ArgumentException>(
                () => _ = new TransformRotationBinder(gameObject.transform, mode: BindMode.TwoWay),
                "TransformRotationBinder перестал отвергать TwoWay");
        }
        #endregion

        #region Helpers
        private (Vector2Component Component, TestComponentVector2Binder Binder) NewVector2Binder(BindMode mode)
        {
            var gameObject = NewGameObject();
            var component = gameObject.AddComponent<Vector2Component>();

            return (component, CreateBinder<TestComponentVector2Binder>(gameObject, mode));
        }

        private (QuaternionComponent Component, TestComponentQuaternionBinder Binder) NewQuaternionBinder(BindMode mode)
        {
            var gameObject = NewGameObject();
            var component = gameObject.AddComponent<QuaternionComponent>();

            return (component, CreateBinder<TestComponentQuaternionBinder>(gameObject, mode));
        }

        private (ObjectComponent Component, TestComponentObjectBinder Binder) NewObjectBinder(BindMode mode)
        {
            var gameObject = NewGameObject();
            var component = gameObject.AddComponent<ObjectComponent>();

            return (component, CreateBinder<TestComponentObjectBinder>(gameObject, mode));
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("BinderTest");
            _spawned.Add(gameObject);

            return gameObject;
        }

        private Texture2D NewTexture()
        {
            var texture = new Texture2D(1, 1);
            _assets.Add(texture);

            return texture;
        }

        /// <summary>
        /// Adds the binder and switches its mode the same way the inspector does — the serialized <c>_mode</c>
        /// field has no public setter.
        /// </summary>
        private static TBinder CreateBinder<TBinder>(GameObject gameObject, BindMode mode)
            where TBinder : MonoBinder
        {
            var binder = gameObject.AddComponent<TBinder>();
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)mode;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            Assert.AreEqual(mode, binder.Mode, "Не удалось выставить режим биндера через SerializedObject");
            return binder;
        }
        #endregion
    }

    internal sealed class Vector2Component : MonoBehaviour
    {
        public Vector2 Value;
    }

    internal sealed class QuaternionComponent : MonoBehaviour
    {
        public Quaternion Value = Quaternion.identity;
    }

    internal sealed class ObjectComponent : MonoBehaviour
    {
        public Texture2D Value;
    }

    internal sealed class TestComponentVector2Binder : ComponentVector2MonoBinder<Vector2Component>
    {
        protected override Vector2 Property
        {
            get => CachedComponent.Value;
            set => CachedComponent.Value = value;
        }
    }

    internal sealed class TestComponentQuaternionBinder : ComponentQuaternionMonoBinder<QuaternionComponent>
    {
        protected override Quaternion Property
        {
            get => CachedComponent.Value;
            set => CachedComponent.Value = value;
        }
    }

    internal sealed class TestComponentObjectBinder : ComponentObjectMonoBinder<ObjectComponent, Texture2D>
    {
        protected override Texture2D Property
        {
            get => CachedComponent.Value;
            set => CachedComponent.Value = value;
        }
    }

    internal sealed class TestTargetVector2Binder : TargetVector2Binder<Vector2Component>
    {
        public TestTargetVector2Binder(
            Vector2Component target,
            IConverter<Vector2, Vector2> converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        protected override Vector2 Property
        {
            get => Target.Value;
            set => Target.Value = value;
        }
    }

    internal sealed class TestTargetQuaternionBinder : TargetQuaternionBinder<QuaternionComponent>
    {
        public TestTargetQuaternionBinder(
            QuaternionComponent target,
            IConverter<Quaternion, Quaternion> converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        protected override Quaternion Property
        {
            get => Target.Value;
            set => Target.Value = value;
        }
    }

    internal sealed class TestTargetObjectBinder : TargetObjectBinder<ObjectComponent, Texture2D>
    {
        public TestTargetObjectBinder(ObjectComponent target, BindMode mode = BindMode.OneWay)
            : base(target, mode) { }

        protected override Texture2D Property
        {
            get => Target.Value;
            set => Target.Value = value;
        }
    }
}
