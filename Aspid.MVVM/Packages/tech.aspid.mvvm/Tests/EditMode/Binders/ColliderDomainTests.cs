using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the collider properties that were left unbound: the capsule's height and direction, the per-collider
    /// layer masks, the contact offset, the mesh cooking options, and the whole 2D domain.
    /// </summary>
    /// <remarks>
    /// 3D colliders were covered more densely than anything else in the package while 2D physics had nothing at all,
    /// and even in 3D the capsule had a radius binder and no height one.
    /// </remarks>
    [TestFixture]
    public sealed class ColliderDomainTests
    {
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

        #region 3D
        [Test]
        public void CapsuleHeight_ReachesTheCollider_AndIsNeverNegative()
        {
            var collider = New<CapsuleCollider>();
            var binder = collider.gameObject.AddComponent<CapsuleColliderHeightMonoBinder>();

            ((IBinder<float>)binder).SetValue(3f);
            Assert.AreEqual(3f, collider.height, 0.001f, "Высота не доехала до коллайдера");

            ((IBinder<float>)binder).SetValue(-1f);
            Assert.AreEqual(0f, collider.height, 0.001f, "Отрицательная высота не обрезана");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.IsFalse(float.IsNaN(collider.height), "NaN дошёл до коллайдера");
        }

        /// <summary>
        /// Unity accepts any integer as the capsule's axis and then behaves as if it were zero, so the binder clamps
        /// to the three axes that exist.
        /// </summary>
        [Test]
        public void CapsuleDirection_IsClampedToTheThreeAxes()
        {
            var collider = New<CapsuleCollider>();
            var binder = collider.gameObject.AddComponent<CapsuleColliderDirectionMonoBinder>();

            ((IBinder<int>)binder).SetValue(2);
            Assert.AreEqual(2, collider.direction, "Ось не доехала до коллайдера");

            ((IBinder<int>)binder).SetValue(7);
            Assert.AreEqual(2, collider.direction, "Ось вне 0..2 не обрезана");
        }

        [Test]
        public void TheLayerMasks_TravelAsIntegers()
        {
            var collider = New<BoxCollider>();
            var include = collider.gameObject.AddComponent<ColliderIncludeLayersMonoBinder>();
            var exclude = collider.gameObject.AddComponent<ColliderExcludeLayersMonoBinder>();

            ((IBinder<int>)include).SetValue(1 << 8);
            ((IBinder<int>)exclude).SetValue(1 << 9);

            Assert.AreEqual(1 << 8, collider.includeLayers.value, "Include-маска не доехала");
            Assert.AreEqual(1 << 9, collider.excludeLayers.value, "Exclude-маска не доехала");
        }

        [Test]
        public void ContactOffset_ReachesTheCollider_AndIsNeverNegative()
        {
            var collider = New<BoxCollider>();
            var binder = collider.gameObject.AddComponent<ColliderContactOffsetMonoBinder>();

            ((IBinder<float>)binder).SetValue(0.05f);
            Assert.AreEqual(0.05f, collider.contactOffset, 0.001f, "Отступ контакта не доехал");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NegativeInfinity);
            Assert.Greater(collider.contactOffset, 0f, "Нефинитный отступ дошёл до коллайдера");
        }

        [Test]
        public void CookingOptions_ReachTheMeshCollider()
        {
            var collider = New<MeshCollider>();
            var binder = collider.gameObject.AddComponent<MeshColliderCookingOptionsMonoBinder>();

            ((IBinder<MeshColliderCookingOptions>)binder).SetValue(MeshColliderCookingOptions.None);

            Assert.AreEqual(MeshColliderCookingOptions.None, collider.cookingOptions, "Опции кукинга не доехали");
        }
        #endregion

        #region 2D
        [Test]
        public void IsTrigger2D_ReachesTheCollider()
        {
            var collider = New<BoxCollider2D>();
            var binder = collider.gameObject.AddComponent<Collider2DIsTriggerMonoBinder>();

            ((IBinder<bool>)binder).SetValue(true);

            Assert.IsTrue(collider.isTrigger, "Триггер не доехал до 2D-коллайдера");
        }

        /// <summary>
        /// A negative offset is ordinary — it is how a crouch moves a collider down — so only a non-finite value is
        /// refused.
        /// </summary>
        [Test]
        public void Offset2D_KeepsNegatives_AndRefusesNonFinite()
        {
            var collider = New<BoxCollider2D>();
            var binder = collider.gameObject.AddComponent<Collider2DOffsetMonoBinder>();

            ((IBinder<Vector2>)binder).SetValue(new Vector2(-1f, -2f));
            Assert.AreEqual(new Vector2(-1f, -2f), collider.offset, "Отрицательное смещение не сохранено");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Vector2>)binder).SetValue(new Vector2(float.NaN, 0f));
            Assert.AreEqual(new Vector2(-1f, -2f), collider.offset, "Нефинитное смещение дошло до коллайдера");
        }

        /// <summary>
        /// Unity ignores a density write unless the attached body computes its mass from its colliders, so the test
        /// has to give it one — which is also what the binder now documents.
        /// </summary>
        [Test]
        public void Density2D_ReachesTheCollider_AndIsNeverNegative()
        {
            var collider = New<BoxCollider2D>();
            var body = collider.gameObject.AddComponent<Rigidbody2D>();
            body.useAutoMass = true;
            var binder = collider.gameObject.AddComponent<Collider2DDensityMonoBinder>();

            ((IBinder<float>)binder).SetValue(2f);
            Assert.AreEqual(2f, collider.density, 0.001f, "Плотность не доехала");

            ((IBinder<float>)binder).SetValue(-4f);
            Assert.AreEqual(0f, collider.density, 0.001f, "Отрицательная плотность не обрезана");
        }

        [Test]
        public void TheShapes2D_TakeTheirSizes()
        {
            var box = New<BoxCollider2D>();
            var circle = New<CircleCollider2D>();
            var capsule = New<CapsuleCollider2D>();

            ((IBinder<Vector2>)box.gameObject.AddComponent<BoxCollider2DSizeMonoBinder>()).SetValue(new Vector2(2f, 3f));
            ((IBinder<float>)circle.gameObject.AddComponent<CircleCollider2DRadiusMonoBinder>()).SetValue(1.5f);
            ((IBinder<Vector2>)capsule.gameObject.AddComponent<CapsuleCollider2DSizeMonoBinder>()).SetValue(new Vector2(1f, 4f));

            Assert.AreEqual(new Vector2(2f, 3f), box.size, "Размер бокса не доехал");
            Assert.AreEqual(1.5f, circle.radius, 0.001f, "Радиус круга не доехал");
            Assert.AreEqual(new Vector2(1f, 4f), capsule.size, "Размер капсулы не доехал");
        }

        /// <summary>
        /// The 2D material binder is built on <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/>, so a
        /// destroyed asset must arrive as <see langword="null"/> rather than as a live reference the Inspector shows
        /// as <c>Missing</c>.
        /// </summary>
        [Test]
        public void Material2D_ADestroyedAssetArrivesAsNull()
        {
            var collider = New<BoxCollider2D>();
            var binder = collider.gameObject.AddComponent<Collider2DMaterialMonoBinder>();
            var material = new PhysicsMaterial2D("Ice");

            try
            {
                ((IBinder<PhysicsMaterial2D>)binder).SetValue(material);
                Assert.AreSame(material, collider.sharedMaterial, "Живой материал не доехал");

                Object.DestroyImmediate(material);
                ((IBinder<PhysicsMaterial2D>)binder).SetValue(material);

                Assert.IsNull(collider.sharedMaterial, "Уничтоженный материал записался как живой");
            }
            finally
            {
                if (material) Object.DestroyImmediate(material);
            }
        }
        #endregion

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var capsule = New<CapsuleCollider>();
            var box = New<BoxCollider>();
            var mesh = New<MeshCollider>();
            var box2D = New<BoxCollider2D>();
            var circle2D = New<CircleCollider2D>();
            var capsule2D = New<CapsuleCollider2D>();

            Assert.IsTrue(new CapsuleColliderHeightBinder(capsule).IsBind);
            Assert.IsTrue(new CapsuleColliderDirectionBinder(capsule).IsBind);
            Assert.IsTrue(new ColliderContactOffsetBinder(box).IsBind);
            Assert.IsTrue(new ColliderIncludeLayersBinder(box).IsBind);
            Assert.IsTrue(new ColliderExcludeLayersBinder(box).IsBind);
            Assert.IsTrue(new MeshColliderCookingOptionsBinder(mesh).IsBind);
            Assert.IsTrue(new Collider2DIsTriggerBinder(box2D).IsBind);
            Assert.IsTrue(new Collider2DOffsetBinder(box2D).IsBind);
            Assert.IsTrue(new Collider2DDensityBinder(box2D).IsBind);
            Assert.IsTrue(new Collider2DMaterialBinder(box2D).IsBind);
            Assert.IsTrue(new BoxCollider2DSizeBinder(box2D).IsBind);
            Assert.IsTrue(new CircleCollider2DRadiusBinder(circle2D).IsBind);
            Assert.IsTrue(new CapsuleCollider2DSizeBinder(capsule2D).IsBind);
        }

        private T New<T>()
            where T : Component
        {
            var gameObject = new GameObject(typeof(T).Name);
            _spawned.Add(gameObject);

            return gameObject.AddComponent<T>();
        }
    }
}
