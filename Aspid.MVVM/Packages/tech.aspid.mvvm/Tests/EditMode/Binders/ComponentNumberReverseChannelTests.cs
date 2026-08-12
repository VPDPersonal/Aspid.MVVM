using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for the <see cref="BindMode.OneWayToSource"/> channel of the MonoBehaviour numeric binders.
    /// </summary>
    /// <remarks>
    /// Mirrors <see cref="TargetNumberReverseChannelTests"/> for
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> and <see cref="ComponentIntMonoBinder{TComponent}"/>,
    /// which inherit the same interface-mapping shape and so shared the same silent failure.
    /// </remarks>
    [TestFixture]
    public sealed class ComponentNumberReverseChannelTests
    {
        private const float FloatProperty = 12.5f;
        private const int IntProperty = 42;

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
        public void FloatMonoBinder_OneWayToSource_DeliversToFloatMember() =>
            Assert.AreEqual(FloatProperty, BindFloatBinder<float>());

        [Test]
        public void FloatMonoBinder_OneWayToSource_DeliversToIntMember() =>
            Assert.AreEqual((int)FloatProperty, BindFloatBinder<int>());

        [Test]
        public void FloatMonoBinder_OneWayToSource_DeliversToLongMember() =>
            Assert.AreEqual((long)FloatProperty, BindFloatBinder<long>());

        [Test]
        public void FloatMonoBinder_OneWayToSource_DeliversToDoubleMember() =>
            Assert.AreEqual((double)FloatProperty, BindFloatBinder<double>());

        [Test]
        public void IntMonoBinder_OneWayToSource_DeliversToIntMember() =>
            Assert.AreEqual(IntProperty, BindIntBinder<int>());

        [Test]
        public void IntMonoBinder_OneWayToSource_DeliversToLongMember() =>
            Assert.AreEqual((long)IntProperty, BindIntBinder<long>());

        [Test]
        public void IntMonoBinder_OneWayToSource_DeliversToFloatMember() =>
            Assert.AreEqual((float)IntProperty, BindIntBinder<float>());

        [Test]
        public void IntMonoBinder_OneWayToSource_DeliversToDoubleMember() =>
            Assert.AreEqual((double)IntProperty, BindIntBinder<double>());

        private T BindFloatBinder<T>()
            where T : struct
        {
            var gameObject = NewGameObject();
            gameObject.AddComponent<FloatComponent>().Value = FloatProperty;

            return Bind<T>(CreateBinder<TestComponentFloatBinder>(gameObject));
        }

        private T BindIntBinder<T>()
            where T : struct
        {
            var gameObject = NewGameObject();
            gameObject.AddComponent<IntComponent>().Value = IntProperty;

            return Bind<T>(CreateBinder<TestComponentIntBinder>(gameObject));
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("BinderTest");
            _spawned.Add(gameObject);

            return gameObject;
        }

        /// <summary>
        /// Adds the binder and switches it to <see cref="BindMode.OneWayToSource"/> the same way the inspector does —
        /// the serialized <c>_mode</c> field has no public setter.
        /// </summary>
        private static TBinder CreateBinder<TBinder>(GameObject gameObject)
            where TBinder : MonoBinder
        {
            var binder = gameObject.AddComponent<TBinder>();
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.OneWayToSource;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            Assert.AreEqual(BindMode.OneWayToSource, binder.Mode, "Не удалось выставить режим биндера через SerializedObject");
            return binder;
        }

        private static T Bind<T>(IBinder binder)
            where T : struct
        {
            var received = default(T);
            var member = new OneWayToSourceStructBindableMember<T>(value => received = value);

            binder.Bind(member);
            return received;
        }
    }

    internal sealed class FloatComponent : MonoBehaviour
    {
        public float Value;
    }

    internal sealed class IntComponent : MonoBehaviour
    {
        public int Value;
    }

    internal sealed class TestComponentFloatBinder : ComponentFloatMonoBinder<FloatComponent>
    {
        protected override float Property
        {
            get => CachedComponent.Value;
            set => CachedComponent.Value = value;
        }
    }

    internal sealed class TestComponentIntBinder : ComponentIntMonoBinder<IntComponent>
    {
        protected override int Property
        {
            get => CachedComponent.Value;
            set => CachedComponent.Value = value;
        }
    }
}
