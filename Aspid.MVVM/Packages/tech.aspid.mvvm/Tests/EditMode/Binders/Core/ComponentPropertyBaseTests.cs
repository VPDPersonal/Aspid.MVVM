using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the property bases the binder set was missing:
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/>, <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/>
    /// and their <c>Target</c> counterparts.
    /// </summary>
    [TestFixture]
    public sealed class ComponentPropertyBaseTests : SceneFixture
    {
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

            Assert.AreEqual(new Vector2(3f, 4f), received, "The ViewModel received a value other than the one held by the property");
        }

        [Test]
        public void Vector2MonoBinder_DropsTheZComponentOfAVector3()
        {
            var (component, binder) = NewVector2Binder(BindMode.OneWay);
            binder.Bind(new OneWayStructBindableMember<Vector2>(Vector2.zero));

            ((IBinder<Vector3>)binder).SetValue(new Vector3(1f, 2f, 3f));

            Assert.AreEqual(new Vector2(1f, 2f), component.Value, "The Vector3 was not applied as (X, Y)");
        }

        [Test]
        public void Vector2MonoBinder_BroadcastsAScalarToBothComponents()
        {
            var (component, binder) = NewVector2Binder(BindMode.OneWay);
            binder.Bind(new OneWayStructBindableMember<Vector2>(Vector2.zero));

            ((IBinder<float>)binder).SetValue(5f);

            Assert.AreEqual(new Vector2(5f, 5f), component.Value, "The scalar did not spread to both components");
        }

        /// <summary>
        /// The property's own type stays the direct call: <see cref="IVector2Binder"/> keeps the Vector3 and scalar
        /// entry points as default interface implementations, so neither can win overload resolution here.
        /// </summary>
        [Test]
        public void Vector2MonoBinder_ADirectVector2Call_AppliesTheVectorItself()
        {
            var (component, binder) = NewVector2Binder(BindMode.OneWay);
            binder.Bind(new OneWayStructBindableMember<Vector2>(Vector2.zero));

            binder.SetValue(new Vector2(7f, 8f));

            Assert.AreEqual(new Vector2(7f, 8f), component.Value, "The direct SetValue(Vector2) call lost the value");
        }

        [Test]
        public void Vector2TargetBinder_OneWayToSource_ReportsExactlyTheTwoComponents()
        {
            var component = Spawn<Vector2Component>();
            component.Value = new Vector2(9f, 10f);

            var binder = new TestTargetVector2Binder(component, mode: BindMode.OneWayToSource);
            var received = default(Vector2);

            binder.Bind(new OneWayToSourceStructBindableMember<Vector2>(value => received = value));

            Assert.AreEqual(new Vector2(9f, 10f), received, "The ViewModel received a value other than the one held by the property");
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
                "The Vector3 was not read as Euler angles");
        }

        [Test]
        public void QuaternionMonoBinder_AppliesAScalarToAllThreeAxes()
        {
            var (component, binder) = NewQuaternionBinder(BindMode.OneWay);
            binder.Bind(new OneWayStructBindableMember<Quaternion>(Quaternion.identity));

            ((IBinder<float>)binder).SetValue(30f);

            Assert.AreEqual(Quaternion.Euler(30f, 30f, 30f).eulerAngles, component.Value.eulerAngles,
                "The scalar was not applied as the same angle on all three axes");
        }

        #endregion

        #region Object
        [Test]
        public void ObjectMonoBinder_ADestroyedObjectArrivesAsNull()
        {
            var (component, binder) = NewObjectBinder(BindMode.OneWay);
            var texture = NewTexture();

            binder.Bind(new OneWayBindableMember<Texture2D>(null));
            Destroy(texture);

            binder.SetValue(texture);

            Assert.IsNull(component.Value, "The destroyed object was written into the property as if it were alive");
        }

        [Test]
        public void ObjectMonoBinder_OneWayToSource_ReportsADestroyedPropertyAsNull()
        {
            var (component, binder) = NewObjectBinder(BindMode.OneWayToSource);
            var texture = NewTexture();

            component.Value = texture;
            Destroy(texture);

            var received = new List<Texture2D>();
            binder.Bind(new OneWayToSourceBindableMember<Texture2D>(received.Add));

            Assert.AreEqual(1, received.Count, "The ViewModel did not receive a value when the binding was made");
            Assert.IsNull(received[0], "The destroyed object reached the ViewModel as if it were alive");
        }

        [Test]
        public void ObjectMonoBinder_ALiveObjectPassesThrough()
        {
            var (component, binder) = NewObjectBinder(BindMode.OneWay);
            var texture = NewTexture();

            binder.Bind(new OneWayBindableMember<Texture2D>(null));
            binder.SetValue(texture);

            Assert.AreSame(texture, component.Value, "The live object did not reach the property");
        }

        [Test]
        public void ObjectTargetBinder_ADestroyedObjectArrivesAsNull()
        {
            var component = Spawn<ObjectComponent>();
            var texture = NewTexture();
            var binder = new TestTargetObjectBinder(component, mode: BindMode.OneWay);

            binder.Bind(new OneWayBindableMember<Texture2D>(null));
            Destroy(texture);

            binder.SetValue(texture);

            Assert.IsNull(component.Value, "The destroyed object was written into the property as if it were alive");
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
            var binder = Spawn<TransformRotationMonoBinder>();

            binder.Bind(new OneWayStructBindableMember<Quaternion>(Quaternion.identity));

            ((IBinder<Vector3>)binder).SetValue(new Vector3(0f, 45f, 0f));
            Assert.AreEqual(Quaternion.Euler(0f, 45f, 0f).eulerAngles, binder.transform.rotation.eulerAngles,
                "Euler angles stopped applying after the move to the base");

            ((IBinder<float>)binder).SetValue(15f);
            Assert.AreEqual(Quaternion.Euler(15f, 15f, 15f).eulerAngles, binder.transform.rotation.eulerAngles,
                "The scalar stopped applying after the move to the base");
        }

        /// <summary>
        /// A rotation property raises no change event, so TwoWay would be a channel that never delivers.
        /// Each rotation binder refuses it in its own constructor rather than degrading silently.
        /// </summary>
        [Test]
        public void TransformRotationBinder_StillRefusesTwoWay()
        {
            var gameObject = Spawn();

            Assert.Throws<System.ArgumentException>(
                () => _ = new TransformRotationBinder(gameObject.transform, mode: BindMode.TwoWay),
                "TransformRotationBinder stopped refusing TwoWay");
        }
        #endregion

        #region Helpers
        private (Vector2Component Component, TestComponentVector2Binder Binder) NewVector2Binder(BindMode mode)
        {
            var component = Spawn<Vector2Component>();
            return (component, CreateBinder<TestComponentVector2Binder>(component.gameObject, mode));
        }

        private (QuaternionComponent Component, TestComponentQuaternionBinder Binder) NewQuaternionBinder(BindMode mode)
        {
            var component = Spawn<QuaternionComponent>();
            return (component, CreateBinder<TestComponentQuaternionBinder>(component.gameObject, mode));
        }

        private (ObjectComponent Component, TestComponentObjectBinder Binder) NewObjectBinder(BindMode mode)
        {
            var component = Spawn<ObjectComponent>();
            return (component, CreateBinder<TestComponentObjectBinder>(component.gameObject, mode));
        }

        private Texture2D NewTexture() => Track(new Texture2D(1, 1));

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

            Assert.AreEqual(mode, binder.Mode, "Could not set the binder's mode through SerializedObject");
            return binder;
        }
        #endregion
    }

    internal sealed class TestComponentVector2Binder : ComponentMonoBinder<Vector2Component, Vector2>, IVector2Binder
    {
        protected override Vector2 Property
        {
            get => CachedComponent.Value;
            set => CachedComponent.Value = value;
        }
    }

    internal sealed class TestComponentQuaternionBinder : ComponentMonoBinder<QuaternionComponent, Quaternion>, IRotationBinder
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

    internal sealed class TestTargetVector2Binder : TargetBinder<Vector2Component, Vector2>, IVector2Binder
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

    internal sealed class TestTargetObjectBinder : TargetObjectBinder<ObjectComponent, Texture2D>
    {
        public TestTargetObjectBinder(ObjectComponent target, IConverter<Texture2D, Texture2D> converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        protected override Texture2D Property
        {
            get => Target.Value;
            set => Target.Value = value;
        }
    }
}
